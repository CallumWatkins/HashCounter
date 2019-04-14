using NUnit.Framework;

namespace HashCounter.Tests
{
    public class Count
    {
        private HashCounter<string> _hashCounter;

        [SetUp]
        public void Setup()
        {
            _hashCounter = new HashCounter<string>();
        }

        [Test]
        public void InitialCountZero()
        {
            Assert.AreEqual(0, _hashCounter.Count);
        }

        [Test]
        public void AddDifferentKeys_IncreaseCount()
        {
            _hashCounter.Add("abc");
            Assert.AreEqual(1, _hashCounter.Count);
            _hashCounter.Add("def");
            Assert.AreEqual(2, _hashCounter.Count);
            _hashCounter.Add("ghi");
            Assert.AreEqual(3, _hashCounter.Count);
        }

        [Test]
        public void AddSameKeys_SameCount()
        {
            _hashCounter.Add("abc");
            Assert.AreEqual(1, _hashCounter.Count);
            _hashCounter.Add("abc");
            Assert.AreEqual(1, _hashCounter.Count);

            _hashCounter.Add("def");
            Assert.AreEqual(2, _hashCounter.Count);
            _hashCounter.Add("def");
            Assert.AreEqual(2, _hashCounter.Count);

            _hashCounter.Add("abc");
            Assert.AreEqual(2, _hashCounter.Count);
            _hashCounter.Add("def");
            Assert.AreEqual(2, _hashCounter.Count);
        }

        [Test]
        public void Clear_ZeroCount()
        {
            _hashCounter.Add("abc");
            _hashCounter.Add("def");
            _hashCounter.Add("ghi");
            _hashCounter.Add("abc");
            _hashCounter.Add("def");

            _hashCounter.Clear();
            Assert.AreEqual(0, _hashCounter.Count);
        }
    }
}
