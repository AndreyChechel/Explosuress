# Explosuress

[![NuGet Status](https://img.shields.io/nuget/v/Explosuress)](https://www.nuget.org/packages/Explosuress/)

Closure-free C# Expressions.

**Explosuress** processes an Expression Tree and removes all closures by evaluating and replacing them with corresponding values. This operation reduces size and memory footprint of the tree.

## Installation

1. Install the NuGet package:

```
Install-Package Explosuress
```
or
```
dotnet add package Explosuress
```

2. Add `using` statement:

```
using System.Linq.Expressions;
```

3. Call the `Explosuress()` extension method to process an expression:

```cs
var closureFreeExpr = expression.Explosuress();
```

## Example

Consider the Expression Tree shown below:
```cs
var local = new Local
{
    Field = 123
};

Expression<Func<int, bool>> expression =
    x => local.Field == x;
    
Console.WriteLine(expression);
```

The tree structure contains a closure to `local.Field`:

```
x => (value(Explosuress.Demo.Program+<>c__DisplayClass0_0).local.Field == x)
```

**Explosuress** can remove the closure:
```cs
var closureFreeExpr = expression.Explosuress();

Console.WriteLine(closureFreeExpr);
```

This is how the closure-free structure will look like:
```
x => (123 == x)
```

# License
[MIT](https://github.com/AndreyChechel/Explosuress/blob/master/LICENSE)
