using HostApp.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace App.Plugins.Swagger;

public class Plugin: IPlugin
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
    }

    public void Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
            app.UseSwagger();
            app.UseSwaggerUI();
        // }
    }
}