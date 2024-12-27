using System.Collections;
using System.Runtime.CompilerServices;
using NickStrupat;

namespace Collections;

public class ConcurrentThing<T>(Int32 capacity) : IAddable<T>, ICountable
{
	public ConcurrentThing() : this(4) {}
	
	private readonly Object syncRoot = new();
	private Int32 count;
	private CacheLineAlignedArray<T> array = new(capacity);
	
	public void Add(T value)
	{
		lock (syncRoot)
		{
			count++;
			if (count > array.Length)
			{
				var newArray = new CacheLineAlignedArray<T>(array.Length * 2);
				for (var i = 0; i < array.Length; i++)
				{
					newArray[i] = array[i];
				}
				array = newArray;
			}
			array[count - 1] = value;
		}
	}

	public Int32 Count
	{
		get
		{
			lock (syncRoot)
			{
				return count;
			}
		}
	}
}

public sealed class CacheLineAlignedArray<T>(Int32 size) : IReadIndexable<T>, IWriteIndexable<T>, IRefIndexable<T>, IRefReadonlyIndexable<T>
{
	private readonly T[] buffer = new T[Multiplier * size];
	public Int32 Length => size;
	public ref T this[Int32 index] => ref buffer[Multiplier * index];
	private static readonly Int32 Multiplier = CacheLine.Size / Unsafe.SizeOf<T>();
    
    T IReadIndexable<T>.this[Int32 index] { get => this[index]; }
    T IWriteIndexable<T>.this[Int32 index] { set => this[index] = value; }
    ref readonly T IRefReadonlyIndexable<T>.this[Int32 index] => ref this[index];
}