using UC = UseCases;

namespace Example.VeryGeneric.UseCases
{
    public class Add : IQuery<int, int, int>
    {
        private readonly UC.IAdd _implementation;

        public Add()
        {
            _implementation = new UC.Add();
        }

        public int Query(int a, int b)
            => _implementation.Execute(a, b);
    }
}