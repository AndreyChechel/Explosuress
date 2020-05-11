# Explosuress
Closure-free C# Expressions.

**Explosuress** processes an Expression Tree and removes all closures by evaluating and replacing them with corresponding values. This operation reduces size and memory footprint of the tree.

## Example

Consider the Expression Tree shown below:
```cs
var local = new Local
{
    Field = 123
};

Expression<Func<int, bool>> expression =
    x => local.Field == x;
```

The tree structure contains a closure to `local.Field`:

```
x => (value(Explosuress.Demo.Program+<>c__DisplayClass0_0).local.Field == x)
```

**Explosuress** can remove the closure:
```cs
var closureFreeExpr = expression.Explosuress();
```

This is how the structure will look like without it:
```
x => (123 == x)
```

# License
[MIT](https://github.com/AndreyChechel/Explosuress/blob/master/LICENSE)
