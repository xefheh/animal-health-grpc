using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.VaccinationExt;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Vaccinations;

internal class VaccinationRegistry : IVaccinationRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IMapper _mapper;

    public VaccinationRegistry(AnimalHealthContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);
    
    public async Task<VaccinationModel> GetVaccinationAsync(VaccinationLookup lookup, CancellationToken cancellationToken)
    {
        var vaccinationId = lookup.Id;
        var vaccination = await
            _context.Vaccinations.FindAsync(new object[] { vaccinationId }, cancellationToken);
        if (vaccination == null) throw new NotFoundException(typeof(Vaccination), vaccinationId);
        return _mapper.Map<VaccinationModel>(vaccination);
    }

    public async Task<VaccinationModelList> GetVaccinationsAsync(CancellationToken cancellationToken)
    {
        var vaccinations = await _context.Vaccinations.ToListAsync(cancellationToken);
        var vaccinationModels = vaccinations.Select(vaccination => _mapper.Map<VaccinationModel>(vaccination));
        var vaccinationModelList = new VaccinationModelList();
        vaccinationModelList.Vaccinations.AddRange(vaccinationModels);
        return vaccinationModelList;
    }

    public async Task<VaccinationLookup> AddVaccinationAsync(VaccinationAddModel addedVaccination, CancellationToken cancellationToken)
    {
        var vaccination = _mapper.Map<Vaccination>(addedVaccination);
        await _context.Vaccinations.AddAsync(vaccination, cancellationToken);
        return new VaccinationLookup { Id = vaccination.Id };
    }

    public async Task<DbSaveCondition> UpdateVaccinationAsync(VaccinationModel updatedVaccination, CancellationToken cancellationToken)
    {
        var updatedDomainVaccination = _mapper.Map<Vaccination>(updatedVaccination);
        var vaccination =
            await _context.Vaccinations.FindAsync(new object[] { updatedDomainVaccination.Id }, cancellationToken);
        if (vaccination == null) throw new NotFoundException(typeof(Vaccination), updatedVaccination.Id);
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
}