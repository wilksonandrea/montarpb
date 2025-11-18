using System;
using System.Collections.Concurrent;
using System.Drawing;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000F1 RID: 241
	public sealed class ColorManagerFactory
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x00002116 File Offset: 0x00000316
		public ColorManagerFactory()
		{
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00007735 File Offset: 0x00005935
		public ColorManager GetManager(ColorStore colorStore, int maxColorChanges, int initialColorChangeCountValue, bool isInCompatibilityMode)
		{
			return new ColorManager(colorStore, new ColorMapper(), maxColorChanges, initialColorChangeCountValue, isInCompatibilityMode);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00020FE4 File Offset: 0x0001F1E4
		public ColorManager GetManager(ConcurrentDictionary<Color, ConsoleColor> colorMap, ConcurrentDictionary<ConsoleColor, Color> consoleColorMap, int maxColorChanges, int initialColorChangeCountValue, bool isInCompatibilityMode)
		{
			ColorStore colorStore = new ColorStore(colorMap, consoleColorMap);
			ColorMapper colorMapper = new ColorMapper();
			return new ColorManager(colorStore, colorMapper, maxColorChanges, initialColorChangeCountValue, isInCompatibilityMode);
		}
	}
}
