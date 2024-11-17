namespace Spec;

public static class HostConfig
{
    private const string Host = "http://localhost";
    private const string Port = "5255";
    private const int TimeoutInSeconds = 10;

    public static HttpClient HttpClient => new()
    {
        BaseAddress = new Uri($"{Host}:{Port}"),
        Timeout = TimeSpan.FromSeconds(TimeoutInSeconds)
    };
}