using System;
using System.Linq.Expressions;

using NUnit.Framework;

namespace Explosuress.Tests
{
    public class StaticMethodTests : AbstractTests
    {
        [SetUp]
        public void Setup()
        {
            Static.RetVal = 123;
        }

        [Test]
        public void StaticMethod_ExplosuressExpression_IsClosureFree()
        {
            Expression<Func<int, bool>> expr =
                x => Static.Method() == x;

            var expl = expr.Explosuress();

            Assert.AreEqual("x => (123 == x)", expl.ToString());
        }

        [Test]
        public void UnchangedRetValue_ComparedToSameValue_ReturnsTrue()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Method() == x
            );

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UnchangedRetValue_ComparedToDifferentValue_ReturnsFalse()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Method() == x
            );

            Assert.IsFalse(fn(456));
        }

        [Test]
        public void UpdatedRetValue_ComparedToOriginalValue_ReturnsTrue()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Method() == x
            );

            Static.RetVal = 456;

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UpdatedRetValue_ComparedToNewValue_ReturnsFalse()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Method() == x
            );

            Static.RetVal = 456;

            Assert.IsFalse(fn(456));
        }

        private static class Static
        {
            public static int RetVal;

            public static int Method() => RetVal;
        }
    }
}