using AnimalHealth.Application.Cache;
using AnimalHealth.Domain.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalHealth.Application.Extensions.IncludeLoadingExtensions
{
    public static class ReportIncludeExtensions
    {
        public static IIncludableQueryable<Report, List<ReportValue>?> LoadIncludes(this IQueryable<Report> reports)
            => reports.Include(report => report.Values);
        
        public static async Task<IList<Report>> GetOrLoadFromCacheAsync(this IQueryable<Report> reports,
            IMemoryCache memoryCache, CancellationToken cancellationToken) =>
            (await memoryCache.GetOrCreateAsync(CacheKeys.ReportCacheKey, async (entry) =>
                await reports.LoadIncludes().ToListAsync(cancellationToken)))!;
    }
}
