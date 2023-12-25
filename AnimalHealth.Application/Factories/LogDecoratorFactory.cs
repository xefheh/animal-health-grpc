using Microsoft.Extensions.Logging;

namespace AnimalHealth.Application.Factories;

public class LogRegistryFactory<TSource, TLog>
{
    private readonly TSource _source;
    private readonly ILogger<TSource> _logger;

    public LogRegistryFactory(TSource source, ILogger<TSource> logger) =>
        (_source, _logger) = (source, logger);

    public TLog CreateLogRegistry() =>
        (TLog)Activator.CreateInstance(typeof(TLog), new object[] { _source, _logger });
}