using System.Collections;
using System.Diagnostics;

namespace Spec;

public class Application
{
    private readonly Process _process;
    private const int Port = 5255;
    private const int Delay = 5000; // 5s

    private Application(Process process)
    {
        _process = process;
    }

    public static async Task<Application> StartNewAsync()
    {
        Console.WriteLine($"Starting WebApi on port {Port}...");
        Process app = StartApplication(Port);

        await WaitForAppToBeAvailableAsync(Port);

        return new Application(app);
    }

    private static async Task WaitForAppToBeAvailableAsync(int port)
    {
        using var httpClient = new HttpClient();
        for (int i = 0; i < 10; i++)
        {
            try
            {
                var response = await httpClient.GetAsync($"http://localhost:{port}/api/current-date-time");
                if (response.IsSuccessStatusCode) return;
            }
            catch (HttpRequestException)
            {
                // Swallow exceptions while waiting for the app
            }

            await Task.Delay(500);
        }

        throw new TimeoutException("WebApi Project did not start within the expected time.");
    }

    private static Process StartApplication(int port)
    {
        var processStartInfo = new ProcessStartInfo("dotnet", $"run --project WebApi --urls=http://localhost:{port}")
        {
            UseShellExecute = false,
            CreateNoWindow = true,
            EnvironmentVariables = { ["ASPNETCORE_ENVIRONMENT"] = "Development" }
        };

        Console.WriteLine("Completed ProcessStartInfo...");

        foreach (DictionaryEntry envVar in Environment.GetEnvironmentVariables())
        {
            processStartInfo.EnvironmentVariables[(string)envVar.Key] = (string)envVar.Value!;
        }

        Console.WriteLine("Copied environment variables...");

        var app = new Process { StartInfo = processStartInfo };
        app.Start();

        Console.WriteLine("Application started.");

        return app;
    }

    public ValueTask DisposeAsync()
    {
        if (!_process.HasExited)
        {
            _process.Kill(entireProcessTree: true);
        }

        _process.Dispose();
        return ValueTask.CompletedTask;
    }
}