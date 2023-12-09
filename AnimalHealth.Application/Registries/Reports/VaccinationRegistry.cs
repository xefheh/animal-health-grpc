using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Registries;                                                       
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityVaccinationReport;
using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Vaccinations;

public class VaccinationRegistry : IVaccinationRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel> _mapper;
    private readonly IEntityMapper<VaccinationReport, ReportModel> _vaccinationReportGrpcMapper;
    private readonly IMapper<VaccinationReport, Report> _vaccinationReportEFMapper;

    public VaccinationRegistry(AnimalHealthContext context, IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel> mapper,
        IEntityMapper<VaccinationReport, ReportModel> vaccinationReportGrpcMapper, 
        IMapper<VaccinationReport, Report> vaccinationReportEFMapper)
    {
        _context = context;
        _mapper = mapper;
        _vaccinationReportGrpcMapper = vaccinationReportGrpcMapper;
        _vaccinationReportEFMapper = vaccinationReportEFMapper;
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

        var report = new VaccinationReport();
        report.GetReport(vaccinations);
        report.User = dates.UserCreator;
        await _context.Reports.AddAsync(_vaccinationReportEFMapper.Map(report), cancellationToken);
        return _vaccinationReportGrpcMapper.Map(report);
    }
}