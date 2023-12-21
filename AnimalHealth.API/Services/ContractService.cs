using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class ContractService : ContractProto.ContractProtoBase
{
    private readonly IContractRegistry _registry;
    private readonly ILogger<ContractService> _logger;

    public ContractService(IContractRegistry registry, ILogger<ContractService> logger) =>
        (_registry, _logger) = (registry, logger);

    public override async Task<ContractModel> GetContract(ContractLookup request, ServerCallContext context)
    {
        _logger.LogInformation("[CONTRACT SERVICE] Invoked to get the contract with id {ID}", request.Id);
        try
        {
            var contract = await _registry.GetContractAsync(request, context.CancellationToken);
            _logger.LogInformation("[CONTRACT SERVICE] Successfully. The resulting gRPC model of the contract: {@Model}", contract);
            return contract;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[CONTRACT SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Contract not exist"));
        }
    }

    public override async Task<ContractModelList> GetContracts(Empty request, ServerCallContext context)
    {
        _logger.LogInformation($"[CONTRACT SERVICE] Invoked to get the contract list");
        var contracts = await _registry.GetContractsAsync(context.CancellationToken);
        _logger.LogInformation("[CONTRACT SERVICE] Successfully. Contract List: {@List}; Count: {Count}",
            contracts.Contracts, contracts.Contracts.Count());
        return contracts;
    }

    public override async Task<ContractLookup> AddContract(ContractAddModel request, ServerCallContext context)
    {
        _logger.LogInformation("[CONTRACT SERVICE] Invoked to add the contract from model: {@Model}", request);
        try
        {
            var contractLookup = await _registry.AddContractAsync(request, context.CancellationToken);
            _logger.LogInformation("[CONTRACT SERVICE] Successfully. Id of added contract: {ID}", contractLookup.Id);
            return contractLookup;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[CONTRACT SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Nested objects not exist"));
        }
    }

    public override async Task<DbSaveCondition> UpdateContract(ContractModel request, ServerCallContext context)
    {
        _logger.LogInformation("[CONTRACT SERVICE] Invoked to update the contract with id {ID}", request.Id);
        try
        {
            var dbCondition = await _registry.UpdateContractAsync(request, context.CancellationToken);
            _logger.LogInformation("[CONTRACT SERVICE] Successfully. Model of updated contract: {Model}", request);
            return dbCondition;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[CONTRACT SERVICE] Error occured: {@Error}", e);
            throw new RpcException(new Status(StatusCode.NotFound, "Nested objects not exist"));
        }
    }

    public override async Task<DbSaveCondition> DeleteContract(ContractLookup request, ServerCallContext context)
    {
        _logger.LogInformation("[CONTRACT SERVICE] Invoked to delete the contract with id: {ID}", request.Id);
        var dbSaveCondition = await _registry.DeleteContractAsync(request, context.CancellationToken);
        _logger.LogInformation("[CONTRACT SERVICE] Successfully. Contract is deleted");
        return dbSaveCondition;
    }
}