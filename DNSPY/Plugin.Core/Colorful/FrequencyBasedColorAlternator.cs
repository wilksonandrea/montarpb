using System;
using System.Drawing;
using System.Linq;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000103 RID: 259
	public sealed class FrequencyBasedColorAlternator : ColorAlternator, IPrototypable<FrequencyBasedColorAlternator>
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x00007B02 File Offset: 0x00005D02
		public FrequencyBasedColorAlternator(int int_2, params Color[] color_1)
			: base(color_1)
		{
			this.int_0 = int_2;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00007B12 File Offset: 0x00005D12
		public new FrequencyBasedColorAlternator Prototype()
		{
			return new FrequencyBasedColorAlternator(this.int_0, base.Colors.smethod_1<Color>().ToArray<Color>());
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00007B2F File Offset: 0x00005D2F
		protected override ColorAlternator PrototypeCore()
		{
			return this.Prototype();
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00007B37 File Offset: 0x00005D37
		public override Color GetNextColor(string input)
		{
			if (base.Colors.Length == 0)
			{
				throw new InvalidOperationException("No colors have been supplied over which to alternate!");
			}
			Color color = base.Colors[this.nextColorIndex];
			this.TryIncrementColorIndex();
			return color;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00021D34 File Offset: 0x0001FF34
		protected override void TryIncrementColorIndex()
		{
			if (this.int_1 >= base.Colors.Length * this.int_0 - 1)
			{
				this.nextColorIndex = 0;
				this.int_1 = 0;
				return;
			}
			this.int_1++;
			this.nextColorIndex = (int)Math.Floor((double)this.int_1 / (double)this.int_0);
		}

		// Token: 0x0400070D RID: 1805
		private int int_0;

		// Token: 0x0400070E RID: 1806
		private int int_1;
	}
}
