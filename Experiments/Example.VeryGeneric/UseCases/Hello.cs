using UC = UseCases;
namespace Example.VeryGeneric.UseCases
{
    public class Hello : ICommand<string>
    {
        private readonly UC.IHello _implementation;
        public Hello(UC.IPrinter printer) 
        {
            _implementation = new UC.Hello(printer);            
        }

        public void Execute(string name)
            => _implementation.Greet(name);
    }
}