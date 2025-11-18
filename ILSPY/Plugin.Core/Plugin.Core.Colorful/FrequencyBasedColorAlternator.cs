using System;
using System.Drawing;
using System.Linq;

namespace Plugin.Core.Colorful;

public sealed class FrequencyBasedColorAlternator : ColorAlternator, IPrototypable<FrequencyBasedColorAlternator>
{
	private int int_0;

	private int int_1;

	public FrequencyBasedColorAlternator(int int_2, params Color[] color_1)
		: base(color_1)
	{
		int_0 = int_2;
	}

	public new FrequencyBasedColorAlternator Prototype()
	{
		return new FrequencyBasedColorAlternator(int_0, base.Colors.smethod_1().ToArray());
	}

	protected override ColorAlternator PrototypeCore()
	{
		return Prototype();
	}

	public override Color GetNextColor(string input)
	{
		if (base.Colors.Length == 0)
		{
			throw new InvalidOperationException("No colors have been supplied over which to alternate!");
		}
		Color result = base.Colors[nextColorIndex];
		TryIncrementColorIndex();
		return result;
	}

	protected override void TryIncrementColorIndex()
	{
		if (int_1 >= base.Colors.Length * int_0 - 1)
		{
			nextColorIndex = 0;
			int_1 = 0;
		}
		else
		{
			int_1++;
			nextColorIndex = (int)Math.Floor((double)int_1 / (double)int_0);
		}
	}
}
