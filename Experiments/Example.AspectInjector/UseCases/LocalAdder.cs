using Example.AspectInjector.Aspects;
using UC = UseCases;

namespace Example.AspectInjector.UseCases
{
    [LogExecution]
    public class LocalAdder : UC.IAdd
    {
        private readonly UC.IAdd _target = new UC.Add();

        public int Execute(int a, int b)
            => _target.Execute(a, b);
    }
}