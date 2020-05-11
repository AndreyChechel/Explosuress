using System;
using System.Linq.Expressions;

namespace Explosuress.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var local = new Local
            {
                Field = 123
            };

            Expression<Func<int, bool>> expr =
                x => local.Field == x;

            var expl = expr.Explosuress();

            Console.WriteLine("Original Expression:");
            Console.WriteLine(expr);

            Console.WriteLine();
            Console.WriteLine("Closure-Free Expression:");
            Console.WriteLine(expl);
        }

        class Local
        {
            public int Field;
        }
    }
}
