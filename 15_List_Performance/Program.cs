
using System.Diagnostics;

var input = Enumerable.Range(0, 100_000_000).ToArray();
var stopwatch = Stopwatch.StartNew();

var list = new List<int>();
foreach (var item in input)
{
    list.Add(item);
}
stopwatch.Stop();
Console.WriteLine($"Took: {stopwatch.ElapsedMilliseconds} ms");  // 550ms


var stopwatch2 = Stopwatch.StartNew();
var listOptimized = new List<int>(input);
stopwatch2.Stop();
Console.WriteLine($"Took: {stopwatch2.ElapsedMilliseconds} ms");  // 111ms It's fast because it uses the CopeTo method

var stopwatch3 = Stopwatch.StartNew();
var listOptimized2 = new List<int>(input.Length);
foreach (var item in input)
{
    list.Add(item);
}
stopwatch3.Stop();
Console.WriteLine($"Took: {stopwatch3.ElapsedMilliseconds} ms");  // 473ms


list.Clear();
list.TrimExcess();  // free a lot of memory
list.AddRange(input);  // use this method rather than add them one by one

list.RemoveRange(5, 100_000);  // use this rather than use remove many times
list.RemoveAt(5);   // use this is better than remove O(n)
list.Remove(99);  // This will search for the 99 element, and use the index, and call removeat method

Console.ReadKey();