using System;
using System.Linq.Expressions;

using NUnit.Framework;

namespace Explosuress.Tests
{
    public class LocalFieldTests : AbstractTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LocalField_ExplosuressExpression_IsClosureFree()
        {
            var local = new Local
            {
                Field = 123
            };

            Expression<Func<int, bool>> expr =
                x => local.Field == x;

            var expl = expr.Explosuress();

            Assert.AreEqual("x => (123 == x)", expl.ToString());
        }

        [Test]
        public void UnchangedField_ComparedToSameValue_ReturnsTrue()
        {
            var local = new Local
            {
                Field = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Field == x
            );

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UnchangedField_ComparedToDifferentValue_ReturnsFalse()
        {
            var local = new Local
            {
                Field = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Field == x
            );

            Assert.IsFalse(fn(456));
        }

        [Test]
        public void UpdatedField_ComparedToOriginalValue_ReturnsTrue()
        {
            var local = new Local
            {
                Field = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Field == x
            );

            local.Field = 456;

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UpdatedField_ComparedToNewValue_ReturnsFalse()
        {
            var local = new Local
            {
                Field = 123
            };

            var fn = Explosuress<Func<int, bool>>(
                x => local.Field == x
            );

            local.Field = 456;

            Assert.IsFalse(fn(456));
        }

        private class Local
        {
            public int Field;
        }
    }
}