namespace Plugin.Core.Colorful
{
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class Console
    {
        private static ColorStore colorStore_0;
        private static ColorManagerFactory colorManagerFactory_0;
        private static ColorManager colorManager_0;
        private static Dictionary<string, COLORREF> dictionary_0;
        private const int int_0 = 0x10;
        private const int int_1 = 1;
        private static readonly string string_0 = "\r\n";
        private static readonly string string_1 = "";
        private static readonly Color color_0 = Color.FromArgb(0, 0, 0);
        private static readonly Color color_1 = Color.FromArgb(0, 0, 0xff);
        private static readonly Color color_2 = Color.FromArgb(0, 0xff, 0xff);
        private static readonly Color color_3 = Color.FromArgb(0, 0, 0x80);
        private static readonly Color color_4 = Color.FromArgb(0, 0x80, 0x80);
        private static readonly Color color_5 = Color.FromArgb(0x80, 0x80, 0x80);
        private static readonly Color color_6 = Color.FromArgb(0, 0x80, 0);
        private static readonly Color color_7 = Color.FromArgb(0x80, 0, 0x80);
        private static readonly Color color_8 = Color.FromArgb(0x80, 0, 0);
        private static readonly Color color_9 = Color.FromArgb(0x80, 0x80, 0);
        private static readonly Color color_10 = Color.FromArgb(0xc0, 0xc0, 0xc0);
        private static readonly Color color_11 = Color.FromArgb(0, 0xff, 0);
        private static readonly Color color_12 = Color.FromArgb(0xff, 0, 0xff);
        private static readonly Color color_13 = Color.FromArgb(0xff, 0, 0);
        private static readonly Color color_14 = Color.FromArgb(0xff, 0xff, 0xff);
        private static readonly Color color_15 = Color.FromArgb(0xff, 0xff, 0);

        public static event ConsoleCancelEventHandler CancelKeyPress;

        static Console()
        {
            bool flag = false;
            try
            {
                dictionary_0 = new ColorMapper().GetBufferColors();
            }
            catch
            {
                flag = true;
            }
            smethod_29(flag);
            System.Console.CancelKeyPress += new ConsoleCancelEventHandler(Plugin.Core.Colorful.Console.smethod_30);
        }

        public static void Beep(int frequency, int duration)
        {
            System.Console.Beep(frequency, duration);
        }

        public static void Clear()
        {
            System.Console.Clear();
        }

        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
        {
            System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
        }

        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
        {
            System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
        }

        public static Stream OpenStandardError() => 
            System.Console.OpenStandardError();

        public static Stream OpenStandardError(int bufferSize) => 
            System.Console.OpenStandardError(bufferSize);

        public static Stream OpenStandardInput() => 
            System.Console.OpenStandardInput();

        public static Stream OpenStandardInput(int bufferSize) => 
            System.Console.OpenStandardInput(bufferSize);

        public static Stream OpenStandardOutput() => 
            System.Console.OpenStandardOutput();

        public static Stream OpenStandardOutput(int bufferSize) => 
            System.Console.OpenStandardOutput(bufferSize);

        public static int Read() => 
            System.Console.Read();

        public static ConsoleKeyInfo ReadKey() => 
            System.Console.ReadKey();

        public static ConsoleKeyInfo ReadKey(bool intercept) => 
            System.Console.ReadKey(intercept);

        public static string ReadLine() => 
            System.Console.ReadLine();

        public static void ReplaceAllColorsWithDefaults()
        {
            colorStore_0 = smethod_28();
            colorManagerFactory_0 = new ColorManagerFactory();
            colorManager_0 = colorManagerFactory_0.GetManager(colorStore_0, 0x10, 1, colorManager_0.IsInCompatibilityMode);
            if (!colorManager_0.IsInCompatibilityMode)
            {
                new ColorMapper().SetBatchBufferColors(dictionary_0);
            }
        }

        public static void ReplaceColor(Color oldColor, Color newColor)
        {
            colorManager_0.ReplaceColor(oldColor, newColor);
        }

        public static void ResetColor()
        {
            System.Console.ResetColor();
        }

        public static void SetBufferSize(int width, int height)
        {
            System.Console.SetBufferSize(width, height);
        }

        public static void SetCursorPosition(int left, int top)
        {
            System.Console.SetCursorPosition(left, top);
        }

        public static void SetError(TextWriter newError)
        {
            System.Console.SetError(newError);
        }

        public static void SetIn(TextReader newIn)
        {
            System.Console.SetIn(newIn);
        }

        public static void SetOut(TextWriter newOut)
        {
            System.Console.SetOut(newOut);
        }

        public static void SetWindowPosition(int left, int top)
        {
            System.Console.SetWindowPosition(left, top);
        }

        public static void SetWindowSize(int width, int height)
        {
            System.Console.SetWindowSize(width, height);
        }

        private static void smethod_0(IEnumerable<KeyValuePair<string, Color>> ienumerable_0, string string_2)
        {
            Class17 class2 = new Class17 {
                ienumerable_0 = ienumerable_0,
                string_0 = string_2
            };
            TaskQueue_0.Enqueue(new Func<Task>(class2.method_0)).Wait();
        }

        private static void smethod_1(StyledString styledString_0, string string_2)
        {
            ConsoleColor foregroundColor = System.Console.ForegroundColor;
            int length = styledString_0.CharacterGeometry.GetLength(0);
            int num2 = styledString_0.CharacterGeometry.GetLength(1);
            int num3 = 0;
            while (num3 < length)
            {
                int num4 = 0;
                while (true)
                {
                    if (num4 >= num2)
                    {
                        num3++;
                        break;
                    }
                    System.Console.ForegroundColor = colorManager_0.GetConsoleColor(styledString_0.ColorGeometry[num3, num4]);
                    if ((num3 == (length - 1)) && (num4 == (num2 - 1)))
                    {
                        System.Console.Write(styledString_0.CharacterGeometry[num3, num4].ToString() + string_2);
                    }
                    else if (num4 == (num2 - 1))
                    {
                        System.Console.Write(styledString_0.CharacterGeometry[num3, num4].ToString() + "\r\n");
                    }
                    else
                    {
                        System.Console.Write(styledString_0.CharacterGeometry[num3, num4]);
                    }
                    num4++;
                }
            }
            System.Console.ForegroundColor = foregroundColor;
        }

        private static void smethod_10<T, U>(Action<T, U> action_0, T gparam_0, U gparam_1, Color color_16)
        {
            System.Console.ForegroundColor = colorManager_0.GetConsoleColor(color_16);
            action_0(gparam_0, gparam_1);
            System.Console.ForegroundColor = System.Console.ForegroundColor;
        }

        private static void smethod_11<T, U>(Action<T, U> action_0, T gparam_0, U gparam_1, ColorAlternator colorAlternator_0)
        {
            Class18<T, U>.callSite_1 ??= CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(Plugin.Core.Colorful.Console)));
            if (Class18<T, U>.callSite_0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                Class18<T, U>.callSite_0 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", null, typeof(Plugin.Core.Colorful.Console), argumentInfo));
            }
            string input = Class18<T, U>.callSite_1(Class18<T, U>.callSite_1.Target, Class18<T, U>.callSite_0.Target(Class18<T, U>.callSite_0, typeof(string), gparam_0.ToString(), gparam_1.smethod_3<U>()));
            Color nextColor = colorAlternator_0.GetNextColor(input);
            System.Console.ForegroundColor = colorManager_0.GetConsoleColor(nextColor);
            action_0(gparam_0, gparam_1);
            System.Console.ForegroundColor = System.Console.ForegroundColor;
        }

        private static void smethod_12<T, U>(string string_2, T gparam_0, U gparam_1, StyleSheet styleSheet_0)
        {
            TextAnnotator annotator = new TextAnnotator(styleSheet_0);
            Class19<T, U>.callSite_1 ??= CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(Plugin.Core.Colorful.Console)));
            if (Class19<T, U>.callSite_0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                Class19<T, U>.callSite_0 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", null, typeof(Plugin.Core.Colorful.Console), argumentInfo));
            }
            smethod_0(annotator.GetAnnotationMap(Class19<T, U>.callSite_1(Class19<T, U>.callSite_1.Target, Class19<T, U>.callSite_0.Target(Class19<T, U>.callSite_0, typeof(string), gparam_0.ToString(), gparam_1.smethod_3<U>()))), string_2);
        }

        private static void smethod_13<T, U>(string string_2, T gparam_0, U gparam_1, Color color_16, Color color_17)
        {
            TextFormatter formatter = new TextFormatter(color_17);
            Class20<T, U>.callSite_1 ??= CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(List<KeyValuePair<string, Color>>), typeof(Plugin.Core.Colorful.Console)));
            if (Class20<T, U>.callSite_0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                Class20<T, U>.callSite_0 = CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetFormatMap", null, typeof(Plugin.Core.Colorful.Console), argumentInfo));
            }
            Color[] colorArray1 = new Color[] { color_16 };
            smethod_0(Class20<T, U>.callSite_1(Class20<T, U>.callSite_1.Target, Class20<T, U>.callSite_0.Target(Class20<T, U>.callSite_0, formatter, gparam_0.ToString(), gparam_1.smethod_3<U>(), colorArray1)), string_2);
        }

        private static void smethod_14<T>(string string_2, T gparam_0, Formatter formatter_0, Color color_16)
        {
            object[] args = new object[] { formatter_0.Target };
            Color[] colors = new Color[] { formatter_0.Color };
            smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), args, colors), string_2);
        }

        private static void smethod_15<T, U>(Action<T, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, Color color_16)
        {
            System.Console.ForegroundColor = colorManager_0.GetConsoleColor(color_16);
            action_0(gparam_0, gparam_1, gparam_2);
            System.Console.ForegroundColor = System.Console.ForegroundColor;
        }

        private static void smethod_16<T, U>(Action<T, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, ColorAlternator colorAlternator_0)
        {
            string input = string.Format(gparam_0.ToString(), gparam_1, gparam_2);
            Color nextColor = colorAlternator_0.GetNextColor(input);
            System.Console.ForegroundColor = colorManager_0.GetConsoleColor(nextColor);
            action_0(gparam_0, gparam_1, gparam_2);
            System.Console.ForegroundColor = System.Console.ForegroundColor;
        }

        private static void smethod_17<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, StyleSheet styleSheet_0)
        {
            smethod_0(new TextAnnotator(styleSheet_0).GetAnnotationMap(string.Format(gparam_0.ToString(), gparam_1, gparam_2)), string_2);
        }

        private static void smethod_18<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, Color color_16, Color color_17)
        {
            TextFormatter formatter = new TextFormatter(color_17);
            Class21<T, U>.callSite_1 ??= CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(List<KeyValuePair<string, Color>>), typeof(Plugin.Core.Colorful.Console)));
            if (Class21<T, U>.callSite_0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                Class21<T, U>.callSite_0 = CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetFormatMap", null, typeof(Plugin.Core.Colorful.Console), argumentInfo));
            }
            U[] localArray1 = new U[] { gparam_1, gparam_2 };
            Color[] colorArray1 = new Color[] { color_16 };
            smethod_0(Class21<T, U>.callSite_1(Class21<T, U>.callSite_1.Target, Class21<T, U>.callSite_0.Target(Class21<T, U>.callSite_0, formatter, gparam_0.ToString(), localArray1.smethod_3<U[]>(), colorArray1)), string_2);
        }

        private static void smethod_19<T>(string string_2, T gparam_0, Formatter formatter_0, Formatter formatter_1, Color color_16)
        {
            object[] args = new object[] { formatter_0.Target, formatter_1.Target };
            Color[] colors = new Color[] { formatter_0.Color, formatter_1.Color };
            smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), args, colors), string_2);
        }

        private static void smethod_2<T>(Action<T> action_0, T gparam_0, Color color_16)
        {
            System.Console.ForegroundColor = colorManager_0.GetConsoleColor(color_16);
            action_0(gparam_0);
            System.Console.ForegroundColor = System.Console.ForegroundColor;
        }

        private static void smethod_20<T, U>(Action<T, U, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, U gparam_3, Color color_16)
        {
            System.Console.ForegroundColor = colorManager_0.GetConsoleColor(color_16);
            action_0(gparam_0, gparam_1, gparam_2, gparam_3);
            System.Console.ForegroundColor = System.Console.ForegroundColor;
        }

        private static void smethod_21<T, U>(Action<T, U, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, U gparam_3, ColorAlternator colorAlternator_0)
        {
            string input = string.Format(gparam_0.ToString(), gparam_1, gparam_2, gparam_3);
            Color nextColor = colorAlternator_0.GetNextColor(input);
            System.Console.ForegroundColor = colorManager_0.GetConsoleColor(nextColor);
            action_0(gparam_0, gparam_1, gparam_2, gparam_3);
            System.Console.ForegroundColor = System.Console.ForegroundColor;
        }

        private static void smethod_22<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, U gparam_3, StyleSheet styleSheet_0)
        {
            smethod_0(new TextAnnotator(styleSheet_0).GetAnnotationMap(string.Format(gparam_0.ToString(), gparam_1, gparam_2, gparam_3)), string_2);
        }

        private static void smethod_23<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, U gparam_3, Color color_16, Color color_17)
        {
            TextFormatter formatter = new TextFormatter(color_17);
            Class22<T, U>.callSite_1 ??= CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(List<KeyValuePair<string, Color>>), typeof(Plugin.Core.Colorful.Console)));
            if (Class22<T, U>.callSite_0 == null)
            {
                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                Class22<T, U>.callSite_0 = CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetFormatMap", null, typeof(Plugin.Core.Colorful.Console), argumentInfo));
            }
            U[] localArray1 = new U[] { gparam_1, gparam_2, gparam_3 };
            Color[] colorArray1 = new Color[] { color_16 };
            smethod_0(Class22<T, U>.callSite_1(Class22<T, U>.callSite_1.Target, Class22<T, U>.callSite_0.Target(Class22<T, U>.callSite_0, formatter, gparam_0.ToString(), localArray1.smethod_3<U[]>(), colorArray1)), string_2);
        }

        private static void smethod_24<T>(string string_2, T gparam_0, Formatter formatter_0, Formatter formatter_1, Formatter formatter_2, Color color_16)
        {
            object[] args = new object[] { formatter_0.Target, formatter_1.Target, formatter_2.Target };
            Color[] colors = new Color[] { formatter_0.Color, formatter_1.Color, formatter_2.Color };
            smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), args, colors), string_2);
        }

        private static void smethod_25<T>(string string_2, T gparam_0, Formatter[] formatter_0, Color color_16)
        {
            Func<Formatter, object> selector = Class16<T>.<>9__36_0;
            if (Class16<T>.<>9__36_0 == null)
            {
                Func<Formatter, object> local1 = Class16<T>.<>9__36_0;
                selector = Class16<T>.<>9__36_0 = new Func<Formatter, object>(Class16<T>.<>9.method_0);
            }
            smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), formatter_0.Select<Formatter, object>(selector).ToArray<object>(), formatter_0.Select<Formatter, Color>((Class16<T>.<>9__36_1 ??= new Func<Formatter, Color>(Class16<T>.<>9.method_1))).ToArray<Color>()), string_2);
        }

        private static void smethod_26<T>(Action<object, Color> action_0, IEnumerable<T> ienumerable_0, Color color_16, Color color_17, int int_2)
        {
            foreach (StyleClass<T> class2 in new GradientGenerator().GenerateGradient<T>(ienumerable_0, color_16, color_17, int_2))
            {
                action_0(class2.Target, class2.Color);
            }
        }

        private static Figlet smethod_27(FigletFont figletFont_0 = null) => 
            (figletFont_0 != null) ? new Figlet(figletFont_0) : new Figlet();

        private static ColorStore smethod_28()
        {
            ConcurrentDictionary<ConsoleColor, Color> dictionary = new ConcurrentDictionary<ConsoleColor, Color>();
            dictionary.TryAdd(ConsoleColor.Black, color_0);
            dictionary.TryAdd(ConsoleColor.Blue, color_1);
            dictionary.TryAdd(ConsoleColor.Cyan, color_2);
            dictionary.TryAdd(ConsoleColor.DarkBlue, color_3);
            dictionary.TryAdd(ConsoleColor.DarkCyan, color_4);
            dictionary.TryAdd(ConsoleColor.DarkGray, color_5);
            dictionary.TryAdd(ConsoleColor.DarkGreen, color_6);
            dictionary.TryAdd(ConsoleColor.DarkMagenta, color_7);
            dictionary.TryAdd(ConsoleColor.DarkRed, color_8);
            dictionary.TryAdd(ConsoleColor.DarkYellow, color_9);
            dictionary.TryAdd(ConsoleColor.Gray, color_10);
            dictionary.TryAdd(ConsoleColor.Green, color_11);
            dictionary.TryAdd(ConsoleColor.Magenta, color_12);
            dictionary.TryAdd(ConsoleColor.Red, color_13);
            dictionary.TryAdd(ConsoleColor.White, color_14);
            dictionary.TryAdd(ConsoleColor.Yellow, color_15);
            return new ColorStore(new ConcurrentDictionary<Color, ConsoleColor>(), dictionary);
        }

        private static void smethod_29(bool bool_0)
        {
            colorStore_0 = smethod_28();
            colorManagerFactory_0 = new ColorManagerFactory();
            colorManager_0 = colorManagerFactory_0.GetManager(colorStore_0, 0x10, 1, bool_0);
            if (!colorManager_0.IsInCompatibilityMode)
            {
                new ColorMapper().SetBatchBufferColors(dictionary_0);
            }
        }

        private static void smethod_3(Action<string> action_0, char[] char_0, int int_2, int int_3, Color color_16)
        {
            string str = char_0.smethod_2<char[]>().Substring(int_2, int_3);
            smethod_2<string>(action_0, str, color_16);
        }

        private static void smethod_30(object sender, ConsoleCancelEventArgs e)
        {
            consoleCancelEventHandler_0(sender, e);
        }

        private static void smethod_4<T>(Action<T> action_0, T gparam_0, ColorAlternator colorAlternator_0)
        {
            Color nextColor = colorAlternator_0.GetNextColor(gparam_0.smethod_2<T>());
            System.Console.ForegroundColor = colorManager_0.GetConsoleColor(nextColor);
            action_0(gparam_0);
            System.Console.ForegroundColor = System.Console.ForegroundColor;
        }

        private static void smethod_5(Action<string> action_0, char[] char_0, int int_2, int int_3, ColorAlternator colorAlternator_0)
        {
            string str = char_0.smethod_2<char[]>().Substring(int_2, int_3);
            smethod_4<string>(action_0, str, colorAlternator_0);
        }

        private static void smethod_6<T>(string string_2, T gparam_0, StyleSheet styleSheet_0)
        {
            smethod_0(new TextAnnotator(styleSheet_0).GetAnnotationMap(gparam_0.smethod_2<T>()), string_2);
        }

        private static void smethod_7(string string_2, StyledString styledString_0, StyleSheet styleSheet_0)
        {
            smethod_8(new TextAnnotator(styleSheet_0).GetAnnotationMap(styledString_0.AbstractValue), styledString_0);
            smethod_1(styledString_0, string_2);
        }

        private static void smethod_8(IEnumerable<KeyValuePair<string, Color>> ienumerable_0, StyledString styledString_0)
        {
            int num = 0;
            foreach (KeyValuePair<string, Color> pair in ienumerable_0)
            {
                int num2 = 0;
                while (num2 < pair.Key.Length)
                {
                    int length = styledString_0.CharacterIndexGeometry.GetLength(0);
                    int num4 = styledString_0.CharacterIndexGeometry.GetLength(1);
                    int num5 = 0;
                    while (true)
                    {
                        if (num5 >= length)
                        {
                            num++;
                            num2++;
                            break;
                        }
                        int num6 = 0;
                        while (true)
                        {
                            if (num6 >= num4)
                            {
                                num5++;
                                break;
                            }
                            if (styledString_0.CharacterIndexGeometry[num5, num6] == num)
                            {
                                styledString_0.ColorGeometry[num5, num6] = pair.Value;
                            }
                            num6++;
                        }
                    }
                }
            }
        }

        private static void smethod_9(string string_2, char[] char_0, int int_2, int int_3, StyleSheet styleSheet_0)
        {
            string str = char_0.smethod_2<char[]>().Substring(int_2, int_3);
            smethod_6<string>(string_2, str, styleSheet_0);
        }

        public static void Write(bool value)
        {
            System.Console.Write(value);
        }

        public static void Write(char value)
        {
            System.Console.Write(value);
        }

        public static void Write(char[] value)
        {
            System.Console.Write(value);
        }

        public static void Write(decimal value)
        {
            System.Console.Write(value);
        }

        public static void Write(double value)
        {
            System.Console.Write(value);
        }

        public static void Write(int value)
        {
            System.Console.Write(value);
        }

        public static void Write(long value)
        {
            System.Console.Write(value);
        }

        public static void Write(object value)
        {
            System.Console.Write(value);
        }

        public static void Write(float value)
        {
            System.Console.Write(value);
        }

        public static void Write(string value)
        {
            System.Console.Write(value);
        }

        public static void Write(uint value)
        {
            System.Console.Write(value);
        }

        public static void Write(ulong value)
        {
            System.Console.Write(value);
        }

        public static void Write(bool value, Color color)
        {
            smethod_2<bool>(new Action<bool>(System.Console.Write), value, color);
        }

        public static void Write(char value, Color color)
        {
            smethod_2<char>(new Action<char>(System.Console.Write), value, color);
        }

        public static void Write(char[] value, Color color)
        {
            smethod_2<char[]>(new Action<char[]>(System.Console.Write), value, color);
        }

        public static void Write(decimal value, Color color)
        {
            smethod_2<decimal>(new Action<decimal>(System.Console.Write), value, color);
        }

        public static void Write(double value, Color color)
        {
            smethod_2<double>(new Action<double>(System.Console.Write), value, color);
        }

        public static void Write(int value, Color color)
        {
            smethod_2<int>(new Action<int>(System.Console.Write), value, color);
        }

        public static void Write(long value, Color color)
        {
            smethod_2<long>(new Action<long>(System.Console.Write), value, color);
        }

        public static void Write(object value, Color color)
        {
            smethod_2<object>(new Action<object>(System.Console.Write), value, color);
        }

        public static void Write(float value, Color color)
        {
            smethod_2<float>(new Action<float>(System.Console.Write), value, color);
        }

        public static void Write(string value, Color color)
        {
            smethod_2<string>(new Action<string>(System.Console.Write), value, color);
        }

        public static void Write(string format, object arg0)
        {
            System.Console.Write(format, arg0);
        }

        public static void Write(string format, params object[] args)
        {
            System.Console.Write(format, args);
        }

        public static void Write(uint value, Color color)
        {
            smethod_2<uint>(new Action<uint>(System.Console.Write), value, color);
        }

        public static void Write(ulong value, Color color)
        {
            smethod_2<ulong>(new Action<ulong>(System.Console.Write), value, color);
        }

        public static void Write(string format, Color color, params object[] args)
        {
            smethod_10<string, object[]>(new Action<string, object[]>(System.Console.Write), format, args, color);
        }

        public static void Write(char[] buffer, int index, int count)
        {
            System.Console.Write(buffer, index, count);
        }

        public static void Write(string format, object arg0, Color color)
        {
            smethod_10<string, object>(new Action<string, object>(System.Console.Write), format, arg0, color);
        }

        public static void Write(string format, object arg0, object arg1)
        {
            System.Console.Write(format, arg0, arg1);
        }

        public static void Write(char[] buffer, int index, int count, Color color)
        {
            smethod_3(new Action<string>(System.Console.Write), buffer, index, count, color);
        }

        public static void Write(string format, object arg0, object arg1, Color color)
        {
            smethod_15<string, object>(new Action<string, object, object>(System.Console.Write), format, arg0, arg1, color);
        }

        public static void Write(string format, object arg0, object arg1, object arg2)
        {
            System.Console.Write(format, arg0, arg1, arg2);
        }

        public static void Write(string format, object arg0, object arg1, object arg2, Color color)
        {
            smethod_20<string, object>(new Action<string, object, object, object>(System.Console.Write), format, arg0, arg1, arg2, color);
        }

        public static void Write(string format, object arg0, object arg1, object arg2, object arg3)
        {
            object[] arg = new object[] { arg0, arg1, arg2, arg3 };
            System.Console.Write(format, arg);
        }

        public static void Write(string format, object arg0, object arg1, object arg2, object arg3, Color color)
        {
            object[] objArray1 = new object[] { arg0, arg1, arg2, arg3 };
            smethod_10<string, object[]>(new Action<string, object[]>(System.Console.Write), format, objArray1, color);
        }

        public static void WriteAlternating(bool value, ColorAlternator alternator)
        {
            smethod_4<bool>(new Action<bool>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(char value, ColorAlternator alternator)
        {
            smethod_4<char>(new Action<char>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(char[] value, ColorAlternator alternator)
        {
            smethod_4<char[]>(new Action<char[]>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(decimal value, ColorAlternator alternator)
        {
            smethod_4<decimal>(new Action<decimal>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(double value, ColorAlternator alternator)
        {
            smethod_4<double>(new Action<double>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(int value, ColorAlternator alternator)
        {
            smethod_4<int>(new Action<int>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(long value, ColorAlternator alternator)
        {
            smethod_4<long>(new Action<long>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(object value, ColorAlternator alternator)
        {
            smethod_4<object>(new Action<object>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(float value, ColorAlternator alternator)
        {
            smethod_4<float>(new Action<float>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(string value, ColorAlternator alternator)
        {
            smethod_4<string>(new Action<string>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(uint value, ColorAlternator alternator)
        {
            smethod_4<uint>(new Action<uint>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(ulong value, ColorAlternator alternator)
        {
            smethod_4<ulong>(new Action<ulong>(System.Console.Write), value, alternator);
        }

        public static void WriteAlternating(string format, ColorAlternator alternator, params object[] args)
        {
            smethod_11<string, object[]>(new Action<string, object[]>(System.Console.Write), format, args, alternator);
        }

        public static void WriteAlternating(string format, object arg0, ColorAlternator alternator)
        {
            smethod_11<string, object>(new Action<string, object>(System.Console.Write), format, arg0, alternator);
        }

        public static void WriteAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
        {
            smethod_5(new Action<string>(System.Console.Write), buffer, index, count, alternator);
        }

        public static void WriteAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
        {
            smethod_16<string, object>(new Action<string, object, object>(System.Console.Write), format, arg0, arg1, alternator);
        }

        public static void WriteAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
        {
            smethod_21<string, object>(new Action<string, object, object, object>(System.Console.Write), format, arg0, arg1, arg2, alternator);
        }

        public static void WriteAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
        {
            object[] objArray1 = new object[] { arg0, arg1, arg2, arg3 };
            smethod_11<string, object[]>(new Action<string, object[]>(System.Console.Write), format, objArray1, alternator);
        }

        public static void WriteAscii(string value)
        {
            WriteAscii(value, (FigletFont) null);
        }

        public static void WriteAscii(string value, FigletFont font)
        {
            WriteLine(smethod_27(font).ToAscii(value).ConcreteValue);
        }

        public static void WriteAscii(string value, Color color)
        {
            WriteAscii(value, null, color);
        }

        public static void WriteAscii(string value, FigletFont font, Color color)
        {
            WriteLine(smethod_27(font).ToAscii(value).ConcreteValue, color);
        }

        public static void WriteAsciiAlternating(string value, ColorAlternator alternator)
        {
            WriteAsciiAlternating(value, null, alternator);
        }

        public static void WriteAsciiAlternating(string value, FigletFont font, ColorAlternator alternator)
        {
            char[] separator = new char[] { '\n' };
            string[] strArray = smethod_27(font).ToAscii(value).ConcreteValue.Split(separator);
            for (int i = 0; i < strArray.Length; i++)
            {
                WriteLineAlternating(strArray[i], alternator);
            }
        }

        public static void WriteAsciiStyled(string value, StyleSheet styleSheet)
        {
            WriteAsciiStyled(value, null, styleSheet);
        }

        public static void WriteAsciiStyled(string value, FigletFont font, StyleSheet styleSheet)
        {
            WriteLineStyled(smethod_27(font).ToAscii(value), styleSheet);
        }

        public static void WriteFormatted(string format, Formatter arg0, Color defaultColor)
        {
            smethod_14<string>(string_1, format, arg0, defaultColor);
        }

        public static void WriteFormatted(string format, Color defaultColor, params Formatter[] args)
        {
            smethod_25<string>(string_1, format, args, defaultColor);
        }

        public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
        {
            smethod_19<string>(string_1, format, arg0, arg1, defaultColor);
        }

        public static void WriteFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
        {
            smethod_13<string, object[]>(string_1, format, args, styledColor, defaultColor);
        }

        public static void WriteFormatted(string format, object arg0, Color styledColor, Color defaultColor)
        {
            smethod_13<string, object>(string_1, format, arg0, styledColor, defaultColor);
        }

        public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
        {
            smethod_24<string>(string_1, format, arg0, arg1, arg2, defaultColor);
        }

        public static void WriteFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
        {
            smethod_18<string, object>(string_1, format, arg0, arg1, styledColor, defaultColor);
        }

        public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
        {
            Formatter[] formatterArray1 = new Formatter[] { arg0, arg1, arg2, arg3 };
            smethod_25<string>(string_1, format, formatterArray1, defaultColor);
        }

        public static void WriteFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
        {
            smethod_23<string, object>(string_1, format, arg0, arg1, arg2, styledColor, defaultColor);
        }

        public static void WriteFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
        {
            object[] objArray1 = new object[] { arg0, arg1, arg2, arg3 };
            smethod_13<string, object[]>(string_1, format, objArray1, styledColor, defaultColor);
        }

        public static void WriteLine()
        {
            System.Console.WriteLine();
        }

        public static void WriteLine(bool value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(char value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(char[] value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(decimal value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(double value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(int value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(long value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(object value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(float value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(uint value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(ulong value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(bool value, Color color)
        {
            smethod_2<bool>(new Action<bool>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(char value, Color color)
        {
            smethod_2<char>(new Action<char>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(char[] value, Color color)
        {
            smethod_2<char[]>(new Action<char[]>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(decimal value, Color color)
        {
            smethod_2<decimal>(new Action<decimal>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(double value, Color color)
        {
            smethod_2<double>(new Action<double>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(int value, Color color)
        {
            smethod_2<int>(new Action<int>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(long value, Color color)
        {
            smethod_2<long>(new Action<long>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(object value, Color color)
        {
            smethod_2<object>(new Action<object>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(float value, Color color)
        {
            smethod_2<float>(new Action<float>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(string value, Color color)
        {
            smethod_2<string>(new Action<string>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(string format, object arg0)
        {
            System.Console.WriteLine(format, arg0);
        }

        public static void WriteLine(string format, params object[] args)
        {
            System.Console.WriteLine(format, args);
        }

        public static void WriteLine(uint value, Color color)
        {
            smethod_2<uint>(new Action<uint>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(ulong value, Color color)
        {
            smethod_2<ulong>(new Action<ulong>(System.Console.WriteLine), value, color);
        }

        public static void WriteLine(string format, Color color, params object[] args)
        {
            smethod_10<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, args, color);
        }

        public static void WriteLine(char[] buffer, int index, int count)
        {
            System.Console.WriteLine(buffer, index, count);
        }

        public static void WriteLine(string format, object arg0, Color color)
        {
            smethod_10<string, object>(new Action<string, object>(System.Console.WriteLine), format, arg0, color);
        }

        public static void WriteLine(string format, object arg0, object arg1)
        {
            System.Console.WriteLine(format, arg0, arg1);
        }

        public static void WriteLine(char[] buffer, int index, int count, Color color)
        {
            smethod_3(new Action<string>(System.Console.WriteLine), buffer, index, count, color);
        }

        public static void WriteLine(string format, object arg0, object arg1, Color color)
        {
            smethod_15<string, object>(new Action<string, object, object>(System.Console.WriteLine), format, arg0, arg1, color);
        }

        public static void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            System.Console.WriteLine(format, arg0, arg1, arg2);
        }

        public static void WriteLine(string format, object arg0, object arg1, object arg2, Color color)
        {
            smethod_20<string, object>(new Action<string, object, object, object>(System.Console.WriteLine), format, arg0, arg1, arg2, color);
        }

        public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
        {
            object[] arg = new object[] { arg0, arg1, arg2, arg3 };
            System.Console.WriteLine(format, arg);
        }

        public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, Color color)
        {
            object[] objArray1 = new object[] { arg0, arg1, arg2, arg3 };
            smethod_10<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, objArray1, color);
        }

        public static void WriteLineAlternating(ColorAlternator alternator)
        {
            smethod_4<string>(new Action<string>(System.Console.Write), string_0, alternator);
        }

        public static void WriteLineAlternating(bool value, ColorAlternator alternator)
        {
            smethod_4<bool>(new Action<bool>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(char value, ColorAlternator alternator)
        {
            smethod_4<char>(new Action<char>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(char[] value, ColorAlternator alternator)
        {
            smethod_4<char[]>(new Action<char[]>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(decimal value, ColorAlternator alternator)
        {
            smethod_4<decimal>(new Action<decimal>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(double value, ColorAlternator alternator)
        {
            smethod_4<double>(new Action<double>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(int value, ColorAlternator alternator)
        {
            smethod_4<int>(new Action<int>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(long value, ColorAlternator alternator)
        {
            smethod_4<long>(new Action<long>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(object value, ColorAlternator alternator)
        {
            smethod_4<object>(new Action<object>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(float value, ColorAlternator alternator)
        {
            smethod_4<float>(new Action<float>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(string value, ColorAlternator alternator)
        {
            smethod_4<string>(new Action<string>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(uint value, ColorAlternator alternator)
        {
            smethod_4<uint>(new Action<uint>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(ulong value, ColorAlternator alternator)
        {
            smethod_4<ulong>(new Action<ulong>(System.Console.WriteLine), value, alternator);
        }

        public static void WriteLineAlternating(string format, ColorAlternator alternator, params object[] args)
        {
            smethod_11<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, args, alternator);
        }

        public static void WriteLineAlternating(string format, object arg0, ColorAlternator alternator)
        {
            smethod_11<string, object>(new Action<string, object>(System.Console.WriteLine), format, arg0, alternator);
        }

        public static void WriteLineAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
        {
            smethod_5(new Action<string>(System.Console.WriteLine), buffer, index, count, alternator);
        }

        public static void WriteLineAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
        {
            smethod_16<string, object>(new Action<string, object, object>(System.Console.WriteLine), format, arg0, arg1, alternator);
        }

        public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
        {
            smethod_21<string, object>(new Action<string, object, object, object>(System.Console.WriteLine), format, arg0, arg1, arg2, alternator);
        }

        public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
        {
            object[] objArray1 = new object[] { arg0, arg1, arg2, arg3 };
            smethod_11<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, objArray1, alternator);
        }

        public static void WriteLineFormatted(string format, Formatter arg0, Color defaultColor)
        {
            smethod_14<string>(string_0, format, arg0, defaultColor);
        }

        public static void WriteLineFormatted(string format, Color defaultColor, params Formatter[] args)
        {
            smethod_25<string>(string_0, format, args, defaultColor);
        }

        public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
        {
            smethod_19<string>(string_0, format, arg0, arg1, defaultColor);
        }

        public static void WriteLineFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
        {
            smethod_13<string, object[]>(string_0, format, args, styledColor, defaultColor);
        }

        public static void WriteLineFormatted(string format, object arg0, Color styledColor, Color defaultColor)
        {
            smethod_13<string, object>(string_0, format, arg0, styledColor, defaultColor);
        }

        public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
        {
            smethod_24<string>(string_0, format, arg0, arg1, arg2, defaultColor);
        }

        public static void WriteLineFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
        {
            smethod_18<string, object>(string_0, format, arg0, arg1, styledColor, defaultColor);
        }

        public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
        {
            Formatter[] formatterArray1 = new Formatter[] { arg0, arg1, arg2, arg3 };
            smethod_25<string>(string_0, format, formatterArray1, defaultColor);
        }

        public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
        {
            smethod_23<string, object>(string_0, format, arg0, arg1, arg2, styledColor, defaultColor);
        }

        public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
        {
            object[] objArray1 = new object[] { arg0, arg1, arg2, arg3 };
            smethod_13<string, object[]>(string_0, format, objArray1, styledColor, defaultColor);
        }

        public static void WriteLineStyled(StyleSheet styleSheet)
        {
            smethod_6<string>(string_1, string_0, styleSheet);
        }

        public static void WriteLineStyled(StyledString value, StyleSheet styleSheet)
        {
            smethod_7(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(bool value, StyleSheet styleSheet)
        {
            smethod_6<bool>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(char value, StyleSheet styleSheet)
        {
            smethod_6<char>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(char[] value, StyleSheet styleSheet)
        {
            smethod_6<char[]>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(decimal value, StyleSheet styleSheet)
        {
            smethod_6<decimal>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(double value, StyleSheet styleSheet)
        {
            smethod_6<double>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(int value, StyleSheet styleSheet)
        {
            smethod_6<int>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(long value, StyleSheet styleSheet)
        {
            smethod_6<long>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(float value, StyleSheet styleSheet)
        {
            smethod_6<float>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(string value, StyleSheet styleSheet)
        {
            smethod_6<string>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(uint value, StyleSheet styleSheet)
        {
            smethod_6<uint>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(ulong value, StyleSheet styleSheet)
        {
            smethod_6<ulong>(string_0, value, styleSheet);
        }

        public static void WriteLineStyled(StyleSheet styleSheet, string format, params object[] args)
        {
            smethod_12<string, object[]>(string_0, format, args, styleSheet);
        }

        public static void WriteLineStyled(string format, object arg0, StyleSheet styleSheet)
        {
            smethod_12<string, object>(string_0, format, arg0, styleSheet);
        }

        public static void WriteLineStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
        {
            smethod_9(string_0, buffer, index, count, styleSheet);
        }

        public static void WriteLineStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
        {
            smethod_17<string, object>(string_0, format, arg0, arg1, styleSheet);
        }

        public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
        {
            smethod_22<string, object>(string_0, format, arg0, arg1, arg2, styleSheet);
        }

        public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
        {
            object[] objArray1 = new object[] { arg0, arg1, arg2, arg3 };
            smethod_12<string, object[]>(string_0, format, objArray1, styleSheet);
        }

        public static void WriteLineWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 0x10)
        {
            smethod_26<T>(new Action<object, Color>(Plugin.Core.Colorful.Console.WriteLine), input, startColor, endColor, maxColorsInGradient);
        }

        public static void WriteStyled(bool value, StyleSheet styleSheet)
        {
            smethod_6<bool>(string_1, value, styleSheet);
        }

        public static void WriteStyled(char value, StyleSheet styleSheet)
        {
            smethod_6<char>(string_1, value, styleSheet);
        }

        public static void WriteStyled(char[] value, StyleSheet styleSheet)
        {
            smethod_6<char[]>(string_1, value, styleSheet);
        }

        public static void WriteStyled(decimal value, StyleSheet styleSheet)
        {
            smethod_6<decimal>(string_1, value, styleSheet);
        }

        public static void WriteStyled(double value, StyleSheet styleSheet)
        {
            smethod_6<double>(string_1, value, styleSheet);
        }

        public static void WriteStyled(int value, StyleSheet styleSheet)
        {
            smethod_6<int>(string_1, value, styleSheet);
        }

        public static void WriteStyled(long value, StyleSheet styleSheet)
        {
            smethod_6<long>(string_1, value, styleSheet);
        }

        public static void WriteStyled(object value, StyleSheet styleSheet)
        {
            smethod_6<object>(string_1, value, styleSheet);
        }

        public static void WriteStyled(float value, StyleSheet styleSheet)
        {
            smethod_6<float>(string_1, value, styleSheet);
        }

        public static void WriteStyled(string value, StyleSheet styleSheet)
        {
            smethod_6<string>(string_1, value, styleSheet);
        }

        public static void WriteStyled(uint value, StyleSheet styleSheet)
        {
            smethod_6<uint>(string_1, value, styleSheet);
        }

        public static void WriteStyled(ulong value, StyleSheet styleSheet)
        {
            smethod_6<ulong>(string_1, value, styleSheet);
        }

        public static void WriteStyled(StyleSheet styleSheet, string format, params object[] args)
        {
            smethod_12<string, object[]>(string_1, format, args, styleSheet);
        }

        public static void WriteStyled(string format, object arg0, StyleSheet styleSheet)
        {
            smethod_12<string, object>(string_1, format, arg0, styleSheet);
        }

        public static void WriteStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
        {
            smethod_9(string_1, buffer, index, count, styleSheet);
        }

        public static void WriteStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
        {
            smethod_17<string, object>(string_1, format, arg0, arg1, styleSheet);
        }

        public static void WriteStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
        {
            smethod_22<string, object>(string_1, format, arg0, arg1, arg2, styleSheet);
        }

        public static void WriteStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
        {
            object[] objArray1 = new object[] { arg0, arg1, arg2, arg3 };
            smethod_12<string, object[]>(string_1, format, objArray1, styleSheet);
        }

        public static void WriteWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 0x10)
        {
            smethod_26<T>(new Action<object, Color>(Plugin.Core.Colorful.Console.Write), input, startColor, endColor, maxColorsInGradient);
        }

        private static TaskQueue TaskQueue_0 { get; }

        public static Color BackgroundColor
        {
            get => 
                colorManager_0.GetColor(System.Console.BackgroundColor);
            set => 
                System.Console.BackgroundColor = colorManager_0.GetConsoleColor(value);
        }

        public static int BufferHeight
        {
            get => 
                System.Console.BufferHeight;
            set => 
                System.Console.BufferHeight = value;
        }

        public static int BufferWidth
        {
            get => 
                System.Console.BufferWidth;
            set => 
                System.Console.BufferWidth = value;
        }

        public static bool CapsLock =>
            System.Console.CapsLock;

        public static int CursorLeft
        {
            get => 
                System.Console.CursorLeft;
            set => 
                System.Console.CursorLeft = value;
        }

        public static int CursorSize
        {
            get => 
                System.Console.CursorSize;
            set => 
                System.Console.CursorSize = value;
        }

        public static int CursorTop
        {
            get => 
                System.Console.CursorTop;
            set => 
                System.Console.CursorTop = value;
        }

        public static bool CursorVisible
        {
            get => 
                System.Console.CursorVisible;
            set => 
                System.Console.CursorVisible = value;
        }

        public static TextWriter Error =>
            System.Console.Error;

        public static Color ForegroundColor
        {
            get => 
                colorManager_0.GetColor(System.Console.ForegroundColor);
            set => 
                System.Console.ForegroundColor = colorManager_0.GetConsoleColor(value);
        }

        public static TextReader In =>
            System.Console.In;

        public static Encoding InputEncoding
        {
            get => 
                System.Console.InputEncoding;
            set => 
                System.Console.InputEncoding = value;
        }

        public static bool IsErrorRedirected =>
            System.Console.IsErrorRedirected;

        public static bool IsInputRedirected =>
            System.Console.IsInputRedirected;

        public static bool IsOutputRedirected =>
            System.Console.IsOutputRedirected;

        public static bool KeyAvailable =>
            System.Console.KeyAvailable;

        public static int LargestWindowHeight =>
            System.Console.LargestWindowHeight;

        public static int LargestWindowWidth =>
            System.Console.LargestWindowWidth;

        public static bool NumberLock =>
            System.Console.NumberLock;

        public static TextWriter Out =>
            System.Console.Out;

        public static Encoding OutputEncoding
        {
            get => 
                System.Console.OutputEncoding;
            set => 
                System.Console.OutputEncoding = value;
        }

        public static string Title
        {
            get => 
                System.Console.Title;
            set => 
                System.Console.Title = value;
        }

        public static bool TreatControlCAsInput
        {
            get => 
                System.Console.TreatControlCAsInput;
            set => 
                System.Console.TreatControlCAsInput = value;
        }

        public static int WindowHeight
        {
            get => 
                System.Console.WindowHeight;
            set => 
                System.Console.WindowHeight = value;
        }

        public static int WindowLeft
        {
            get => 
                System.Console.WindowLeft;
            set => 
                System.Console.WindowLeft = value;
        }

        public static int WindowTop
        {
            get => 
                System.Console.WindowTop;
            set => 
                System.Console.WindowTop = value;
        }

        public static int WindowWidth
        {
            get => 
                System.Console.WindowWidth;
            set => 
                System.Console.WindowWidth = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class Class15
        {
            public static readonly Plugin.Core.Colorful.Console.Class15 <>9 = new Plugin.Core.Colorful.Console.Class15();

            internal void method_0(object sender, ConsoleCancelEventArgs e)
            {
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class Class16<T>
        {
            public static readonly Plugin.Core.Colorful.Console.Class16<T> <>9;
            public static Func<Formatter, object> <>9__36_0;
            public static Func<Formatter, Color> <>9__36_1;

            static Class16()
            {
                Plugin.Core.Colorful.Console.Class16<T>.<>9 = new Plugin.Core.Colorful.Console.Class16<T>();
            }

            internal object method_0(Formatter formatter_0) => 
                formatter_0.Target;

            internal Color method_1(Formatter formatter_0) => 
                formatter_0.Color;
        }

        [CompilerGenerated]
        private sealed class Class17
        {
            public IEnumerable<KeyValuePair<string, Color>> ienumerable_0;
            public string string_0;
            public Action action_0;

            internal Task method_0()
            {
                Action action = this.action_0;
                if (this.action_0 == null)
                {
                    Action local1 = this.action_0;
                    action = this.action_0 = new Action(this.method_1);
                }
                return Task.Factory.StartNew(action);
            }

            internal void method_1()
            {
                ConsoleColor foregroundColor = System.Console.ForegroundColor;
                int num = 1;
                foreach (KeyValuePair<string, Color> pair in this.ienumerable_0)
                {
                    System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(pair.Value);
                    if (num == this.ienumerable_0.Count<KeyValuePair<string, Color>>())
                    {
                        System.Console.Write(pair.Key + this.string_0);
                    }
                    else
                    {
                        System.Console.Write(pair.Key);
                    }
                    num++;
                }
                System.Console.ForegroundColor = foregroundColor;
            }
        }

        [CompilerGenerated]
        private static class Class18<T, U>
        {
            public static CallSite<Func<CallSite, Type, string, object, object>> callSite_0;
            public static CallSite<Func<CallSite, object, string>> callSite_1;
        }

        [CompilerGenerated]
        private static class Class19<T, U>
        {
            public static CallSite<Func<CallSite, Type, string, object, object>> callSite_0;
            public static CallSite<Func<CallSite, object, string>> callSite_1;
        }

        [CompilerGenerated]
        private static class Class20<T, U>
        {
            public static CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>> callSite_0;
            public static CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>> callSite_1;
        }

        [CompilerGenerated]
        private static class Class21<T, U>
        {
            public static CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>> callSite_0;
            public static CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>> callSite_1;
        }

        [CompilerGenerated]
        private static class Class22<T, U>
        {
            public static CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>> callSite_0;
            public static CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>> callSite_1;
        }
    }
}

