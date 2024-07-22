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
        Console.WriteLine("welcome to to-do list");
        while (run)
        {
            Console.WriteLine("(1 - add task, 2 - show tasks, 0 - exit)");
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
                    case 2: Read();
                    break;
                    default: Console.WriteLine("invalid action");
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error: {e} \n try again");
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

            // print table header in console table format
            string[] columnNames = ["task_id", "task_desc", "status_desc"];
            int columnWidth = 15; // adjust the column width
            for (int i = 0; i < columnNames.Length; i++)
            {
                Console.Write(columnNames[i].PadRight(columnWidth));
            }
            Console.WriteLine("");
            
            // show data of each column assigned to reader
            while(reader.Read() == true)
            {
                // if null in sql then empty string, ternary: type value = condition ? then : else
                string task_id = (reader["task_id"] != DBNull.Value ? reader["task_id"].ToString() : string.Empty);
                string task_desc = (reader["task_desc"] != DBNull.Value ? reader["task_desc"].ToString() : string.Empty);
                string status_desc = (reader["status_desc"] != DBNull.Value ? reader["status_desc"].ToString() : string.Empty);
                string[] columns = [task_id, task_desc, status_desc];
                 // print row lines
                for (int i = 0; i <= (columnWidth * columns.Length); i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine("");
                // print table rows in console table format
                for (int i = 0; i < columns.Length; i++)
                {
                    Console.Write(columns[i].PadRight(columnWidth));
                }
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