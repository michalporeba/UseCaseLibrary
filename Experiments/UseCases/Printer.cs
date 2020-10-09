using System;

namespace UseCases
{
    public interface IPrinter
    {
        void Print(string text);
    }
    public class Printer : IPrinter
    {
        public void Print(string text)
            => Console.Out.WriteLine(text);
    }
}
