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

        public override Task<ReportModel> GetReport(ReportLookup request, ServerCallContext context) =>
            _registry.GetReportAsync(request, context.CancellationToken);

        public override Task<ReportLookup> DeleteReport(ReportLookup request, ServerCallContext context) =>
            _registry.DeleteReportAsync(request, context.CancellationToken);

        public override Task<ReportLookup> ApproveReport(ChangeReportState request, ServerCallContext context) =>
            _registry.ApproveReportAsync(request, context.CancellationToken);

        public override Task<ReportLookup> SendReport(ChangeReportState request, ServerCallContext context) =>
            _registry.SendReportAsync(request, context.CancellationToken);

        public override Task<ReportLookup> CancelReport(ChangeReportState request, ServerCallContext context) =>
            _registry.CancelReportAsync(request, context.CancellationToken);

        public override Task<ReportModelList> GetReportsByUser(UserModel request, ServerCallContext context) =>
            _registry.GetReportsByUserAsync(request, context.CancellationToken);

        public override Task<ReportModelList> GetReportsByPeriod(DatesPeriod request, ServerCallContext context) =>
            _registry.GetReportsByPeriodAsync(request, context.CancellationToken);

        public override Task<ReportMetaData> GetReportMetaData(Empty request, ServerCallContext context) =>
            _registry.GetReportMetaDataAsync(request, context.CancellationToken);   
    }
}
