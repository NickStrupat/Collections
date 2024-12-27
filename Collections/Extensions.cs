using System.Runtime.CompilerServices;

namespace Collections;

public static class Extensions
{
	public static T ThrowIfArgNull<T>(this T? value, [CallerArgumentExpression(nameof(value))] String name = "") where T : class
	{
		ArgumentNullException.ThrowIfNull(value, name);
		return value;
	}
}