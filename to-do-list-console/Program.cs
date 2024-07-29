
namespace to_do_list;

class Program
{
    // connection options to sql server
    static readonly string connectionString = "Server=localhost\\SQLEXPRESS;Database=projects;Integrated Security=True;Trusted_Connection=True";

    static async Task Main(string[] args)
    {
        // seed the database with ADO.NET
        MyDbContext myDbContext = new MyDbContext(connectionString);
        await myDbContext.SeedDatabase();

        bool run = true;
        while (run)
        {
            Console.WriteLine("1 - add duty, 2 - show duties, 3 - update duty, 4 - delete duty, 0 - exit");
            Console.Write("choose action: ");
            // catch every error that is in this statement or in the methods in this statement
            try 
            {
                // action user input
                int action = Convert.ToInt32(Console.ReadLine());
                int statusId, dutyId;
                string dutyDesc;

                switch(action)
                {
                    case 0: 
                        Console.WriteLine("bye!");
                        run = false;
                        break;
                    case 1: 
                        Console.Write("enter duty desc: ");
                        dutyDesc = Console.ReadLine() ?? string.Empty;
                        await myDbContext.Create(dutyDesc);
                        break;
                    case 2: 
                        await myDbContext.Read();
                        break;
                    case 3: 
                        Console.Write("enter duty id: ");
                        dutyId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("enter new status id: ");
                        statusId = Convert.ToInt32(Console.ReadLine());
                        await myDbContext.Update(dutyId, statusId);
                        break;
                    case 4:
                        Console.Write("enter duty id: ");
                        dutyId = Convert.ToInt32(Console.ReadLine()); 
                        await myDbContext.Delete(dutyId);
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