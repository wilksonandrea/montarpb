using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public abstract class ColorAlternator : IPrototypable<ColorAlternator>
	{
		protected int nextColorIndex;

		public Color[] Colors
		{
			get;
			set;
		}

		public ColorAlternator()
		{
			this.Colors = new Color[0];
		}

		public ColorAlternator(params Color[] color_1)
		{
			this.Colors = color_1;
		}

		public abstract Color GetNextColor(string input);

		public ColorAlternator Prototype()
		{
			return this.PrototypeCore();
		}

		protected abstract ColorAlternator PrototypeCore();

		protected abstract void TryIncrementColorIndex();
	}
}