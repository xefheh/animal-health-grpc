using AnimalHealth.Application.Cache;
using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalHealth.Application.Registries;

public class ContractRegistry : IContractRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IMemoryCache _cache;
    private readonly IEntityMapper<Contract, ContractAddModel, ContractModel> _mapper;

    public ContractRegistry(AnimalHealthContext context,
        IMemoryCache cache,
        IEntityMapper<Contract, ContractAddModel, ContractModel> mapper)
    {
        _context = context;
        _cache = cache;
        _mapper = mapper;
    }
    
    public async Task<ContractModel> GetContractAsync(ContractLookup lookup, CancellationToken cancellationToken)
    {
        var contracts = await _context.Contracts.GetOrLoadFromCacheAsync(_cache, cancellationToken);
        var contractId = lookup.Id;
        var resultContract = contracts.FirstOrDefault(contract => contract.Id == contractId) ??
                         await _context.Contracts.FirstOrDefaultAsync(contract => contract.Id == contractId,
                             cancellationToken);
        if (resultContract == default(Contract)) throw new NotFoundException(typeof(Contract), contractId);
        return _mapper.Map(resultContract);
    }

    public async Task<ContractModelList> GetContractsAsync(CancellationToken cancellationToken)
    {
        var contracts = await _context.Contracts.GetOrLoadFromCacheAsync(_cache, cancellationToken);
        var contractModels = contracts.Select(contract => _mapper.Map(contract));
        var contractModelList = new ContractModelList();
        contractModelList.Contracts.AddRange(contractModels);
        return contractModelList;
    }

    public async Task<ContractLookup> AddContractAsync(ContractAddModel addedContract, CancellationToken cancellationToken)
    {
        var contract = _mapper.Map(addedContract);
        await _context.Contracts.AddAsync(contract, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        _cache.Remove(CacheKeys.ContractCacheKey);
        return new ContractLookup() { Id = contract.Id };
    }

    public async Task<DbSaveCondition> UpdateContractAsync(ContractModel updatedContract, CancellationToken cancellationToken)
    {
        var updatedDomainContract = _mapper.Map(updatedContract);
        var contract = await _context.Contracts.LoadIncludes()
            .FirstOrDefaultAsync(contract => contract.Id == updatedContract.Id, cancellationToken);
        if (contract == default(Contract)) throw new NotFoundException(typeof(Contract), updatedContract.Id);
        contract.UpdateFields(updatedDomainContract);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        _cache.Remove(CacheKeys.ContractCacheKey);
        return new DbSaveCondition { Code = saveCode };
    }

    public async Task<DbSaveCondition> DeleteContractAsync(ContractLookup lookup, CancellationToken cancellationToken)
    {
        var contractId = lookup.Id;
        var contractMock = new Contract { Id = contractId };
        _context.Contracts.Attach(contractMock);
        _context.Contracts.Remove(contractMock);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        _cache.Remove(CacheKeys.ContractCacheKey);
        return new DbSaveCondition { Code = saveCode };
    }
}