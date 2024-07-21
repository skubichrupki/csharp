using System;
using System.Data.SqlClient;

namespace to_do_list;

class Program
{
    // connection options to sql server
    static string connectionString = "Server=localhost\\SQLEXPRESS;Database=projects;Integrated Security=True;";

    static void Main(string[] args)
    {
        Read();
    }

    static void Read()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM [todo].[status]";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read() == true)
            {
                Console.WriteLine($"ID: {reader["status_id"]} Description: {reader["status_desc"]}");
            }
        }
    }
}