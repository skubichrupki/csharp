
namespace to_do_list;

class Program
{
    // connection options to sql server
    static string connectionString = "Server=localhost\\SQLEXPRESS;Database=projects;Integrated Security=True;";

    static async Task Main(string[] args)
    {
        // seed the database with ADO.NET
        DbContext dbContext = new DbContext(connectionString);
        await dbContext.SeedDatabase();

        bool run = true;
        while (run)
        {
            Console.WriteLine("1 - add task, 2 - show tasks, 3 - update task, 4 - delete task, 0 - exit");
            Console.Write("choose action: ");
            // catch every error that is in this statement or in the methods in this statement
            try 
            {
                // action user input
                int action = Convert.ToInt32(Console.ReadLine());
                int statusId, taskId;
                string taskDesc;

                switch(action)
                {
                    case 0: 
                        Console.WriteLine("bye!");
                        run = false;
                        break;
                    case 1: 
                        Console.Write("enter task desc: ");
                        taskDesc = Console.ReadLine() ?? string.Empty;
                        await dbContext.Create(taskDesc);
                        break;
                    case 2: 
                        await dbContext.Read();
                        break;
                    case 3: 
                        Console.Write("enter task id: ");
                        taskId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("enter new status id: ");
                        statusId = Convert.ToInt32(Console.ReadLine());
                        await dbContext.Update(taskId, statusId);
                        break;
                    case 4:
                        Console.Write("enter task id: ");
                        taskId = Convert.ToInt32(Console.ReadLine()); 
                        await dbContext.Delete(taskId);
                        break;
                    default: 
                        Console.WriteLine("invalid action");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"error: {e.Message} \n try again");
            }
        }
    }
}