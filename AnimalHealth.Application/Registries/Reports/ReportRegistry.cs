using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Extensions.IncludeLoadingExtensions;
using AnimalHealth.Application.Interfaces;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Mapping.ReportMappings.VaccinationReportMappings;
using AnimalHealth.Application.Models;
using AnimalHealth.Domain.BasicReportEntities;
using AnimalHealth.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimalHealth.Application.Registries.Reports
{
    public class ReportRegistry : IReportRegistry
    {
        readonly AnimalHealthContext _context;

        public async Task<DbSaveCondition> DeleteReportAsync
            (ReportLookup lookup, CancellationToken cancellationToken)
        public async Task<DbSaveCondition> DeleteContractAsync(ReportLookup lookup, CancellationToken cancellationToken)
        {
            return null;
            /*var reportId = lookup.Id;
            var reportMock = new Report { Id = reportId };
            _context.Reports.Attach(reportMock);
            _context.Reports.Remove(reportMock);
            var saveCode = await _context.SaveChangesAsync(cancellationToken);
            return new DbSaveCondition { Code = saveCode };*/
        }

        public async Task<ReportModelList> GetReportsAsync(ReportUserName user, CancellationToken cancellationToken)
            var username = data.User;
            var reports = await _context.Reports
                .LoadIncludes()
                .Where(x => x.User == username)
                .ToListAsync(cancellationToken);

            var reportModels = reports.Select(report => _mapper.Map(report));
            var reportModels = reports.Select(report => _mapper.Map<ReportModel>(report));
            var ReportModelList = new ReportModelList();
            return ReportModelList;
        }

        public async Task<ReportLookup> ChangeReportStateAsync(ReportStateModel message, CancellationToken cancellationToken)
        {
            var reportId = message.Id;
            object obj = null;
            if (!Enum.TryParse(typeof(ReportState), message.State, out obj))
                throw new IncorretReportStateException("This report state does not exist!");

            var newReportState = (ReportState)obj;
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
                throw new NotFoundException(typeof(Report), reportId);

            if (report.State == ReportState.Created && newReportState == ReportState.Sent)
                throw new IncorrectChangeReportStateException("You cannot send report until it not was approved!");
            if (report.State == ReportState.Sent && newReportState == ReportState.Approved)
                throw new IncorrectChangeReportStateException("You cannot approved after it has been send!");


            report.State = newReportState;
            var saveCode = await _context.SaveChangesAsync(cancellationToken);

            var lookup = new ReportLookup { Id = report.Id };
            return lookup;
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
            return ReportModelList;*/
        }
    }
}