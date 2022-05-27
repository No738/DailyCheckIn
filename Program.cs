using Serilog;

namespace GenshinCheckIn
{
    internal class Program
    {
        private static void Main()
        {
            BuildLogger();
            Log.Information("Starting a Genshin Impact auto daily check-in tool...");

            AutoDailyCheckIn.Run();
        }

        private static void BuildLogger()
        {
            var loggerConfiguration = new LoggerConfiguration();

#if DEBUG
            loggerConfiguration.MinimumLevel.Debug();
#endif

            Log.Logger = loggerConfiguration
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss}] {Level:u3} > {Message:lj}{NewLine}{Exception}")
                .CreateLogger(); ;
        }
    }
}