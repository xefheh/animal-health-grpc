using AnimalHealth.Application.Cache;
using AnimalHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalHealth.Application.Extensions.IncludeLoadingExtensions;

public static class OrganizationIncludeExtension
{
    public static IIncludableQueryable<Organization, Locality?> LoadIncludes(
        this IQueryable<Organization> organizations) => organizations
        .Include(organization => organization.Locality);
    
    public static async Task<IList<Organization>> GetOrLoadFromCacheAsync(this IQueryable<Organization> organizations,
        IMemoryCache memoryCache, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync(CacheKeys.OrganizationCacheKey, async (entry) =>
            await organizations.LoadIncludes().ToListAsync(cancellationToken)))!;
}