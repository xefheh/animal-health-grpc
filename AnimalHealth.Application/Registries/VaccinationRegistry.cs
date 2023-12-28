using AnimalHealth.Application.Cache;
using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Domain.Reports;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalHealth.Application.Registries;

public class VaccinationRegistry : IVaccinationRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IMemoryCache _cache;
    private readonly IReportRegistry _reportRegistry;
    private readonly IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel> _mapper;
    private readonly IEntityMapper<Report, ReportModel> _reportGrpcMapper;
    private readonly IEntityMapper<User, UserModel> _userGrpcMapper;

    public VaccinationRegistry(AnimalHealthContext context, IMemoryCache cache, IReportRegistry reportRegistry,
        IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel> mapper, 
        IEntityMapper<Report, ReportModel> reportGrpcMapper, 
        IEntityMapper<User, UserModel> userGrpcMapper)
    {
        _context = context;
        _cache = cache;
        _reportRegistry = reportRegistry;
        _mapper = mapper;
        _reportGrpcMapper = reportGrpcMapper;
        _userGrpcMapper = userGrpcMapper;
    }

    public async Task<VaccinationModel> GetVaccinationAsync(VaccinationLookup lookup, CancellationToken cancellationToken)
    {
        var vaccinations = await _context.Vaccinations.GetOrLoadFromCacheAsync(_cache, cancellationToken);
        var vaccinationId = lookup.Id;
        var resultVaccination = vaccinations.FirstOrDefault(vaccination => vaccination.Id == vaccinationId) ??
                                await _context.Vaccinations.FirstOrDefaultAsync(vaccination => vaccination.Id == vaccinationId,
                                    cancellationToken);
        if (resultVaccination == default(Vaccination)) throw new NotFoundException(typeof(Vaccination), vaccinationId);
        return _mapper.Map(resultVaccination);
    }

    public async Task<VaccinationModelList> GetVaccinationsAsync(CancellationToken cancellationToken)
    {
        var vaccinations = await _context.Vaccinations.GetOrLoadFromCacheAsync(_cache, cancellationToken);
        var vaccinationModels = vaccinations.Select(vaccination => _mapper.Map(vaccination));
        var vaccinationModelList = new VaccinationModelList();
        vaccinationModelList.Vaccinations.AddRange(vaccinationModels);
        return vaccinationModelList;
    }

    public async Task<VaccinationLookup> AddVaccinationAsync(VaccinationAddModel addedVaccination, CancellationToken cancellationToken)
    {
        var vaccination = _mapper.Map(addedVaccination);
        await _context.Vaccinations.AddAsync(vaccination, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        _cache.Remove(CacheKeys.VaccinationCacheKey);
        return new VaccinationLookup { Id = vaccination.Id };
    }

    public async Task<DbSaveCondition> UpdateVaccinationAsync(VaccinationModel updatedVaccination, CancellationToken cancellationToken)
    {
        var updatedDomainVaccination = _mapper.Map(updatedVaccination);
        var vaccination = await _context.Vaccinations.LoadIncludes()
            .FirstOrDefaultAsync(vaccination => vaccination.Id == updatedVaccination.Id, cancellationToken);
        if (vaccination == default(Vaccination)) throw new NotFoundException(typeof(Vaccination), updatedVaccination.Id);
        vaccination.UpdateFields(updatedDomainVaccination);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        _cache.Remove(CacheKeys.VaccinationCacheKey);
        return new DbSaveCondition { Code = saveCode };
    }

    public async Task<DbSaveCondition> DeleteVaccinationAsync(VaccinationLookup lookup, CancellationToken cancellationToken)
    {
        var vaccinationId = lookup.Id;
        var vaccinationMock = new Vaccination { Id = vaccinationId };
        _context.Vaccinations.Attach(vaccinationMock);
        _context.Vaccinations.Remove(vaccinationMock);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        _cache.Remove(CacheKeys.VaccinationCacheKey);
        return new DbSaveCondition { Code = saveCode };
    }

    public async Task<ReportModel> GetVaccinationReportAsync(GetReport dates, CancellationToken cancellationToken)
    {
        var allVaccinations = await _context.Vaccinations.GetOrLoadFromCacheAsync(_cache, cancellationToken);
        
        var vaccinations = allVaccinations
            .Where(ins => ins.Date >= dates.DateStart.ToDateTime() && ins.Date <= dates.DateEnd.ToDateTime())
            .ToList();

        var creator = _userGrpcMapper.Map(dates.UserCreator);
        var creator2 = _userGrpcMapper.Map(dates.SecondUser);
        var report = new Report("По нас.пункту и вакцинам", creator, creator2);
        report.GetReport(vaccinations, (vaccination) => vaccination.GetLocalityVaccine());

        await _reportRegistry.AddReportAsync(report, cancellationToken);

        return _reportGrpcMapper.Map(report);
    }
}