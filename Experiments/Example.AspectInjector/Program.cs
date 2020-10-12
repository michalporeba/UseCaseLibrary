using Example.AspectInjector.UseCases;
using UseCases;

namespace Example.AspectInjector
{
    class Program
    {
        static void Main(string[] args)
        {
            var printer = new Printer();
            
            var greeter = new LocalHello(printer);
            var adder = new LocalAdder();
            //var thrower = new LocalThrower();
            
            greeter.Greet("World");
            var result = adder.Execute(1, 2);
            
        }
    }
}
