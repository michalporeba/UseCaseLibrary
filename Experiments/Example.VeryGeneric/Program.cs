using System;
using UC = UseCases;
using Example.VeryGeneric.UseCases;

namespace Example.VeryGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            var printer = new UC.Printer();
            
            var handler = new UseCaseHandler(printer);
            var greeter = new Hello(printer);
            var adder = new Add();
            var thrower = new Throw();
            
            handler.Do(greeter, "World");
            var result = handler.Query<IQuery<int, int, int>, int, int, int>(adder, 1, 2);
            handler.Do(thrower);
        }
    }
}
