using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class InspectionService : InspectionProto.InspectionProtoBase
{
    private readonly IInspectionRegistry _registry;

    public InspectionService(IInspectionRegistry registry) => _registry = registry;

    public override async Task<InspectionModel> GetInspection(InspectionLookup request, ServerCallContext context) =>
        await _registry.GetInspectionAsync(request, context.CancellationToken);

    public override async Task<InspectionModelList> GetInspections(Empty request, ServerCallContext context) =>
        await _registry.GetInspectionsAsync(context.CancellationToken);

    public override async Task<InspectionLookup> AddInspection(InspectionAddModel request, ServerCallContext context) =>
        await _registry.AddInspectionAsync(request, context.CancellationToken);

    public override async Task<DbSaveCondition> UpdateInspection(InspectionModel request, ServerCallContext context) =>
        await _registry.UpdateInspectionAsync(request, context.CancellationToken);

    public override async Task<DbSaveCondition> DeleteInspection(InspectionLookup request, ServerCallContext context) =>
        await _registry.DeleteInspectionAsync(request, context.CancellationToken);

    public override async Task<ReportModel> GetAnimalTypeReport(ReportDates request, ServerCallContext context) =>
        await _registry.GetAnimalTypeReportAsync(request, context.CancellationToken);

    public override async Task<ReportModel> GetDiseaseReport(ReportDates request, ServerCallContext context) =>
        await _registry.GetDiseaseReportAsync(request, context.CancellationToken);
}