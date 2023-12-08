using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Extensions.VaccinationExt;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Reports.LocalityVaccinationReport;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Vaccinations;

internal class VaccinationRegistry : IVaccinationRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel> _mapper;

    public VaccinationRegistry(AnimalHealthContext context, IEntityMapper<Vaccination, VaccinationAddModel, VaccinationModel> mapper) =>
        (_context, _mapper) = (context, mapper);
    
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

    public async Task<ReportModel> GetVaccinationReportAsync(ReportDates dates, CancellationToken cancellationToken)
    {
        return null;
        /*var vaccinations = await _context.Vaccinations
            .LoadIncludes()
            .Where(ins => ins.Date >= dates.DateStart.ToDateTime() && ins.Date <= dates.DateEnd.ToDateTime())
            .ToListAsync(cancellationToken);

        var report = new VaccinationReport();
        report.GetReport(vaccinations);
        return _mapper.Map<ReportModel>(report);*/
    }
}