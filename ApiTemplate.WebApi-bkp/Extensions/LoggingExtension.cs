using Serilog;
using ILogger = Serilog.ILogger;

namespace ApiTemplate.WebApi.Extensions;

public static class LoggingExtension
{
    public static ILogger Configure(this ILoggingBuilder logging)
    {
        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        logging.ClearProviders();
        logging.AddSerilog(logger);

        return logger;
    }
}
