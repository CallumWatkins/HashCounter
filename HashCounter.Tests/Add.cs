using System;
using NUnit.Framework;

namespace HashCounter.Tests
{
    public class Add
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
            Assert.Throws<ArgumentNullException>(() => _hashCounter.Add(null));
            Assert.Throws<ArgumentNullException>(() => _hashCounter.Add(null, 5));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-100)]
        public void InvalidAddend_ArgumentOutOfRangeException(int addend)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _hashCounter.Add("abc", addend));
        }

        [Test]
        public void AddSingle_ValidNewCounterValue()
        {
            Assert.AreEqual(1, _hashCounter.Add("abc"));
            Assert.AreEqual(2, _hashCounter.Add("abc"));
            Assert.AreEqual(3, _hashCounter.Add("abc"));
            Assert.AreEqual(3, _hashCounter["abc"]);

            Assert.AreEqual(1, _hashCounter.Add("def"));
            Assert.AreEqual(2, _hashCounter.Add("def"));
            Assert.AreEqual(3, _hashCounter.Add("def"));
            Assert.AreEqual(3, _hashCounter["def"]);

            Assert.AreEqual(4, _hashCounter.Add("abc"));
            Assert.AreEqual(4, _hashCounter.Add("def"));
            Assert.AreEqual(4, _hashCounter["abc"]);
            Assert.AreEqual(4, _hashCounter["def"]);
        }


        [Test]
        public void AddSpecifiedValue_ValidNewCounterValue()
        {
            Assert.AreEqual(2, _hashCounter.Add("abc", 2));
            Assert.AreEqual(3, _hashCounter.Add("abc", 1));
            Assert.AreEqual(6, _hashCounter.Add("abc", 3));
            Assert.AreEqual(6, _hashCounter["abc"]);

            Assert.AreEqual(100, _hashCounter.Add("def", 100));
            Assert.AreEqual(150, _hashCounter.Add("def", 50));
            Assert.AreEqual(160, _hashCounter.Add("def", 10));
            Assert.AreEqual(160, _hashCounter["def"]);

            Assert.AreEqual(11, _hashCounter.Add("abc", 5));
            Assert.AreEqual(165, _hashCounter.Add("def", 5));
            Assert.AreEqual(11, _hashCounter["abc"]);
            Assert.AreEqual(165, _hashCounter["def"]);

            Assert.AreEqual(int.MaxValue, _hashCounter.Add("ghi", int.MaxValue));
            Assert.AreEqual(int.MaxValue, _hashCounter["ghi"]);
        }

        [Test]
        public void AddAboveMaxValue_OverflowException()
        {
            _hashCounter.Add("abc", int.MaxValue);
            Assert.Throws<OverflowException>(() => _hashCounter.Add("abc"));
            Assert.Throws<OverflowException>(() => _hashCounter.Add("abc", 1));

            _hashCounter.Add("def");
            Assert.Throws<OverflowException>(() => _hashCounter.Add("def", int.MaxValue));
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