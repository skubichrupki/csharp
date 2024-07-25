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
        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string createTablesQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'status' AND xtype = 'U')
                CREATE TABLE [todo].[status] 
                (
                status_id int primary key identity (1,1),
                status_desc varchar(50)
                );
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'task' AND xtype = 'U')
                CREATE TABLE [todo].[task] 
                (
                task_id int primary key identity (1,1),
                task_desc varchar(50) not null,
                status_id int,
                add_date datetime2
                );
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'v_task' AND xtype = 'V')
                EXECUTE('CREATE VIEW [todo].[v_task] AS
                SELECT task_id, task_desc, status_desc, add_date
                FROM [todo].[task] as task 
                INNER JOIN [todo].[status] AS stat ON stat.status_id = task.status_id')
                ";

            string insertDataQuery = @"
                IF NOT EXISTS (SELECT TOP (1) * FROM [todo].[status])
                INSERT INTO [todo].[status] (status_desc)
                VALUES ('to-do'), ('wip'), ('done');
                ";

            using (SqlCommand command = new SqlCommand(createTablesQuery, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
            using (SqlCommand command = new (insertDataQuery, connection)) // same op diff syntax
            {
                await command.ExecuteNonQueryAsync();
            }
            Console.WriteLine("database seeded");
        }
        catch (Exception e)
        {
            Console.WriteLine($"error: {e}\ndatabase not seeded");
        }
    }
}