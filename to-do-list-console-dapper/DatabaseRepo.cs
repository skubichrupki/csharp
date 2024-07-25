using System.Data;
using System.Data.SqlClient;
using Dapper;

public class DatabaseRepo
{
    public readonly string connectionString;

    public DatabaseRepo(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // delete
    public async Task Delete(TaskTable taskTable)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM [todo].[task] WHERE task_id = @TaskId";
            await connection.ExecuteAsync(query, new {TaskId = taskTable.TaskId});
        }
    }
}