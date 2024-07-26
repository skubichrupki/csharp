using System.Data.SqlClient;

namespace to_do_list;

public class DbContext
{
    private readonly string connectionString;

    // constructor to accept arguments from Program.cs
    public DbContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task SeedDatabase()
    {
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

        Console.WriteLine(await ExecuteMyQuery(createTablesQuery));
        Console.WriteLine(await ExecuteMyQuery(insertDataQuery));
        Console.WriteLine("database seeded");
    }
    public async Task Read()
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        await connection.OpenAsync();
        string query = "SELECT * FROM [todo].[v_task];";
        using SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        // print header
        string[] columnNames = ["task_id", "task_desc", "status_desc", "add_date"];
        int columnWidth = 15; // adjust the column width
        for (int i = 0; i < columnNames.Length; i++)
        {
            Console.Write(columnNames[i].PadRight(columnWidth));
        }
        Console.WriteLine("");

        // print body
        while (reader.Read()) // true if more rows, one row at a time
        {
            for (int i = 0; i < reader.FieldCount; i++) // one column of row at a time
            {
                string element = Convert.ToString(reader[i]) ?? string.Empty;
                Console.Write(element.PadRight(15));
                // next column in row
            }
            Console.WriteLine(""); 
            // next row
        }
        reader.Close();
    }

    public async Task Create(string taskDesc)
    {
        string query = $"INSERT INTO [todo].[task] (task_desc, status_id, add_date) VALUES ('{taskDesc}', 1, getdate());";
        Console.WriteLine(await ExecuteMyQuery(query));
    }

    public async Task Update(int taskId, int statusId)
    {
        string query = $"UPDATE [todo].[task] SET status_id = {statusId} WHERE task_id = {taskId};";
        Console.WriteLine(await ExecuteMyQuery(query));
    }

    public async Task Delete(int taskId)
    {
        string query = $"DELETE FROM [todo].[task] WHERE task_id = {taskId};";
        Console.WriteLine(await ExecuteMyQuery(query));
    }

    public async Task<int> ExecuteMyQuery(string query)
    {
        try
        {
            // connection object with connection string
            using SqlConnection connection = new SqlConnection(connectionString);
            // open connection
            await connection.OpenAsync();
            // sql command object with query string and connection string
            using SqlCommand command = new SqlCommand(query, connection);
            // returing number of affected rows by executed command
            var result = await command.ExecuteNonQueryAsync();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
    }
}