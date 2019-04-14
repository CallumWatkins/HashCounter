using System.Collections.Generic;
using NUnit.Framework;

namespace HashCounter.Tests
{
    public class Keys
    {
        private HashCounter<string> _hashCounter;

        [SetUp]
        public void Setup()
        {
            _hashCounter = new HashCounter<string>();
        }

        [Test]
        public void Empty_ZeroKeys()
        {
            ICollection<string> keys = _hashCounter.Keys;
            Assert.Zero(keys.Count);
        }

        [Test]
        public void HasExpectedKeys()
        {
            _hashCounter.Add("abc");
            _hashCounter.Add("def");
            _hashCounter.Add("ghi");

            ICollection<string> keys = _hashCounter.Keys;
            Assert.AreEqual(3, keys.Count);
            Assert.That(keys.Contains("abc"));
            Assert.That(keys.Contains("def"));
            Assert.That(keys.Contains("ghi"));
        }

        [Test]
        public void DoesNotHaveRemovedKeys()
        {
            _hashCounter["abc"] = 8;
            _hashCounter["def"] = 150;
            _hashCounter["ghi"] = 25;

            _hashCounter.Remove("abc");
            _hashCounter["def"] = 0;
            _hashCounter.Subtract("ghi", 25);

            ICollection<string> keys = _hashCounter.Keys;
            Assert.Zero(keys.Count);
        }
    }
}
