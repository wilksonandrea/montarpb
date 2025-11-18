using System;

namespace Plugin.Core.Colorful;

public sealed class ConsoleAccessException : Exception
{
	public ConsoleAccessException()
		: base($"Color conversion failed because a handle to the actual windows console was not found.")
	{
	}
}
