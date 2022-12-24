
Locate in repository root

Drop database
dotnet ef database drop --project .\src\Infrastructure\ --startup-project .\src\WebUI\

Clear all migrations from DB
dotnet ef database update --project .\src\Infrastructure\ --startup-project .\src\WebUI\ 0

List all migrations
dotnet ef migrations list --project .\src\Infrastructure\ --startup-project .\src\WebUI\

Add migration
dotnet ef migrations add --project .\src\Infrastructure\ --startup-project .\src\WebUI\ 'Migration name'

Update database with pending migrations
dotnet ef database update --project .\src\Infrastructure\ --startup-project .\src\WebUI\
