using Serilog;

namespace Spec.Hooks;

[Binding]
public class LoggerHooks
{
    public static ILogger GetLogger() =>
        Log.Logger ?? throw new InvalidOperationException("Logger not initialized.");

    [BeforeTestRun]
    public static void SetupLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("../../../../WebApi/Logs/test-log-.txt", rollingInterval: RollingInterval.Hour)
            .CreateLogger();

        Log.Information("[SpecFlow.LoggerHooks]: SpecFlow test run started");
    }

    [AfterTestRun]
    public static void TearDownLogging()
    {
        if (Log.Logger is null)
        {
            throw new InvalidOperationException("Logger not initialized.");
        }

        Log.Information("[SpecFlow.LoggerHooks]: SpecFlow test run finished");
        Log.CloseAndFlush();
    }
}