using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Domain.Reports;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Registries.Reports
{
    public class ReportRegistryList : IReportRegistry
    {
        static int id = 0;
        static Dictionary<int, Report> reports = new Dictionary<int, Report>();
        readonly IEntityMapper<Report, ReportModel> _mapper;
        readonly IEntityMapper<User, UserModel> _userMapper;

        public ReportRegistryList(IEntityMapper<Report, ReportModel> mapper, 
            IEntityMapper<User, UserModel> userMapper)
        {
            _mapper = mapper;
            _userMapper = userMapper;
        }

        public Task<ReportLookup> AddReportAsync(Report report, CancellationToken cancellationToken)
        {
            id += 1;
            reports.Add(id, report);
            report.Id = id;
            var task = Task.Factory.StartNew(() => new ReportLookup { Id = id });
            return task;
        }

        public Task<ReportLookup> DeleteReportAsync(ReportLookup lookup, CancellationToken cancellationToken)
        {
            var report = reports[lookup.Id];
            reports.Remove(lookup.Id);
            var task = Task.Factory.StartNew(() => new ReportLookup { Id = lookup.Id });
            return task;
        }

        public Task<ReportModel> GetReportAsync(ReportLookup request, CancellationToken cancellationToken)
        {
            var report = reports[request.Id];
            if (report == null) throw new NotFoundException(typeof(Report), request.Id);
            var task = Task.Factory.StartNew(() => _mapper.Map(report));
            return task;
        }

        public Task<ReportMetaData> GetReportMetaDataAsync(Empty request, CancellationToken cancellationToken)
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
            var task = Task.Factory.StartNew(() => data);
            return task;
        }

        public Task<ReportModelList> GetReportsByPeriodAsync(DatesPeriod period, CancellationToken cancellationToken)
        {
            var dateStart = period.DateStart.ToDateTime();
            var dateEnd = period.DateEnd.ToDateTime();
            var reports = ReportRegistryList.reports.Values.Where(x => x.CreateDate >= dateStart && x.CreateDate <= dateEnd)
                .ToList();
            var reportModels = reports.Select(report => _mapper.Map(report));
            var reportModelList = new ReportModelList();
            reportModelList.Reports.AddRange(reportModels);
            foreach (var report in reportModelList.Reports)
                report.Values.Clear();
            var task = Task.Factory.StartNew(() => reportModelList);
            return task;
        }

        public Task<ReportModelList> GetReportsByUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            var userid = user.Id;
            var reports = ReportRegistryList.reports.Values
                .Where(x => x.Creator.Id == userid)
                .ToList();
            var reportModels = reports.Select(report => _mapper.Map(report));
            var reportModelList = new ReportModelList();
            reportModelList.Reports.AddRange(reportModels);
            foreach (var report in reportModelList.Reports)
                report.Values.Clear();        
            var task = Task.Factory.StartNew(() => reportModelList);
            return task;
        }

        public Task<ReportLookup> ApproveReportAsync(ChangeReportState request, CancellationToken cancellationToken)
        {
            var report = reports[request.ReportId];
            var changer = _userMapper.Map(request.Changer);
            var secondApprover = _userMapper.Map(request.SecondApprover);
            var date = request.DateChange.ToDateTime();
            if (report == null) throw new NotFoundException(typeof(Report), request.ReportId);
            report.GoNextState(date, new List<User> { changer, secondApprover });
            var task = Task.Factory.StartNew(() => new ReportLookup { Id = request.ReportId });
            return task;
        }

        public Task<ReportLookup> SendReportAsync(ChangeReportState request, CancellationToken cancellationToken)
        {
            var report = reports[request.ReportId];
            var changer = _userMapper.Map(request.Changer);
            var receiver = _userMapper.Map(request.Receiver);
            var date = request.DateChange.ToDateTime();
            if (report == null) throw new NotFoundException(typeof(Report), request.ReportId);
            report.GoNextState(date, new List<User> { changer, receiver });
            var task = Task.Factory.StartNew(() => new ReportLookup { Id = request.ReportId });
            return task;
        }
    }
}