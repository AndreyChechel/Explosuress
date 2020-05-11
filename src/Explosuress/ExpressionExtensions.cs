using Explosuress;

namespace System.Linq.Expressions
{
    /// <summary>
    /// Explosuress extensions for Expression Trees. 
    /// </summary>
    public static class ExplosuressExtensions
    {
        private static readonly ExplosuressRewriter _explosuress = new ExplosuressRewriter();

        /// <summary>
        /// Produces a closure-free Expression Tree.
        /// </summary>
        /// <typeparam name="TExpression">The type of the expression to be processed.</typeparam>
        /// <param name="expression">Expression tree to be processed.</param>
        /// <returns>Updated Expression Tree having no closures.</returns>
        public static TExpression Explosuress<TExpression>(this TExpression expression)
            where TExpression : Expression
        {
            var updatedExpr = _explosuress.Visit(expression);

            return (TExpression)updatedExpr;
        }
    }
}
