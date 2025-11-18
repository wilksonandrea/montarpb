using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public sealed class ColorStore
	{
		public ConcurrentDictionary<Color, ConsoleColor> Colors
		{
			get;
			private set;
		}

		public ConcurrentDictionary<ConsoleColor, Color> ConsoleColors
		{
			get;
			private set;
		}

		public ColorStore(ConcurrentDictionary<Color, ConsoleColor> concurrentDictionary_2, ConcurrentDictionary<ConsoleColor, Color> concurrentDictionary_3)
		{
			this.Colors = concurrentDictionary_2;
			this.ConsoleColors = concurrentDictionary_3;
		}

		public ConsoleColor Replace(Color oldColor, Color newColor)
		{
			ConsoleColor consoleColor;
			if (!this.Colors.TryRemove(oldColor, out consoleColor))
			{
				throw new ArgumentException("An attempt was made to replace a nonexistent color in the ColorStore!");
			}
			this.Colors.TryAdd(newColor, consoleColor);
			this.ConsoleColors[consoleColor] = newColor;
			return consoleColor;
		}

		public bool RequiresUpdate(Color color)
		{
			return !this.Colors.ContainsKey(color);
		}

		public void Update(ConsoleColor oldColor, Color newColor)
		{
			this.Colors.TryAdd(newColor, oldColor);
			this.ConsoleColors[oldColor] = newColor;
		}
	}
}