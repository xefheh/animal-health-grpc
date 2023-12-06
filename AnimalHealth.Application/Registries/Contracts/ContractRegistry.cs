using System.ComponentModel;
using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.ContractExt;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Entities;
using AnimalHealth.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Contracts;

public class ContractRegistry : IContractRegistry
{
    private readonly AnimalHealthContext _context;
    private readonly IMapper _mapper;

    public ContractRegistry(AnimalHealthContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);
    
    public async Task<ContractModel> GetContractAsync(ContractLookup lookup, CancellationToken cancellationToken)
    {
        var contractId = lookup.Id;
        var contract = await _context.Contracts.LoadIncludes()
            .FirstOrDefaultAsync(contract => contract.Id == contractId, cancellationToken);
        if (contract == default(Contract)) throw new NotFoundException(typeof(Contract), contractId);
        return _mapper.Map<ContractModel>(contract);
    }

    public async Task<ContractModelList> GetContractsAsync(CancellationToken cancellationToken)
    {
        var contracts = await _context.Contracts.LoadIncludes().ToListAsync(cancellationToken);
        var contractModels = contracts.Select(contract => _mapper.Map<ContractModel>(contract));
        var contractModelList = new ContractModelList();
        contractModelList.Contracts.AddRange(contractModels);
        return contractModelList;
    }

    public async Task<ContractLookup> AddContractAsync(ContractAddModel addedContract, CancellationToken cancellationToken)
    {
        var contract = _mapper.Map<Contract>(addedContract);
        await _context.AddAsync(contract, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return new ContractLookup() { Id = contract.Id };
    }

    public async Task<DbSaveCondition> UpdateContractAsync(ContractModel updatedContract, CancellationToken cancellationToken)
    {
        var updatedDomainContract = _mapper.Map<Contract>(updatedContract);
        var contract = await _context.Contracts.LoadIncludes()
            .FirstOrDefaultAsync(contract => contract.Id == updatedContract.Id, cancellationToken);
        if (contract == default(Contract)) throw new NotFoundException(typeof(Contract), updatedContract.Id);
        contract.UpdateFields(updatedDomainContract);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        return new DbSaveCondition { Code = saveCode };
    }

    public async Task<DbSaveCondition> DeleteContractAsync(ContractLookup lookup, CancellationToken cancellationToken)
    {
        var contractId = lookup.Id;
        var contractMock = new Contract { Id = contractId };
        _context.Contracts.Attach(contractMock);
        _context.Contracts.Remove(contractMock);
        var saveCode = await _context.SaveChangesAsync(cancellationToken);
        return new DbSaveCondition { Code = saveCode };
    }
}