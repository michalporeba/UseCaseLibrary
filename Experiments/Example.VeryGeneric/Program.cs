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
            var divider = new Divide();
            
            handler.Do(greeter, "World");
            var result = handler.Query<IQuery<float, float, float>, float, float, float>(divider, 1, 2);
            handler.Query<IQuery<float, float, float>, float, float, float>(divider, 3, 0);
        }
    }
}
