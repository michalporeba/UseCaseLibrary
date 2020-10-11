using System;
using System.Linq.Expressions;
using UseCases;

namespace Example.MockLike
{
    public interface IUseCase<T>
    {
        void Do(Expression<Action<T>> expression);
        TResult Do<TResult>(Expression<Func<T, TResult>> expression);
    }
    
    public class UseCase<T> : IUseCase<T>
    {
        private readonly T _target;
        private readonly IPrinter _printer;

        public UseCase(IServiceProvider serviceProvider, IPrinter printer)
        {
            _target = (T)serviceProvider.GetService(typeof(T));
            _printer = printer;
        }
        
        public void Do(Expression<Action<T>> expression)
        {
            _printer.Print($"Log: Starting execution of {typeof(T)}");
            try
            {
                expression.Compile().Invoke(_target);
                _printer.Print($"Log: Finished execution of {typeof(T)}");
            }
            catch (Exception ex)
            {
                _printer.Print("Log: Failed execution");
            }
        }

        public TResult Do<TResult>(Expression<Func<T, TResult>> expression)
        {
            _printer.Print($"Log: Starting execution of {typeof(T)}");
            TResult result;
            try
            {
                result = expression.Compile().Invoke(_target);
                _printer.Print($"Log: Finished execution of {typeof(T)}");
                return result;
            }
            catch (Exception ex)
            {
                _printer.Print("Log: Failed execution");
                throw;
            }
        }
    }
}