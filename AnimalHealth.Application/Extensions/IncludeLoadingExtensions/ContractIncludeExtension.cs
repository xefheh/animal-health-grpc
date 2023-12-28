using AnimalHealth.Application.Cache;
using AnimalHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalHealth.Application.Extensions.IncludeLoadingExtensions;

public static class ContractIncludeExtension
{
    public static IIncludableQueryable<Contract, Locality?> LoadIncludes(this IQueryable<Contract> contracts) =>
        contracts
            .Include(contract => contract.Customer)
            .ThenInclude(organization => organization.Locality)
            .Include(contract => contract.Executor)
            .ThenInclude(organization => organization.Locality);
    
    public static async Task<IList<Contract>> GetOrLoadFromCacheAsync(this IQueryable<Contract> contracts,
        IMemoryCache memoryCache, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync(CacheKeys.ContractCacheKey, async (entry) =>
            await contracts.LoadIncludes().ToListAsync(cancellationToken)))!;
}