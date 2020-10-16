using UC = UseCases;

namespace Example.VeryGeneric.UseCases
{
    public class Divide : IQuery<float, float, float>
    {
        private readonly UC.IDivide _implementation;

        public Divide()
        {
            _implementation = new UC.Divide();
        }

        public float Query(float a, float b)
            => _implementation.Execute(a, b);
    }
}