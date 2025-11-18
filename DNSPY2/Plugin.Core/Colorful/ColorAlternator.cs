using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000E4 RID: 228
	public abstract class ColorAlternator : IPrototypable<ColorAlternator>
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000066C4 File Offset: 0x000048C4
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x000066CC File Offset: 0x000048CC
		public Color[] Colors
		{
			[CompilerGenerated]
			get
			{
				return this.color_0;
			}
			[CompilerGenerated]
			set
			{
				this.color_0 = value;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000066D5 File Offset: 0x000048D5
		public ColorAlternator()
		{
			this.Colors = new Color[0];
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000066E9 File Offset: 0x000048E9
		public ColorAlternator(params Color[] color_1)
		{
			this.Colors = color_1;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000066F8 File Offset: 0x000048F8
		public ColorAlternator Prototype()
		{
			return this.PrototypeCore();
		}

		// Token: 0x0600080B RID: 2059
		protected abstract ColorAlternator PrototypeCore();

		// Token: 0x0600080C RID: 2060
		public abstract Color GetNextColor(string input);

		// Token: 0x0600080D RID: 2061
		protected abstract void TryIncrementColorIndex();

		// Token: 0x04000698 RID: 1688
		[CompilerGenerated]
		private Color[] color_0;

		// Token: 0x04000699 RID: 1689
		protected int nextColorIndex;
	}
}
