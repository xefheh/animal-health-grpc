using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class OrganizationService : OrganizationProto.OrganizationProtoBase
{
    private readonly IOrganizationRegistry _registry;
    private readonly ILogger<OrganizationService> _logger;

    public OrganizationService(IOrganizationRegistry registry, ILogger<OrganizationService> logger) =>
        (_registry, _logger) = (registry, logger);

    public override async Task<OrganizationModel> GetOrganization(OrganizationLookup request, ServerCallContext context)
    {
        _logger.LogInformation("[ORGANIZATION SERVICE] Invoked to get the organization with Tin {Tin}", request.Tin);
        try
        {
            var organization = await _registry.GetOrganizationAsync(request, context.CancellationToken);
            _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. The resulting gRPC model of the inspection: {@Model}", organization);
            return organization;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[ORGANIZATION SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Organization not exist"));
        }
    }

    public override async Task<OrganizationModelList> GetOrganizations(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"[ORGANIZATION SERVICE] Invoked to get the organization list");
        var organizations = await _registry.GetOrganizationsAsync(context.CancellationToken);
        _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. Organization List: {@List}; Count: {Count}",
            organizations.Organizations, organizations.Organizations.Count());
        return organizations;
    }

    public override async Task<OrganizationLookup> AddOrganization(OrganizationAddModel request,
        ServerCallContext context)
    {
        _logger.LogInformation("[ORGANIZATION SERVICE] Invoked to add the organization from model: {@Model}", request);
        try
        {
            var organizationLookup = await _registry.AddOrganizationAsync(request, context.CancellationToken);
            _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. Tin of added organization: {Tin}", organizationLookup.Tin);
            return organizationLookup;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[ORGANIZATION SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Nested objects not exist"));
        }
    }

    public override async Task<DbSaveCondition> UpdateOrganization(OrganizationModel request, ServerCallContext context)
    {
        _logger.LogInformation("[ORGANIZATION SERVICE] Invoked to update the organization with Tin {Tin}", request.Tin);
        try
        {
            var dbCondition = await _registry.UpdateOrganizationAsync(request, context.CancellationToken);
            _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. Model of updated organization: {Model}", request);
            return dbCondition;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[ORGANIZATION SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Nested objects not exist"));
        }
    }

    public override async Task<DbSaveCondition> DeleteOrganization(OrganizationLookup request,
        ServerCallContext context)
    {
        _logger.LogInformation("[ORGANIZATION SERVICE] Invoked to delete the inspection with Tin: {Tin}", request.Tin);
        var dbSaveCondition = await _registry.DeleteOrganizationAsync(request, context.CancellationToken);
        _logger.LogInformation("[ORGANIZATION SERVICE] Successfully. Organization is deleted");
        return dbSaveCondition;
    }
}