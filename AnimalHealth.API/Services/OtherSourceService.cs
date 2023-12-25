using AnimalHealth.Application.Factories;
using AnimalHealth.Application.Interfaces.OtherSource;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Logging;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class OtherSourceService : OtherResourceProto.OtherResourceProtoBase
{
    private readonly IOtherSource _otherSource;

    public OtherSourceService(LogRegistryFactory<IOtherSource, LogOtherSource> factory,
        ILogger<IOtherSource> logger) => _otherSource = factory.CreateLogRegistry();

    public override async Task<AnimalModelList> GetAnimals(Empty request, ServerCallContext context) =>
        await _otherSource.GetAnimalsAsync(context.CancellationToken);

    public override async Task<DiseaseModelList> GetDiseases(Empty request, ServerCallContext context) =>
        await _otherSource.GetDiseasesAsync(context.CancellationToken);

    public override async Task<VaccineModelList> GetVaccines(Empty request, ServerCallContext context) =>
        await _otherSource.GetVaccinesAsync(context.CancellationToken);

    public override async Task<LocalityModelList> GetLocalities(Empty request, ServerCallContext context) =>
        await _otherSource.GetLocalitiesAsync(context.CancellationToken);

    public override async Task<UserModelList> GetUsers(Empty request, ServerCallContext context) =>
        await _otherSource.GetUsersAsync(context.CancellationToken);
}