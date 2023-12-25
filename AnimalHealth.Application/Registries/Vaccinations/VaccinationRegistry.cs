using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Reports;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;
using AnimalHealth.Domain.Identity;

namespace AnimalHealth.Application.Registries.Vaccinations;

public class VaccinationRegistry : IVaccinationRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IReportRegistry _reportRegistry;
    private readonly IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel> _mapper;
    private readonly IEntityMapper<Report, ReportModel> _reportGrpcMapper;
    private readonly IEntityMapper<User, UserModel> _userGrpcMapper;

    public VaccinationRegistry(AnimalHealthContext context, IReportRegistry reportRegistry,
        IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel> mapper, 
        IEntityMapper<Report, ReportModel> reportGrpcMapper, 
        IEntityMapper<User, UserModel> userGrpcMapper)
    {
        _context = context;
        _reportRegistry = reportRegistry;
        _mapper = mapper;
        _reportGrpcMapper = reportGrpcMapper;
        _userGrpcMapper = userGrpcMapper;
    }

    public async Task<VaccinationModel> GetVaccinationAsync(VaccinationLookup lookup, CancellationToken cancellationToken)
    {
        var vaccinationId = lookup.Id;
        var vaccination = await _context.Vaccinations.LoadIncludes()
            .FirstOrDefaultAsync(vaccination => vaccination.Id == vaccinationId, cancellationToken);
        if (vaccination == default(Vaccination)) throw new NotFoundException(typeof(Vaccination), vaccinationId);
        return _mapper.Map(vaccination);
    }

    public async Task<VaccinationModelList> GetVaccinationsAsync(CancellationToken cancellationToken)
    {
        var vaccinations = await _context.Vaccinations.LoadIncludes().ToListAsync(cancellationToken);
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
        return new DbSaveCondition { Code = saveCode };
    }

    public async Task<DbSaveCondition> DeleteVaccinationAsync(VaccinationLookup lookup, CancellationToken cancellationToken)
    {
        var vaccinationId = lookup.Id;
        var vaccinationMock = new Vaccination { Id = vaccinationId };
        _context.Vaccinations.Attach(vaccinationMock);
        _context.Vaccinations.Remove(vaccinationMock);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        return new DbSaveCondition { Code = saveCode };
    }

    public async Task<ReportModel> GetVaccinationReportAsync(GetReport dates, CancellationToken cancellationToken)
    { 
        var vaccinations = await _context.Vaccinations
            .LoadIncludes()
            .Where(ins => ins.Date >= dates.DateStart.ToDateTime() && ins.Date <= dates.DateEnd.ToDateTime())
            .ToListAsync(cancellationToken);

        var report = new Report("По нас.пункту и вакцинам");
        report.Creator = _userGrpcMapper.Map(dates.UserCreator);
        report.GetReport(vaccinations, (vaccination) => vaccination.GetLocalityVaccine());

        await _reportRegistry.AddReportAsync(report, cancellationToken);

        return _reportGrpcMapper.Map(report);
    }
}