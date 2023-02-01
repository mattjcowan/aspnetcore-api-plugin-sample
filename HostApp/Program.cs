using System.Reflection;
using HostApp;
using HostApp.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder = builder.Services.AddControllers();

var plugins = new List<IPlugin>();
var pluginsBaseDirectory = builder.Configuration["PluginsBaseDirectory"];
if (!string.IsNullOrWhiteSpace(pluginsBaseDirectory))
{
    var pluginAssemblies = PluginManager.LoadPluginAssemblies(pluginsBaseDirectory);
    foreach(var pluginAssembly in pluginAssemblies)
    {
        plugins.AddRange(PluginManager.CreatePlugins(pluginAssembly));
        mvcBuilder.AddApplicationPart(pluginAssembly);
    }
    foreach (var plugin in plugins)
        plugin.ConfigureServices(builder);
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

foreach (var plugin in plugins)
    plugin.Configure(app);

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();