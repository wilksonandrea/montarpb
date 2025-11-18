using System;
using System.Drawing;

namespace Plugin.Core.Utility
{
	// Token: 0x02000028 RID: 40
	public class ColorUtil
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00002116 File Offset: 0x00000316
		public ColorUtil()
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00015454 File Offset: 0x00013654
		// Note: this type is marked as 'beforefieldinit'.
		static ColorUtil()
		{
		}

		// Token: 0x04000078 RID: 120
		public static Color White = Color.FromArgb(255, ColorTranslator.FromHtml("#FFFFFF"));

		// Token: 0x04000079 RID: 121
		public static Color Black = Color.FromArgb(255, ColorTranslator.FromHtml("#000000"));

		// Token: 0x0400007A RID: 122
		public static Color Red = Color.FromArgb(255, ColorTranslator.FromHtml("#FF0000"));

		// Token: 0x0400007B RID: 123
		public static Color Green = Color.FromArgb(255, ColorTranslator.FromHtml("#00FF00"));

		// Token: 0x0400007C RID: 124
		public static Color Blue = Color.FromArgb(255, ColorTranslator.FromHtml("#0000FF"));

		// Token: 0x0400007D RID: 125
		public static Color Yellow = Color.FromArgb(255, ColorTranslator.FromHtml("#FFFF00"));

		// Token: 0x0400007E RID: 126
		public static Color Fuchsia = Color.FromArgb(255, ColorTranslator.FromHtml("#FF00FF"));

		// Token: 0x0400007F RID: 127
		public static Color Cyan = Color.FromArgb(255, ColorTranslator.FromHtml("#00FFFF"));

		// Token: 0x04000080 RID: 128
		public static Color Silver = Color.FromArgb(255, ColorTranslator.FromHtml("#C0C0C0"));

		// Token: 0x04000081 RID: 129
		public static Color LightGrey = Color.FromArgb(255, ColorTranslator.FromHtml("#D3D3D3"));
	}
}
