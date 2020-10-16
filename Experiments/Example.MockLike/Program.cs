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
                .AddScoped<IDivide, Divide>()
                .AddScoped<IUseCase<IHello>, UseCase<IHello>>()
                .AddScoped<IUseCase<IDivide>, UseCase<IDivide>>()
                .BuildServiceProvider();

            var greeter = sp.GetService<IUseCase<IHello>>();
            var divider = sp.GetService<IUseCase<IDivide>>();

            greeter.Do(x => x.Greet("World"));
            var result = divider.Do(x => x.Execute(1,2));
            divider.Do(x => x.Execute(3, 0));
        }
    }
}
