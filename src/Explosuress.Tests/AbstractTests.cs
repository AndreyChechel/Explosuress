using System.Linq.Expressions;

namespace Explosuress.Tests
{
    public abstract class AbstractTests
    {
        protected TDelegate Explosuress<TDelegate>(Expression<TDelegate> expression)
        {
            return expression.Explosuress().Compile();
        }
    }
}