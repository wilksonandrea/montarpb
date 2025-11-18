using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public abstract class ColorAlternator : IPrototypable<ColorAlternator>
{
	[CompilerGenerated]
	private Color[] color_0;

	protected int nextColorIndex;

	public Color[] Colors
	{
		[CompilerGenerated]
		get
		{
			return color_0;
		}
		[CompilerGenerated]
		set
		{
			color_0 = value;
		}
	}

	public ColorAlternator()
	{
		Colors = new Color[0];
	}

	public ColorAlternator(params Color[] color_1)
	{
		Colors = color_1;
	}

	public ColorAlternator Prototype()
	{
		return PrototypeCore();
	}

	protected abstract ColorAlternator PrototypeCore();

	public abstract Color GetNextColor(string input);

	protected abstract void TryIncrementColorIndex();
}
