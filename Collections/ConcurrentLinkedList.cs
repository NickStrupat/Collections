namespace Collections;

public sealed class ConcurrentLinkedList<T>
{
	private Node head = tail;
	
	private static readonly Node tail = new(default!);
	
	private sealed class Node
	{
		public readonly T Value;
		public volatile Node Next;
		public Node(T value) => (Value, Next) = (value, this);
	}
	
	public void Add(T value)
	{
		var newNode = new Node(value);
		var oldNode = Interlocked.Exchange(ref head, newNode);
		newNode.Next = oldNode;
	}

	public IEnumerable<T> GetAndClear()
	{
		var node = Interlocked.Exchange(ref head, tail);
		return node == tail ? [] : NodeValueEnumerable(node);

		static IEnumerable<T> NodeValueEnumerable(Node node)
		{
			do
			{
				yield return node.Value;
				Node next;
				do next = node.Next; // another thread might not have assigned the next node reference yet
				while (next == node); // so spin until it has
				node = next;
			}
			while (node != tail);
		}
	}
}