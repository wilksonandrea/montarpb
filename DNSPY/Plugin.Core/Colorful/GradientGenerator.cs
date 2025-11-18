using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000104 RID: 260
	public sealed class GradientGenerator
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x00021D94 File Offset: 0x0001FF94
		public List<StyleClass<T>> GenerateGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient)
		{
			GradientGenerator.Class29<T> @class = new GradientGenerator.Class29<T>();
			@class.int_1 = maxColorsInGradient;
			List<T> list = input.ToList<T>();
			@class.int_0 = list.Count / @class.int_1;
			int num = list.Count % @class.int_1;
			List<StyleClass<T>> list2 = new List<StyleClass<T>>();
			Color color = Color.Empty;
			T t = default(T);
			Func<int, int> func = new Func<int, int>(GradientGenerator.Class28<T>.<>9.method_0);
			Func<int, int> func2 = new Func<int, int>(GradientGenerator.Class28<T>.<>9.method_1);
			int num2 = func(num);
			int num3 = 0;
			@class.func_0 = new Func<int, bool>(GradientGenerator.Class28<T>.<>9.method_2);
			Func<int, int, T, T, bool> func3 = new Func<int, int, T, T, bool>(@class.method_0);
			Func<int, bool> func4 = new Func<int, bool>(@class.method_1);
			for (int i = 0; i < list.Count; i++)
			{
				T t2 = list[i];
				num2++;
				if (func3(i, num2, t2, t) && func4(num3))
				{
					color = this.method_0(i, startColor, endColor, list.Count);
					t = t2;
					num2 = func2(num2);
					num3++;
				}
				list2.Add(new StyleClass<T>(t2, color));
			}
			return list2;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00021EF0 File Offset: 0x000200F0
		private Color method_0(int int_0, Color color_0, Color color_1, int int_1)
		{
			int num = int_1 - 1;
			int num2 = (int)(color_0.R - color_1.R);
			int num3 = (int)(color_0.G - color_1.G);
			int num4 = (int)(color_0.B - color_1.B);
			int num5 = (int)((double)color_0.R + (double)(-(double)num2) * ((double)int_0 / (double)num));
			double num6 = (double)color_0.G + (double)(-(double)num3) * ((double)int_0 / (double)num);
			double num7 = (double)color_0.B + (double)(-(double)num4) * ((double)int_0 / (double)num);
			return Color.FromArgb(num5, (int)num6, (int)num7);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00002116 File Offset: 0x00000316
		public GradientGenerator()
		{
		}

		// Token: 0x02000105 RID: 261
		[CompilerGenerated]
		[Serializable]
		private sealed class Class28<T>
		{
			// Token: 0x060009A2 RID: 2466 RVA: 0x00007B64 File Offset: 0x00005D64
			// Note: this type is marked as 'beforefieldinit'.
			static Class28()
			{
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x00002116 File Offset: 0x00000316
			public Class28()
			{
			}

			// Token: 0x060009A4 RID: 2468 RVA: 0x00007B70 File Offset: 0x00005D70
			internal int method_0(int int_0)
			{
				if (int_0 <= 1)
				{
					return 0;
				}
				return -1;
			}

			// Token: 0x060009A5 RID: 2469 RVA: 0x00007B79 File Offset: 0x00005D79
			internal int method_1(int int_0)
			{
				if (int_0 != 0)
				{
					return 0;
				}
				return -1;
			}

			// Token: 0x060009A6 RID: 2470 RVA: 0x00007B81 File Offset: 0x00005D81
			internal bool method_2(int int_0)
			{
				return int_0 == 0;
			}

			// Token: 0x0400070F RID: 1807
			public static readonly GradientGenerator.Class28<T> <>9 = new GradientGenerator.Class28<T>();

			// Token: 0x04000710 RID: 1808
			public static Func<int, int> <>9__0_0;

			// Token: 0x04000711 RID: 1809
			public static Func<int, int> <>9__0_1;

			// Token: 0x04000712 RID: 1810
			public static Func<int, bool> <>9__0_2;
		}

		// Token: 0x02000106 RID: 262
		[CompilerGenerated]
		private sealed class Class29<T>
		{
			// Token: 0x060009A7 RID: 2471 RVA: 0x00002116 File Offset: 0x00000316
			public Class29()
			{
			}

			// Token: 0x060009A8 RID: 2472 RVA: 0x00007B87 File Offset: 0x00005D87
			internal bool method_0(int int_2, int int_3, T gparam_0, T gparam_1)
			{
				return (int_3 > this.int_0 - 1 && !gparam_0.Equals(gparam_1)) || this.func_0(int_2);
			}

			// Token: 0x060009A9 RID: 2473 RVA: 0x00007BB8 File Offset: 0x00005DB8
			internal bool method_1(int int_2)
			{
				return int_2 < this.int_1;
			}

			// Token: 0x04000713 RID: 1811
			public int int_0;

			// Token: 0x04000714 RID: 1812
			public Func<int, bool> func_0;

			// Token: 0x04000715 RID: 1813
			public int int_1;
		}
	}
}
