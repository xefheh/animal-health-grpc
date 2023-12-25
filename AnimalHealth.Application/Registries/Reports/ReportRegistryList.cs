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
            var reports = ReportRegistryList.reports.Values.Where(x => x.ChangeDate >= dateStart && x.ChangeDate <= dateEnd)
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
                .Where(x => x.Changer.Id == userid)
                .ToList();
            var reportModels = reports.Select(report => _mapper.Map(report));
            var reportModelList = new ReportModelList();
            reportModelList.Reports.AddRange(reportModels);
            foreach (var report in reportModelList.Reports)
                report.Values.Clear();
            var task = Task.Factory.StartNew(() => reportModelList);
            return task;
        }

        public Task<ReportLookup> GoNextStateAsync(ChangeReportState request, CancellationToken cancellationToken)
        {
            if (!reports.ContainsKey(request.ReportId))
                throw new NotFoundException(typeof(Report), request.ReportId);
            var report = reports[request.ReportId];
            var changer = _userMapper.Map(request.Changer);
            var date = request.DateChange.ToDateTime();
            var requestUser = _userMapper.Map(request.AdditionalChanger);

            report.GoNextState(date, new List<User> { changer, requestUser });
            var task = Task.Factory.StartNew(() => new ReportLookup { Id = request.ReportId });
            return task;
        }
    }
}