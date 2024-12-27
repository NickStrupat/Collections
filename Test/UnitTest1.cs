using System;
using System.Collections.Generic;
using System.Linq;
using Collections;
using FluentAssertions;
using Xunit;

namespace Test;

public class UnitTest1
{
	[Theory]
	[MemberData(nameof(Data))]
	public void OutOfRange(UIntPtr size)
	{
		using RawMemory memory = new(size);
		var get = () => memory[size + 1];
		get.Should().ThrowExactly<ArgumentOutOfRangeException>();
	}

	public static IEnumerable<Object[]> Data => data.Select(x => new Object[] {x});

	private static readonly UIntPtr[] data =
	{
		1,
		255,
		UInt32.MaxValue,
		(UIntPtr) ((UInt64) UInt32.MaxValue * 2)
	};
}
