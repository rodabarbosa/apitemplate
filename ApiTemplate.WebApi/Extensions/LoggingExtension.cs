using Serilog;

namespace ApiTemplate.WebApi.Extensions;

public static class LoggingExtension
{
    public static void Configure(this ILoggingBuilder logging)
    {
        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        logging.ClearProviders();
        logging.AddSerilog(logger);
    }
}
