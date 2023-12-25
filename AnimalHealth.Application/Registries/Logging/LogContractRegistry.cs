using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using Microsoft.Extensions.Logging;

namespace AnimalHealth.Application.Registries.Logging;

public class LogContractRegistry : IContractRegistry
{
    private readonly IContractRegistry _registry;
    private readonly ILogger<IContractRegistry> _logger;

    public LogContractRegistry(IContractRegistry registry, ILogger<IContractRegistry> logger) =>
        (_registry, _logger) = (registry, logger);

    public async Task<ContractModel> GetContractAsync(ContractLookup lookup, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CONTRACT SERVICE] Invoked to get the contract with id {ID}", lookup.Id);
        try
        {
            var contract = await _registry.GetContractAsync(lookup, cancellationToken);
            _logger.LogInformation("[CONTRACT SERVICE] Successfully. The resulting gRPC model of the contract: {@Model}", contract);
            return contract;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[CONTRACT SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<ContractModelList> GetContractsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[CONTRACT SERVICE] Invoked to get the contract list");
        var contracts = await _registry.GetContractsAsync(cancellationToken);
        _logger.LogInformation("[CONTRACT SERVICE] Successfully. Contract List: {@List}; Count: {Count}",
            contracts.Contracts, contracts.Contracts.Count);
        return contracts;
    }

    public async Task<ContractLookup> AddContractAsync(ContractAddModel addedContract, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CONTRACT SERVICE] Invoked to add the contract from model: {@Model}", addedContract);
        try
        {
            var contractLookup = await _registry.AddContractAsync(addedContract, cancellationToken);
            _logger.LogInformation("[CONTRACT SERVICE] Successfully. Id of added contract: {ID}", contractLookup.Id);
            return contractLookup;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[CONTRACT SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<DbSaveCondition> UpdateContractAsync(ContractModel updatedContract, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CONTRACT SERVICE] Invoked to update the contract with id {ID}", updatedContract.Id);
        try
        {
            var dbCondition = await _registry.UpdateContractAsync(updatedContract, cancellationToken);
            _logger.LogInformation("[CONTRACT SERVICE] Successfully. Model of updated contract: {Model}", updatedContract);
            return dbCondition;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[CONTRACT SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<DbSaveCondition> DeleteContractAsync(ContractLookup lookup, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[CONTRACT SERVICE] Invoked to delete the contract with id: {ID}", lookup.Id);
        var dbSaveCondition = await _registry.DeleteContractAsync(lookup, cancellationToken);
        _logger.LogInformation("[CONTRACT SERVICE] Successfully. Contract is deleted");
        return dbSaveCondition;
    }
}