using UC = UseCases;
namespace Example.VeryGeneric.UseCases
{
    public class Hello : UC.Hello
    {
        public Hello(UC.IPrinter printer) 
            : base(printer)
        {
            
        }
    }
}