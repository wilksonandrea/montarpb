using System;
using System.Collections.Concurrent;
using System.Drawing;

namespace Plugin.Core.Colorful
{
	public sealed class ColorManagerFactory
	{
		public ColorManagerFactory()
		{
		}

		public ColorManager GetManager(ColorStore colorStore, int maxColorChanges, int initialColorChangeCountValue, bool isInCompatibilityMode)
		{
			return new ColorManager(colorStore, new ColorMapper(), maxColorChanges, initialColorChangeCountValue, isInCompatibilityMode);
		}

		public ColorManager GetManager(ConcurrentDictionary<Color, ConsoleColor> colorMap, ConcurrentDictionary<ConsoleColor, Color> consoleColorMap, int maxColorChanges, int initialColorChangeCountValue, bool isInCompatibilityMode)
		{
			return new ColorManager(new ColorStore(colorMap, consoleColorMap), new ColorMapper(), maxColorChanges, initialColorChangeCountValue, isInCompatibilityMode);
		}
	}
}