using System;
using System.Drawing;

namespace Plugin.Core.Colorful;

public static class ColorExtensions
{
	public static ConsoleColor ToNearestConsoleColor(this Color color)
	{
		ConsoleColor result = ConsoleColor.Black;
		double num = double.MaxValue;
		foreach (ConsoleColor value in Enum.GetValues(typeof(ConsoleColor)))
		{
			string name = Enum.GetName(typeof(ConsoleColor), value);
			name = (string.Equals(name, "DarkYellow", StringComparison.Ordinal) ? "Orange" : name);
			Color color2 = Color.FromName(name);
			double num2 = Math.Pow(color2.R - color.R, 2.0) + Math.Pow(color2.G - color.G, 2.0) + Math.Pow(color2.B - color.B, 2.0);
			if (num2 != 0.0)
			{
				if (num2 < num)
				{
					num = num2;
					result = value;
				}
				continue;
			}
			return value;
		}
		return result;
	}
}
