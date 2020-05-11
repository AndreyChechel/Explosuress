using System;
using System.Linq.Expressions;

using NUnit.Framework;

namespace Explosuress.Tests
{
    public class LocalMethodTests : AbstractTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LocalMethod_ExplosuressExpression_IsClosureFree()
        {
            var local = new Local
            {
                RetVal = 123
            };

            Expression<Func<int, bool>> expr =
                x => local.Method() == x;

            var expl = expr.Explosuress();

            Assert.AreEqual("x => (123 == x)", expl.ToString());
        }

        [Test]
        public void UnchangedRetValue_ComparedToSameValue_ReturnsTrue()
        {
            var local = new Local
            {
                RetVal = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Method() == x
            );

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UnchangedRetValue_ComparedToDifferentValue_ReturnsFalse()
        {
            var local = new Local
            {
                RetVal = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Method() == x
            );

            Assert.IsFalse(fn(456));
        }

        [Test]
        public void UpdatedRetValue_ComparedToOriginalValue_ReturnsTrue()
        {
            var local = new Local
            {
                RetVal = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Method() == x
            );

            local.RetVal = 456;

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UpdatedRetValue_ComparedToNewValue_ReturnsFalse()
        {
            var local = new Local
            {
                RetVal = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Method() == x
            );

            local.RetVal = 456;

            Assert.IsFalse(fn(456));
        }

        private class Local
        {
            public int RetVal;

            public int Method() => RetVal;

        }
    }
}