using AnimalHealth.Application.Interfaces.OtherSource;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.OtherViews;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class OtherSourceService : OtherResourceProto.OtherResourceProtoBase
{
    private readonly IOtherSource _otherSource;

    public OtherSourceService(IOtherSource otherSource) => _otherSource = otherSource;

    public override Task<AnimalModelList> GetAnimals(Empty request, ServerCallContext context) =>
        _otherSource.GetAnimalsAsync(context.CancellationToken);

    public override Task<DiseaseModelList> GetDiseases(Empty request, ServerCallContext context) =>
        _otherSource.GetDiseasesAsync(context.CancellationToken);

    public override Task<VaccineModelList> GetVaccines(Empty request, ServerCallContext context) =>
        _otherSource.GetVaccinesAsync(context.CancellationToken);

    public override Task<LocalityModelList> GetLocalities(Empty request, ServerCallContext context) =>
        _otherSource.GetLocalitiesAsync(context.CancellationToken);

    public override Task<UserModelList> GetUsers(Empty request, ServerCallContext context) =>
        _otherSource.GetUsersAsync(context.CancellationToken);
}