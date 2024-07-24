using System;
using System.Data.SqlClient;

namespace to_do_list;

public class DatabaseSeeder
{
    private readonly string connectionString;

    // constructor to accept arguments from Program.cs
    public DatabaseSeeder(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task SeedDatabase()
    {
        Console.WriteLine(connectionString);
    }
}