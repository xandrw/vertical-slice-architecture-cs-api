using Serilog;

namespace Spec.Hooks;

// [Binding]
public class LoggerHooks
{
    [BeforeTestRun]
    public static void SetupLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("../../../../WebApi/Logs/test-log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        Log.Information("SpecFlow test run started");
    }

    [AfterTestRun]
    public static void TearDownLogging()
    {
        Log.Information("SpecFlow test run finished");
        Log.CloseAndFlush();
    }
}