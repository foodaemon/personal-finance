# Personal Finance

## Compiling:
- dotnet restore src/Finance.Web
- dotnet build src/Finance.Web

## Publishing
- dotnet publish -c Release -f netcoreapp2.0
- dotnet publish -c Release -f netcoreapp2.0 -r linux-x64 --self-contained
- dotnet publish -o /output/dir -c Release -f netcoreapp2.0 -r osx.10.12-x64

## Git Commit Message convention
### DO
- Write the summary line and description of what you have done in the imperative mode, that is as if you were commanding someone. Start the line with "Fix", "Add", "Change" instead of "Fixed", "Added", "Changed".
- Always leave the second line blank.
- Line break the commit message (to make the commit message readable without having to scroll horizontally in gitk).

### DON'T
- Don't end the summary line with a period - it's a title and titles don't end with a period.