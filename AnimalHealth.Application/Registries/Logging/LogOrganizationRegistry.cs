using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using Microsoft.Extensions.Logging;

namespace AnimalHealth.Application.Registries.Logging;

public class LogOrganizationRegistry : IOrganizationRegistry
{
    private readonly IOrganizationRegistry _registry;
    private readonly ILogger<IOrganizationRegistry> _logger;

    public LogOrganizationRegistry(IOrganizationRegistry registry, ILogger<IOrganizationRegistry> logger) =>
        (_registry, _logger) = (registry, logger);
    
    public async Task<OrganizationModel> GetOrganizationAsync(OrganizationLookup lookup, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ORGANIZATION SERVICE] Invoked to get the organization with Tin {Tin}", lookup.Tin);
        try
        {
            var organization = await _registry.GetOrganizationAsync(lookup, cancellationToken);
            _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. The resulting gRPC model of the inspection: {@Model}", organization);
            return organization;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[ORGANIZATION SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<OrganizationModelList> GetOrganizationsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[ORGANIZATION SERVICE] Invoked to get the organization list");
        var organizations = await _registry.GetOrganizationsAsync(cancellationToken);
        _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. Organization List: {@List}; Count: {Count}",
            organizations.Organizations, organizations.Organizations.Count);
        return organizations;
    }

    public async Task<OrganizationLookup> AddOrganizationAsync(OrganizationAddModel addedOrganization, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ORGANIZATION SERVICE] Invoked to add the organization from model: {@Model}", addedOrganization);
        try
        {
            var organizationLookup = await _registry.AddOrganizationAsync(addedOrganization, cancellationToken);
            _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. Tin of added organization: {Tin}", organizationLookup.Tin);
            return organizationLookup;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[ORGANIZATION SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<DbSaveCondition> UpdateOrganizationAsync(OrganizationModel updatedOrganization, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ORGANIZATION SERVICE] Invoked to update the organization with Tin {Tin}", updatedOrganization.Tin);
        try
        {
            var dbCondition = await _registry.UpdateOrganizationAsync(updatedOrganization, cancellationToken);
            _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. Model of updated organization: {Model}", updatedOrganization);
            return dbCondition;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[ORGANIZATION SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<DbSaveCondition> DeleteOrganizationAsync(OrganizationLookup lookup, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[ORGANIZATION SERVICE] Invoked to delete the inspection with Tin: {Tin}", lookup.Tin);
        var dbSaveCondition = await _registry.DeleteOrganizationAsync(lookup, cancellationToken);
        _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. Organization is deleted");
        return dbSaveCondition;
    }
}