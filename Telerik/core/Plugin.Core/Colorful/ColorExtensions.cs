using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public static class ColorExtensions
	{
		public static ConsoleColor ToNearestConsoleColor(this Color color)
		{
			ConsoleColor consoleColor;
			ConsoleColor consoleColor1 = ConsoleColor.Black;
			double num = double.MaxValue;
			IEnumerator enumerator = Enum.GetValues(typeof(ConsoleColor)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ConsoleColor current = (ConsoleColor)enumerator.Current;
					string name = Enum.GetName(typeof(ConsoleColor), current);
					name = (string.Equals(name, "DarkYellow", StringComparison.Ordinal) ? "Orange" : name);
					Color color1 = Color.FromName(name);
					double num1 = Math.Pow((double)(color1.R - color.R), 2) + Math.Pow((double)(color1.G - color.G), 2) + Math.Pow((double)(color1.B - color.B), 2);
					if (num1 != 0)
					{
						if (num1 >= num)
						{
							continue;
						}
						num = num1;
						consoleColor1 = current;
					}
					else
					{
						consoleColor = current;
						return consoleColor;
					}
				}
				return consoleColor1;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return consoleColor;
		}
	}
}