using AnimalHealth.Application.Cache;
using AnimalHealth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalHealth.Application.Extensions.IncludeLoadingExtensions;

public static class VaccinationIncludeExtension
{
    public static IIncludableQueryable<Vaccination, Locality?> LoadIncludes(this IQueryable<Vaccination> inspections) => inspections
        .Include(inspection => inspection.Vaccine)
        .Include(inspection => inspection.User)
        .Include(inspection => inspection.Animal)
        .Include(inspection => inspection.Contract)
        .ThenInclude(contract => contract.Customer)
        .ThenInclude(organization => organization.Locality)
        .Include(inspection => inspection.Contract)
        .ThenInclude(contract => contract.Executor)
        .ThenInclude(organization => organization.Locality);
    
    public static async Task<IList<Vaccination>> GetOrLoadFromCacheAsync(this IQueryable<Vaccination> vaccinations,
        IMemoryCache memoryCache, CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync(CacheKeys.ReportCacheKey, async (entry) =>
            await vaccinations.LoadIncludes().AsNoTracking().ToListAsync(cancellationToken)))!;
}