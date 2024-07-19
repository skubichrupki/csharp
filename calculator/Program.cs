using System.Collections;
using System.Globalization;
using System.Xml.XPath;

namespace csharp_calculator;

public class Program
{
    static void Main(string[] args)
    {
        int num1, num2, operation;

        int DoTheMath(int num1, int num2, int operation)
        {
            int result;
            switch (operation)
            {
                case 1: result = num1 + num2;
                    break;
                case 2: result = num1 - num2;
                    break;
                case 3: result = num1 * num2;
                    break;
                case 4: 
                    if (num2 == 0) 
                    {
                        Console.WriteLine("divided by 0");
                        result = num1;
                    }
                    else
                    {
                        result = num1 / num2;
                    }
                break;
                default: 
                    Console.WriteLine("weird operation, try again");
                    result = num1;
                break;
            }
            return result;
        }

        // user input 
        Console.WriteLine("calculator");
        Console.Write("first number: ");
        num1 = Convert.ToInt32(Console.ReadLine());
        
        while (true)
        {
            // user input 
            try 
            {
                Console.WriteLine("(1 +) (2 -) (3 *) (4 /)");
                Console.Write("operation: ");
                operation = Convert.ToInt32(Console.ReadLine());
                Console.Write("second number: ");
                num2 = Convert.ToInt32(Console.ReadLine());
                
                int result = DoTheMath(num1, num2, operation); 

                // new initial value is the result of math operation
                Console.WriteLine($"result: {result}");
                num1 = result;
            }
            
            catch (Exception e)
            {
                Console.WriteLine($"error: {e}");
                Console.WriteLine("try again");
            }
        }

        // prevent the program from exiting by user input
        // Console.ReadKey();
    }
}