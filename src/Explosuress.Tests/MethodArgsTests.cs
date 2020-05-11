using System;
using System.Linq.Expressions;

using NUnit.Framework;

namespace Explosuress.Tests
{
    public class MethodArgsTests : AbstractTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LocalVariables_PassedToLocalMethod_IsClosureFree()
        {
            var a = 100;
            var b = 200;
            var local = new Local();

            Expression<Func<int, int, int>> expr =
                (x, y) => local.Sum(a, b);

            var expl = expr.Explosuress();

            Assert.AreEqual("(x, y) => 300", expl.ToString());
        }

        [Test]
        public void LocalVariables_PassedToInstanceMethod_IsClosureFree()
        {
            var a = 100;
            var b = 200;
            var local = new Local();

            Expression<Func<int, int, int>> expr =
                (x, y) => Sum(a, b);

            var expl = expr.Explosuress();

            Assert.AreEqual("(x, y) => 300", expl.ToString());
        }

        [Test]
        public void LocalFields_PassedToInstanceMethod_IsClosureFree()
        {
            var local = new Local
            {
                A = 100,
                B = 200
            };

            Expression<Func<int, int, int>> expr =
                (x, y) => Sum(local.A, local.B);

            var expl = expr.Explosuress();

            Assert.AreEqual("(x, y) => 300", expl.ToString());
        }

        [Test]
        public void LocalProperties_PassedToInstanceMethod_IsClosureFree()
        {
            var local = new Local
            {
                PropertyA = 100,
                PropertyB = 200
            };

            Expression<Func<int, int, int>> expr =
                (x, y) => Sum(local.PropertyA, local.PropertyB);

            var expl = expr.Explosuress();

            Assert.AreEqual("(x, y) => 300", expl.ToString());
        }

        [Test]
        public void StaticFields_PassedToInstanceMethod_IsClosureFree()
        {
            Static.A = 100;
            Static.B = 200;

            Expression<Func<int, int, int>> expr =
                (x, y) => Sum(Static.A, Static.B);

            var expl = expr.Explosuress();

            Assert.AreEqual("(x, y) => 300", expl.ToString());
        }

        [Test]
        public void StaticProperties_PassedToInstanceMethod_IsClosureFree()
        {
            Static.PropertyA = 100;
            Static.PropertyB = 200;

            Expression<Func<int, int, int>> expr =
                (x, y) => Sum(Static.PropertyA, Static.PropertyB);

            var expl = expr.Explosuress();

            Assert.AreEqual("(x, y) => 300", expl.ToString());
        }

        [Test]
        public void MethodInvocationResult_PassedToInstanceMethod_IsClosureFree()
        {
            var local = new Local();

            Expression<Func<int, int, int>> expr =
                (x, y) => Sum(GetA(), GetB());

            var expl = expr.Explosuress();

            Assert.AreEqual("(x, y) => 300", expl.ToString());
        }

        private int GetA() => 100;

        private int GetB() => 200;

        private int Sum(int a, int b) => a + b;

        private class Local
        {
            public int A;

            public int B;

            public int PropertyA { get; set; }

            public int PropertyB { get; set; }

            public int Sum(int a, int b) => a + b;
        }

        private static class Static
        {
            public static int A;

            public static int B;

            public static int PropertyA { get; set; }

            public static int PropertyB { get; set; }
        }
    }
}