using Microsoft.Extensions.Configuration;
using Serilog;

namespace NetCoreSamples.Logging.Lib
{
    public static class SerilogSetup
    {
        /// <summary>
        /// Configures Serilog with the provided <see cref="IConfiguration">
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/></param>
        public static void ConfigureSerilog(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
