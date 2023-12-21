using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services
{
    public class ReportService : ReportProto.ReportProtoBase
    {
        private readonly IReportRegistry _registry;
        private readonly ILogger<ReportService> _logger;

        public ReportService(IReportRegistry registry, ILogger<ReportService> logger) =>
            (_registry, _logger) = (registry, logger);

        public override async Task<ReportModel> GetReport(ReportLookup request, ServerCallContext context)
        {
            _logger.LogInformation("[REPORT SERVICE] Invoked to get the report with Id {ID}", request.Id);
            try
            {
                var report = await _registry.GetReportAsync(request, context.CancellationToken);
                _logger.LogInformation("[REPORT SERVICE] Successfully. The resulting gRPC model of the report: {@Model}", report);
                return report;
            }
            catch (NotFoundException e)
            {
                _logger.LogWarning("[REPORT SERVICE] Error occured: {@Error}", e);
                throw new RpcException(new Status(StatusCode.NotFound, "Report not exist"));
            }
        }

        public override async Task<ReportLookup> DeleteReport(ReportLookup request, ServerCallContext context)
        {
            _logger.LogInformation("[REPORT SERVICE] Invoked to delete the report with id: {ID}", request.Id);
            var dbSaveCondition = await _registry.DeleteReportAsync(request, context.CancellationToken);
            _logger.LogInformation("[REPORT SERVICE] Successfully. Report is deleted");
            return dbSaveCondition;
        }

        public override async Task<ReportLookup> GoNextState(ChangeReportState request, ServerCallContext context)
        {
            _logger.LogInformation("[REPORT SERVICE] User: {User} invoked to change state of report with id: {ID}", request.Changer,
                request.ReportId);
            var reportLookup = await _registry.GoNextStateAsync(request, context.CancellationToken);
            _logger.LogInformation("[REPORT SERVICE] User: {User} successfully change state of report with id: {ID}",
                request.Changer, reportLookup.Id);
            return reportLookup;
        }

        public override async Task<ReportModelList> GetReportsByUser(UserModel request, ServerCallContext context)
        {
            _logger.LogInformation("[REPORT SERVICE] Invoked to get the reports by user: {Name}", request.Name);
            var reports = await _registry.GetReportsByUserAsync(request, context.CancellationToken);
            _logger.LogInformation("[REPORT SERVICE] Successfully. Report List: {@List} (Count: {Count}",
                reports.Reports, reports.Reports.Count);
            return reports;
        }

        public override async Task<ReportModelList> GetReportsByPeriod(DatesPeriod request, ServerCallContext context)
        {
            _logger.LogInformation("[REPORT SERVICE] Invoked to get the reports by Period: ({Start} / {End})", request.DateStart,
                request.DateEnd);
            var reports = await _registry.GetReportsByPeriodAsync(request, context.CancellationToken);
            _logger.LogInformation("[REPORT SERVICE] Successfully. Report List: {@List} (Count: {Count}",
                reports.Reports, reports.Reports.Count);
            return reports;
        }

        public override async Task<ReportMetaData> GetReportMetaData(Empty request, ServerCallContext context)
        {
            _logger.LogInformation("[REPORT SERVICE] Invoked to get the report metadata");
            var metaData = await _registry.GetReportMetaDataAsync(request, context.CancellationToken);
            _logger.LogInformation("[REPORT SERVICE] Successfully. Report rus names: {@RusList}, Report eng names: {@EngNames}, Column names: {@ColumnNames}",
                metaData.RusReportNames, metaData.EngReportNames, metaData.RusColumnNames);
            return metaData;
        }
    }
}