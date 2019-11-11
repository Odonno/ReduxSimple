### To build .NET Standard packages

```
dotnet pack -c Release
```

### To build UWP packages

```
msbuild /t:Restore /p:Configuration=Release
msbuild /t:Pack /p:Configuration=Release
```