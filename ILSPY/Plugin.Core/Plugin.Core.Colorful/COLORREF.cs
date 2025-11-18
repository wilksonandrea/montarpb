using System.Drawing;

namespace Plugin.Core.Colorful;

public struct COLORREF
{
	private uint uint_0;

	internal COLORREF(Color color_0)
	{
		uint_0 = (uint)(color_0.R + (color_0.G << 8) + (color_0.B << 16));
	}

	internal COLORREF(uint uint_1, uint uint_2, uint uint_3)
	{
		uint_0 = uint_1 + (uint_2 << 8) + (uint_3 << 16);
	}

	public override string ToString()
	{
		return uint_0.ToString();
	}
}
