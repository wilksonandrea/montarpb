using System;
using System.Drawing;
using System.Linq;

namespace Plugin.Core.Colorful
{
	public sealed class PatternBasedColorAlternator<T> : ColorAlternator, IPrototypable<PatternBasedColorAlternator<T>>
	{
		private PatternCollection<T> patternCollection_0;

		private bool bool_0;

		public PatternBasedColorAlternator(PatternCollection<T> patternCollection_1, params Color[] color_1) : base(color_1)
		{
			this.patternCollection_0 = patternCollection_1;
		}

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

		public new PatternBasedColorAlternator<T> Prototype()
		{
			return new PatternBasedColorAlternator<T>(this.patternCollection_0.Prototype(), Class23.smethod_1<Color>(base.Colors).ToArray<Color>());
		}

		protected override ColorAlternator PrototypeCore()
		{
			return this.Prototype();
		}

		protected override void TryIncrementColorIndex()
		{
			if (this.nextColorIndex >= (int)base.Colors.Length - 1)
			{
				this.nextColorIndex = 0;
				return;
			}
			this.nextColorIndex++;
		}
	}
}