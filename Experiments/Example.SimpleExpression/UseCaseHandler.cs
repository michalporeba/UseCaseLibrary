using System;

namespace Example.SimpleExpression
{
    public class UseCaseHandler
    {
        public void Invoke(Action action)
        {
            action.Invoke();
        }

        public T Query<T>(Func<T> action)
        {
            return action.Invoke();
        }
    }
}