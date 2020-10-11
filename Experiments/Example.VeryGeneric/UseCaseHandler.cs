using System;
using UseCases;

namespace Example.VeryGeneric
{
    public class UseCaseHandler
    {
        private readonly IPrinter _printer;

        public UseCaseHandler(IPrinter printer)
        {
            _printer = printer;
        }
        
        public void Do<T>(T target)
            where T : ICommand
        {
            Execute(() =>
            {
                target.Execute();
                return true;
            });
        }

        public void Do<T, T1>(T target, T1 p1)
            where T : ICommand<T1>
        {
            Execute(() =>
            {
                target.Execute(p1);
                return true;
            });
        }

        public void Do<T, T1, T2>(T target, T1 p1, T2 p2)
            where T : ICommand<T1, T2>
        {
            Execute(() =>
            {
                target.Execute(p1, p2);
                return true;
            });
        }

        public TResult Query<T, TResult>(T target)
            where T : IQuery<TResult>
        {
            return Execute(target.Query);
        }

        public TResult Query<T, T1, TResult>(T target, T1 p1)
            where T : IQuery<T1, TResult>
        {
            return Execute(() => target.Query(p1));
        }

        public TResult Query<T, T1, T2, TResult>(T target, T1 p1, T2 p2)
            where T : IQuery<T1, T2, TResult>
        {
            return Execute(() => target.Query(p1, p2));
        }

        private T Execute<T>(Func<T> func)
        {
            T result;
            
            _printer.Print("Log: Starting execution of a use case");
            try
            {
                result = func.Invoke();
                _printer.Print("Log: Finished execution of a use case");
            }
            catch (Exception ex)
            {
                _printer.Print("Log: Failed to execute a use case");
                throw;
            }

            return result;
        }
    }
}