using AnimalHealth.Domain.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AnimalHealth.Application.Extensions.IncludeLoadingExtensions
{
    public static class ReportIncludeExtensions
    {
        public static IIncludableQueryable<Report, List<ReportValue>?> LoadIncludes(this IQueryable<Report> reports)
            => reports.Include(report => report.Values);
    }
}
