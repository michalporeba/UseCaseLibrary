using System;
using Example.AspectInjector.Aspects;
using UC = UseCases;

namespace Example.AspectInjector.UseCases
{
    [LogExecution]
    public class LocalHello : UC.IHello
    {
        private readonly UC.IHello _target;

        public LocalHello(UC.IPrinter printer)
        {
            _target = new UC.Hello(printer);
        }
        
        public void Greet(string name)
        {
            _target.Greet(name);
        }
    }
}