namespace Example.VeryGeneric
{
    public interface IQuery<TResult>
    {
        TResult Query();
    }

    public interface IQuery<T1, TResult>
    {
        TResult Query(T1 p1);
    }

    public interface IQuery<T1, T2, TResult>
    {
        TResult Query(T1 p1, T2 p2);
    }
}