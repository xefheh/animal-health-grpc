using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Factories;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using AnimalHealth.Application.Registries.Logging;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class ContractService : ContractProto.ContractProtoBase
{
    private readonly IContractRegistry _registry;

    public ContractService(LogRegistryFactory<IContractRegistry, LogContractRegistry> factory,
        ILogger<IContractRegistry> logger) => _registry = factory.CreateLogRegistry();

    public override async Task<ContractModel> GetContract(ContractLookup request, ServerCallContext context)
    {
        try
        {
            return await _registry.GetContractAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<ContractModelList> GetContracts(Empty request, ServerCallContext context) =>
        await _registry.GetContractsAsync(context.CancellationToken);

    public override async Task<ContractLookup> AddContract(ContractAddModel request, ServerCallContext context)
    {
        try
        {
            return await _registry.AddContractAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<DbSaveCondition> UpdateContract(ContractModel request, ServerCallContext context)
    {
        try
        {
            return await _registry.UpdateContractAsync(request, context.CancellationToken);
        }
        catch (NotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
    }

    public override async Task<DbSaveCondition> DeleteContract(ContractLookup request, ServerCallContext context) =>
        await _registry.DeleteContractAsync(request, context.CancellationToken);
}