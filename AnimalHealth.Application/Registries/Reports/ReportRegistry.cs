using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Reports
{
    public class ReportRegistry : IReportRegistry
    {
        readonly AnimalHealthContext _context;
        readonly IMapper _mapper;

        public ReportRegistry(AnimalHealthContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<DbSaveCondition> DeleteContractAsync(ReportLookup lookup, CancellationToken cancellationToken)
        {
            var reportId = lookup.Id;
            var reportMock = new Report { Id = reportId };
            _context.Reports.Attach(reportMock);
            _context.Reports.Remove(reportMock);
            var saveCode = await _context.SaveChangesAsync(cancellationToken);
            return new DbSaveCondition { Code = saveCode };
        }

        public async Task<ReportModelList> GetReportsAsync(ReportUserName user, CancellationToken cancellationToken)
        {
            var reports = await _context.Reports.ToListAsync(cancellationToken);
            var reportModels = reports.Select(report => _mapper.Map<ReportModel>(report));
            var ReportModelList = new ReportModelList();
            ReportModelList.Reports.AddRange(reportModels);
            return ReportModelList;
        }
    }
}