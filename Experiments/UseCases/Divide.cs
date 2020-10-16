namespace UseCases
{
    public interface IDivide
    {
        float Execute(float a, float b);
    }
    
    public class Divide : IDivide
    {
        public float Execute(float a, float b) => a / b;
    }
}