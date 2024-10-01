using DotNetEnv;

namespace WebApi.Config;

public static class EnvConfig
{
    public static void ConfigureEnvVariables(this IConfigurationBuilder config)
    {
        Env.TraversePath().Load();
        config.AddEnvironmentVariables();
    }
}