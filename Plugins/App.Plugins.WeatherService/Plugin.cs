using HostApp.Shared;
using Microsoft.AspNetCore.Builder;

namespace App.Plugins.WeatherService;

public class Plugin: IPlugin
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        Console.WriteLine($"ConfigureServices :: {typeof(Plugin).FullName}");
    }

    public void Configure(WebApplication app)
    {
        Console.WriteLine($"Configure :: {typeof(Plugin).FullName}");
    }
}