# Personal Finance

## Compiling:

* dotnet restore src/Finance/Web
* dotnet build src/Finance/Web

## Publishing

* dotnet publish -c Release -f netcoreapp2.0
* dotnet publish -c Release -f netcoreapp2.0 -r ubuntu.16.04-x64 --self-contained
* dotnet publish -o /output/dir -c Release -f netcoreapp2.0 -r osx.10.12-x64
