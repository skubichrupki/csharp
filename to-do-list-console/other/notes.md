eq needed:

- sql server express
- dotnet sdk
- dotnet add package System.Data.SqlClient
- vscode
- azure data studio

*constructor*
``` cs
public DatabaseSeeder(string connectionString)
    {
        this.connectionString = connectionString;
    }
```

*Task = void but for async tasks*
``` cs
public async Task SeedDatabase()
    {
        await //...
    }
```