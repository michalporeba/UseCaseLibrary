# UseCaseLibrary
A search for simpler translation between use case modelling, development and execution with an aspect of Aspect Oriented Programming

## Objectives

 - some AOP like ability, but focused on use cases as an "API" for the business layer
 - a compile time feature - no dynamic generation or compile time weaving
 - ability to log, audit, re-execute and handle exceptions with not much code

## The Challenge

The use cases offer a conviniet structure to implement user requirements
in user understandable terms, and units of work that make sense to them too. 
They form a layer where the domain model gets exercised. However, typically
nonfunctional requirements need to be implemented too making the code hard 
to read an mistakes easier to make.  

To support the requirements of execution and exception logging we need
to add extra code. We can either put it in the use case itself, or keep
the use case simple and add the extra code outside, making the execution more
complex. Keeping the use case code simple can look something like this:

The use case:
```csharp
class Divisor 
{
    public float Divise(float a, float b) => a / b;
}
```

The execution (perhaps in a WebAPI action):
```csharp
_logger?.Log("Starting execution of Divise use case");
float result = 0.0;
try 
{
    var divisor = new Divisor();
    result = divisor.Divise(1,2);
}
catch (Exception ex) 
{
    _logger?.Log("Failed to execut Divise use case", ex);
    throw;
}
finally 
{
    _logger?.Log("Finished execution of Divise use case");
    return result;
}
```

The above code allows for clutter-less business logic, but each execution
of the use case requires a lot of code. Considering that a use case 
may be invoked from different entry points it might be necessary to copy 
and paste the above many times.

The alternative approach is to put this extra code inside a use case like so:

```csharp
class Divisor
{
    private readonly ILogger _logger;
    public Divisor(ILogger logger)
    {
        _logger = logger;
    }

    public float Divise(float a, float b) 
    {
        _logger?.Log("Starting execution of Divise use case");
        int result = null;

        try 
        {
            result = a / b 
        }
        catch (Exception ex) 
        {
            _logger?.Log("Failed to execut Divise use case", ex);
            throw;
        }
        finally 
        {
            _logger?.Log("Finished execution of Divise use case");
            return result;
        }       
    }
}
```

The simpler invocation:
```csharp
var logger = new Logger();
var divisor = new Divisor(logger);
return divisor.divise(1,2);
```

This approach has a clear advantage in that regardless where from the use case
is executed it will log exactly the same, and will always handle the exceptions
in the same way. The flip side is that the invocation of the use case becomes more complex as the `ILogger` instnace needs to be provided, and in the implementing 
class there is more clatter due to managing the dependencies coming through the
constructor. Finally the testing in this approach becomes more complex as the setup
requires more mocking. This should be obvious from the simple example above 
even though it has a single dependency. Typically there are others, auditing, 
custom error handling, permission checks, retry policies and similar concerns all 
need to be implemented. 

An argument could be made that dependency injection containers solve the problem
with instantiation for execution and mocking frameworks solve the problem with 
more difficult testing, but in my opinion those are workaround not solutions. 

The resulting use case code remains unnecessrily cluttered with a repeated code. 
A real alternative would be a use case execution layer, that could be standardised 
and add functionality outside and around of the core business functionality. 

Aspect Oriented Programming is a natural candidate, but I haven't seen yet an AOP
package for .Net which would convince me, that this is the solution, so in this 
project I will compare various AOP options available and a few alternatives. 

## Expermients 

To compare different appraoches I will implment the same 
(or as similar as possible) solutions using different teachniques. 
All will be based on the same two, simple use cases: 

 - `Divide` which can divide two numbers. It is an example of a use case 
 which encapsulates a query behaviour and has a potential of throwing an exception.
 - `Hello` which can say hello. It is an example of a use case 
 which encapsulates a command behaviour. 
 
All examples in this document will assume the use cases have been instantiated
and are represented by variables `divisor` and `greeter` respectively. Divisor
will be called twice, to return the result, or throw an exception.

Without any additions the three use cases could be executed as follows:

```csharp
greeter.Greet("World");            // prints: Hello, World!
var result = divisor.Divide(1,2);  // result: 0.5
divisor.Divide(3,0);               // throws System.Exception();
```

### Option 1
```csharp
handler.Invoke(usecase, x => x.Greet("World"));
return handler.Query(usecase, x => x.Divide(1,2));
return handler.Query(usecase, x => x.Divide(3,0));
```
Potentially the handler be static. The syntax is very concise, but it looks a bit odd and is not obvious what it is. Dependencies would haver to be both on the handler and the usecase to ensure both are instantiated by IoC container. 

### Option 2 (Example.FluidExecutor)
```csharp
handler.On(usecase).Invoke(x => x.Greet("World"));
return handler.Using(usecase).Query(x => x.Divide(1,2));
```
Modern looking fluent interface, but essentially it is still the above option 1 and it is longer


### Option 3 (Example.MockLike)
This example is influenced by the Moq implementation of Mocks.

```
var proxy = UseCase.Factory.Create<Hello>();
proxy.Do(x => x.Greet("World"));

var proxy = services.Resolve<IUseCase<Divide>>();
return proxy.Do(x => x.Divide(1,2));

return proxy.Do(x => x.Divide(3,0));
```

Moq like approach with `Expression` provides access to the type, and isntance while maintaining fairly legible syntax. Proxy has to be created for each type independently, perhaps using a factory to have access to registrations. 

Autofac type resolvers could be used to make it less obtrusive, but likely it would require a dependency on a specific proxy like `IUseCase<Hello>` which doesn't look too bad. 

### Option 4 (Example.VeryGeneric)
```
handler.Do(usecase, "World");
return handler.Do(usecase, 1, 2));
return handler.Do(usecase, 3, 0));
```
Estetically pleasing, but it would work with only one standard method name. 
It requires a lot of interfaces to be defined, the same as 16 `Action` and 16 `Func` defined by the .net framework. It is in line with what Microsoft has done, but some of 
their methods tend to go agains OOP and might not be the best choice. 

There is also a practical problem. I was not able to make the query look like the example above, and instead this what I ended up with:

```
return handler.Query<IQuery<float, float, float>, float, float, float>(divisor, 1, 2);
```
Unless there is a way to simplify it this appraoch will not be of any use. 


### Option 5 (Example.SimpleExpression)
```
handler.Do(() => usecase.Greet("World"))
return handler.Do(() => usecase.Divide(1,2));
return handler.Do(() => usecase.Divide(3,0));
```

Very flexiblty, but there is no obvious access to the use case type or instance.

## Next steps
 - [ ] add [AspectInjector](https://github.com/pamidur/aspect-injector) implementation.
 - [ ] consider implementing DynamiProxy (old RealProxy) way just for comparison. [check this](https://nearsoft.com/blog/aspect-oriented-programming-aop-in-net-core-and-c-using-autofac-and-dynamicproxy/)
 - [ ] write description of the use case approach, and what are the expectations
 - [ ] consider why existing AOP libraries are not a good fit (they might be?)
 - [ ] add registrations for handlers (before and after execution)
 - [ ] add registrations for error handling including exception translation
 - [ ] add output converters to various channels (perhaps a builder pattern)
 - [ ] retries (with cashed payload)
 - [ ] async