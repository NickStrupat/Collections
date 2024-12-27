using System.ComponentModel;

namespace Collections;

public struct ConcurrentHashCodeBuilder
{
	private Int32 hashCode;
	
	public void Add<T>(T value) => Add(value, DefaultEqualityComparer<T>.Instance);
	
	public void Add<T, TEc>(T value, TEc equalityComparer) where TEc : IEqualityComparer<T>
	{
		if (typeof(T).IsValueType || value is not null)
			Interlocked.Add(ref hashCode, equalityComparer.GetHashCode(value!));
	}
	
	public void Remove<T>(T value) => Remove(value, DefaultEqualityComparer<T>.Instance);
	
	public void Remove<T, TEc>(T value, TEc equalityComparer) where TEc : IEqualityComparer<T>
	{
		if (typeof(T).IsValueType || value is not null)
			Interlocked.Add(ref hashCode, -equalityComparer.GetHashCode(value!));
	}
	
	public Int32 CurrentHashCode => Interlocked.CompareExchange(ref hashCode, 0, 0);

	[Obsolete($"{nameof(HashCodeBuilder)} is a mutable struct and should not be compared with other {nameof(HashCodeBuilder)}s. Use ToHashCode to retrieve the computed hash code.", error: true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Int32 GetHashCode() => throw new NotSupportedException();

	[Obsolete($"{nameof(HashCodeBuilder)} is a mutable struct and should not be compared with other {nameof(HashCodeBuilder)}s.", error: true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Boolean Equals(Object? obj) => throw new NotSupportedException();
}