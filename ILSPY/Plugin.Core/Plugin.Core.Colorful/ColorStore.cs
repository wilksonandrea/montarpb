using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public sealed class ColorStore
{
	[CompilerGenerated]
	private ConcurrentDictionary<Color, ConsoleColor> concurrentDictionary_0;

	[CompilerGenerated]
	private ConcurrentDictionary<ConsoleColor, Color> concurrentDictionary_1;

	public ConcurrentDictionary<Color, ConsoleColor> Colors
	{
		[CompilerGenerated]
		get
		{
			return concurrentDictionary_0;
		}
		[CompilerGenerated]
		private set
		{
			concurrentDictionary_0 = value;
		}
	}

	public ConcurrentDictionary<ConsoleColor, Color> ConsoleColors
	{
		[CompilerGenerated]
		get
		{
			return concurrentDictionary_1;
		}
		[CompilerGenerated]
		private set
		{
			concurrentDictionary_1 = value;
		}
	}

	public ColorStore(ConcurrentDictionary<Color, ConsoleColor> concurrentDictionary_2, ConcurrentDictionary<ConsoleColor, Color> concurrentDictionary_3)
	{
		Colors = concurrentDictionary_2;
		ConsoleColors = concurrentDictionary_3;
	}

	public void Update(ConsoleColor oldColor, Color newColor)
	{
		Colors.TryAdd(newColor, oldColor);
		ConsoleColors[oldColor] = newColor;
	}

	public ConsoleColor Replace(Color oldColor, Color newColor)
	{
		if (!Colors.TryRemove(oldColor, out var value))
		{
			throw new ArgumentException("An attempt was made to replace a nonexistent color in the ColorStore!");
		}
		Colors.TryAdd(newColor, value);
		ConsoleColors[value] = newColor;
		return value;
	}

	public bool RequiresUpdate(Color color)
	{
		return !Colors.ContainsKey(color);
	}
}
