using System;
using System.Drawing;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000E5 RID: 229
	public sealed class ColorAlternatorFactory
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x00002116 File Offset: 0x00000316
		public ColorAlternatorFactory()
		{
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00006700 File Offset: 0x00004900
		public ColorAlternator GetAlternator(string[] patterns, params Color[] colors)
		{
			return new PatternBasedColorAlternator<string>(new TextPatternCollection(patterns), colors);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0000670E File Offset: 0x0000490E
		public ColorAlternator GetAlternator(int frequency, params Color[] colors)
		{
			return new FrequencyBasedColorAlternator(frequency, colors);
		}
	}
}
