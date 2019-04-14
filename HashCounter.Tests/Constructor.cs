using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace HashCounter.Tests
{
    [TestFixture(typeof(object))]
    [TestFixture(typeof(string))]
    [TestFixture(typeof(int))]
    public class Constructor<TKey>
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Default()
        {
            new HashCounter<string>();
        }

        [Test]
        public void ValidComparer_Null()
        {
            // Check both with and without a given capacity
            var hashCounter = new HashCounter<TKey>(null);
            Assert.AreEqual(EqualityComparer<TKey>.Default, hashCounter.Comparer);

            hashCounter = new HashCounter<TKey>(1, null);
            Assert.AreEqual(EqualityComparer<TKey>.Default, hashCounter.Comparer);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(int.MinValue)]
        public void InvalidCapacity(int capacity)
        {
            // Check both with and without a given comparer
            Assert.Throws<ArgumentOutOfRangeException>(() => new HashCounter<TKey>(capacity));
            Assert.Throws<ArgumentOutOfRangeException>(() => new HashCounter<TKey>(capacity, EqualityComparer<TKey>.Default));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(100)]
        [TestCase(1000)]
        public void ValidCapacity(int capacity)
        {
            // Check both with and without a given comparer
            new HashCounter<TKey>(capacity);
            new HashCounter<TKey>(capacity, EqualityComparer<TKey>.Default);
        }
    }
}