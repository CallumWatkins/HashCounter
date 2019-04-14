using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace HashCounter.Tests
{
    public class Enumerator
    {
        private HashCounter<string> _hashCounter;

        [SetUp]
        public void Setup()
        {
            _hashCounter = new HashCounter<string>();
        }

        [Test]
        public void NotNull()
        {
            using (IEnumerator<KeyValuePair<string, int>> enumerator = _hashCounter.GetEnumerator())
            {
                Assert.NotNull(enumerator);
            }

            _hashCounter["abc"] = 8;
            _hashCounter["def"] = 150;
            _hashCounter["ghi"] = 25;

            using (IEnumerator<KeyValuePair<string, int>> enumerator = _hashCounter.GetEnumerator())
            {
                Assert.NotNull(enumerator);
            }
        }

        [Test]
        public void NonGenericEnumeratorSameAsGeneric()
        {
            IEnumerator nonGenericEnumerator = ((IEnumerable)_hashCounter).GetEnumerator();
            var genericEnumerator = (IEnumerator<KeyValuePair<string, int>>) nonGenericEnumerator;
            genericEnumerator.Dispose();
        }

        [Test]
        public void HasExpectedPairs()
        {
            _hashCounter["abc"] = 8;
            _hashCounter["def"] = 150;
            _hashCounter["ghi"] = 25;

            using (IEnumerator<KeyValuePair<string, int>> enumerator = _hashCounter.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current.Key)
                    {
                        case "abc": Assert.AreEqual(8, enumerator.Current.Value); break;
                        case "def": Assert.AreEqual(150, enumerator.Current.Value); break;
                        case "ghi": Assert.AreEqual(25, enumerator.Current.Value); break;
                        default: Assert.Fail("Unexpected key/value pair in enumerator."); break;
                    }
                }
            }
        }

        [Test]
        public void HasAllExpectedKeysWithoutDuplicates()
        {
            _hashCounter["abc"] = 8;
            _hashCounter["def"] = 150;
            _hashCounter["ghi"] = 25;

            var enumeratedKeys = new List<string>();
            using (IEnumerator<KeyValuePair<string, int>> enumerator = _hashCounter.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumeratedKeys.Add(enumerator.Current.Key);
                }
            }
            Assert.AreEqual(1, enumeratedKeys.Count(key => key == "abc"));
            Assert.AreEqual(1, enumeratedKeys.Count(key => key == "def"));
            Assert.AreEqual(1, enumeratedKeys.Count(key => key == "ghi"));
        }



        [Test]
        public void RemoveKey()
        {
            _hashCounter["abc"] = 8;
            _hashCounter["def"] = 150;
            _hashCounter["ghi"] = 25;

            _hashCounter.Remove("abc");
            _hashCounter["def"] = 0;
            _hashCounter.Subtract("ghi", 25);

            using (IEnumerator<KeyValuePair<string, int>> enumerator = _hashCounter.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current.Key)
                    {
                        case "abc":
                        case "def":
                        case "ghi":
                            Assert.Fail("Removed key present in enumerator.");
                            break;
                    }
                }
            }
        }
    }
}