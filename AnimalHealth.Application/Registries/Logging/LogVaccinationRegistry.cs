using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace AnimalHealth.Application.Registries.Logging;

public class LogVaccinationRegistry : IVaccinationRegistry
{
    private readonly IVaccinationRegistry _registry;
    private readonly ILogger<IVaccinationRegistry> _logger;

    public LogVaccinationRegistry(IVaccinationRegistry registry, ILogger<IVaccinationRegistry> logger) =>
        (_registry, _logger) = (registry, logger);
    
    public async Task<VaccinationModel> GetVaccinationAsync(VaccinationLookup lookup, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[VACCINATION SERVICE] Invoked to get the vaccination with Id: {Id}", lookup.Id);
        try
        {
            var vaccination = await _registry.GetVaccinationAsync(lookup, cancellationToken);
            _logger.LogInformation("[VACCINATION SERVICE] Successfully. The resulting gRPC model of the vaccination: {@Model}", vaccination);
            return vaccination;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[VACCINATION SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<VaccinationModelList> GetVaccinationsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[VACCINATION SERVICE] Invoked to get the vaccination list");
        var vaccinations = await _registry.GetVaccinationsAsync(cancellationToken);
        _logger.LogInformation("[VACCINATION SERVICE] Successfully. Vaccination List: {@List}; Count: {Count}",
            vaccinations.Vaccinations, vaccinations.Vaccinations.Count);
        return vaccinations;
    }

    public async Task<VaccinationLookup> AddVaccinationAsync(VaccinationAddModel addedVaccination, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[VACCINATION SERVICE] Invoked to add the vaccination from model: {@Model}", addedVaccination);
        try
        {
            var vaccinationLookup = await _registry.AddVaccinationAsync(addedVaccination, cancellationToken);
            _logger.LogInformation("[VACCINATION SERVICE] Successfully. Id of added vaccination: {ID}", vaccinationLookup.Id);
            return vaccinationLookup;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[VACCINATION SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<DbSaveCondition> UpdateVaccinationAsync(VaccinationModel updatedVaccination, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[VACCINATION SERVICE] Invoked to update the vaccination with id {ID}", updatedVaccination.Id);
        try
        {
            var dbCondition = await _registry.UpdateVaccinationAsync(updatedVaccination, cancellationToken);
            _logger.LogInformation("[VACCINATION SERVICE] Successfully. Model of updated vaccination: {Model}", updatedVaccination);
            return dbCondition;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[VACCINATION SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<DbSaveCondition> DeleteVaccinationAsync(VaccinationLookup lookup, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[VACCINATION SERVICE] Invoked to delete the vaccination with id: {ID}", lookup.Id);
        var dbSaveCondition = await _registry.DeleteVaccinationAsync(lookup, cancellationToken);
        _logger.LogInformation("[VACCINATION SERVICE] Successfully. Vaccination is deleted");
        return dbSaveCondition;
    }

    public async Task<ReportModel> GetVaccinationReportAsync(GetReport dates, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[VACCINATION SERVICE] User: {User} invoke to get VACCINATION report. Period: ({Start} / {End})",
            dates.UserCreator,
            dates.DateStart, dates.DateEnd);
        var reportModel = await _registry.GetVaccinationReportAsync(dates, cancellationToken);
        _logger.LogInformation("[VACCINATION SERVICE] Successfully. VACCINATION Report created by {User}. Report values: {ReportValue} (Count: {Count})",
            dates.UserCreator, reportModel.Values, reportModel.Values.Count);
        return reportModel;
    }
}