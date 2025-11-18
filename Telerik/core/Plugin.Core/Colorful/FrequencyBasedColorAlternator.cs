using System;
using System.Drawing;
using System.Linq;

namespace Plugin.Core.Colorful
{
	public sealed class FrequencyBasedColorAlternator : ColorAlternator, IPrototypable<FrequencyBasedColorAlternator>
	{
		private int int_0;

		private int int_1;

		public FrequencyBasedColorAlternator(int int_2, params Color[] color_1) : base(color_1)
		{
			this.int_0 = int_2;
		}

		public override Color GetNextColor(string input)
		{
			if (base.Colors.Length == 0)
			{
				throw new InvalidOperationException("No colors have been supplied over which to alternate!");
			}
			Color colors = base.Colors[this.nextColorIndex];
			this.TryIncrementColorIndex();
			return colors;
		}

		public new FrequencyBasedColorAlternator Prototype()
		{
			return new FrequencyBasedColorAlternator(this.int_0, Class23.smethod_1<Color>(base.Colors).ToArray<Color>());
		}

		protected override ColorAlternator PrototypeCore()
		{
			return this.Prototype();
		}

		protected override void TryIncrementColorIndex()
		{
			if (this.int_1 >= (int)base.Colors.Length * this.int_0 - 1)
			{
				this.nextColorIndex = 0;
				this.int_1 = 0;
				return;
			}
			this.int_1++;
			this.nextColorIndex = (int)Math.Floor((double)this.int_1 / (double)this.int_0);
		}
	}
}