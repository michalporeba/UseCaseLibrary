using System;

namespace UseCases
{
    public interface IHello
    {
        void Greet(string name);
    }
    
    public class Hello : IHello
    {
        private readonly IPrinter printer;
        
        public Hello(IPrinter printer)
        {
            this.printer = printer ?? throw new ArgumentNullException(nameof(printer));
        }

        public void Greet(string name) => printer.Print($"Hello, {name}");
    }
}