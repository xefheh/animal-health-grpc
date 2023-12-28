using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Mapping.Interfaces;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using AnimalHealth.Domain.Identity;
using AnimalHealth.Domain.Reports;
using Google.Protobuf.WellKnownTypes;

namespace AnimalHealth.Application.Registries
{
    public class ReportRegistryList : IReportRegistry
    {
        private static int _id = 0;
        private static readonly Dictionary<int, Report> Reports = new Dictionary<int, Report>();
        private readonly IEntityMapper<Report, ReportModel> _mapper;
        private readonly IEntityMapper<User, UserModel> _userMapper;

        public ReportRegistryList(IEntityMapper<Report, ReportModel> mapper,
            IEntityMapper<User, UserModel> userMapper)
        {
            _mapper = mapper;
            _userMapper = userMapper;
        }

        public Task<ReportLookup> AddReportAsync(Report report, CancellationToken cancellationToken)
        {
            _id += 1;
            Reports.Add(_id, report);
            report.Id = _id;
            var task = Task.Factory.StartNew(() => new ReportLookup { Id = _id }, cancellationToken);
            return task;
        }

        public Task<ReportLookup> DeleteReportAsync(ReportLookup lookup, CancellationToken cancellationToken)
        {
            var report = Reports[lookup.Id];
            Reports.Remove(lookup.Id);
            var task = Task.Factory.StartNew(() => new ReportLookup { Id = lookup.Id }, cancellationToken);
            return task;
        }

        public Task<ReportModel> GetReportAsync(ReportLookup request, CancellationToken cancellationToken)
        {
            var report = Reports[request.Id];
            if (report == null) throw new NotFoundException(typeof(Report), request.Id);
            var task = Task.Factory.StartNew(() => _mapper.Map(report), cancellationToken);
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
            var task = Task.Factory.StartNew(() => data, cancellationToken);
            return task;
        }

        public Task<ReportModelList> GetReportsByPeriodAsync(DatesPeriod period, CancellationToken cancellationToken)
        {
            var dateStart = period.DateStart.ToDateTime();
            var dateEnd = period.DateEnd.ToDateTime();
            var reports = Reports.Values.Where(x => x.ChangeDate >= dateStart && x.ChangeDate <= dateEnd)
                .ToList();
            var reportModels = reports.Select(report => _mapper.Map(report));
            var reportModelList = new ReportModelList();
            reportModelList.Reports.AddRange(reportModels);
            foreach (var report in reportModelList.Reports)
                report.Values.Clear();
            var task = Task.Factory.StartNew(() => reportModelList, cancellationToken);
            return task;
        }

        public Task<ReportModelList> GetReportsByUserAsync(UserModel user, CancellationToken cancellationToken)
        {
            var userid = user.Id;
            var reports = Reports.Values
                .Where(x => x.Changer.Id == userid)
                .ToList();
            var reportModels = reports.Select(report => _mapper.Map(report));
            var reportModelList = new ReportModelList();
            reportModelList.Reports.AddRange(reportModels);
            foreach (var report in reportModelList.Reports)
                report.Values.Clear();
            var task = Task.Factory.StartNew(() => reportModelList, cancellationToken);
            return task;
        }

        public Task<ReportLookup> GoNextStateAsync(ChangeReportState request, CancellationToken cancellationToken)
        {
            if (!Reports.TryGetValue(request.ReportId, out var value))
                throw new NotFoundException(typeof(Report), request.ReportId);
            var changer = _userMapper.Map(request.Changer);
            var date = request.DateChange.ToDateTime();
            var requestUser = _userMapper.Map(request.AdditionalChanger);

            value.GoNextState(date, new List<User> { changer, requestUser });
            var task = Task.Factory.StartNew(() => new ReportLookup { Id = request.ReportId }, cancellationToken);
            return task;
        }
    }
}