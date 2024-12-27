using System.Runtime.InteropServices;

namespace Collections;

public readonly unsafe struct RawAlignedMemory : IEquatable<RawAlignedMemory>, IDisposable
{
	private readonly void* pointer;
	private readonly Int32 size;

	public RawAlignedMemory(Int32 size, Int32 alignment)
	{
		pointer = NativeMemory.AlignedAlloc(checked((UIntPtr) size), checked((UIntPtr) alignment));
		this.size = size;
	}

	public void Dispose() => NativeMemory.AlignedFree(pointer);

	public Span<Byte> Span => new(pointer, size);

	public Boolean Equals(RawAlignedMemory other) => pointer == other.pointer && size == other.size;
	public override Boolean Equals(Object? obj) => obj is RawAlignedMemory other && Equals(other);
	public override Int32 GetHashCode() => HashCode.Combine((Int64) pointer, size);

	public static Boolean operator ==(RawAlignedMemory left, RawAlignedMemory right) => left.Equals(right);
	public static Boolean operator !=(RawAlignedMemory left, RawAlignedMemory right) => !(left == right);
}