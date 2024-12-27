namespace Collections;

public class Set<T> : ISetAddable<T>, IRemovable<T>, IContains<T>, ICountable, IClearable
{
	private readonly HashSet<T> set;
	private HashCodeBuilder hcb;
	
	public Set() => set = new();
	public Set(IEnumerable<T> values) => set = new(values);
	public Set(Int32 capacity) => set = new(capacity);
	public Set(IEqualityComparer<T> comparer) => set = new(comparer);
	public Set(IEnumerable<T> values, IEqualityComparer<T> comparer) => set = new(values, comparer);
	public Set(Int32 capacity, IEqualityComparer<T> comparer) => set = new(capacity, comparer);

	public Boolean Add(T value)
	{
		var result = set.Add(value);
		hcb.Add(value);
		return result;
	}

	public Boolean Remove(T value)
	{
		var result = set.Remove(value);
		if (result) hcb.Remove(value);
		return result;
	}

	public Boolean Contains(T value) => set.Contains(value);

	public Int32 Count => set.Count;
	
	public void Clear() { set.Clear(); hcb = new(); }
}