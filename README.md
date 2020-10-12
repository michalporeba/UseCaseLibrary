# UseCaseLibrary
A search for simpler translation between use case modelling, development and execution with an aspect of Aspect Oriented Programming

## Objectives

 - some AOP like ability, but focused on use cases as an "API" for the business layer
 - a compile time feature - no dynamic generation or compile time weaving
 - ability to log, audit, re-execute and handle exceptions with not much code

## Expermients 

### Option 0 (Example.Plain)
```
usecase.Greet("World");
return usecase.Add(1,2);
```

### Option 1
```
handler.Invoke(usecase, x => x.Say("hello"));
return handler.Query(usecase, x => x.Add(1,2));
```
Potentially the handler be static. The syntax is very concise, but it looks a bit odd and is not obvious what it is. Dependencies would haver to be both on the handler and the usecase to ensure both are instantiated by IoC container. 

### Option 2 (Example.FluidExecutor)
```
handler.On(usecase).Invoke(x => x.Say("hello"));
return handler.Using(usecase).Query(x => x.Add(1,2));
```
Modern looking fluent interface, but essentially it is still the above option 1 and it is longer


### Option 3 (Example.MockLike)
This example is influenced by the Moq implementation of Mocks.

```
var proxy = UseCase.Factory.Create<Hello>();
proxy.Do(x => x.Say("hello"));

var proxy = services.Resolve<IUseCase<Add>>();
return proxy.Do(x => x.Add(1,2));
```

Moq like approach with `Expression` provides access to the type, and isntance while maintaining fairly legible syntax. Proxy has to be created for each type independently, perhaps using a factory to have access to registrations. 

Autofac type resolvers could be used to make it less obtrusive, but likely it would require a dependency on a specific proxy like `IUseCase<Hello>` which doesn't look too bad. 

### Option 4 (Example.VeryGeneric)
```
handler.Do(usecase, "Hello");
return handler.Do(usecase, 1, 2));
```
Estetically pleasing, but it would work with only one standard method name. 
It requires a lot of interfaces to be defined, the same as 16 `Action` and 16 `Func` defined by the .net framework. It is in line with what Microsoft has done, but some of 
their methods tend to go agains OOP and might not be the best choice. 

There is also a practical problem. I was not able to make the query look like the example above, and instead this what I ended up with:

```
return handler.Query<IQuery<int, int, int>, int, int, int>(adder, 1, 2);
```
Unless there is a way to simplify it this appraoch will not be of any use. 


### Option 5 (Example.SimpleExpression)
```
handler.Do(() => usecase.Say("Hello"))
return handler.Do(() => usecase.Add(1,2));
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