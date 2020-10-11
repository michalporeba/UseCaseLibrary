using UC = UseCases;

namespace Example.VeryGeneric.UseCases
{
    public class Throw : ICommand
    {
        private readonly UC.IThrow _implementation = new UC.Throw();

        public void Execute()
            => _implementation.Execute();
    }
}