using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Collections;
using Concurrent;

ConcurrentAppendOnlySinglyLinkedList<int> ll = new();

// ll.Add(10);
// ll.Add(20);
// var prev = ll.Clear();
// foreach (var i in prev)
// 	Console.WriteLine(i);
// return;

var l = new ConcurrentBag<int>();
Parallel.For(0, 10_000_000, i =>
{
	ll.Add(i);
	if (i % 200 == 0)
		foreach (var i1 in ll.GetAndClear())
			l.Add(i1);
});
foreach (var i1 in ll.GetAndClear())
	l.Add(i1);
var items = l.ToArray();
Array.Sort(items);
Console.WriteLine($"lc {l.Count:N0}");
Console.WriteLine($"il {items.Length:N0}");
Console.WriteLine($"* {ll.Misses:N0} misses");
return;

// ConcurrentThing<string> thing = new(1);
// thing.Add("Hello");
// thing.Add("World");
// Console.WriteLine(thing.Count); // 2

for (var i = 0; i < 30000; i++)
{
	ConcurrentLinkedList<int> linkedList = new();
// linkedList.Add(1);
// linkedList.Add(2);
// linkedList.Add(3);
	Parallel.For(0, 1_000, linkedList.Add);
	//Console.WriteLine(linkedList.Count); // 1000
	var linkedListEnumerable = linkedList.GetAndClear();
	var list = new Collections.List<int>(100_000);
	foreach (var item in linkedListEnumerable)
		list.Add(item);
	//list.Sort();
	;
	if (list.Count != 1_000)
	{
		Console.BackgroundColor = ConsoleColor.Red;
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine(list.Count);
		Console.ResetColor();
		return;
	}
	// else
	// 	Console.WriteLine(list.Count);
	if (i % 1000 == 0)
		Console.WriteLine(i);
}