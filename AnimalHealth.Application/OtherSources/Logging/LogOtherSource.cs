using AnimalHealth.Application.Models;
using AnimalHealth.Application.OtherSources.Interfaces;
using Microsoft.Extensions.Logging;

namespace AnimalHealth.Application.OtherSources.Logging;

public class LogOtherSource : IOtherSource
{
    private readonly IOtherSource _source;
    private readonly ILogger<IOtherSource> _logger;

    public LogOtherSource(IOtherSource source, ILogger<IOtherSource> logger) =>
        (_source, _logger) = (source, logger);
    
    public async Task<AnimalModelList> GetAnimalsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the animal list");
        var animals = await _source.GetAnimalsAsync(cancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. Animal List: {@List}; Count: {Count}",
            animals.Animals, animals.Animals.Count);
        return animals;
    }

    public async Task<DiseaseModelList> GetDiseasesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the disease list");
        var diseases = await _source.GetDiseasesAsync(cancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. Disease List: {@List}; Count: {Count}",
            diseases.Diseases, diseases.Diseases.Count);
        return diseases;
    }

    public async Task<VaccineModelList> GetVaccinesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the vaccine list");
        var vaccines = await _source.GetVaccinesAsync(cancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. Vaccine List: {@List}; Count: {Count}",
            vaccines.Vaccines, vaccines.Vaccines.Count);
        return vaccines;
    }

    public async Task<LocalityModelList> GetLocalitiesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the locality list");
        var localities = await _source.GetLocalitiesAsync(cancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. Locality List: {@List}; Count: {Count}",
            localities.Localities, localities.Localities.Count);
        return localities;
    }

    public async Task<UserModelList> GetUsersAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[OTHER SOURCE] Invoked to get the users list");
        var users = await _source.GetUsersAsync(cancellationToken);
        _logger.LogInformation("[OTHER SOURCE] Successfully. User List: {@List}; Count: {Count}",
            users.Users, users.Users.Count);
        return users;
    }
}