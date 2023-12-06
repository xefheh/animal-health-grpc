using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services;

public class ContractService : ContractProto.ContractProtoBase
{
    private readonly IContractRegistry _registry;

    public ContractService(IContractRegistry registry) => _registry = registry;

    public override async Task<ContractModel> GetContract(ContractLookup request, ServerCallContext context) =>
        await _registry.GetContractAsync(request, context.CancellationToken);

    public override async Task<ContractModelList> GetContracts(Empty request, ServerCallContext context) =>
        await _registry.GetContractsAsync(context.CancellationToken);

    public override async Task<ContractLookup> AddContract(ContractAddModel request, ServerCallContext context) =>
        await _registry.AddContractAsync(request, context.CancellationToken);

    public override async Task<DbSaveCondition> UpdateContract(ContractModel request, ServerCallContext context) =>
        await _registry.UpdateContractAsync(request, context.CancellationToken);

    public override async Task<DbSaveCondition> DeleteContract(ContractLookup request, ServerCallContext context) =>
        await _registry.DeleteContractAsync(request, context.CancellationToken);
}