### Migration Commands

execute below command from /car-session/server folder

> dotnet ef migrations add InitialCreate -o Infrastructure/Persistence/Migrations/ --context AppDbContext

> dotnet ef database update -s ./server.csproj

> dotnet ef database drop -s ./server.csproj --context AppDbContext