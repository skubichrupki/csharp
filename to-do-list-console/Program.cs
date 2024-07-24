using System;
using System.Data.SqlClient;

namespace to_do_list;

class Program
{
    // connection options to sql server
    static string connectionString = "Server=localhost\\SQLEXPRESS;Database=projects;Integrated Security=True;";

    static async Task Main(string[] args)
    {

        // seed the database
        DatabaseSeeder seeder = new DatabaseSeeder(connectionString);
        await seeder.SeedDatabase();

        bool run = true;
        Console.WriteLine("welcome to to-do list");
        while (run)
        {
            Console.WriteLine("\n1 - add task, 2 - show tasks, 3 - update task, 4 - delete task, 0 - exit");
            Console.Write("choose action: ");
            // catch every error that is in this statement or in the methods in this statement
            try 
            {
                // action user input
                int action = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");

                switch(action)
                {
                    case 0: Console.WriteLine("bye!");
                    run = false; // stop the while loop
                    break;
                    case 1: Create();
                    break;
                    case 2: Read();
                    break;
                    case 3: Update();
                    break;
                    case 4: Delete();
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
            string queryString = "SELECT * FROM [todo].[v_task];";

            // #4 initialize the command instance with query and connection params
            SqlCommand command = new SqlCommand(queryString, connection);  
            // execute SELECT query (ExecuteReader) and assign the SELECT data to reader instance of SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // print table header in console table format
            string[] columnNames = ["task_id", "task_desc", "status_desc", "add_date"];
            int columnWidth = 15; // adjust the column width
            for (int i = 0; i < columnNames.Length; i++)
            {
                Console.Write(columnNames[i].PadRight(columnWidth));
            }
            Console.WriteLine("");
            
            // show data of each column assigned to reader
            while(reader.Read() == true)
            {
                // List object with elements of type string
                List<string> columnList = new List<string>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    // ?? = isnull then
                    string tmp = Convert.ToString(reader[i]) ?? string.Empty;
                    columnList.Add(tmp);
                }
                string[] columns = columnList.ToArray();

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
            string val = Console.ReadLine() ?? string.Empty;
            string queryString = $@"
            INSERT INTO [todo].[task] (task_desc, status_id, add_date) 
            VALUES ('{val}', 1, getdate());";
            SqlCommand command = new SqlCommand(queryString, connection);
            // ExecuteNonQuery() returns number of affected rows or -1 for DDL;
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"rows affected: {rowsAffected}");
        }
    }
    static void Update()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.Write("enter task id: ");
            int task_id = Convert.ToInt32(Console.ReadLine());
            Console.Write("1 - to-do, 2 - wip, 3 - done\nenter new status: ");
            int status_id = Convert.ToInt32(Console.ReadLine());
            string queryString = $@"
            UPDATE [todo].[task]
            SET status_id = {status_id}
            WHERE task_id = {task_id};";
            SqlCommand command = new SqlCommand(queryString, connection);
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"rows affected: {rowsAffected}");
        }
    }
    static void Delete()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("enter task id: ");
            int task_id = Convert.ToInt32(Console.ReadLine());
            string queryString = $@"
            DELETE FROM [todo].[task]
            WHERE task_id = {task_id};";
            SqlCommand command = new SqlCommand(queryString, connection);
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"rows affected: {rowsAffected}");
        }
    }
}