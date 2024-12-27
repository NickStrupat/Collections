namespace Collections;

public readonly ref struct InterlockedInt32Ref
{
	private readonly ref Int32 value;
	public InterlockedInt32Ref(ref Int32 value) => this.value = ref value;

	public Int32 Load() => Interlocked.CompareExchange(ref value, default, default);
	public Int32 Store(Int32 value) => Interlocked.Exchange(ref this.value, value);
	public Int32 Add(Int32 value) => Interlocked.Add(ref this.value, value);
	public Int32 CompareExchange(Int32 value, Int32 comparand) => Interlocked.CompareExchange(ref this.value, value, comparand);
}