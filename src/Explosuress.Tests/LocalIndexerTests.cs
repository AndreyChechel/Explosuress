using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NUnit.Framework;

namespace Explosuress.Tests
{
    public class LocalIndexerTests : AbstractTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void LocalIndexer_ExplosuressExpression_IsClosureFree()
        {
            var local = new Local();
            local[0] = 123;

            Expression<Func<int, bool>> expr =
                x => local[0] == x;

            var expl = expr.Explosuress();

            Assert.AreEqual("x => (123 == x)", expl.ToString());
        }

        [Test]
        public void UnchangedIndexer_ComparedToSameValue_ReturnsTrue()
        {
            var local = new Local();
            local[0] = 123;

            var fn = Explosuress<Func<int, bool>>(
                x => local[0] == x
            );

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UnchangedIndexer_ComparedToDifferentValue_ReturnsFalse()
        {
            var local = new Local();
            local[0] = 123;

            var fn = Explosuress<Func<int, bool>>(
                x => local[0] == x
            );

            Assert.IsFalse(fn(456));
        }

        [Test]
        public void UpdatedIndexer_ComparedToOriginalValue_ReturnsTrue()
        {
            var local = new Local();
            local[0] = 123;

            var fn = Explosuress<Func<int, bool>>(
                x => local[0] == x
            );

            local[0] = 456;

            Assert.IsTrue(fn(123));
        }

        [Test]
        public void UpdatedIndexer_ComparedToNewValue_ReturnsFalse()
        {
            var local = new Local();
            local[0] = 123;

            var fn = Explosuress<Func<int, bool>>(
                x => local[0] == x
            );

            local[0] = 456;

            Assert.IsFalse(fn(456));
        }

        private class Local
        {
            private readonly Dictionary<int, int> _dict = new Dictionary<int, int>();

            public int this[int key]
            {
                get => _dict[key];
                set => _dict[key] = value;
            }
        }
    }
}