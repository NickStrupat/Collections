using System.Collections;

namespace Collections;

public interface IList<T> : ICovariantList<T>, IContravariantList<T>, IClearable, ICountable, ICopyableTo<T>;

public interface ICovariantList<out T> : IIterable<T>, IReadIndexable<T>;
public interface IContravariantList<in T> : IAddable<T>, IRemovable<T>, IInsertable<T>, IIndexableOf<T>, IWriteIndexable<T>, IContains<T>;

public interface IIterable<out T> : IEnumerable<T>
{
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public interface IAddable<in T>
{
    public void Add(T value);
}

public interface IRemovable<in T>
{
    public Boolean Remove(T value);
}

public interface IInsertable<in T>
{
    public void Insert(Int32 index, T value);
}

public interface ISetAddable<in T>
{
    public Boolean Add(T value);
}

public interface IIndexableOf<in T>
{
    Int32 IndexOf(T item);
}

public interface IReadIndexable<out T>
{
    T this[Int32 index] { get; }
}

public interface IWriteIndexable<in T>
{
    T this[Int32 index] { set; }
}

public interface IRefIndexable<T>
{
    ref T this[Int32 index] { get; }
}

public interface IRefReadonlyIndexable<T>
{
    ref readonly T this[Int32 index] { get; }
}

// public interface IRefImmutableIndexable<T> where T : unmanaged
// {
//     RefImmutable<T> this[Int32 index] { get; }
// }
//
// public ref struct RefImmutable<T> where T : unmanaged
// {
//     private ref readonly T value;
//
//     public RefImmutable(in T value) => this.value = ref value;
//
//     public ref readonly T Value => ref value;
// }

public interface IContiguous<T>
{
    Span<T> AsSpan();
}

public interface IContiguousManaged<T>
{
    Memory<T> AsMemory();
}

public interface IContiguousReadOnly<T>
{
    ReadOnlySpan<T> AsReadOnlySpan();
}

public interface IContiguousManagedReadOnly<T>
{
    ReadOnlyMemory<T> AsReadOnlyMemory();
}

public interface IContains<in T>
{
    Boolean Contains(T value);
}

public interface ICopyableTo<T>
{
    void CopyTo(IContiguous<T> contiguous, Int32 arrayIndex);
}

public interface ICountable
{
    Int32 Count { get; }
}

public interface IClearable
{
    void Clear();
}