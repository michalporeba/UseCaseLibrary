# UseCaseLibrary
A search for simpler translation between use case modelling, development and execution with an aspect of Aspect Oriented Programming

## Objectives

 - some AOP like ability, but focused on use cases as an "API" for the business layer
 - a compile time feature - no dynamic generation or compile time weaving
 - ability to log, audit, re-execute and handle exceptions with not much code

## Expermients 

option 0
```
usecase.Say("hello");
return usecase.Add(1,2);
```

option 1
```
handler.Invoke(usecase, x => x.Say("hello"));
return handler.Query(usecase, x => x.Add(1,2));
```
Potentially the handler be static. The syntax is very concise, but it looks a bit odd and is not obvious what it is. Dependencies would haver to be both on the handler and the usecase to ensure both are instantiated by IoC container. 

option 2
```
handler.On(usecase).Invoke(x => x.Say("hello"));
return handler.Using(usecase).Query(x => x.Add(1,2));
```
Modern looking fluent interface, but essentially it is still the above option 1 and it is longer


option 3
```
var proxy = UseCase.Factory.Create<Hello>();
proxy.Do(x => x.Say("hello"));

var proxy = services.Resolve<IUseCase<Add>>();
return proxy.Do(x => x.Add(1,2));
```

Moq like approach with `Expression` provides access to the type, and isntance while maintaining fairly legible syntax. Proxy has to be created for each type independently, perhaps using a factory to have access to registrations. 

Autofac type resolvers could be used to make it less obtrusive, but likely it would require a dependency on a specific proxy like `IUseCase<Hello>` which doesn't look too bad. 

option 4
```
handler.Do(usecase, "Hello");
return handler.Do(usecase, 1, 2));
```
quite elegant, and in line with Func<> or Action<> approach but there is no easy solution to returning values

option 5
```
handler.Do(() => usecase.Say("Hello"))
return handler.Do(() => usecase.Add(1,2));
```

Very flexiblty, but there is no obvious access to the use case type or instance.