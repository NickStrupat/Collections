using System.Runtime.CompilerServices;

namespace Collections;

public static class InterlockedExtensions2
{
    public static Int64 InterlockedRead(ref readonly this Int64 value) => Interlocked.Read(ref Unsafe.AsRef(in value));
    public static UInt64 InterlockedRead(ref readonly this UInt64 value) => Interlocked.Read(ref Unsafe.AsRef(in value));
	
    public static Int32 InterlockedAdd(ref this Int32 value, Int32 addend) => Interlocked.Add(ref value, addend);
    public static UInt32 InterlockedAdd(ref this UInt32 value, UInt32 addend) => Interlocked.Add(ref value, addend);
    public static Int64 InterlockedAdd(ref this Int64 value, Int64 addend) => Interlocked.Add(ref value, addend);
    public static UInt64 InterlockedAdd(ref this UInt64 value, UInt64 addend) => Interlocked.Add(ref value, addend);
	
    public static Int32 InterlockedIncrement(ref this Int32 value) => Interlocked.Increment(ref value);
    public static UInt32 InterlockedIncrement(ref this UInt32 value) => Interlocked.Increment(ref value);
    public static Int64 InterlockedIncrement(ref this Int64 value) => Interlocked.Increment(ref value);
    public static UInt64 InterlockedIncrement(ref this UInt64 value) => Interlocked.Increment(ref value);
	
    public static Int32 InterlockedDecrement(ref this Int32 value) => Interlocked.Decrement(ref value);
    public static UInt32 InterlockedDecrement(ref this UInt32 value) => Interlocked.Decrement(ref value);
    public static Int64 InterlockedDecrement(ref this Int64 value) => Interlocked.Decrement(ref value);
    public static UInt64 InterlockedDecrement(ref this UInt64 value) => Interlocked.Decrement(ref value);
}