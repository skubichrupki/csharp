using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using to_do_list_console_ef.models;

namespace to_do_list_console_ef;

class Program
{
    static void Main(string[] args)
    {
        // scope for Database Context
        using (var context = new MyDbContext())
         {
            context.Database.EnsureCreated();

            Console.WriteLine("1 - add duty, 2 - show duties, 3 - update duty, 4 - delete duty, 0 - exit");
            Console.Write("choose action: ");
            int action = Convert.ToInt32(Console.ReadLine());
            int statusId, dutyId;
            string dutyDesc;

            switch(action)
            {
                case 0: 
                    Console.WriteLine("bye!");
                    break;
                case 1: 
                    Console.Write("enter duty desc: ");
                    dutyDesc = Console.ReadLine() ?? string.Empty;
                    // create new Tasks object with data
                    var duty = new Duty()
                    {
                        // Duty_ID is auto-incremented
                        DutyDesc = dutyDesc,
                        StatusID = 1
                    };
                    context.Duty.Add(duty); // create
                    context.SaveChanges();
                    break;
                case 2:
                    var dutyList = context.Duty.ToList(); // read
                    foreach (var i in dutyList)
                    {
                        Console.WriteLine(i.DutyID);
                        Console.WriteLine("test");
                    }
                    break;
                case 3:
                    Console.Write("enter duty id: ");
                    dutyId = Convert.ToInt32(Console.ReadLine());
                    Console.Write("enter new status id: ");
                    statusId = Convert.ToInt32(Console.ReadLine());
                    break;
                case 4:
                    Console.Write("enter duty id: ");
                    dutyId = Convert.ToInt32(Console.ReadLine());
                    var dutyRecord = context.Duty.Where(record => record.DutyID == dutyId).FirstOrDefault(); // where
                    if (dutyRecord != null)
                    {
                        context.Duty.Remove(dutyRecord); // delete
                    }
                    break;
                default: 
                    Console.WriteLine("invalid action");
                    break;
            }
        }
        Console.WriteLine("data added");
    }
}
