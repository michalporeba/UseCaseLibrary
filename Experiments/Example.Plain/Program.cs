using System;
using UseCases;

namespace Example.Plain
{
    static class Program
    {
        static void Main(string[] args)
        {
            var printer = new Printer();
            
            var greeter = new Hello(printer);
            var adder = new Add();
            var thrower = new Throw();
            
            Console.Out.WriteLine("Log: Starting execution of Hello use case");
            greeter.Greet("Michal");
            Console.Out.WriteLine("Log: Finished execution of Hello use case");
            
            Console.Out.WriteLine("Log: Starting execution of Add use case");
            var result = adder.Execute(1, 2);
            Console.Out.WriteLine("Log: Finished execution of Add use case");
            Console.Out.WriteLine(result);

            try
            {
                Console.Out.WriteLine("Log: Starting execution of Throws use case");
                thrower.Execute();
                Console.Out.WriteLine("Log: Finished execution of Throws use case");   
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Log: Failed to execute Throw use case");
            }

        }
    }
}
