using System;
using System.Drawing;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000E6 RID: 230
	public static class ColorExtensions
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		public static ConsoleColor ToNearestConsoleColor(this Color color)
		{
			ConsoleColor consoleColor = ConsoleColor.Black;
			double num = double.MaxValue;
			foreach (object obj in Enum.GetValues(typeof(ConsoleColor)))
			{
				ConsoleColor consoleColor2 = (ConsoleColor)obj;
				string text = Enum.GetName(typeof(ConsoleColor), consoleColor2);
				text = (string.Equals(text, "DarkYellow", StringComparison.Ordinal) ? "Orange" : text);
				Color color2 = Color.FromName(text);
				double num2 = Math.Pow((double)(color2.R - color.R), 2.0) + Math.Pow((double)(color2.G - color.G), 2.0) + Math.Pow((double)(color2.B - color.B), 2.0);
				if (num2 == 0.0)
				{
					return consoleColor2;
				}
				if (num2 < num)
				{
					num = num2;
					consoleColor = consoleColor2;
				}
			}
			return consoleColor;
		}
	}
}
