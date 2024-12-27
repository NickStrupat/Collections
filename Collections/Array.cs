namespace Collections;

public sealed class Array<T>(UInt32 size)
	: IReadIndexable<T>, IWriteIndexable<T>
	, IRefIndexable<T>, IRefReadonlyIndexable<T>
	, ICountable, IContiguous<T>, IContiguousManaged<T>
	, IContiguousReadOnly<T>, IContiguousManagedReadOnly<T>
	, IContains<T>, IIndexableOf<T>
	, IEquatable<Array<T>>
{
	private HashCodeBuilder hashCode;
	private readonly T[] array = new T[size];

	public T this[Int32 index]
	{
		get => array[index];
		set => array[index] = value;
	}

	ref T IRefIndexable<T>.this[Int32 index] => ref array[index];
	ref readonly T IRefReadonlyIndexable<T>.this[Int32 index] => ref array[index];
	
	public Int32 Count => array.Length;
	
	public Span<T> AsSpan() => array.AsSpan();
	public Memory<T> AsMemory() => array.AsMemory();
	public ReadOnlySpan<T> AsReadOnlySpan() => array.AsSpan();
	public ReadOnlyMemory<T> AsReadOnlyMemory() => array.AsMemory();
	public Boolean Contains(T value) => Array.IndexOf(array, value) != -1;
	
	public Boolean Equals(Array<T>? other) => other is not null && ReferenceEquals(this, other);
	public Int32 IndexOf(T item) => Array.IndexOf(array, item);

	public override Boolean Equals(Object? obj) => obj is Array<T> other && Equals(other);
	public override Int32 GetHashCode() => hashCode.CurrentHashCode;
}