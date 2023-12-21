using AnimalHealth.Application.Interfaces.OtherSource;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class OtherSourceService : OtherResourceProto.OtherResourceProtoBase
{
    private readonly IOtherSource _otherSource;
    private readonly ILogger<OtherSourceService> _logger;

    public OtherSourceService(IOtherSource otherSource, ILogger<OtherSourceService> logger) =>
        (_otherSource, _logger) = (otherSource, logger);

    public override async Task<AnimalModelList> GetAnimals(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the animal list");
        var animals = await _otherSource.GetAnimalsAsync(context.CancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. Animal List: {@List}; Count: {Count}",
            animals.Animals, animals.Animals.Count());
        return animals;
    }

    public override async Task<DiseaseModelList> GetDiseases(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the disease list");
        var diseases = await _otherSource.GetDiseasesAsync(context.CancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. Disease List: {@List}; Count: {Count}",
            diseases.Diseases, diseases.Diseases.Count());
        return diseases;
    }

    public override async Task<VaccineModelList> GetVaccines(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the vaccine list");
        var vaccines = await _otherSource.GetVaccinesAsync(context.CancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. Vaccine List: {@List}; Count: {Count}",
            vaccines.Vaccines, vaccines.Vaccines.Count());
        return vaccines;
    }

    public override async Task<LocalityModelList> GetLocalities(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the locality list");
        var localities = await _otherSource.GetLocalitiesAsync(context.CancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. Locality List: {@List}; Count: {Count}",
            localities.Localities, localities.Localities.Count());
        return localities;
    }

    public override async Task<UserModelList> GetUsers(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the users list");
        var users = await _otherSource.GetUsersAsync(context.CancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. User List: {@List}; Count: {Count}",
            users.Users, users.Users.Count());
        return users;
    }
}