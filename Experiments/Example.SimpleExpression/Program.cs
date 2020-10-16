using System;
using UseCases;

namespace Example.SimpleExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            var printer = new Printer();
            
            var handler = new UseCaseHandler();
            var greeter = new Hello(printer);
            var divider = new Divide();

            handler.Invoke(() => greeter.Greet("Michal"));
            var result = handler.Query(() => divider.Execute(1,2));
            Console.Out.WriteLine(result);
            handler.Invoke(() => divider.Execute(3, 0));
        }
    }
}
