using System;
using NUnit.Framework;

namespace HashCounter.Tests
{
    public class Indexer
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
            Assert.Throws<ArgumentNullException>(() => _ = _hashCounter[null]);
            Assert.Throws<ArgumentNullException>(() => _hashCounter[null] = 5);
        }

        [Test]
        public void GetNonExistentKey_Zero()
        {
            Assert.AreEqual(0, _hashCounter["abc"]);
        }

        [Test]
        public void SetValue_CorrectGetValue()
        {
            _hashCounter["abc"] = 5;
            Assert.AreEqual(5, _hashCounter["abc"]);
            _hashCounter["abc"] = 3;
            Assert.AreEqual(3, _hashCounter["abc"]);
        }

        [Test]
        public void SetZero_KeyRemoved()
        {
            _hashCounter.Add("abc", 5);
            _hashCounter["abc"] = 0;
            Assert.AreEqual(0, _hashCounter["abc"]);
            Assert.AreEqual(0, _hashCounter.Count);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-100)]
        public void InvalidSet_ArgumentOutOfRangeException(int setValue)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _hashCounter["abc"] = setValue);

            _hashCounter.Add("def", 5);
            Assert.Throws<ArgumentOutOfRangeException>(() => _hashCounter["def"] = setValue);
        }
    }
}
