using System;
using System.Drawing;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000102 RID: 258
	public sealed class Formatter
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00007AD3 File Offset: 0x00005CD3
		public object Target
		{
			get
			{
				return this.styleClass_0.Target;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00007AE0 File Offset: 0x00005CE0
		public Color Color
		{
			get
			{
				return this.styleClass_0.Color;
			}
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00007AED File Offset: 0x00005CED
		public Formatter(object object_0, Color color_0)
		{
			this.styleClass_0 = new StyleClass<object>(object_0, color_0);
		}

		// Token: 0x0400070C RID: 1804
		private StyleClass<object> styleClass_0;
	}
}
