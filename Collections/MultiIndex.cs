using System.Runtime.CompilerServices;

namespace Collections;

public struct VList<T> : IAddable<T>, IReadIndexable<T>, IWriteIndexable<T>
{
	private List<T> __list;
	private List<T> list => __list ??= new();

	public void Add(T value) => list.Add(value);
	public T this[Int32 index] { get => list[index]; set => list[index] = value; }
}

public class MultiIndex
{
	public MultiIndex()
	{
		using var page = new RawAlignedMemory(Environment.SystemPageSize, Environment.SystemPageSize);
		page.Span.Fill(42);
	}
}
