using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Domain.Reports;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Reports
{
    public class ReportRegistry : IReportRegistry
    {
        readonly AnimalHealthContext _context;
        readonly IEntityMapper<Report, ReportModel> _mapper;
        readonly IEntityMapper<User, UserModel> _userMapper;

        public ReportRegistry(AnimalHealthContext context, IEntityMapper<Report, ReportModel> mapper, 
            IEntityMapper<User, UserModel> userMapper)
        {
            _context = context;
            _mapper = mapper;
            _userMapper = userMapper;
        }

        public async Task<DbSaveCondition> AddReportAsync(Report report, CancellationToken cancellationToken)
        {
            await _context.Reports.AddAsync(report, cancellationToken);
            var saveCode = await _context.SaveChangesAsync(cancellationToken);
            return new DbSaveCondition { Code = saveCode };*/
        }

        public async Task<ReportModel> GetReportAsync(ReportLookup request, CancellationToken cancellationToken)
        {
            var reportId = request.Id;
            var report = await _context.Reports.Where(x => x.Id == reportId).FirstOrDefaultAsync(cancellationToken);
            if (report == default(Report)) throw new NotFoundException(typeof(Report), reportId);
            var reportModel = _mapper.Map(report);
            return reportModel;
        }

        public async Task<ReportModelList> GetReportsByPeriodAsync(DatesPeriod period, CancellationToken cancellationToken)
        {
            var dateStart = period.DateStart.ToDateTime();
            var dateEnd = period.DateEnd.ToDateTime();
            var reports = await _context.Reports
                .Where(x => x.CreateDate >= dateStart && x.CreateDate <= dateEnd)
                .ToListAsync(cancellationToken);

            var reportModels = reports.Select(report => _mapper.Map(report));
            var reportModelList = new ReportModelList();
            reportModelList.Reports.AddRange(reportModels);
            return reportModelList;
        }

        public async Task<ReportModelList> GetReportsByUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            var userid = user.Id;
            var reports = await _context.Reports
                .LoadIncludes()
                .Where(x => x.User.Id == userid)
                .ToListAsync(cancellationToken);

            var reportModels = reports.Select(report => _mapper.Map(report));
            var reportModels = reports.Select(report => _mapper.Map<ReportModel>(report));
            var ReportModelList = new ReportModelList();
            return ReportModelList;
        }

        public async Task<ReportLookup> DeleteReportAsync(ReportLookup lookup, CancellationToken cancellationToken)
        {
            var reportId = lookup.Id;
            var reportMock = new Report { Id = reportId };
            _context.Reports.Attach(reportMock);
            _context.Reports.Remove(reportMock);
            var saveCode = await _context.SaveChangesAsync(cancellationToken);
            return lookup;
        }

        public async Task<ReportLookup> ApproveReportAsync(ChangeReportState request, CancellationToken cancellationToken)
        {
            var reportId = request.ReportId;
            var changer = _userMapper.Map(request.Changer);
            var date = request.DateChange.ToDateTime();
            var report = await _context.Reports.Where(x => x.Id == reportId).FirstOrDefaultAsync(cancellationToken);
            if (report == default(Report)) throw new NotFoundException(typeof(Report), reportId);
            report.Approve(date, changer);
            return new ReportLookup { Id = reportId };
        }

        public async Task<ReportLookup> SendReportAsync(ChangeReportState request, CancellationToken cancellationToken)
        {
            var reportId = request.ReportId;
            var changer = _userMapper.Map(request.Changer);
            var date = request.DateChange.ToDateTime();
            var report = await _context.Reports.Where(x => x.Id == reportId).FirstOrDefaultAsync(cancellationToken);
            if (report == default(Report)) throw new NotFoundException(typeof(Report), reportId);
            report.Send(date, changer);
            return new ReportLookup { Id = reportId };
        }

        public async Task<ReportLookup> CancelReportAsync(ChangeReportState request, CancellationToken cancellationToken)
        {
            var reportId = request.ReportId;
            var changer = _userMapper.Map(request.Changer);
            var date = request.DateChange.ToDateTime();
            var report = await _context.Reports.Where(x => x.Id == reportId).FirstOrDefaultAsync(cancellationToken);
            if (report == default(Report)) throw new NotFoundException(typeof(Report), reportId);
            report.Cancel(date, changer);
            return new ReportLookup { Id = reportId };
        }

        public async Task<ReportMetaData> GetReportMetaDataAsync(Google.Protobuf.WellKnownTypes.Empty request, CancellationToken cancellationToken)
        {
            var data = new ReportMetaData();
            data.EngReportNames.AddRange(new string[]
                {
                    "DiseaseReport",
                    "AnimalTypeReport",
                    "VaccinationReport"
                });
            data.RusReportNames.AddRange(new string[]
                {
                    "По нас.пункту и болезням",
                    "По нас.пункту и типам животных",
                    "По нас.пункту и вакцинам"
                });
            data.RusColumnNames.AddRange(new string[]
                 {
                    "Нас.пункт",
                    "Болезнь",
                    "Тип жив-ого",
                    "Вакцина"
                 });
            return data;
        }
    }
}