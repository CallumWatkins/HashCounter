using System;
using NUnit.Framework;

namespace HashCounter.Tests
{
    public class Contains
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
            Assert.Throws<ArgumentNullException>(() => _hashCounter.Contains(null));
        }

        [Test]
        public void NonExistentKey_False()
        {
            Assert.False(_hashCounter.Contains("abc"));
        }

        [Test]
        public void ValidKey_True()
        {
            _hashCounter.Add("abc");
            Assert.True(_hashCounter.Contains("abc"));
        }

        [Test]
        public void RemovedKey_False()
        {
            _hashCounter["abc"] = 8;
            _hashCounter["def"] = 150;
            _hashCounter["ghi"] = 25;

            _hashCounter.Remove("abc");
            _hashCounter["def"] = 0;
            _hashCounter.Subtract("ghi", 25);

            Assert.False(_hashCounter.Contains("abc"));
            Assert.False(_hashCounter.Contains("def"));
            Assert.False(_hashCounter.Contains("ghi"));
        }
    }
}
