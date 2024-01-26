﻿using Microsoft.Extensions.Configuration;
using Serilog;

namespace NetCoreSamples.Logging.Lib
{
    public static class SerilogSetup
    {
        public static void ConfigureSerilog(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
