using System;
using System.Collections;
using System.Collections.Generic;

namespace HashCounter
{
    /// <summary>
    /// Represents a collection of keys and integer counters.
    /// </summary>
    /// <typeparam name="TKey">The type of keys to store counters for.</typeparam>
    public class HashCounter<TKey> : IEnumerable<KeyValuePair<TKey, int>>
    {
        /// <summary>
        /// Represents the mapping between keys and their associated counters.
        /// </summary>
        private readonly Dictionary<TKey, int> _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCounter{TKey}"/> class that is empty, has the default initial
        /// capacity, and uses the default equality comparer for the key type.
        /// </summary>
        public HashCounter()
            => _map = new Dictionary<TKey, int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCounter{TKey}"/> class that is empty, has the specified initial
        /// capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="HashCounter{TKey}"/> can contain.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="capacity"/> is less than 0.</exception>
        public HashCounter(int capacity)
            => _map = new Dictionary<TKey, int>(capacity);

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCounter{TKey}"/> class that is empty, has the default initial
        /// capacity, and uses the specified <see cref="IEqualityComparer{TKey}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="IEqualityComparer{TKey}"/> implementation to use when comparing keys, or null
        /// to use the default <see cref="IEqualityComparer{TKey}"/> for the type of the key.</param>
        public HashCounter(IEqualityComparer<TKey> comparer)
            => _map = new Dictionary<TKey, int>(comparer);

        /// <summary>
        /// Initializes a new instance of the <see cref="HashCounter{TKey}"/> class that is empty, has the specified initial
        /// capacity, and uses the specified <see cref="IEqualityComparer{TKey}"/>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="HashCounter{TKey}"/> can contain.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer{TKey}"/> implementation to use when comparing keys, or null
        /// to use the default <see cref="IEqualityComparer{TKey}"/> for the type of the key.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="capacity"/> is less than 0.</exception>
        public HashCounter(int capacity, IEqualityComparer<TKey> comparer)
            => _map = new Dictionary<TKey, int>(capacity, comparer);

        /// <summary>
        /// Gets or sets the value of the counter associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the counter to get or set.</param>
        /// <returns></returns>
        public int this[TKey key]
        {
            get => _map.TryGetValue(key, out int count) ? count : 0;
            set
            {
                if (value < 0) { throw new ArgumentOutOfRangeException(nameof(value), value, "New count cannot be less than 0."); }

                if (value == 0)
                {
                    Remove(key);
                }
                else
                {
                    if (_map.ContainsKey(key))
                    {
                        _map[key] = value;
                    }
                    else
                    {
                        Add(key, value);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="HashCounter{TKey}"/>.
        /// </summary>
        public ICollection<TKey> Keys => _map.Keys;

        /// <summary>
        /// Gets the <see cref="IEqualityComparer{TKey}"/> that is used to determine equality of keys.
        /// </summary>
        public IEqualityComparer<TKey> Comparer => _map.Comparer;

        /// <summary>
        /// Gets the number of non-zero counters contained in the <see cref="HashCounter{TKey}"/>.
        /// </summary>
        public int Count => _map.Count;

        /// <summary>
        /// Determines whether the <see cref="HashCounter{TKey}"/> contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="HashCounter{TKey}"/>.</param>
        /// <returns>True if the <see cref="HashCounter{TKey}"/> contains a non-zero counter with the specified key; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is null.</exception>
        public bool Contains(TKey key) => _map.ContainsKey(key);

        /// <summary>
        /// Adds 1 to the counter associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the counter to add to.</param>
        /// <returns>The new counter value for the given key.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is null.</exception>
        /// <exception cref="OverflowException">Thrown if the addition of 1 to the existing counter value causes an integer overflow.</exception>
        public int Add(TKey key) => Add(key, 1);

        /// <summary>
        /// Adds a specified number to the counter associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the counter to add to.</param>
        /// <param name="addend">The number to add to the counter.</param>
        /// <returns>The new counter value for the given key.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is null.</exception>
        /// <exception cref="OverflowException">Thrown if the addition of the existing counter value and <paramref name="addend"/> causes an integer overflow.</exception>
        public int Add(TKey key, int addend)
        {
            if (addend < 1) { throw new ArgumentOutOfRangeException(nameof(addend), addend, "Addend cannot be less than 1."); }

            if (_map.TryGetValue(key, out int count))
            {
                checked { count += addend; }
                return _map[key] = count;
            }
            else
            {
                _map.Add(key, addend);
                return addend;
            }
        }

        /// <summary>
        /// Subtracts 1 from the counter associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the counter to subtract from.</param>
        /// <returns>The new counter value for the given key.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is null.</exception>
        public int Subtract(TKey key) => Subtract(key, 1);

        /// <summary>
        /// Subtracts a specified number from the counter associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the counter to subtract from.</param>
        /// <param name="subtrahend">The number to subtract from the counter.</param>
        /// <returns>The new counter value for the given key.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="subtrahend"/> is less than 1.</exception>
        public int Subtract(TKey key, int subtrahend)
        {
            if (subtrahend < 1) { throw new ArgumentOutOfRangeException(nameof(subtrahend), subtrahend, "Subtrahend cannot be less than 1."); }

            if (_map.TryGetValue(key, out int count))
            {
                if (count <= subtrahend)
                {
                    _map.Remove(key);
                    return 0;
                }
                else
                {
                    return _map[key] = count - subtrahend;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Removes the counter for a given key. Equivalent to setting the value of the counter to zero.
        /// </summary>
        /// <param name="key">The key of the counter to remove.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is null.</exception>
        public void Remove(TKey key) => _map.Remove(key);

        /// <summary>
        /// Removes the counters for all keys.
        /// </summary>
        public void Clear() => _map.Clear();

        /// <summary>
        /// Returns an enumerator that iterates through the counters.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the counters.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the counters.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the counters.</returns>
        public IEnumerator<KeyValuePair<TKey, int>> GetEnumerator() => _map.GetEnumerator();
    }
}
