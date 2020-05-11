using System;
using System.Linq.Expressions;

using NUnit.Framework;

namespace Explosuress.Tests
{
    public class StaticFieldTests : AbstractTests
    {
        [SetUp]
        public void Setup()
        {
            Static.Field = 123;
        }

        [Test]
        public void StaticField_ExplosuressExpression_IsClosureFree()
        {
            Expression<Func<int, bool>> expr =
                x => Static.Field == x;

            var expl = expr.Explosuress();

            Assert.AreEqual("x => (123 == x)", expl.ToString());
        }

        [Test]
        public void UnchangedField_ComparedToSameValue_ReturnsTrue()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Field == x
            );

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UnchangedField_ComparedToDifferentValue_ReturnsFalse()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Field == x
            );

            Assert.IsFalse(fn(456));
        }

        [Test]
        public void UpdatedField_ComparedToOriginalValue_ReturnsTrue()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Field == x
            );

            Static.Field = 456;

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UpdatedField_ComparedToNewValue_ReturnsFalse()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Field == x
            );

            Static.Field = 456;

            Assert.IsFalse(fn(456));
        }

        private static class Static
        {
            public static int Field;
        }
    }
}