using AnimalHealth.Application.Cache;
using AnimalHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalHealth.Application.Extensions.IncludeLoadingExtensions;

public static class InspectionIncludeExtension
{
    public static IIncludableQueryable<Inspection, Locality?> LoadIncludes(this IQueryable<Inspection> inspections) => inspections
        .Include(inspection => inspection.Disease)
        .Include(inspection => inspection.User)
        .Include(inspection => inspection.InspectedAnimal)
        .Include(inspection => inspection.Contract)
            .ThenInclude(contract => contract.Customer)
                .ThenInclude(organization => organization.Locality)
        .Include(inspection => inspection.Contract)
            .ThenInclude(contract => contract.Executor)
                .ThenInclude(organization => organization.Locality);

    public static async Task<IList<Inspection>> GetOrLoadFromCacheAsync(this IQueryable<Inspection> inspections,
        IMemoryCache memoryCache, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync(CacheKeys.InspectionCacheKey, async (entry) =>
            await inspections.LoadIncludes().AsNoTracking().ToListAsync(cancellationToken)))!;
}