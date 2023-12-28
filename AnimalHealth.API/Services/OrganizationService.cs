using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class OrganizationService : OrganizationProto.OrganizationProtoBase
{
    private readonly IOrganizationRegistry _registry;

    public OrganizationService(IOrganizationRegistry registry) => _registry = registry;

    public override async Task<OrganizationModel> GetOrganization(OrganizationLookup request, ServerCallContext context)
    {
        try
        {
            return await _registry.GetOrganizationAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<OrganizationModelList> GetOrganizations(Empty request, ServerCallContext context) =>
        await _registry.GetOrganizationsAsync(context.CancellationToken);

    public override async Task<OrganizationLookup> AddOrganization(OrganizationAddModel request,
        ServerCallContext context)
    {
        try
        {
            return await _registry.AddOrganizationAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<DbSaveCondition> UpdateOrganization(OrganizationModel request, ServerCallContext context)
    {
        try
        {
            return await _registry.UpdateOrganizationAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<DbSaveCondition> DeleteOrganization(OrganizationLookup request,
        ServerCallContext context) => await _registry.DeleteOrganizationAsync(request, context.CancellationToken);
}