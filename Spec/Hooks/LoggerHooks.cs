using Serilog;
using Serilog.Core;

namespace Spec.Hooks;

[Binding]
public class LoggerHooks
{
    private static Logger? _logger;
    
    [BeforeTestRun]
    public static void SetupLogging()
    {
        _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("../../../../WebApi/Logs/test-log-.txt", rollingInterval: RollingInterval.Hour)
            .CreateLogger();
        
        _logger.Information("[SpecFlow.LoggerHooks]: SpecFlow test run started");
    }

    [AfterTestRun]
    public static void TearDownLogging()
    {
        if (_logger is null)
        {
            throw new InvalidOperationException("Logger not initialized.");
        }
        
        _logger.Information("[SpecFlow.LoggerHooks]: SpecFlow test run finished");
        _logger.Dispose();
    }
    
    public static ILogger GetLogger() => _logger ?? throw new InvalidOperationException("Logger not initialized.");
}