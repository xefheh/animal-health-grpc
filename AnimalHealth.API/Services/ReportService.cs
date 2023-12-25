using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Factories;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using AnimalHealth.Application.Registries.Logging;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AnimalHealth.API.Services
{
    public class ReportService : ReportProto.ReportProtoBase
    {
        private readonly IReportRegistry _registry;

        public ReportService(LogRegistryFactory<IReportRegistry, LogReportRegistry> factory,
            ILogger<IReportRegistry> logger) => _registry = factory.CreateLogRegistry();

        public override async Task<ReportModel> GetReport(ReportLookup request, ServerCallContext context)
        {
            try
            {
                return await _registry.GetReportAsync(request, context.CancellationToken);
            }
            catch (NotFoundException e)
            {
                throw new RpcException(new Status(StatusCode.NotFound, e.Message));
            }
        }

        public override async Task<ReportLookup> DeleteReport(ReportLookup request, ServerCallContext context) =>
            await _registry.DeleteReportAsync(request, context.CancellationToken);

        public override async Task<ReportLookup> GoNextState(ChangeReportState request, ServerCallContext context) =>
            await _registry.GoNextStateAsync(request, context.CancellationToken);

        public override async Task<ReportModelList> GetReportsByUser(UserModel request, ServerCallContext context) =>
            await _registry.GetReportsByUserAsync(request, context.CancellationToken);

        public override async Task<ReportModelList>
            GetReportsByPeriod(DatesPeriod request, ServerCallContext context) =>
            await _registry.GetReportsByPeriodAsync(request, context.CancellationToken);

        public override async Task<ReportMetaData> GetReportMetaData(Empty request, ServerCallContext context) =>
            await _registry.GetReportMetaDataAsync(request, context.CancellationToken);
    }
}