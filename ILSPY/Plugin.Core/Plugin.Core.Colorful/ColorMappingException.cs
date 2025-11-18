using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public sealed class ColorMappingException : Exception
{
	[CompilerGenerated]
	private int int_0;

	public int ErrorCode
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		private set
		{
			int_0 = value;
		}
	}

	public ColorMappingException(int int_1)
		: base($"Color conversion failed with system error code {int_1}!")
	{
		ErrorCode = int_1;
	}
}
