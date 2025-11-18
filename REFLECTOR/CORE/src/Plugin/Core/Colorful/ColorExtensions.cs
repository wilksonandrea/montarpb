namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public static class ColorExtensions
    {
        public static ConsoleColor ToNearestConsoleColor(this Color color)
        {
            ConsoleColor color5;
            ConsoleColor black = ConsoleColor.Black;
            double maxValue = double.MaxValue;
            using (IEnumerator enumerator = Enum.GetValues(typeof(ConsoleColor)).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        ConsoleColor current = (ConsoleColor) enumerator.Current;
                        string name = Enum.GetName(typeof(ConsoleColor), current);
                        Color color4 = Color.FromName(string.Equals(name, "DarkYellow", StringComparison.Ordinal) ? "Orange" : name);
                        double num2 = (Math.Pow((double) (color4.R - color.R), 2.0) + Math.Pow((double) (color4.G - color.G), 2.0)) + Math.Pow((double) (color4.B - color.B), 2.0);
                        if (num2 != 0.0)
                        {
                            if (num2 >= maxValue)
                            {
                                continue;
                            }
                            maxValue = num2;
                            black = current;
                            continue;
                        }
                        color5 = current;
                    }
                    else
                    {
                        return black;
                    }
                    break;
                }
            }
            return color5;
        }
    }
}

