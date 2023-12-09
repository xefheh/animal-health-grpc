using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Reports
{
    public class ReportRegistry : IReportRegistry
    {
        readonly AnimalHealthContext _context;
        readonly IEntityMapper<Report, ReportModel> _mapper;
        public ReportRegistry(AnimalHealthContext context, IEntityMapper<Report, ReportModel> mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DbSaveCondition> DeleteContractAsync
            (ReportLookup lookup, CancellationToken cancellationToken)
        {
            var reportId = lookup.Id;
            var reportMock = new Report { Id = reportId };
            _context.Reports.Attach(reportMock);
            _context.Reports.Remove(reportMock);
            var saveCode = await _context.SaveChangesAsync(cancellationToken);
            return new DbSaveCondition { Code = saveCode };
        }

        public async Task<ReportModelList> GetReportsAsync
            (ReportUserName data, CancellationToken cancellationToken)
        {
            var username = data.User;
            var reports = await _context.Reports
                .Where(x => x.User == username)
                .ToListAsync(cancellationToken);

            var reportModels = reports.Select(report => _mapper.Map(report));
            var ReportModelList = new ReportModelList();
            ReportModelList.Reports.AddRange(reportModels);
            return ReportModelList;
        }
    }
}