using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class InspectionService : InspectionProto.InspectionProtoBase
{
    private readonly IInspectionRegistry _registry;
    private readonly ILogger<InspectionService> _logger;

    public InspectionService(IInspectionRegistry registry, ILogger<InspectionService> logger) => 
        (_registry, _logger) = (registry, logger);

    public override async Task<InspectionModel> GetInspection(InspectionLookup request, ServerCallContext context)
    {
        _logger.LogInformation("Invoked to get the inspection with id {ID}", request.Id);
        try
        {
            var inspection = await _registry.GetInspectionAsync(request, context.CancellationToken);
            _logger.LogInformation("[INSPECTION SERVICE] Successfully. The resulting gRPC model of the inspection: {@Model}", inspection);
            return inspection;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[INSPECTION SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Inspection not exist"));
        }
    }

    public override async Task<InspectionModelList> GetInspections(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"[INSPECTION SERVICE] Invoked to get the inspection list");
        var inspections = await _registry.GetInspectionsAsync(context.CancellationToken);
        _logger.LogInformation("[INSPECTION SERVICE] Successfully. Inspection List: {@List}; Count: {Count}",
            inspections.Inspections, inspections.Inspections.Count());
        return inspections;
    }

    public override async Task<InspectionLookup> AddInspection(InspectionAddModel request, ServerCallContext context)
    {
        _logger.LogInformation("[INSPECTION SERVICE] Invoked to add the inspection from model: {@Model}", request);
        try
        {
            var inspectionLookup = await _registry.AddInspectionAsync(request, context.CancellationToken);
            _logger.LogInformation("[INSPECTION SERVICE] Successfully. Id of added inspection: {ID}", inspectionLookup.Id);
            return inspectionLookup;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[INSPECTION SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Nested objects not exist"));
        }
    }

    public override async Task<DbSaveCondition> UpdateInspection(InspectionModel request, ServerCallContext context)
    {
        _logger.LogInformation("[INSPECTION SERVICE] Invoked to update the inspection with id {ID}", request.Id);
        try
        {
            var dbCondition = await _registry.UpdateInspectionAsync(request, context.CancellationToken);
            _logger.LogInformation("[INSPECTION SERVICE] Successfully. Model of updated inspection: {Model}", request);
            return dbCondition;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[INSPECTION SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Nested objects not exist"));
        }
    }

    public override async Task<DbSaveCondition> DeleteInspection(InspectionLookup request, ServerCallContext context)
    {
        _logger.LogInformation("[INSPECTION SERVICE] Invoked to delete the inspection with id: {ID}", request.Id);
        var dbSaveCondition = await _registry.DeleteInspectionAsync(request, context.CancellationToken);
        _logger.LogInformation("[INSPECTION SERVICE] Successfully. Inspection is deleted");
        return dbSaveCondition;
    }

    public override async Task<ReportModel> GetAnimalTypeReport(GetReport request, ServerCallContext context)
    {
        _logger.LogInformation("[INSPECTION SERVICE] User: {User} invoke to get ANIMAL TYPE report. Period: ({Start} / {End})",
            request.UserCreator,
            request.DateStart, request.DateEnd);
        var reportModel = await _registry.GetAnimalTypeReportAsync(request, context.CancellationToken);
        _logger.LogInformation("[INSPECTION SERVICE] Successfully. ANIAMAL TYPE Report created by {User}. Report values: {ReportValue} (Count: {Count})",
            request.UserCreator, reportModel.Values, reportModel.Values.Count);
        return reportModel;
    }

    public override async Task<ReportModel> GetDiseaseReport(GetReport request, ServerCallContext context)
    {
        _logger.LogInformation("[INSPECTION SERVICE] User: {User} invoke to get DISEASE report. Period: ({Start} / {End})",
            request.UserCreator,
            request.DateStart, request.DateEnd);
        var reportModel = await _registry.GetAnimalTypeReportAsync(request, context.CancellationToken);
        _logger.LogInformation("[INSPECTION SERVICE] Successfully. DISEASE Report created by {User}. Report values: {ReportValue} (Count: {Count})",
            request.UserCreator, reportModel.Values, reportModel.Values.Count);
        return reportModel;
    }
}