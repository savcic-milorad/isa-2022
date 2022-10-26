
Locate in repository root

Clear all migrations
dotnet ef database update --project .\src\Infrastructure\ --startup-project .\src\WebUI\ 0

List all migrations
dotnet ef migrations list --project .\src\Infrastructure\ --startup-project .\src\WebUI\

Add initial migration
dotnet ef migrations add --project .\src\Infrastructure\ --startup-project .\src\WebUI\ 'Initial migration'

Update database
dotnet ef database update --project .\src\Infrastructure\ --startup-project .\src\WebUI\
