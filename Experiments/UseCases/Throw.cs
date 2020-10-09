namespace UseCases
{
    public interface IThrow
    {
        void Execute();
    }
    
    public class Throw : IThrow
    {
        public void Execute()
        {
            throw new System.Exception();
        }
    }
}