using System;
using System.Drawing;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000F7 RID: 247
	public struct COLORREF
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x000077DD File Offset: 0x000059DD
		internal COLORREF(Color color_0)
		{
			this.uint_0 = (uint)((int)color_0.R + ((int)color_0.G << 8) + ((int)color_0.B << 16));
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00007801 File Offset: 0x00005A01
		internal COLORREF(uint uint_1, uint uint_2, uint uint_3)
		{
			this.uint_0 = uint_1 + (uint_2 << 8) + (uint_3 << 16);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00007813 File Offset: 0x00005A13
		public override string ToString()
		{
			return this.uint_0.ToString();
		}

		// Token: 0x040006EB RID: 1771
		private uint uint_0;
	}
}
