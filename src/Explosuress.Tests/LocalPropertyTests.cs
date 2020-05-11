using System;
using System.Linq.Expressions;

using NUnit.Framework;

namespace Explosuress.Tests
{
    public class LocalPropertyTests : AbstractTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LocalProperty_ExplosuressExpression_IsClosureFree()
        {
            var local = new Local
            {
                Property = 123
            };

            Expression<Func<int, bool>> expr =
                x => local.Property == x;

            var expl = expr.Explosuress();

            Assert.AreEqual("x => (123 == x)", expl.ToString());
        }

        [Test]
        public void UnchangedProperty_ComparedToSameValue_ReturnsTrue()
        {
            var local = new Local
            {
                Property = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Property == x
            );

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UnchangedProperty_ComparedToDifferentValue_ReturnsFalse()
        {
            var local = new Local
            {
                Property = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Property == x
            );

            Assert.IsFalse(fn(456));
        }

        [Test]
        public void UpdatedProperty_ComparedToOriginalValue_ReturnsTrue()
        {
            var local = new Local
            {
                Property = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Property == x
            );

            local.Property = 456;

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UpdatedProperty_ComparedToNewValue_ReturnsFalse()
        {
            var local = new Local
            {
                Property = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Property == x
            );

            local.Property = 456;

            Assert.IsFalse(fn(456));
        }

        private class Local
        {
            public int Property { get; set; }
        }
    }
}