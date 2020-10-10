using System.Security.Cryptography;

namespace Example.VeryGeneric
{
    public interface ICommand
    {
        void Execute();
    }

    public interface ICommand<T1>
    {
        void Execute(T1 p1);
    }

    public interface ICommand<T1, T2>
    {
        void Execute(T1 p1, T2 p2);
    }
}