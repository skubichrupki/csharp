using System;
using System.Data.SqlClient;

namespace to_do_list;

class Program
{

    static async Task Main(string[] args)
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=projects;Integrated Security=True;";
        DatabaseRepo databaseRepo = new DatabaseRepo(connectionString);

        // delete task
        TaskTable delete = new TaskTable {TaskId = 1}; // same as delete.TaskId = 1
        await databaseRepo.Delete(delete);
        Console.WriteLine($"task with id {delete.TaskId} deleted");
    }
}