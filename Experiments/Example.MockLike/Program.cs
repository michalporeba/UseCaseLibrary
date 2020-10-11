using UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Example.MockLike
{
    class Program
    {
        static void Main(string[] args)
        {
            // for the brevity of the example at the moment I'm not using
            // DI libraries like Autofac, and instead using bare minimum
            // based on .net core dependency injection
            var sp = new ServiceCollection()
                .AddScoped<IPrinter, Printer>()
                .AddScoped<IHello, Hello>()
                .AddScoped<IAdd, Add>()
                .AddScoped<IThrow, Throw>()
                .AddScoped<IUseCase<IHello>, UseCase<IHello>>()
                .AddScoped<IUseCase<IAdd>, UseCase<IAdd>>()
                .AddScoped<IUseCase<IThrow>, UseCase<IThrow>>()
                .BuildServiceProvider();

            var greeter = sp.GetService<IUseCase<IHello>>();
            var adder = sp.GetService<IUseCase<IAdd>>();
            var thrower = sp.GetService<IUseCase<IThrow>>();

            greeter.Do(x => x.Greet("World"));
            var result = adder.Do(x => x.Execute(1,2));
            thrower.Do(x => x.Execute());
        }
    }
}
