using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Explosuress
{
    /// <summary>
    /// Rewrites an Expression Tree - removes closure references when possible.
    /// Closures are replaced with corresponding constant values.
    /// </summary>
    internal class ExplosuressRewriter : ExpressionVisitor
    {
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            // Method closure can be resolved if:
            // 1. The object reference can be resolved
            // 2. All method arguments can be resolved

            // Resolve the object,
            // `null` object describes a static method invocation.
            object? obj = null;
            var canDeclosure = true;

            if (!(node.Object is null))
            {
                var objExpr = Visit(node.Object);

                if (objExpr is ConstantExpression objConstExpr)
                {
                    obj = objConstExpr.Value;
                }
                else
                {
                    canDeclosure = false;
                }
            }

            if (canDeclosure)
            {
                // Resolve the arguments
                // (`null` value means "not resolved")
                var args = ResolveArguments(node.Arguments);

                if (!(args is null))
                {
                    var methodResult = node.Method.Invoke(obj, args);

                    return Expression.Constant(methodResult);
                }
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var fieldOrProp = FieldOrPropertyAdapter.TryCreate(node.Member);

            if (!(fieldOrProp is null))
            {
                // MemberExpression.Expression is `null` when the expression
                // describes a static member reference.
                if (node.Expression is null)
                {
                    var staticValue = fieldOrProp.GetValue(null);
                    return Expression.Constant(staticValue);
                }

                // If the child expression is a constant expression, evaluate
                // it to the referred member value.
                var childExpr = Visit(node.Expression);

                if (childExpr is ConstantExpression constExpr)
                {
                    var constValue = fieldOrProp.GetValue(constExpr.Value);
                    return Expression.Constant(constValue);
                }
            }

            return base.VisitMember(node);
        }

        /// <summary>
        /// Resolves arguments of method invocation Expression
        /// (replaces closures with corresponding constant values)
        /// </summary>
        private object[]? ResolveArguments(ReadOnlyCollection<Expression> arguments)
        {
            var argsCount = arguments.Count;

            if (argsCount == 0)
            {
                return Array.Empty<object>();
            }

            var args = new object[argsCount];

            for (var i = 0; i < argsCount; i++)
            {
                var argExpr = Visit(arguments[i]);

                if (argExpr is ConstantExpression argConstExpr)
                {
                    args[i] = argConstExpr.Value;
                }
                else
                {
                    // If at least one argument is not a constant expression, 
                    // return `null`
                    return null;
                }
            }

            return args;
        }
    }
}
