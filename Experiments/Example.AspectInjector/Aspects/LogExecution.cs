using System;
using AspectInjector.Broker;

namespace Example.AspectInjector.Aspects
{
    [Aspect(Scope.Global)]
    [Injection(typeof(LogExecution))]
    public class LogExecution : Attribute
    {
        [Advice(Kind.Before, Targets = Target.Method)]
        public void OnEntry([Argument(Source.Name)] string name)
        {
            Console.Out.WriteLine($"Log: Starting execution of {name}");
        }
        
        [Advice(Kind.After, Targets = Target.Method)]
        public void OnExit([Argument(Source.Name)] string name)
        {
            Console.Out.WriteLine($"Log: Finished execution of {name}");
        }
    }
}