using Microsoft.AspNetCore.Builder;

namespace HostApp.Shared;

public interface IPlugin
{
    void ConfigureServices(WebApplicationBuilder builder);
    void Configure(WebApplication app);
}