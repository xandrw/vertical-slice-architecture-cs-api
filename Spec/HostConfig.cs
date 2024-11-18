namespace Spec;

public static class HostConfig
{
    public const string Host = "http://localhost";
    public const string Port = "5255";
    private const int TimeoutInSeconds = 10;

    public static HttpClient HttpClient => new()
    {
        BaseAddress = new Uri($"{Host}:{Port}"),
        Timeout = TimeSpan.FromSeconds(TimeoutInSeconds)
    };
}