using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using Microsoft.Extensions.Logging;

namespace AnimalHealth.Application.Registries.Logging;

public class LogInspectionRegistry : IInspectionRegistry
{
    private readonly IInspectionRegistry _registry;
    private readonly ILogger<IInspectionRegistry> _logger;

    public LogInspectionRegistry(IInspectionRegistry registry, ILogger<IInspectionRegistry> logger) =>
        (_registry, _logger) = (registry, logger);
    
    public async Task<InspectionModel> GetInspectionAsync(InspectionLookup lookup, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Invoked to get the inspection with id {ID}", lookup.Id);
        try
        {
            var inspection = await _registry.GetInspectionAsync(lookup, cancellationToken);
            _logger.LogInformation("[INSPECTION SERVICE] Successfully. The resulting gRPC model of the inspection: {@Model}", inspection);
            return inspection;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[INSPECTION SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<InspectionModelList> GetInspectionsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[INSPECTION SERVICE] Invoked to get the inspection list");
        var inspections = await _registry.GetInspectionsAsync(cancellationToken);
        _logger.LogInformation("[INSPECTION SERVICE] Successfully. Inspection List: {@List}; Count: {Count}",
            inspections.Inspections, inspections.Inspections.Count);
        return inspections;
    }

    public async Task<InspectionLookup> AddInspectionAsync(InspectionAddModel addedInspection, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[INSPECTION SERVICE] Invoked to add the inspection from model: {@Model}", addedInspection);
        try
        {
            var inspectionLookup = await _registry.AddInspectionAsync(addedInspection, cancellationToken);
            _logger.LogInformation("[INSPECTION SERVICE] Successfully. Id of added inspection: {ID}", inspectionLookup.Id);
            return inspectionLookup;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[INSPECTION SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<DbSaveCondition> UpdateInspectionAsync(InspectionModel updatedInspection, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[INSPECTION SERVICE] Invoked to update the inspection with id {ID}", updatedInspection.Id);
        try
        {
            var dbCondition = await _registry.UpdateInspectionAsync(updatedInspection, cancellationToken);
            _logger.LogInformation("[INSPECTION SERVICE] Successfully. Model of updated inspection: {Model}", updatedInspection);
            return dbCondition;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[INSPECTION SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<DbSaveCondition> DeleteInspectionAsync(InspectionLookup lookup, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[INSPECTION SERVICE] Invoked to delete the inspection with id: {ID}", lookup.Id);
        var dbSaveCondition = await _registry.DeleteInspectionAsync(lookup, cancellationToken);
        _logger.LogInformation("[INSPECTION SERVICE] Successfully. Inspection is deleted");
        return dbSaveCondition;
    }

    public async Task<ReportModel> GetAnimalTypeReportAsync(GetReport dates, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[INSPECTION SERVICE] User: {User} invoke to get ANIMAL TYPE report. Period: ({Start} / {End})",
            dates.UserCreator,
            dates.DateStart, dates.DateEnd);
        var reportModel = await _registry.GetAnimalTypeReportAsync(dates, cancellationToken);
        _logger.LogInformation("[INSPECTION SERVICE] Successfully. ANIMAL TYPE Report created by {User}. Report values: {ReportValue} (Count: {Count})",
            dates.UserCreator, reportModel.Values, reportModel.Values.Count);
        return reportModel;
    }

    public async Task<ReportModel> GetDiseaseReportAsync(GetReport dates, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[INSPECTION SERVICE] User: {User} invoke to get DISEASE report. Period: ({Start} / {End})",
            dates.UserCreator,
            dates.DateStart, dates.DateEnd);
        var reportModel = await _registry.GetDiseaseReportAsync(dates, cancellationToken);
        _logger.LogInformation("[INSPECTION SERVICE] Successfully. DISEASE Report created by {User}. Report values: {ReportValue} (Count: {Count})",
            dates.UserCreator, reportModel.Values, reportModel.Values.Count);
        return reportModel;
    }
}