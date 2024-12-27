namespace Collections;

public sealed class DefaultEqualityComparer<T> : IEqualityComparer<T>
{
	public static DefaultEqualityComparer<T> Instance { get; } = new();
	
	private DefaultEqualityComparer() {}
	
	public Boolean Equals(T? x, T? y)
	{
		if (typeof(T).IsValueType)
			return x!.Equals(y!);
		
		if (ReferenceEquals(x, y))
			return true;
		if (x is null || y is null)
			return false;
		return x.Equals(y);
	}

	public Int32 GetHashCode(T value)
	{
		if (typeof(T).IsValueType || value is not null)
			return value!.GetHashCode();
		return 0;
	}
}