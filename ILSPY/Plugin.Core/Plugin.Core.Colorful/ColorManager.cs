using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public sealed class ColorManager
{
	[CompilerGenerated]
	private bool bool_0;

	private ColorMapper colorMapper_0;

	private ColorStore colorStore_0;

	private int int_0;

	private int int_1;

	public bool IsInCompatibilityMode
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		private set
		{
			bool_0 = value;
		}
	}

	public ColorManager(ColorStore colorStore_1, ColorMapper colorMapper_1, int int_2, int int_3, bool bool_1)
	{
		colorStore_0 = colorStore_1;
		colorMapper_0 = colorMapper_1;
		int_0 = int_3;
		int_1 = int_2;
		IsInCompatibilityMode = bool_1;
	}

	public Color GetColor(ConsoleColor color)
	{
		return colorStore_0.ConsoleColors[color];
	}

	public void ReplaceColor(Color oldColor, Color newColor)
	{
		if (!IsInCompatibilityMode)
		{
			ConsoleColor oldColor2 = colorStore_0.Replace(oldColor, newColor);
			colorMapper_0.MapColor(oldColor2, newColor);
		}
	}

	public ConsoleColor GetConsoleColor(Color color)
	{
		if (IsInCompatibilityMode)
		{
			return color.ToNearestConsoleColor();
		}
		try
		{
			return method_0(color);
		}
		catch
		{
			return color.ToNearestConsoleColor();
		}
	}

	private ConsoleColor method_0(Color color_0)
	{
		if (method_1() && colorStore_0.RequiresUpdate(color_0))
		{
			ConsoleColor oldColor = (ConsoleColor)int_0;
			colorMapper_0.MapColor(oldColor, color_0);
			colorStore_0.Update(oldColor, color_0);
			int_0++;
		}
		if (colorStore_0.Colors.ContainsKey(color_0))
		{
			return colorStore_0.Colors[color_0];
		}
		return colorStore_0.Colors.Last().Value;
	}

	private bool method_1()
	{
		return int_0 < int_1;
	}
}
