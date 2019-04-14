# HashCounter
A data structure that represents a collection of keys and integer counters.

## Example Usage
In this example, unique words are counted using a `HashCounter<string>`.
Each invocation of `Add` increments the counter associated with the specified key by 1.

```c#
static void CountUniqueWords(IEnumerable<string> input)
{
    HashCounter<string> counter = new HashCounter<string>();
    // Or, to ignore case for example, you can supply your own equality comparer:
    // new HashCounter<string>(StringComparer.InvariantCultureIgnoreCase);
    
    foreach (string word in input) counter.Add(word);
    
    foreach (KeyValuePair<string, int> count in counter)
        { Console.WriteLine($"'{count.Key}': {count.Value}"); }
}

CountUniqueWords(new []{"Apple", "Banana", "Grape", "Banana", "Pear",
                        "Raspberry", "Apple", "Banana", "Raspberry"});
```
Produces the following output:
> 'Apple': 2  
> 'Banana': 3  
> 'Grape': 1  
> 'Pear': 1  
> 'Raspberry': 2

Continuing from the above example, the counters can be decremented or removed entirely.
Each invocation of `Subtract` decrements the counter associated with the specified key by 1.

```c#
counter.Subtract("Apple");
counter.Subtract("Banana", 2); // Subtracts 2 from the counter instead of 1
counter.Remove("Raspberry");
```
> 'Apple': 1  
> 'Banana': 1  
> 'Grape': 1  
> 'Pear': 1

An alternative to the `Add`, `Subtract` and `Remove` methods is the indexer of the HashCounter which can be used to get or set the value of a counter associated with the specified key.

```c#
counter["Grape"] = 3;
counter["Pear"] = counter["Grape"] + 1;
```
> 'Apple': 1  
> 'Banana': 1  
> 'Grape': 3  
> 'Pear': 4

Once the counter associated with a given key reaches 0, it is no longer shown in the enumerator. Likewise, accessing the counter for a key which was never added or reached 0 will return 0.
```c#
Console.WriteLine($"'Raspberry': {counter["Raspberry"]}");
Console.WriteLine($"'Blueberry': {counter["Blueberry"]}");
```
> 'Raspberry': 0  
> 'Blueberry': 0
