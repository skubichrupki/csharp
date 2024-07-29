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

            // create new Tasks object with data
            var duty = new Duty()
            {
                // Duty_ID is auto-incremented
                DutyDesc = "test",
                StatusID = 1
            };

            // add object task to Table Task in DbContext
            context.Duty.Add(duty);
            context.SaveChanges();
         }
        Console.WriteLine("data added");
    }
}
