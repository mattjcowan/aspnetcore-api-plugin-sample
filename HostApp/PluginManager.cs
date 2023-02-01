using System.Reflection;
using HostApp.Shared;

namespace HostApp;

public static class PluginManager
{
    public static IEnumerable<Assembly> LoadPluginAssemblies(string baseDirectory)
    {
        if (!Directory.Exists(baseDirectory))
        {
            var programAssemblyDirectory = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            if (!string.IsNullOrWhiteSpace(programAssemblyDirectory))
            {
                baseDirectory = Path.GetFullPath(Path.Combine(programAssemblyDirectory,
                    baseDirectory));
            }
        }

        if (!Directory.Exists(baseDirectory)) yield break;
        
        var pluginAssemblyPaths = Directory.GetFiles(baseDirectory, "App.Plugins.*.dll", SearchOption.AllDirectories);
        foreach (var pluginAssemblyPath in pluginAssemblyPaths)
        {
            Console.WriteLine($"Loading plugins from: {pluginAssemblyPath}");
            var loadContext = new PluginLoadContext(pluginAssemblyPath);
            yield return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginAssemblyPath)));
        }
    }

    public static IEnumerable<IPlugin> CreatePlugins(Assembly assembly)
    {
        var count = 0;

        foreach (var type in assembly.GetTypes())
        {
            if (!typeof(IPlugin).IsAssignableFrom(type)) continue;
            if (Activator.CreateInstance(type) is not IPlugin result) continue;
                
            count++;
            yield return result;
        }

        if (count != 0) yield break;
        
        var availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
        throw new ApplicationException(
            $"Can't find any type which implements IPlugin in {assembly} from {assembly.Location}.\n" +
            $"Available types: {availableTypes}");
    }
}