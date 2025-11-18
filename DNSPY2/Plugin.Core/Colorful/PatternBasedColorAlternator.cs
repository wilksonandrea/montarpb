using System;
using System.Drawing;
using System.Linq;

namespace Plugin.Core.Colorful
{
	// Token: 0x0200010A RID: 266
	public sealed class PatternBasedColorAlternator<T> : ColorAlternator, IPrototypable<PatternBasedColorAlternator<T>>
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x00007C6F File Offset: 0x00005E6F
		public PatternBasedColorAlternator(PatternCollection<T> patternCollection_1, params Color[] color_1)
			: base(color_1)
		{
			this.patternCollection_0 = patternCollection_1;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00007C86 File Offset: 0x00005E86
		public new PatternBasedColorAlternator<T> Prototype()
		{
			return new PatternBasedColorAlternator<T>(this.patternCollection_0.Prototype(), base.Colors.smethod_1<Color>().ToArray<Color>());
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00007CA8 File Offset: 0x00005EA8
		protected override ColorAlternator PrototypeCore()
		{
			return this.Prototype();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00022068 File Offset: 0x00020268
		public override Color GetNextColor(string input)
		{
			if (base.Colors.Length == 0)
			{
				throw new InvalidOperationException("No colors have been supplied over which to alternate!");
			}
			if (this.bool_0)
			{
				this.bool_0 = false;
				return base.Colors[this.nextColorIndex];
			}
			if (this.patternCollection_0.MatchFound(input))
			{
				this.TryIncrementColorIndex();
			}
			return base.Colors[this.nextColorIndex];
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00007CB0 File Offset: 0x00005EB0
		protected override void TryIncrementColorIndex()
		{
			if (this.nextColorIndex >= base.Colors.Length - 1)
			{
				this.nextColorIndex = 0;
				return;
			}
			this.nextColorIndex++;
		}

		// Token: 0x04000719 RID: 1817
		private PatternCollection<T> patternCollection_0;

		// Token: 0x0400071A RID: 1818
		private bool bool_0 = true;
	}
}
