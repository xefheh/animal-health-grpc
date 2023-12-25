using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Factories;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Logging;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class InspectionService : InspectionProto.InspectionProtoBase
{
    private readonly IInspectionRegistry _registry;

    public InspectionService(LogRegistryFactory<IInspectionRegistry, LogInspectionRegistry> factory,
        ILogger<IInspectionRegistry> logger) => _registry = factory.CreateLogRegistry();

    public override async Task<InspectionModel> GetInspection(InspectionLookup request, ServerCallContext context)
    {
        try
        {
            return await _registry.GetInspectionAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<InspectionModelList> GetInspections(Empty request, ServerCallContext context) =>
        await _registry.GetInspectionsAsync(context.CancellationToken);

    public override async Task<InspectionLookup> AddInspection(InspectionAddModel request, ServerCallContext context)
    {
        try
        {
            return await _registry.AddInspectionAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<DbSaveCondition> UpdateInspection(InspectionModel request, ServerCallContext context)
    {
        try
        {
            return await _registry.UpdateInspectionAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<DbSaveCondition> DeleteInspection(InspectionLookup request, ServerCallContext context) =>
        await _registry.DeleteInspectionAsync(request, context.CancellationToken);

    public override async Task<ReportModel> GetAnimalTypeReport(GetReport request, ServerCallContext context) =>
        await _registry.GetAnimalTypeReportAsync(request, context.CancellationToken);

    public override async Task<ReportModel> GetDiseaseReport(GetReport request, ServerCallContext context) =>
        await _registry.GetDiseaseReportAsync(request, context.CancellationToken);
}