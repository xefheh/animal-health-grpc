using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class VaccinationService : VaccinationProto.VaccinationProtoBase
{
    private readonly IVaccinationRegistry _registry;

    public VaccinationService(IVaccinationRegistry registry) => _registry = registry;

    public override async Task<VaccinationModel> GetVaccination(VaccinationLookup request, ServerCallContext context) =>
        await _registry.GetVaccinationAsync(request, context.CancellationToken);

    public override async Task<VaccinationModelList> GetVaccinations(Empty request, ServerCallContext context) =>
        await _registry.GetVaccinationsAsync(context.CancellationToken);

    public override async Task<VaccinationLookup> AddVaccination(VaccinationAddModel request, ServerCallContext context) =>
        await _registry.AddVaccinationAsync(request, context.CancellationToken);

    public override async Task<DbSaveCondition> UpdateVaccination(VaccinationModel request, ServerCallContext context) =>
        await _registry.UpdateVaccinationAsync(request, context.CancellationToken);

    public override async Task<DbSaveCondition> DeleteVaccination(VaccinationLookup request, ServerCallContext context) =>
        await _registry.DeleteVaccinationAsync(request, context.CancellationToken);

    public override async Task<ReportModel> GetVaccinationReport(ReportDates request, ServerCallContext context) =>
        await _registry.GetVaccinationReportAsync(request, context.CancellationToken);
}