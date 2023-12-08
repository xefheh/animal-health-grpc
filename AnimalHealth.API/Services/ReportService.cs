using AnimalHealth.Application.Interfaces.Registries;
using AnimalHealth.Application.Models;
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
            await _registry.DeleteContractAsync(request, context.CancellationToken);
    }
}
