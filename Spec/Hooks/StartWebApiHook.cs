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

    private const int MaxRetryAttempts = 10;
    private const int RetryDelayMilliseconds = 1000;
    private static readonly HttpClient HttpClient = HostConfig.HttpClient;

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

            Logger.Information("[WebAPI]: Waiting for the Web API to become available...");

            if (!WaitForWebApiReadiness())
            {
                throw new Exception("[WebAPI]: The Web API did not become available within the expected time.");
            }

            Logger.Information("[WebAPI]: Web API is ready.");
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

    private static bool WaitForWebApiReadiness()
    {
        const string healthCheckEndpoint = "/api/health";

        for (var attempt = 1; attempt <= MaxRetryAttempts; attempt++)
        {
            try
            {
                var response = HttpClient.GetAsync(healthCheckEndpoint).Result;
                if (response.IsSuccessStatusCode) return true;
            }
            catch
            {
                // Ignore exceptions as the API might not be ready yet
            }

            Logger.Information($"[WebAPI]: Attempt {attempt}/{MaxRetryAttempts} - Web API not ready yet.");
            Thread.Sleep(RetryDelayMilliseconds);
        }

        return false;
    }
}