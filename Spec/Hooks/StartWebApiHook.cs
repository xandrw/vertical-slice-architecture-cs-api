using System.Diagnostics;
using Serilog;

namespace Spec.Hooks;

[Binding]
public class StartWebApiHook
{
    private static Process? _webApiProcess;

    private static readonly string WebApiProjectPath =
        Path.Combine(Directory.GetCurrentDirectory(), "../../../../WebApi");

    private static readonly ILogger Logger = LoggerHooks.GetLogger();

    private const int MillisecondsTimeout = 10_000;

    [BeforeTestRun]
    public static void StartWebApi()
    {
        if (!Directory.Exists(WebApiProjectPath))
        {
            throw new DirectoryNotFoundException($"WebApi project path not found: {WebApiProjectPath}");
        }

        try
        {
            _webApiProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "run",
                    WorkingDirectory = WebApiProjectPath,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            _webApiProcess.OutputDataReceived += (_, args) =>
            {
                if (!string.IsNullOrWhiteSpace(args.Data))
                {
                    Logger.Information($"[WebAPI]: {args.Data}");
                }
            };

            _webApiProcess.ErrorDataReceived += (_, args) =>
            {
                if (!string.IsNullOrWhiteSpace(args.Data))
                {
                    Logger.Error($"[WebAPI-Error]: {args.Data}");
                }
            };

            _webApiProcess.Start();
            _webApiProcess.BeginOutputReadLine();
            _webApiProcess.BeginErrorReadLine();

            Logger.Information($"[WebAPI]: Sleeping for {MillisecondsTimeout / 1000} seconds");
            Thread.Sleep(MillisecondsTimeout);
            Logger.Information("[WebAPI]: Process started");
        }
        catch (Exception e)
        {
            Logger.Error($"[WebAPI-Error]: {e}");
            throw;
        }
    }

    [AfterTestRun]
    public static void StopWebApi()
    {
        if (_webApiProcess is null || _webApiProcess.HasExited)
        {
            Logger.Information("WebAPI process stopped.");
            return;
        }
        
        Logger.Information("Stopping WebAPI process...");
        _webApiProcess.Kill();
        _webApiProcess.WaitForExit();
        _webApiProcess.Dispose();
        Logger.Information("WebAPI process stopped.");
    }
}