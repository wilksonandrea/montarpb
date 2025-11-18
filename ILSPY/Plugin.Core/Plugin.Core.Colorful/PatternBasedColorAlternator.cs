using System;
using System.Drawing;
using System.Linq;

namespace Plugin.Core.Colorful;

public sealed class PatternBasedColorAlternator<T> : ColorAlternator, IPrototypable<PatternBasedColorAlternator<T>>
{
	private PatternCollection<T> patternCollection_0;

	private bool bool_0 = true;

	public PatternBasedColorAlternator(PatternCollection<T> patternCollection_1, params Color[] color_1)
		: base(color_1)
	{
		patternCollection_0 = patternCollection_1;
	}

	public new PatternBasedColorAlternator<T> Prototype()
	{
		return new PatternBasedColorAlternator<T>(patternCollection_0.Prototype(), base.Colors.smethod_1().ToArray());
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
		if (bool_0)
		{
			bool_0 = false;
			return base.Colors[nextColorIndex];
		}
		if (patternCollection_0.MatchFound(input))
		{
			TryIncrementColorIndex();
		}
		return base.Colors[nextColorIndex];
	}

	protected override void TryIncrementColorIndex()
	{
		if (nextColorIndex >= base.Colors.Length - 1)
		{
			nextColorIndex = 0;
		}
		else
		{
			nextColorIndex++;
		}
	}
}
