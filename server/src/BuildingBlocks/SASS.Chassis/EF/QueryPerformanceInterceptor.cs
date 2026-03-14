using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace SASS.Chassis.EF;

/// <summary>
/// Interceptors to log warning if query is slow
/// </summary>
/// <param name="logger"></param>
internal sealed class QueryPerformanceInterceptor(ILogger<QueryPerformanceInterceptor> logger ) : DbCommandInterceptor
{
    private const long QueryThresholdMs = 100;

    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        LogIfSlow(command, eventData.Duration.TotalMilliseconds);
        return base.ReaderExecuted(command, eventData, result);
    }

    public override async ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    {
        LogIfSlow(command, eventData.Duration.TotalMilliseconds);
        return await base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    private void LogIfSlow(DbCommand command, double elapsedMs)
    {
        if (elapsedMs <= QueryThresholdMs)
        {
            return;
        }

        var commandText = command.CommandText;

        if (command.Parameters.Count > 0)
        {
            commandText += " | Parameters: " + string.Join(", ", command.Parameters.Cast<DbParameter>()
                .Select(p => $"{p.ParameterName}={p.Value}"));
        }

        logger.LogWarning(
            "Slow query detected: {CommandText} | Elapsed time: {ElapsedMilliseconds} ms",
            commandText,
            elapsedMs
        );
    }
}
