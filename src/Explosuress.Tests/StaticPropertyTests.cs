using System;
using System.Linq.Expressions;

using NUnit.Framework;

namespace Explosuress.Tests
{
    public class StaticPropertyTests : AbstractTests
    {
        [SetUp]
        public void Setup()
        {
            Static.Property = 123;
        }

        [Test]
        public void StaticProperty_ExplosuressExpression_IsClosureFree()
        {
            Expression<Func<int, bool>> expr =
                x => Static.Property == x;

            var expl = expr.Explosuress();

            Assert.AreEqual("x => (123 == x)", expl.ToString());
        }

        [Test]
        public void UnchangedProperty_ComparedToSameValue_ReturnsTrue()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Property == x
            );

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UnchangedProperty_ComparedToDifferentValue_ReturnsFalse()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Property == x
            );

            Assert.IsFalse(fn(456));
        }

        [Test]
        public void UpdatedProperty_ComparedToOriginalValue_ReturnsTrue()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Property == x
            );

            Static.Property = 456;

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UpdatedProperty_ComparedToNewValue_ReturnsFalse()
        {
            var fn = Explosuress<Func<int, bool>>(
                x => Static.Property == x
            );

            Static.Property = 456;

            Assert.IsFalse(fn(456));
        }

        private static class Static
        {
            public static int Property { get; set; }
        }
    }
}