using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services
{
    public class ReportService : ReportProto.ReportProtoBase
    {
        readonly IReportRegistry _registry;

        public ReportService(IReportRegistry registry) =>
            _registry = registry;

        public override async Task<ReportModelList> GetReports(ReportUserName request, ServerCallContext context) =>
            await _registry.GetReportsAsync(request, context.CancellationToken);

        public override async Task<DbSaveCondition> DeleteReport(ReportLookup request, ServerCallContext context) =>
            await _registry.DeleteReportAsync(request, context.CancellationToken);

        public override async Task<ReportLookup> ChangeReportState(ReportStateModel request, ServerCallContext context) =>
            await _registry.ChangeReportStateAsync(request, context.CancellationToken);

        public override async Task<ReportMetaData> GetReportMetaData(Empty request, ServerCallContext context) =>
            await _registry.GetReportMetaDataAsync(request, context.CancellationToken);
    }
}
