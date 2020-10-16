using Example.AspectInjector.Aspects;
using UC = UseCases;

namespace Example.AspectInjector.UseCases
{
    [LogExecution]
    public class LocalDivider : UC.IDivide
    {
        private readonly UC.IDivide _target = new UC.Divide();

        public float Execute(float a, float b)
            => _target.Execute(a, b);
    }
}