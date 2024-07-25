using System.Data;
using System.Data.SqlClient;
using Dapper;

public class DbContext
{
    public readonly string connectionString;

    public DbContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // delete
    public async Task Delete(TaskTable taskTable)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        string query = "DELETE FROM [todo].[task] WHERE task_id = @TaskId";
        int rowsAffected = await connection.ExecuteAsync(query, new {TaskId = taskTable.TaskId});
        Console.WriteLine(rowsAffected);
    }
}