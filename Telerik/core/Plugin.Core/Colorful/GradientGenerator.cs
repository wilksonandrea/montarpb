using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public sealed class GradientGenerator
	{
		public GradientGenerator()
		{
		}

		public List<StyleClass<T>> GenerateGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient)
		{
			List<T> list = input.ToList<T>();
			int count = list.Count / maxColorsInGradient;
			int ınt32 = list.Count % maxColorsInGradient;
			List<StyleClass<T>> styleClasses = new List<StyleClass<T>>();
			Color empty = Color.Empty;
			T t = default(T);
			Func<int, int> int0 = (int int_0) => {
				if (int_0 != 0)
				{
					return 0;
				}
				return -1;
			};
			int int01 = new Func<int, int>((int int_0) => {
				if (int_0 <= 1)
				{
					return 0;
				}
				return -1;
			})(ınt32);
			int ınt321 = 0;
			Func<int, bool> func = (int int_0) => int_0 == 0;
			Func<int, int, T, T, bool> int3 = (int int_2, int int_3, T gparam_0, T gparam_1) => {
				if (int_3 > count - 1 && !gparam_0.Equals(gparam_1))
				{
					return true;
				}
				return func(int_2);
			};
			Func<int, bool> int2 = (int int_2) => int_2 < maxColorsInGradient;
			for (int i = 0; i < list.Count; i++)
			{
				T ıtem = list[i];
				int01++;
				if (int3(i, int01, ıtem, t) && int2(ınt321))
				{
					empty = this.method_0(i, startColor, endColor, list.Count);
					t = ıtem;
					int01 = int0(int01);
					ınt321++;
				}
				styleClasses.Add(new StyleClass<T>(ıtem, empty));
			}
			return styleClasses;
		}

		private Color method_0(int int_0, Color color_0, Color color_1, int int_1)
		{
			int int1 = int_1 - 1;
			int r = color_0.R - color_1.R;
			int g = color_0.G - color_1.G;
			int b = color_0.B - color_1.B;
			double num = (double)color_0.R + (double)(-r) * ((double)int_0 / (double)int1);
			double g1 = (double)color_0.G + (double)(-g) * ((double)int_0 / (double)int1);
			double b1 = (double)color_0.B + (double)(-b) * ((double)int_0 / (double)int1);
			return Color.FromArgb((int)num, (int)g1, (int)b1);
		}
	}
}