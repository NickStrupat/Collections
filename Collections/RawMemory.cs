using System.Collections;
using System.Runtime.InteropServices;

namespace Collections;

public sealed unsafe class RawMemory : IEquatable<RawMemory>, IDisposable, IEnumerable<Byte>
{
	private UIntPtr pointer;
	private readonly UIntPtr size;

	public RawMemory(UIntPtr size)
	{
		pointer = (UIntPtr) NativeMemory.AllocZeroed(new UIntPtr(size));
		this.size = size;
	}

	~RawMemory() => Dispose();

	public void Dispose()
	{
		GC.SuppressFinalize(this);
		if (Interlocked.Exchange(ref pointer, UIntPtr.Zero) != UIntPtr.Zero)
			NativeMemory.Free((void*) pointer);
	}

	private UIntPtr CheckIndex(UIntPtr index) => index >= size ? throw new ArgumentOutOfRangeException(nameof(index)) : index;

	public Byte this[UIntPtr index]
	{
		get => ((Byte*) pointer)[CheckIndex(index)];
		set => ((Byte*) pointer)[CheckIndex(index)] = value;
	}

	public Span<Byte> GetSpan(UIntPtr offset, Int32 size) => new((void*) (pointer + offset), size);

	public Boolean Equals(RawMemory? other) => other is not null && pointer == other.pointer && size == other.size;
	public override Boolean Equals(Object? obj) => obj is RawMemory other && Equals(other);
	public override Int32 GetHashCode() => HashCode.Combine((Int64) pointer, size);

	public static Boolean operator ==(RawMemory left, RawMemory right)
	{
		ArgumentNullException.ThrowIfNull(left);
		return left.Equals(right);
	}

	public static Boolean operator !=(RawMemory left, RawMemory right) => !(left == right);

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerator<Byte> GetEnumerator() => new Enumerator(this);

	public sealed class Enumerator : IEnumerator<Byte>
	{
		private UIntPtr index;
		private readonly RawMemory rawMemory;
		public Enumerator(RawMemory rawMemory) => this.rawMemory = rawMemory;

		public Boolean MoveNext()
		{
			if (rawMemory.size == 0)
				return false;
			if (Current == rawMemory[rawMemory.size - 1])
				return false;
			index++;
			return true;
		}

		public void Reset() => index = 0;

		public Byte Current => rawMemory[index];

		Object IEnumerator.Current => Current;

		public void Dispose() {}
	}
}
