using System.Diagnostics;

namespace Spec.Hooks;

[Binding]
public class StartWebApiHook
{
    private static Process? _webApiProcess;
    private static readonly string WebApiProjectPath =
        Path.Combine(Directory.GetCurrentDirectory(), "../../../../WebApi");

    [BeforeTestRun]
    public static void StartWebApi()
    {
        if (!Directory.Exists(WebApiProjectPath))
        {
            throw new DirectoryNotFoundException($"WebApi project path not found: {WebApiProjectPath}");
        }

        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "run",
            WorkingDirectory = WebApiProjectPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        _webApiProcess = new Process { StartInfo = startInfo };

        _webApiProcess.OutputDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                Console.WriteLine($"[WebAPI]: {args.Data}");
            }
        };

        _webApiProcess.ErrorDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                Console.Error.WriteLine($"[WebAPI-Error]: {args.Data}");
            }
        };

        _webApiProcess.Start();
        _webApiProcess.BeginOutputReadLine();
        _webApiProcess.BeginErrorReadLine();

        Thread.Sleep(10000);
        Console.WriteLine("WebAPI process started.");
    }

    [AfterTestRun]
    public static void StopWebApi()
    {
        if (_webApiProcess is not null && !_webApiProcess.HasExited)
        {
            Console.WriteLine("Stopping WebAPI process...");
            _webApiProcess.Kill();
            _webApiProcess.Dispose();
            Console.WriteLine("WebAPI process stopped.");
        }
    }
}