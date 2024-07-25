using System;
using System.Data.SqlClient;

namespace to_do_list;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=projects;Integrated Security=True;";
        DbContext dbContext = new DbContext(connectionString);

        // delete task
        Console.Write("enter task id: ");
        int taskId = Convert.ToInt32(Console.ReadLine());
        TaskTable taskTableDelete = new TaskTable {TaskId = taskId}; // same as delete.TaskId = taskId
        await dbContext.Delete(taskTableDelete);
    }
}