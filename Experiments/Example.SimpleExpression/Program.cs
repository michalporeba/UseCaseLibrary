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
            var adder = new Add();
            var thrower = new Throw();

            handler.Invoke(() => greeter.Greet("Michal"));
            var result = handler.Query(() => adder.Execute(1,2));
            Console.Out.WriteLine(result);
            handler.Invoke(() => thrower.Execute());

        }
    }
}
