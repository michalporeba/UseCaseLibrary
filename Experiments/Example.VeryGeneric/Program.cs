using System;
using Example.VeryGeneric.UseCases;

namespace Example.VeryGeneric
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
            
            handler.Do(greeter, "Michal");
            
        }
    }
}
