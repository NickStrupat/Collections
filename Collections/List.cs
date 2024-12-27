namespace Collections;

public class List<T> : IList<T>
{
	private readonly System.Collections.Generic.List<T> list;
	private HashCodeBuilder hcb;
	
	public List() => list = new();
	public List(IEnumerable<T> collection) => list = new(collection);
	public List(Int32 capacity) => list = new(capacity);
	
	public void Add(T value) { list.Add(value); hcb.Add(value); }
	public Boolean Remove(T value) { var result = list.Remove(value); hcb.Remove(value); return result; }
	public void Insert(Int32 index, T item) { list.Insert(index, item); hcb.Add(item); }
	public void RemoveAt(Int32 index) { var item = this[index]; list.RemoveAt(index); hcb.Remove(item); }
	public void Clear() { list.Clear(); hcb = new(); }

	public override Int32 GetHashCode() => hcb.CurrentHashCode;
	public override Boolean Equals(Object? obj) => obj is List<T> other && GetHashCode() == other.GetHashCode();
	
	public void CopyTo(IContiguous<T> contiguous, Int32 arrayIndex) => list.CopyTo(contiguous.ThrowIfArgNull().AsSpan()[arrayIndex..]);
    
	public Boolean Contains(T item) => list.Contains(item);

	public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
	
	public Int32 Count => list.Count;

	public Int32 IndexOf(T item) => list.IndexOf(item);

	public T this[Int32 index]
	{
		get => list[index];
		set => list[index] = value;
	}
}
