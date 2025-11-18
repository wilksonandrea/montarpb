namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public sealed class GradientGenerator
    {
        public List<StyleClass<T>> GenerateGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient)
        {
            Class29<T> class2 = new Class29<T> {
                int_1 = maxColorsInGradient
            };
            List<T> list = input.ToList<T>();
            class2.int_0 = list.Count / class2.int_1;
            int arg = list.Count % class2.int_1;
            List<StyleClass<T>> list2 = new List<StyleClass<T>>();
            Color empty = Color.Empty;
            T local = default(T);
            Func<int, int> func = Class28<T>.<>9__0_1 ??= new Func<int, int>(Class28<T>.<>9.method_1);
            int num2 = (Class28<T>.<>9__0_0 ??= new Func<int, int>(Class28<T>.<>9.method_0))(arg);
            int num3 = 0;
            Func<int, bool> func1 = Class28<T>.<>9__0_2;
            if (Class28<T>.<>9__0_2 == null)
            {
                Func<int, bool> local4 = Class28<T>.<>9__0_2;
                func1 = Class28<T>.<>9__0_2 = new Func<int, bool>(Class28<T>.<>9.method_2);
            }
            class2.func_0 = func1;
            Func<int, int, T, T, bool> func2 = new Func<int, int, T, T, bool>(class2.method_0);
            Func<int, bool> func3 = new Func<int, bool>(class2.method_1);
            for (int i = 0; i < list.Count; i++)
            {
                T local2 = list[i];
                num2++;
                if (func2(i, num2, local2, local) && func3(num3))
                {
                    empty = this.method_0(i, startColor, endColor, list.Count);
                    local = local2;
                    num2 = func(num2);
                    num3++;
                }
                list2.Add(new StyleClass<T>(local2, empty));
            }
            return list2;
        }

        private Color method_0(int int_0, Color color_0, Color color_1, int int_1)
        {
            int num = int_1 - 1;
            int num2 = color_0.R - color_1.R;
            int num3 = color_0.G - color_1.G;
            int num4 = color_0.B - color_1.B;
            double num5 = color_0.G + (-num3 * (((double) int_0) / ((double) num)));
            double num6 = color_0.B + (-num4 * (((double) int_0) / ((double) num)));
            return Color.FromArgb((int) (color_0.R + (-num2 * (((double) int_0) / ((double) num)))), (int) num5, (int) num6);
        }

        [Serializable, CompilerGenerated]
        private sealed class Class28<T>
        {
            public static readonly GradientGenerator.Class28<T> <>9;
            public static Func<int, int> <>9__0_0;
            public static Func<int, int> <>9__0_1;
            public static Func<int, bool> <>9__0_2;

            static Class28()
            {
                GradientGenerator.Class28<T>.<>9 = new GradientGenerator.Class28<T>();
            }

            internal int method_0(int int_0) => 
                (int_0 > 1) ? -1 : 0;

            internal int method_1(int int_0) => 
                (int_0 == 0) ? -1 : 0;

            internal bool method_2(int int_0) => 
                int_0 == 0;
        }

        [CompilerGenerated]
        private sealed class Class29<T>
        {
            public int int_0;
            public Func<int, bool> func_0;
            public int int_1;

            internal bool method_0(int int_2, int int_3, T gparam_0, T gparam_1) => 
                ((int_3 <= (this.int_0 - 1)) || gparam_0.Equals(gparam_1)) ? this.func_0(int_2) : true;

            internal bool method_1(int int_2) => 
                int_2 < this.int_1;
        }
    }
}

