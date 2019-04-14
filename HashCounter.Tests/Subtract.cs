using System;
using NUnit.Framework;

namespace HashCounter.Tests
{
    public class Subtract
    {
        private HashCounter<string> _hashCounter;

        [SetUp]
        public void Setup()
        {
            _hashCounter = new HashCounter<string>();
        }

        [Test]
        public void InvalidKey_Null()
        {
            Assert.Throws<ArgumentNullException>(() => _hashCounter.Subtract(null));
            Assert.Throws<ArgumentNullException>(() => _hashCounter.Subtract(null, 5));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-100)]
        public void InvalidAddend_ArgumentOutOfRangeException(int subtrahend)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _hashCounter.Subtract("abc", subtrahend));
        }

        [Test]
        public void SubtractSingle_ValidNewCounterValue()
        {
            _hashCounter["abc"] = 4;
            _hashCounter["def"] = 150;
            _hashCounter["ghi"] = 25;

            Assert.AreEqual(3, _hashCounter.Subtract("abc"));
            Assert.AreEqual(2, _hashCounter.Subtract("abc"));
            Assert.AreEqual(1, _hashCounter.Subtract("abc"));
            Assert.AreEqual(1, _hashCounter["abc"]);

            Assert.AreEqual(149, _hashCounter.Subtract("def"));
            Assert.AreEqual(148, _hashCounter.Subtract("def"));
            Assert.AreEqual(147, _hashCounter.Subtract("def"));
            Assert.AreEqual(147, _hashCounter["def"]);

            Assert.AreEqual(0, _hashCounter.Subtract("abc"));
            Assert.AreEqual(146, _hashCounter.Subtract("def"));
            Assert.AreEqual(0, _hashCounter["abc"]);
            Assert.AreEqual(146, _hashCounter["def"]);
        }

        [Test]
        public void SubtractSingleNonExistentKey_ValidNewCounterValue()
        {
            Assert.AreEqual(0, _hashCounter.Subtract("abc"));
        }

        [Test]
        public void SubtractSpecifiedValue_ValidNewCounterValue()
        {
            _hashCounter["abc"] = 8;
            _hashCounter["def"] = 150;
            _hashCounter["ghi"] = 25;

            Assert.AreEqual(6, _hashCounter.Subtract("abc", 2));
            Assert.AreEqual(5, _hashCounter.Subtract("abc", 1));
            Assert.AreEqual(2, _hashCounter.Subtract("abc", 3));
            Assert.AreEqual(2, _hashCounter["abc"]);

            Assert.AreEqual(50, _hashCounter.Subtract("def", 100));
            Assert.AreEqual(45, _hashCounter.Subtract("def", 5));
            Assert.AreEqual(35, _hashCounter.Subtract("def", 10));
            Assert.AreEqual(35, _hashCounter["def"]);

            Assert.AreEqual(0, _hashCounter.Subtract("abc", 5));
            Assert.AreEqual(30, _hashCounter.Subtract("def", 5));
            Assert.AreEqual(0, _hashCounter["abc"]);
            Assert.AreEqual(30, _hashCounter["def"]);

            Assert.AreEqual(0, _hashCounter.Subtract("ghi", int.MaxValue));
            Assert.AreEqual(0, _hashCounter["ghi"]);
        }

        [Test]
        public void SubtractBelowZero_CounterZero()
        {
            _hashCounter["abc"] = 5;
            Assert.AreEqual(0, _hashCounter.Subtract("abc", int.MaxValue));
            Assert.AreEqual(0, _hashCounter["abc"]);
        }

        [Test]
        public void AddAboveMaxValue_SameCounterValue()
        {
            _hashCounter.Add("abc", int.MaxValue - 3);
            Assert.Throws<OverflowException>(() => _hashCounter.Add("abc", 4));
            Assert.AreEqual(int.MaxValue - 3, _hashCounter["abc"]);
        }
    }
}