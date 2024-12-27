using System;
using System.Collections.Generic;
using System.Threading;

namespace Concurrent;

public sealed class ConcurrentAppendOnlySinglyLinkedList<T>
{
	public Int64 Misses;
	private Node head = tail;
	
	private static readonly Node unassigned = new(default!, default!);
	private static readonly Node tail = new(default!, default!);
	
	internal sealed class Node
	{
		public readonly T Value;
		//public volatile Node Next;
		public Node Next;
		public Node(T value, Node next) => (Value, Next) = (value, next);
	}
	
	public void Add(T value)
	{
		var newNode = new Node(value, unassigned);
		var oldNode = Interlocked.Exchange(ref head, newNode);
		//newNode.Next = oldNode;
		Interlocked.Exchange(ref newNode.Next, oldNode);
		//Volatile.Write(ref newNode.Next, oldNode);
	}

	public IEnumerable<T> GetAndClear()
	{
		var node = Interlocked.Exchange(ref head, tail);
		return node == tail ? [] : NodeValueEnumerable(node);

		IEnumerable<T> NodeValueEnumerable(Node node)
		{
			// while (node != tail)
			// {
			// 	yield return node.Value;
			// 	// Node next;
			// 	// do next = Interlocked.CompareExchange(ref node.Next, default!, default!); // another thread might not have assigned the next node reference yet
			// 	// while (ReferenceEquals(next, node)); // so spin until it has
			// 	// node = next;
			// }
			
			
			var count = 0;
			do
			{
				yield return node.Value;
				Node next = node.Next; // try a cheap, normal read first
				if (next == unassigned)
				{
					for (;;) // 
					{
						//next = node.Next; // another thread might not have assigned the next node reference yet
						next = Interlocked.CompareExchange(ref node.Next, default!, default!);
						//next = Volatile.Read(ref node.Next);
						if (next != unassigned)
							break;
						count++;
					}
				}

				// do
				// {
				// 	//next = node.Next; // another thread might not have assigned the next node reference yet
				// 	next = Interlocked.CompareExchange(ref node.Next, default!, default!);
				// 	//next = Volatile.Read(ref node.Next);
				// 	count++;
				// } while (next == node); // so spin until it has
				node = next;
			}
			while (node != tail);
			if (count > 1)
				Interlocked.Add(ref Misses, count);
		}
	}

	public Enumerable Clear() => new(Interlocked.Exchange(ref head, tail));
	
	public readonly ref struct Enumerable
	{
		private readonly Node head;
		internal Enumerable(Node head) => this.head = head;
		public Enumerator GetEnumerator() => new(head);
	}
	
	public ref struct Enumerator
	{
		private Node node;
		internal Enumerator(Node node) => this.node = new Node(default!, node);
		
		public T Current => node.Value;
		
		public bool MoveNext()
		{
			Node next = node.Next;
			if (next == unassigned)
			{
				for (;;)
				{
					next = Interlocked.CompareExchange(ref node.Next, default!, default!);
					if (next != unassigned)
						break;
				}
			}
			node = next;
			return node != tail;
		}
	}
}