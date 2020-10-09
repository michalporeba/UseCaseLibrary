namespace UseCases
{
    public interface IAdd
    {
        int Execute(int a, int b);
    }
    
    public class Add : IAdd
    {
        public int Execute(int a, int b) => a + b;
    }
}