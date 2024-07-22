using System;
using System.Data.SqlClient;

namespace to_do_list;

class Program
{
    // connection options to sql server
    static string connectionString = "Server=localhost\\SQLEXPRESS;Database=projects;Integrated Security=True;";

    static void Main(string[] args)
    {
        bool run = true;
        while (run)
        {
            Read();
            Console.WriteLine("(1 - add task, 0 - exit)");
            Console.Write("choose action: ");
            try 
            {
                int action = Convert.ToInt32(Console.ReadLine());
                switch(action)
                {
                    case 0: Console.WriteLine("bye!");
                    run = false; // stop the while loop
                    break;
                    case 1: Create();
                    break;
                    default: Console.WriteLine("invalid action");
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error: {e}");
            }
        }
    }

    static void Read()
    {
        // #1 connection object
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // #2 open connection
            connection.Open();
            // #3 query
            string queryString = @"
            SELECT task_id, task_desc, status_desc 
            FROM [todo].[task] as task 
            INNER JOIN [todo].[status] AS stat ON stat.status_id = task.status_id";

            // #4 initialize the command instance with query and connection params
            SqlCommand command = new SqlCommand(queryString, connection);  

            // execute SELECT query (ExecuteReader) and assign the SELECT data to reader instance of SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // show data of each column assigned to reader
            while(reader.Read() == true)
            {
                // if null in sql then empty string - value = if ? then : else
                string task_id = (reader["task_id"] != DBNull.Value ? reader["task_id"].ToString() : string.Empty);
                string task_desc = (reader["task_desc"] != DBNull.Value ? reader["task_desc"].ToString() : string.Empty);
                string status_desc = (reader["status_desc"] != DBNull.Value ? reader["status_desc"].ToString() : string.Empty);
                string result = $"| {task_id.PadRight(4)} | {task_desc.PadRight(10)} | {status_desc.PadRight(10)} |";
                // print result in a console table format;
                Console.WriteLine(result);
                Console.Write("+");
                for (int i = 1; i < (result.Length-1); i++) 
                {
                    Console.Write("-");
                }
                Console.Write("+");
                Console.WriteLine("");

            }
            reader.Close();
        }
    }
    static void Create()
    {
        using(SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.Write("enter task name: ");
            string val = Console.ReadLine();
            string queryString = $@"
            INSERT INTO [todo].[task] (task_desc, status_id) 
            VALUES ('{val}', 1);";
            SqlCommand command = new SqlCommand(queryString, connection);
            // ExecuteNonQuery() returns number of affected rows or -1 for DDL;
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"rows affected: {rowsAffected}");
        }
    }
}