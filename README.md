# aspnetcore-api-plugin-sample

Rudimentary sample modular plugin based application, based on https://learn.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support.

## Try it out

```sh
npm run build
cd dist/host
./HostApp
```

Plugins are loaded dynamically from `dist/plugins/*` ...

- dist/plugins/App.Plugins.WeatherService/App.Plugins.WeatherService.dll
- dist/plugins/App.Plugins.Swagger/App.Plugins.Swagger.dll

Host app runs from `dist/host` ...

### Notes

A more advanced setup would do dynamic reloading of plugins, and would also
demonstrate using the dependency injection container further. TBD ...

