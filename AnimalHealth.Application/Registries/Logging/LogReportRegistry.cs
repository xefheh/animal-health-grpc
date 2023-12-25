using AnimalHealth.Application.Exceptions;
using AnimalHealth.Application.Models;
using AnimalHealth.Application.Registries.Interfaces;
using AnimalHealth.Domain.Reports;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;

namespace AnimalHealth.Application.Registries.Logging;

public class LogReportRegistry : IReportRegistry
{
    private readonly IReportRegistry _registry;
    private readonly ILogger<IReportRegistry> _logger;

    public LogReportRegistry(IReportRegistry registry, ILogger<IReportRegistry> logger) =>
        (_registry, _logger) = (registry, logger);

    public Task<ReportLookup> AddReportAsync(Report report, CancellationToken cancellationToken) =>
        _registry.AddReportAsync(report, cancellationToken);

    public async Task<ReportModel> GetReportAsync(ReportLookup request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[REPORT SERVICE] Invoked to get the report with Id {ID}", request.Id);
        try
        {
            var report = await _registry.GetReportAsync(request, cancellationToken);
            _logger.LogInformation("[REPORT SERVICE] Successfully. The resulting gRPC model of the report: {@Model}", report);
            return report;
        }
        catch (NotFoundException e)
        {
            _logger.LogWarning("[REPORT SERVICE] Error occured: {@Error}", e);
            throw;
        }
    }

    public async Task<ReportModelList> GetReportsByPeriodAsync(DatesPeriod period, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[REPORT SERVICE] Invoked to get the reports by Period: ({Start} / {End})", period.DateStart,
            period.DateEnd);
        var reports = await _registry.GetReportsByPeriodAsync(period, cancellationToken);
        _logger.LogInformation("[REPORT SERVICE] Successfully. Report List: {@List} (Count: {Count}",
            reports.Reports, reports.Reports.Count);
        return reports;
    }

    public async Task<ReportModelList> GetReportsByUserAsync(UserModel user, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[REPORT SERVICE] Invoked to get the reports by user: {Name}", user.Name);
        var reports = await _registry.GetReportsByUserAsync(user, cancellationToken);
        _logger.LogInformation("[REPORT SERVICE] Successfully. Report List: {@List} (Count: {Count}",
            reports.Reports, reports.Reports.Count);
        return reports;
    }

    public async Task<ReportLookup> DeleteReportAsync(ReportLookup lookup, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[REPORT SERVICE] Invoked to delete the report with id: {ID}", lookup.Id);
        var dbSaveCondition = await _registry.DeleteReportAsync(lookup, cancellationToken);
        _logger.LogInformation("[REPORT SERVICE] Successfully. Report is deleted");
        return dbSaveCondition;
    }

    public async Task<ReportLookup> GoNextStateAsync(ChangeReportState request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[REPORT SERVICE] User: {User} invoked to change state of report with id: {ID}", request.Changer,
            request.ReportId);
        var reportLookup = await _registry.GoNextStateAsync(request, cancellationToken);
        _logger.LogInformation("[REPORT SERVICE] User: {User} successfully change state of report with id: {ID}",
            request.Changer, reportLookup.Id);
        return reportLookup;
    }

    public async Task<ReportMetaData> GetReportMetaDataAsync(Empty request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("[REPORT SERVICE] Invoked to get the report metadata");
        var metaData = await _registry.GetReportMetaDataAsync(request, cancellationToken);
        _logger.LogInformation("[REPORT SERVICE] Successfully. Report rus names: {@RusList}, Report eng names: {@EngNames}, Column names: {@ColumnNames}",
            metaData.RusReportNames, metaData.EngReportNames, metaData.RusColumnNames);
        return metaData;
    }
}