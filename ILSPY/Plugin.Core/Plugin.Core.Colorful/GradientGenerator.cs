using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public sealed class GradientGenerator
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class28<T>
	{
		public static readonly Class28<T> _003C_003E9 = new Class28<T>();

		public static Func<int, int> _003C_003E9__0_0;

		public static Func<int, int> _003C_003E9__0_1;

		public static Func<int, bool> _003C_003E9__0_2;

		internal int method_0(int int_0)
		{
			if (int_0 <= 1)
			{
				return 0;
			}
			return -1;
		}

		internal int method_1(int int_0)
		{
			if (int_0 != 0)
			{
				return 0;
			}
			return -1;
		}

		internal bool method_2(int int_0)
		{
			return int_0 == 0;
		}
	}

	[CompilerGenerated]
	private sealed class Class29<T>
	{
		public int int_0;

		public Func<int, bool> func_0;

		public int int_1;

		internal bool method_0(int int_2, int int_3, T gparam_0, T gparam_1)
		{
			if (int_3 > int_0 - 1 && !gparam_0.Equals(gparam_1))
			{
				return true;
			}
			return func_0(int_2);
		}

		internal bool method_1(int int_2)
		{
			return int_2 < int_1;
		}
	}

	public List<StyleClass<T>> GenerateGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient)
	{
		List<T> list = input.ToList();
		int int_4 = list.Count / maxColorsInGradient;
		int arg = list.Count % maxColorsInGradient;
		List<StyleClass<T>> list2 = new List<StyleClass<T>>();
		Color color_ = Color.Empty;
		T arg2 = default(T);
		Func<int, int> obj = (int int_0) => (int_0 > 1) ? (-1) : 0;
		Func<int, int> func = (int int_0) => (int_0 == 0) ? (-1) : 0;
		int num = obj(arg);
		int num2 = 0;
		Func<int, bool> func_0 = (int int_0) => int_0 == 0;
		Func<int, int, T, T, bool> func2 = (int int_2, int int_3, T gparam_0, T gparam_1) => (int_3 > int_4 - 1 && !gparam_0.Equals(gparam_1)) || func_0(int_2);
		Func<int, bool> func3 = (int int_2) => int_2 < maxColorsInGradient;
		for (int i = 0; i < list.Count; i++)
		{
			T val = list[i];
			num++;
			if (func2(i, num, val, arg2) && func3(num2))
			{
				color_ = method_0(i, startColor, endColor, list.Count);
				arg2 = val;
				num = func(num);
				num2++;
			}
			list2.Add(new StyleClass<T>(val, color_));
		}
		return list2;
	}

	private Color method_0(int int_0, Color color_0, Color color_1, int int_1)
	{
		int num = int_1 - 1;
		int num2 = color_0.R - color_1.R;
		int num3 = color_0.G - color_1.G;
		int num4 = color_0.B - color_1.B;
		double num5 = (double)(int)color_0.R + (double)(-num2) * ((double)int_0 / (double)num);
		double num6 = (double)(int)color_0.G + (double)(-num3) * ((double)int_0 / (double)num);
		double num7 = (double)(int)color_0.B + (double)(-num4) * ((double)int_0 / (double)num);
		return Color.FromArgb((int)num5, (int)num6, (int)num7);
	}
}
