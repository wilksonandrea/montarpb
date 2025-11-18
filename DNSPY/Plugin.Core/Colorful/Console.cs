using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000E7 RID: 231
	public static class Console
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00006717 File Offset: 0x00004917
		private static TaskQueue TaskQueue_0
		{
			[CompilerGenerated]
			get
			{
				return Console.taskQueue_0;
			}
		} = new TaskQueue();

		// Token: 0x06000813 RID: 2067 RVA: 0x0001FF18 File Offset: 0x0001E118
		private static void smethod_0(IEnumerable<KeyValuePair<string, Color>> ienumerable_0, string string_2)
		{
			Console.Class17 @class = new Console.Class17();
			@class.ienumerable_0 = ienumerable_0;
			@class.string_0 = string_2;
			Console.TaskQueue_0.Enqueue(new Func<Task>(@class.method_0)).Wait();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001FF54 File Offset: 0x0001E154
		private static void smethod_1(StyledString styledString_0, string string_2)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			int length = styledString_0.CharacterGeometry.GetLength(0);
			int length2 = styledString_0.CharacterGeometry.GetLength(1);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(styledString_0.ColorGeometry[i, j]);
					if (i == length - 1 && j == length2 - 1)
					{
						Console.Write(styledString_0.CharacterGeometry[i, j].ToString() + string_2);
					}
					else if (j == length2 - 1)
					{
						Console.Write(styledString_0.CharacterGeometry[i, j].ToString() + "\r\n");
					}
					else
					{
						Console.Write(styledString_0.CharacterGeometry[i, j]);
					}
				}
			}
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0000671E File Offset: 0x0000491E
		private static void smethod_2<T>(Action<T> action_0, T gparam_0, Color color_16)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(color_16);
			action_0(gparam_0);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00020038 File Offset: 0x0001E238
		private static void smethod_3(Action<string> action_0, char[] char_0, int int_2, int int_3, Color color_16)
		{
			string text = char_0.smethod_2<char[]>().Substring(int_2, int_3);
			Console.smethod_2<string>(action_0, text, color_16);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002005C File Offset: 0x0001E25C
		private static void smethod_4<T>(Action<T> action_0, T gparam_0, ColorAlternator colorAlternator_0)
		{
			Color nextColor = colorAlternator_0.GetNextColor(gparam_0.smethod_2<T>());
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(nextColor);
			action_0(gparam_0);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00020098 File Offset: 0x0001E298
		private static void smethod_5(Action<string> action_0, char[] char_0, int int_2, int int_3, ColorAlternator colorAlternator_0)
		{
			string text = char_0.smethod_2<char[]>().Substring(int_2, int_3);
			Console.smethod_4<string>(action_0, text, colorAlternator_0);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00006741 File Offset: 0x00004941
		private static void smethod_6<T>(string string_2, T gparam_0, StyleSheet styleSheet_0)
		{
			Console.smethod_0(new TextAnnotator(styleSheet_0).GetAnnotationMap(gparam_0.smethod_2<T>()), string_2);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0000675A File Offset: 0x0000495A
		private static void smethod_7(string string_2, StyledString styledString_0, StyleSheet styleSheet_0)
		{
			Console.smethod_8(new TextAnnotator(styleSheet_0).GetAnnotationMap(styledString_0.AbstractValue), styledString_0);
			Console.smethod_1(styledString_0, string_2);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000200BC File Offset: 0x0001E2BC
		private static void smethod_8(IEnumerable<KeyValuePair<string, Color>> ienumerable_0, StyledString styledString_0)
		{
			int num = 0;
			foreach (KeyValuePair<string, Color> keyValuePair in ienumerable_0)
			{
				for (int i = 0; i < keyValuePair.Key.Length; i++)
				{
					int length = styledString_0.CharacterIndexGeometry.GetLength(0);
					int length2 = styledString_0.CharacterIndexGeometry.GetLength(1);
					for (int j = 0; j < length; j++)
					{
						for (int k = 0; k < length2; k++)
						{
							if (styledString_0.CharacterIndexGeometry[j, k] == num)
							{
								styledString_0.ColorGeometry[j, k] = keyValuePair.Value;
							}
						}
					}
					num++;
				}
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00020188 File Offset: 0x0001E388
		private static void smethod_9(string string_2, char[] char_0, int int_2, int int_3, StyleSheet styleSheet_0)
		{
			string text = char_0.smethod_2<char[]>().Substring(int_2, int_3);
			Console.smethod_6<string>(string_2, text, styleSheet_0);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0000677A File Offset: 0x0000497A
		private static void smethod_10<T, U>(Action<T, U> action_0, T gparam_0, U gparam_1, Color color_16)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(color_16);
			action_0(gparam_0, gparam_1);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000201AC File Offset: 0x0001E3AC
		private static void smethod_11<T, U>(Action<T, U> action_0, T gparam_0, U gparam_1, ColorAlternator colorAlternator_0)
		{
			if (Console.Class18<T, U>.callSite_1 == null)
			{
				Console.Class18<T, U>.callSite_1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(Console)));
			}
			Func<CallSite, object, string> target = Console.Class18<T, U>.callSite_1.Target;
			CallSite callSite_ = Console.Class18<T, U>.callSite_1;
			if (Console.Class18<T, U>.callSite_0 == null)
			{
				Console.Class18<T, U>.callSite_0 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", null, typeof(Console), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			string text = target(callSite_, Console.Class18<T, U>.callSite_0.Target(Console.Class18<T, U>.callSite_0, typeof(string), gparam_0.ToString(), gparam_1.smethod_3<U>()));
			Color nextColor = colorAlternator_0.GetNextColor(text);
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(nextColor);
			action_0(gparam_0, gparam_1);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000202A0 File Offset: 0x0001E4A0
		private static void smethod_12<T, U>(string string_2, T gparam_0, U gparam_1, StyleSheet styleSheet_0)
		{
			TextAnnotator textAnnotator = new TextAnnotator(styleSheet_0);
			if (Console.Class19<T, U>.callSite_1 == null)
			{
				Console.Class19<T, U>.callSite_1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(Console)));
			}
			Func<CallSite, object, string> target = Console.Class19<T, U>.callSite_1.Target;
			CallSite callSite_ = Console.Class19<T, U>.callSite_1;
			if (Console.Class19<T, U>.callSite_0 == null)
			{
				Console.Class19<T, U>.callSite_0 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", null, typeof(Console), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			string text = target(callSite_, Console.Class19<T, U>.callSite_0.Target(Console.Class19<T, U>.callSite_0, typeof(string), gparam_0.ToString(), gparam_1.smethod_3<U>()));
			Console.smethod_0(textAnnotator.GetAnnotationMap(text), string_2);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00020380 File Offset: 0x0001E580
		private static void smethod_13<T, U>(string string_2, T gparam_0, U gparam_1, Color color_16, Color color_17)
		{
			TextFormatter textFormatter = new TextFormatter(color_17);
			if (Console.Class20<T, U>.callSite_1 == null)
			{
				Console.Class20<T, U>.callSite_1 = CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(List<KeyValuePair<string, Color>>), typeof(Console)));
			}
			Func<CallSite, object, List<KeyValuePair<string, Color>>> target = Console.Class20<T, U>.callSite_1.Target;
			CallSite callSite_ = Console.Class20<T, U>.callSite_1;
			if (Console.Class20<T, U>.callSite_0 == null)
			{
				Console.Class20<T, U>.callSite_0 = CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetFormatMap", null, typeof(Console), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			Console.smethod_0(target(callSite_, Console.Class20<T, U>.callSite_0.Target(Console.Class20<T, U>.callSite_0, textFormatter, gparam_0.ToString(), gparam_1.smethod_3<U>(), new Color[] { color_16 })), string_2);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00020468 File Offset: 0x0001E668
		private static void smethod_14<T>(string string_2, T gparam_0, Formatter formatter_0, Color color_16)
		{
			Console.smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), new object[] { formatter_0.Target }, new Color[] { formatter_0.Color }), string_2);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0000679E File Offset: 0x0000499E
		private static void smethod_15<T, U>(Action<T, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, Color color_16)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(color_16);
			action_0(gparam_0, gparam_1, gparam_2);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000204B8 File Offset: 0x0001E6B8
		private static void smethod_16<T, U>(Action<T, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, ColorAlternator colorAlternator_0)
		{
			string text = string.Format(gparam_0.ToString(), gparam_1, gparam_2);
			Color nextColor = colorAlternator_0.GetNextColor(text);
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(nextColor);
			action_0(gparam_0, gparam_1, gparam_2);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00020510 File Offset: 0x0001E710
		private static void smethod_17<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, StyleSheet styleSheet_0)
		{
			TextAnnotator textAnnotator = new TextAnnotator(styleSheet_0);
			string text = string.Format(gparam_0.ToString(), gparam_1, gparam_2);
			Console.smethod_0(textAnnotator.GetAnnotationMap(text), string_2);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00020550 File Offset: 0x0001E750
		private static void smethod_18<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, Color color_16, Color color_17)
		{
			TextFormatter textFormatter = new TextFormatter(color_17);
			if (Console.Class21<T, U>.callSite_1 == null)
			{
				Console.Class21<T, U>.callSite_1 = CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(List<KeyValuePair<string, Color>>), typeof(Console)));
			}
			Func<CallSite, object, List<KeyValuePair<string, Color>>> target = Console.Class21<T, U>.callSite_1.Target;
			CallSite callSite_ = Console.Class21<T, U>.callSite_1;
			if (Console.Class21<T, U>.callSite_0 == null)
			{
				Console.Class21<T, U>.callSite_0 = CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetFormatMap", null, typeof(Console), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			Console.smethod_0(target(callSite_, Console.Class21<T, U>.callSite_0.Target(Console.Class21<T, U>.callSite_0, textFormatter, gparam_0.ToString(), new U[] { gparam_1, gparam_2 }.smethod_3<U[]>(), new Color[] { color_16 })), string_2);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002064C File Offset: 0x0001E84C
		private static void smethod_19<T>(string string_2, T gparam_0, Formatter formatter_0, Formatter formatter_1, Color color_16)
		{
			Console.smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), new object[] { formatter_0.Target, formatter_1.Target }, new Color[] { formatter_0.Color, formatter_1.Color }), string_2);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000067C4 File Offset: 0x000049C4
		private static void smethod_20<T, U>(Action<T, U, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, U gparam_3, Color color_16)
		{
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(color_16);
			action_0(gparam_0, gparam_1, gparam_2, gparam_3);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000206B0 File Offset: 0x0001E8B0
		private static void smethod_21<T, U>(Action<T, U, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, U gparam_3, ColorAlternator colorAlternator_0)
		{
			string text = string.Format(gparam_0.ToString(), gparam_1, gparam_2, gparam_3);
			Color nextColor = colorAlternator_0.GetNextColor(text);
			ConsoleColor foregroundColor = Console.ForegroundColor;
			Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(nextColor);
			action_0(gparam_0, gparam_1, gparam_2, gparam_3);
			Console.ForegroundColor = foregroundColor;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00020714 File Offset: 0x0001E914
		private static void smethod_22<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, U gparam_3, StyleSheet styleSheet_0)
		{
			TextAnnotator textAnnotator = new TextAnnotator(styleSheet_0);
			string text = string.Format(gparam_0.ToString(), gparam_1, gparam_2, gparam_3);
			Console.smethod_0(textAnnotator.GetAnnotationMap(text), string_2);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0002075C File Offset: 0x0001E95C
		private static void smethod_23<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, U gparam_3, Color color_16, Color color_17)
		{
			TextFormatter textFormatter = new TextFormatter(color_17);
			if (Console.Class22<T, U>.callSite_1 == null)
			{
				Console.Class22<T, U>.callSite_1 = CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(List<KeyValuePair<string, Color>>), typeof(Console)));
			}
			Func<CallSite, object, List<KeyValuePair<string, Color>>> target = Console.Class22<T, U>.callSite_1.Target;
			CallSite callSite_ = Console.Class22<T, U>.callSite_1;
			if (Console.Class22<T, U>.callSite_0 == null)
			{
				Console.Class22<T, U>.callSite_0 = CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetFormatMap", null, typeof(Console), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			Console.smethod_0(target(callSite_, Console.Class22<T, U>.callSite_0.Target(Console.Class22<T, U>.callSite_0, textFormatter, gparam_0.ToString(), new U[] { gparam_1, gparam_2, gparam_3 }.smethod_3<U[]>(), new Color[] { color_16 })), string_2);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00020860 File Offset: 0x0001EA60
		private static void smethod_24<T>(string string_2, T gparam_0, Formatter formatter_0, Formatter formatter_1, Formatter formatter_2, Color color_16)
		{
			Console.smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), new object[] { formatter_0.Target, formatter_1.Target, formatter_2.Target }, new Color[] { formatter_0.Color, formatter_1.Color, formatter_2.Color }), string_2);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000208DC File Offset: 0x0001EADC
		private static void smethod_25<T>(string string_2, T gparam_0, Formatter[] formatter_0, Color color_16)
		{
			Console.smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), formatter_0.Select(new Func<Formatter, object>(Console.Class16<T>.<>9.method_0)).ToArray<object>(), formatter_0.Select(new Func<Formatter, Color>(Console.Class16<T>.<>9.method_1)).ToArray<Color>()), string_2);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002095C File Offset: 0x0001EB5C
		private static void smethod_26<T>(Action<object, Color> action_0, IEnumerable<T> ienumerable_0, Color color_16, Color color_17, int int_2)
		{
			foreach (StyleClass<T> styleClass in new GradientGenerator().GenerateGradient<T>(ienumerable_0, color_16, color_17, int_2))
			{
				action_0(styleClass.Target, styleClass.Color);
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000067EC File Offset: 0x000049EC
		private static Figlet smethod_27(FigletFont figletFont_0 = null)
		{
			if (figletFont_0 == null)
			{
				return new Figlet();
			}
			return new Figlet(figletFont_0);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000209CC File Offset: 0x0001EBCC
		private static ColorStore smethod_28()
		{
			ConcurrentDictionary<Color, ConsoleColor> concurrentDictionary = new ConcurrentDictionary<Color, ConsoleColor>();
			ConcurrentDictionary<ConsoleColor, Color> concurrentDictionary2 = new ConcurrentDictionary<ConsoleColor, Color>();
			concurrentDictionary2.TryAdd(ConsoleColor.Black, Console.color_0);
			concurrentDictionary2.TryAdd(ConsoleColor.Blue, Console.color_1);
			concurrentDictionary2.TryAdd(ConsoleColor.Cyan, Console.color_2);
			concurrentDictionary2.TryAdd(ConsoleColor.DarkBlue, Console.color_3);
			concurrentDictionary2.TryAdd(ConsoleColor.DarkCyan, Console.color_4);
			concurrentDictionary2.TryAdd(ConsoleColor.DarkGray, Console.color_5);
			concurrentDictionary2.TryAdd(ConsoleColor.DarkGreen, Console.color_6);
			concurrentDictionary2.TryAdd(ConsoleColor.DarkMagenta, Console.color_7);
			concurrentDictionary2.TryAdd(ConsoleColor.DarkRed, Console.color_8);
			concurrentDictionary2.TryAdd(ConsoleColor.DarkYellow, Console.color_9);
			concurrentDictionary2.TryAdd(ConsoleColor.Gray, Console.color_10);
			concurrentDictionary2.TryAdd(ConsoleColor.Green, Console.color_11);
			concurrentDictionary2.TryAdd(ConsoleColor.Magenta, Console.color_12);
			concurrentDictionary2.TryAdd(ConsoleColor.Red, Console.color_13);
			concurrentDictionary2.TryAdd(ConsoleColor.White, Console.color_14);
			concurrentDictionary2.TryAdd(ConsoleColor.Yellow, Console.color_15);
			return new ColorStore(concurrentDictionary, concurrentDictionary2);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		private static void smethod_29(bool bool_0)
		{
			Console.colorStore_0 = Console.smethod_28();
			Console.colorManagerFactory_0 = new ColorManagerFactory();
			Console.colorManager_0 = Console.colorManagerFactory_0.GetManager(Console.colorStore_0, 16, 1, bool_0);
			if (!Console.colorManager_0.IsInCompatibilityMode)
			{
				new ColorMapper().SetBatchBufferColors(Console.dictionary_0);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x000067FD File Offset: 0x000049FD
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x0000680E File Offset: 0x00004A0E
		public static Color BackgroundColor
		{
			get
			{
				return Console.colorManager_0.GetColor(Console.BackgroundColor);
			}
			set
			{
				Console.BackgroundColor = Console.colorManager_0.GetConsoleColor(value);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00006820 File Offset: 0x00004A20
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x00006827 File Offset: 0x00004A27
		public static int BufferHeight
		{
			get
			{
				return Console.BufferHeight;
			}
			set
			{
				Console.BufferHeight = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0000682F File Offset: 0x00004A2F
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x00006836 File Offset: 0x00004A36
		public static int BufferWidth
		{
			get
			{
				return Console.BufferWidth;
			}
			set
			{
				Console.BufferWidth = value;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0000683E File Offset: 0x00004A3E
		public static bool CapsLock
		{
			get
			{
				return Console.CapsLock;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00006845 File Offset: 0x00004A45
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x0000684C File Offset: 0x00004A4C
		public static int CursorLeft
		{
			get
			{
				return Console.CursorLeft;
			}
			set
			{
				Console.CursorLeft = value;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00006854 File Offset: 0x00004A54
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0000685B File Offset: 0x00004A5B
		public static int CursorSize
		{
			get
			{
				return Console.CursorSize;
			}
			set
			{
				Console.CursorSize = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00006863 File Offset: 0x00004A63
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x0000686A File Offset: 0x00004A6A
		public static int CursorTop
		{
			get
			{
				return Console.CursorTop;
			}
			set
			{
				Console.CursorTop = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x00006872 File Offset: 0x00004A72
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x00006879 File Offset: 0x00004A79
		public static bool CursorVisible
		{
			get
			{
				return Console.CursorVisible;
			}
			set
			{
				Console.CursorVisible = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00006881 File Offset: 0x00004A81
		public static TextWriter Error
		{
			get
			{
				return Console.Error;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00006888 File Offset: 0x00004A88
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00006899 File Offset: 0x00004A99
		public static Color ForegroundColor
		{
			get
			{
				return Console.colorManager_0.GetColor(Console.ForegroundColor);
			}
			set
			{
				Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(value);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x000068AB File Offset: 0x00004AAB
		public static TextReader In
		{
			get
			{
				return Console.In;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x000068B2 File Offset: 0x00004AB2
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x000068B9 File Offset: 0x00004AB9
		public static Encoding InputEncoding
		{
			get
			{
				return Console.InputEncoding;
			}
			set
			{
				Console.InputEncoding = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x000068C1 File Offset: 0x00004AC1
		public static bool IsErrorRedirected
		{
			get
			{
				return Console.IsErrorRedirected;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x000068C8 File Offset: 0x00004AC8
		public static bool IsInputRedirected
		{
			get
			{
				return Console.IsInputRedirected;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x000068CF File Offset: 0x00004ACF
		public static bool IsOutputRedirected
		{
			get
			{
				return Console.IsOutputRedirected;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x000068D6 File Offset: 0x00004AD6
		public static bool KeyAvailable
		{
			get
			{
				return Console.KeyAvailable;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x000068DD File Offset: 0x00004ADD
		public static int LargestWindowHeight
		{
			get
			{
				return Console.LargestWindowHeight;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x000068E4 File Offset: 0x00004AE4
		public static int LargestWindowWidth
		{
			get
			{
				return Console.LargestWindowWidth;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x000068EB File Offset: 0x00004AEB
		public static bool NumberLock
		{
			get
			{
				return Console.NumberLock;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x000068F2 File Offset: 0x00004AF2
		public static TextWriter Out
		{
			get
			{
				return Console.Out;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x000068F9 File Offset: 0x00004AF9
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00006900 File Offset: 0x00004B00
		public static Encoding OutputEncoding
		{
			get
			{
				return Console.OutputEncoding;
			}
			set
			{
				Console.OutputEncoding = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00006908 File Offset: 0x00004B08
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x0000690F File Offset: 0x00004B0F
		public static string Title
		{
			get
			{
				return Console.Title;
			}
			set
			{
				Console.Title = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00006917 File Offset: 0x00004B17
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x0000691E File Offset: 0x00004B1E
		public static bool TreatControlCAsInput
		{
			get
			{
				return Console.TreatControlCAsInput;
			}
			set
			{
				Console.TreatControlCAsInput = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00006926 File Offset: 0x00004B26
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0000692D File Offset: 0x00004B2D
		public static int WindowHeight
		{
			get
			{
				return Console.WindowHeight;
			}
			set
			{
				Console.WindowHeight = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00006935 File Offset: 0x00004B35
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0000693C File Offset: 0x00004B3C
		public static int WindowLeft
		{
			get
			{
				return Console.WindowLeft;
			}
			set
			{
				Console.WindowLeft = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00006944 File Offset: 0x00004B44
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0000694B File Offset: 0x00004B4B
		public static int WindowTop
		{
			get
			{
				return Console.WindowTop;
			}
			set
			{
				Console.WindowTop = value;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00006953 File Offset: 0x00004B53
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x0000695A File Offset: 0x00004B5A
		public static int WindowWidth
		{
			get
			{
				return Console.WindowWidth;
			}
			set
			{
				Console.WindowWidth = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600085C RID: 2140 RVA: 0x00020B18 File Offset: 0x0001ED18
		// (remove) Token: 0x0600085D RID: 2141 RVA: 0x00020B4C File Offset: 0x0001ED4C
		public static event ConsoleCancelEventHandler CancelKeyPress
		{
			[CompilerGenerated]
			add
			{
				ConsoleCancelEventHandler consoleCancelEventHandler = Console.consoleCancelEventHandler_0;
				ConsoleCancelEventHandler consoleCancelEventHandler2;
				do
				{
					consoleCancelEventHandler2 = consoleCancelEventHandler;
					ConsoleCancelEventHandler consoleCancelEventHandler3 = (ConsoleCancelEventHandler)Delegate.Combine(consoleCancelEventHandler2, value);
					consoleCancelEventHandler = Interlocked.CompareExchange<ConsoleCancelEventHandler>(ref Console.consoleCancelEventHandler_0, consoleCancelEventHandler3, consoleCancelEventHandler2);
				}
				while (consoleCancelEventHandler != consoleCancelEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ConsoleCancelEventHandler consoleCancelEventHandler = Console.consoleCancelEventHandler_0;
				ConsoleCancelEventHandler consoleCancelEventHandler2;
				do
				{
					consoleCancelEventHandler2 = consoleCancelEventHandler;
					ConsoleCancelEventHandler consoleCancelEventHandler3 = (ConsoleCancelEventHandler)Delegate.Remove(consoleCancelEventHandler2, value);
					consoleCancelEventHandler = Interlocked.CompareExchange<ConsoleCancelEventHandler>(ref Console.consoleCancelEventHandler_0, consoleCancelEventHandler3, consoleCancelEventHandler2);
				}
				while (consoleCancelEventHandler != consoleCancelEventHandler2);
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00020B80 File Offset: 0x0001ED80
		static Console()
		{
			bool flag = false;
			try
			{
				Console.dictionary_0 = new ColorMapper().GetBufferColors();
			}
			catch
			{
				flag = true;
			}
			Console.smethod_29(flag);
			Console.CancelKeyPress += Console.smethod_30;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00006962 File Offset: 0x00004B62
		public static void Write(bool value)
		{
			Console.Write(value);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0000696A File Offset: 0x00004B6A
		public static void Write(bool value, Color color)
		{
			Console.smethod_2<bool>(new Action<bool>(Console.Write), value, color);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0000697F File Offset: 0x00004B7F
		public static void WriteAlternating(bool value, ColorAlternator alternator)
		{
			Console.smethod_4<bool>(new Action<bool>(Console.Write), value, alternator);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00006994 File Offset: 0x00004B94
		public static void WriteStyled(bool value, StyleSheet styleSheet)
		{
			Console.smethod_6<bool>(Console.string_1, value, styleSheet);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000069A2 File Offset: 0x00004BA2
		public static void Write(char value)
		{
			Console.Write(value);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000069AA File Offset: 0x00004BAA
		public static void Write(char value, Color color)
		{
			Console.smethod_2<char>(new Action<char>(Console.Write), value, color);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x000069BF File Offset: 0x00004BBF
		public static void WriteAlternating(char value, ColorAlternator alternator)
		{
			Console.smethod_4<char>(new Action<char>(Console.Write), value, alternator);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000069D4 File Offset: 0x00004BD4
		public static void WriteStyled(char value, StyleSheet styleSheet)
		{
			Console.smethod_6<char>(Console.string_1, value, styleSheet);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000069E2 File Offset: 0x00004BE2
		public static void Write(char[] value)
		{
			Console.Write(value);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000069EA File Offset: 0x00004BEA
		public static void Write(char[] value, Color color)
		{
			Console.smethod_2<char[]>(new Action<char[]>(Console.Write), value, color);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000069FF File Offset: 0x00004BFF
		public static void WriteAlternating(char[] value, ColorAlternator alternator)
		{
			Console.smethod_4<char[]>(new Action<char[]>(Console.Write), value, alternator);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00006A14 File Offset: 0x00004C14
		public static void WriteStyled(char[] value, StyleSheet styleSheet)
		{
			Console.smethod_6<char[]>(Console.string_1, value, styleSheet);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00006A22 File Offset: 0x00004C22
		public static void Write(decimal value)
		{
			Console.Write(value);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00006A2A File Offset: 0x00004C2A
		public static void Write(decimal value, Color color)
		{
			Console.smethod_2<decimal>(new Action<decimal>(Console.Write), value, color);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00006A3F File Offset: 0x00004C3F
		public static void WriteAlternating(decimal value, ColorAlternator alternator)
		{
			Console.smethod_4<decimal>(new Action<decimal>(Console.Write), value, alternator);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00006A54 File Offset: 0x00004C54
		public static void WriteStyled(decimal value, StyleSheet styleSheet)
		{
			Console.smethod_6<decimal>(Console.string_1, value, styleSheet);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00006A62 File Offset: 0x00004C62
		public static void Write(double value)
		{
			Console.Write(value);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00006A6A File Offset: 0x00004C6A
		public static void Write(double value, Color color)
		{
			Console.smethod_2<double>(new Action<double>(Console.Write), value, color);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00006A7F File Offset: 0x00004C7F
		public static void WriteAlternating(double value, ColorAlternator alternator)
		{
			Console.smethod_4<double>(new Action<double>(Console.Write), value, alternator);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00006A94 File Offset: 0x00004C94
		public static void WriteStyled(double value, StyleSheet styleSheet)
		{
			Console.smethod_6<double>(Console.string_1, value, styleSheet);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00006AA2 File Offset: 0x00004CA2
		public static void Write(float value)
		{
			Console.Write(value);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00006AAA File Offset: 0x00004CAA
		public static void Write(float value, Color color)
		{
			Console.smethod_2<float>(new Action<float>(Console.Write), value, color);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00006ABF File Offset: 0x00004CBF
		public static void WriteAlternating(float value, ColorAlternator alternator)
		{
			Console.smethod_4<float>(new Action<float>(Console.Write), value, alternator);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00006AD4 File Offset: 0x00004CD4
		public static void WriteStyled(float value, StyleSheet styleSheet)
		{
			Console.smethod_6<float>(Console.string_1, value, styleSheet);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00006AE2 File Offset: 0x00004CE2
		public static void Write(int value)
		{
			Console.Write(value);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00006AEA File Offset: 0x00004CEA
		public static void Write(int value, Color color)
		{
			Console.smethod_2<int>(new Action<int>(Console.Write), value, color);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00006AFF File Offset: 0x00004CFF
		public static void WriteAlternating(int value, ColorAlternator alternator)
		{
			Console.smethod_4<int>(new Action<int>(Console.Write), value, alternator);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00006B14 File Offset: 0x00004D14
		public static void WriteStyled(int value, StyleSheet styleSheet)
		{
			Console.smethod_6<int>(Console.string_1, value, styleSheet);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00006B22 File Offset: 0x00004D22
		public static void Write(long value)
		{
			Console.Write(value);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00006B2A File Offset: 0x00004D2A
		public static void Write(long value, Color color)
		{
			Console.smethod_2<long>(new Action<long>(Console.Write), value, color);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00006B3F File Offset: 0x00004D3F
		public static void WriteAlternating(long value, ColorAlternator alternator)
		{
			Console.smethod_4<long>(new Action<long>(Console.Write), value, alternator);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00006B54 File Offset: 0x00004D54
		public static void WriteStyled(long value, StyleSheet styleSheet)
		{
			Console.smethod_6<long>(Console.string_1, value, styleSheet);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00006B62 File Offset: 0x00004D62
		public static void Write(object value)
		{
			Console.Write(value);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00006B6A File Offset: 0x00004D6A
		public static void Write(object value, Color color)
		{
			Console.smethod_2<object>(new Action<object>(Console.Write), value, color);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00006B7F File Offset: 0x00004D7F
		public static void WriteAlternating(object value, ColorAlternator alternator)
		{
			Console.smethod_4<object>(new Action<object>(Console.Write), value, alternator);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00006B94 File Offset: 0x00004D94
		public static void WriteStyled(object value, StyleSheet styleSheet)
		{
			Console.smethod_6<object>(Console.string_1, value, styleSheet);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00006BA2 File Offset: 0x00004DA2
		public static void Write(string value)
		{
			Console.Write(value);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00006BAA File Offset: 0x00004DAA
		public static void Write(string value, Color color)
		{
			Console.smethod_2<string>(new Action<string>(Console.Write), value, color);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00006BBF File Offset: 0x00004DBF
		public static void WriteAlternating(string value, ColorAlternator alternator)
		{
			Console.smethod_4<string>(new Action<string>(Console.Write), value, alternator);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00006BD4 File Offset: 0x00004DD4
		public static void WriteStyled(string value, StyleSheet styleSheet)
		{
			Console.smethod_6<string>(Console.string_1, value, styleSheet);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00006BE2 File Offset: 0x00004DE2
		public static void Write(uint value)
		{
			Console.Write(value);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00006BEA File Offset: 0x00004DEA
		public static void Write(uint value, Color color)
		{
			Console.smethod_2<uint>(new Action<uint>(Console.Write), value, color);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00006BFF File Offset: 0x00004DFF
		public static void WriteAlternating(uint value, ColorAlternator alternator)
		{
			Console.smethod_4<uint>(new Action<uint>(Console.Write), value, alternator);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00006C14 File Offset: 0x00004E14
		public static void WriteStyled(uint value, StyleSheet styleSheet)
		{
			Console.smethod_6<uint>(Console.string_1, value, styleSheet);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00006C22 File Offset: 0x00004E22
		public static void Write(ulong value)
		{
			Console.Write(value);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00006C2A File Offset: 0x00004E2A
		public static void Write(ulong value, Color color)
		{
			Console.smethod_2<ulong>(new Action<ulong>(Console.Write), value, color);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00006C3F File Offset: 0x00004E3F
		public static void WriteAlternating(ulong value, ColorAlternator alternator)
		{
			Console.smethod_4<ulong>(new Action<ulong>(Console.Write), value, alternator);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00006C54 File Offset: 0x00004E54
		public static void WriteStyled(ulong value, StyleSheet styleSheet)
		{
			Console.smethod_6<ulong>(Console.string_1, value, styleSheet);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00006C62 File Offset: 0x00004E62
		public static void Write(string format, object arg0)
		{
			Console.Write(format, arg0);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00006C6B File Offset: 0x00004E6B
		public static void Write(string format, object arg0, Color color)
		{
			Console.smethod_10<string, object>(new Action<string, object>(Console.Write), format, arg0, color);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00006C81 File Offset: 0x00004E81
		public static void WriteAlternating(string format, object arg0, ColorAlternator alternator)
		{
			Console.smethod_11<string, object>(new Action<string, object>(Console.Write), format, arg0, alternator);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00006C97 File Offset: 0x00004E97
		public static void WriteStyled(string format, object arg0, StyleSheet styleSheet)
		{
			Console.smethod_12<string, object>(Console.string_1, format, arg0, styleSheet);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00006CA6 File Offset: 0x00004EA6
		public static void WriteFormatted(string format, object arg0, Color styledColor, Color defaultColor)
		{
			Console.smethod_13<string, object>(Console.string_1, format, arg0, styledColor, defaultColor);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00006CB6 File Offset: 0x00004EB6
		public static void WriteFormatted(string format, Formatter arg0, Color defaultColor)
		{
			Console.smethod_14<string>(Console.string_1, format, arg0, defaultColor);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00006CC5 File Offset: 0x00004EC5
		public static void Write(string format, params object[] args)
		{
			Console.Write(format, args);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00006CCE File Offset: 0x00004ECE
		public static void Write(string format, Color color, params object[] args)
		{
			Console.smethod_10<string, object[]>(new Action<string, object[]>(Console.Write), format, args, color);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public static void WriteAlternating(string format, ColorAlternator alternator, params object[] args)
		{
			Console.smethod_11<string, object[]>(new Action<string, object[]>(Console.Write), format, args, alternator);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00006CFA File Offset: 0x00004EFA
		public static void WriteStyled(StyleSheet styleSheet, string format, params object[] args)
		{
			Console.smethod_12<string, object[]>(Console.string_1, format, args, styleSheet);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00006D09 File Offset: 0x00004F09
		public static void WriteFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
		{
			Console.smethod_13<string, object[]>(Console.string_1, format, args, styledColor, defaultColor);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00006D19 File Offset: 0x00004F19
		public static void WriteFormatted(string format, Color defaultColor, params Formatter[] args)
		{
			Console.smethod_25<string>(Console.string_1, format, args, defaultColor);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00006D28 File Offset: 0x00004F28
		public static void Write(char[] buffer, int index, int count)
		{
			Console.Write(buffer, index, count);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00006D32 File Offset: 0x00004F32
		public static void Write(char[] buffer, int index, int count, Color color)
		{
			Console.smethod_3(new Action<string>(Console.Write), buffer, index, count, color);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00006D49 File Offset: 0x00004F49
		public static void WriteAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
		{
			Console.smethod_5(new Action<string>(Console.Write), buffer, index, count, alternator);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00006D60 File Offset: 0x00004F60
		public static void WriteStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
		{
			Console.smethod_9(Console.string_1, buffer, index, count, styleSheet);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00006D70 File Offset: 0x00004F70
		public static void Write(string format, object arg0, object arg1)
		{
			Console.Write(format, arg0, arg1);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00006D7A File Offset: 0x00004F7A
		public static void Write(string format, object arg0, object arg1, Color color)
		{
			Console.smethod_15<string, object>(new Action<string, object, object>(Console.Write), format, arg0, arg1, color);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00006D91 File Offset: 0x00004F91
		public static void WriteAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
		{
			Console.smethod_16<string, object>(new Action<string, object, object>(Console.Write), format, arg0, arg1, alternator);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public static void WriteStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
		{
			Console.smethod_17<string, object>(Console.string_1, format, arg0, arg1, styleSheet);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public static void WriteFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
		{
			Console.smethod_18<string, object>(Console.string_1, format, arg0, arg1, styledColor, defaultColor);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00006DCA File Offset: 0x00004FCA
		public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
		{
			Console.smethod_19<string>(Console.string_1, format, arg0, arg1, defaultColor);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00006DDA File Offset: 0x00004FDA
		public static void Write(string format, object arg0, object arg1, object arg2)
		{
			Console.Write(format, arg0, arg1, arg2);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00006DE5 File Offset: 0x00004FE5
		public static void Write(string format, object arg0, object arg1, object arg2, Color color)
		{
			Console.smethod_20<string, object>(new Action<string, object, object, object>(Console.Write), format, arg0, arg1, arg2, color);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00006DFE File Offset: 0x00004FFE
		public static void WriteAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
		{
			Console.smethod_21<string, object>(new Action<string, object, object, object>(Console.Write), format, arg0, arg1, arg2, alternator);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00006E17 File Offset: 0x00005017
		public static void WriteStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
		{
			Console.smethod_22<string, object>(Console.string_1, format, arg0, arg1, arg2, styleSheet);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00006E29 File Offset: 0x00005029
		public static void WriteFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
		{
			Console.smethod_23<string, object>(Console.string_1, format, arg0, arg1, arg2, styledColor, defaultColor);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00006E3D File Offset: 0x0000503D
		public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
		{
			Console.smethod_24<string>(Console.string_1, format, arg0, arg1, arg2, defaultColor);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00006E4F File Offset: 0x0000504F
		public static void Write(string format, object arg0, object arg1, object arg2, object arg3)
		{
			Console.Write(format, new object[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00006E6E File Offset: 0x0000506E
		public static void Write(string format, object arg0, object arg1, object arg2, object arg3, Color color)
		{
			Console.smethod_10<string, object[]>(new Action<string, object[]>(Console.Write), format, new object[] { arg0, arg1, arg2, arg3 }, color);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00006E9B File Offset: 0x0000509B
		public static void WriteAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
		{
			Console.smethod_11<string, object[]>(new Action<string, object[]>(Console.Write), format, new object[] { arg0, arg1, arg2, arg3 }, alternator);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00006EC8 File Offset: 0x000050C8
		public static void WriteStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
		{
			Console.smethod_12<string, object[]>(Console.string_1, format, new object[] { arg0, arg1, arg2, arg3 }, styleSheet);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00006EEE File Offset: 0x000050EE
		public static void WriteFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
		{
			Console.smethod_13<string, object[]>(Console.string_1, format, new object[] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00006F16 File Offset: 0x00005116
		public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
		{
			Console.smethod_25<string>(Console.string_1, format, new Formatter[] { arg0, arg1, arg2, arg3 }, defaultColor);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00006F3C File Offset: 0x0000513C
		public static void WriteLine()
		{
			Console.WriteLine();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00006F43 File Offset: 0x00005143
		public static void WriteLineAlternating(ColorAlternator alternator)
		{
			Console.smethod_4<string>(new Action<string>(Console.Write), Console.string_0, alternator);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00006F5C File Offset: 0x0000515C
		public static void WriteLineStyled(StyleSheet styleSheet)
		{
			Console.smethod_6<string>(Console.string_1, Console.string_0, styleSheet);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00006F6E File Offset: 0x0000516E
		public static void WriteLine(bool value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00006F76 File Offset: 0x00005176
		public static void WriteLine(bool value, Color color)
		{
			Console.smethod_2<bool>(new Action<bool>(Console.WriteLine), value, color);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00006F8B File Offset: 0x0000518B
		public static void WriteLineAlternating(bool value, ColorAlternator alternator)
		{
			Console.smethod_4<bool>(new Action<bool>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00006FA0 File Offset: 0x000051A0
		public static void WriteLineStyled(bool value, StyleSheet styleSheet)
		{
			Console.smethod_6<bool>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00006FAE File Offset: 0x000051AE
		public static void WriteLine(char value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00006FB6 File Offset: 0x000051B6
		public static void WriteLine(char value, Color color)
		{
			Console.smethod_2<char>(new Action<char>(Console.WriteLine), value, color);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00006FCB File Offset: 0x000051CB
		public static void WriteLineAlternating(char value, ColorAlternator alternator)
		{
			Console.smethod_4<char>(new Action<char>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00006FE0 File Offset: 0x000051E0
		public static void WriteLineStyled(char value, StyleSheet styleSheet)
		{
			Console.smethod_6<char>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00006FEE File Offset: 0x000051EE
		public static void WriteLine(char[] value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00006FF6 File Offset: 0x000051F6
		public static void WriteLine(char[] value, Color color)
		{
			Console.smethod_2<char[]>(new Action<char[]>(Console.WriteLine), value, color);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0000700B File Offset: 0x0000520B
		public static void WriteLineAlternating(char[] value, ColorAlternator alternator)
		{
			Console.smethod_4<char[]>(new Action<char[]>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00007020 File Offset: 0x00005220
		public static void WriteLineStyled(char[] value, StyleSheet styleSheet)
		{
			Console.smethod_6<char[]>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0000702E File Offset: 0x0000522E
		public static void WriteLine(decimal value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00007036 File Offset: 0x00005236
		public static void WriteLine(decimal value, Color color)
		{
			Console.smethod_2<decimal>(new Action<decimal>(Console.WriteLine), value, color);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0000704B File Offset: 0x0000524B
		public static void WriteLineAlternating(decimal value, ColorAlternator alternator)
		{
			Console.smethod_4<decimal>(new Action<decimal>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00007060 File Offset: 0x00005260
		public static void WriteLineStyled(decimal value, StyleSheet styleSheet)
		{
			Console.smethod_6<decimal>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0000706E File Offset: 0x0000526E
		public static void WriteLine(double value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00007076 File Offset: 0x00005276
		public static void WriteLine(double value, Color color)
		{
			Console.smethod_2<double>(new Action<double>(Console.WriteLine), value, color);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0000708B File Offset: 0x0000528B
		public static void WriteLineAlternating(double value, ColorAlternator alternator)
		{
			Console.smethod_4<double>(new Action<double>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000070A0 File Offset: 0x000052A0
		public static void WriteLineStyled(double value, StyleSheet styleSheet)
		{
			Console.smethod_6<double>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x000070AE File Offset: 0x000052AE
		public static void WriteLine(float value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000070B6 File Offset: 0x000052B6
		public static void WriteLine(float value, Color color)
		{
			Console.smethod_2<float>(new Action<float>(Console.WriteLine), value, color);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000070CB File Offset: 0x000052CB
		public static void WriteLineAlternating(float value, ColorAlternator alternator)
		{
			Console.smethod_4<float>(new Action<float>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000070E0 File Offset: 0x000052E0
		public static void WriteLineStyled(float value, StyleSheet styleSheet)
		{
			Console.smethod_6<float>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000070EE File Offset: 0x000052EE
		public static void WriteLine(int value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000070F6 File Offset: 0x000052F6
		public static void WriteLine(int value, Color color)
		{
			Console.smethod_2<int>(new Action<int>(Console.WriteLine), value, color);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0000710B File Offset: 0x0000530B
		public static void WriteLineAlternating(int value, ColorAlternator alternator)
		{
			Console.smethod_4<int>(new Action<int>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00007120 File Offset: 0x00005320
		public static void WriteLineStyled(int value, StyleSheet styleSheet)
		{
			Console.smethod_6<int>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0000712E File Offset: 0x0000532E
		public static void WriteLine(long value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00007136 File Offset: 0x00005336
		public static void WriteLine(long value, Color color)
		{
			Console.smethod_2<long>(new Action<long>(Console.WriteLine), value, color);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0000714B File Offset: 0x0000534B
		public static void WriteLineAlternating(long value, ColorAlternator alternator)
		{
			Console.smethod_4<long>(new Action<long>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00007160 File Offset: 0x00005360
		public static void WriteLineStyled(long value, StyleSheet styleSheet)
		{
			Console.smethod_6<long>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0000716E File Offset: 0x0000536E
		public static void WriteLine(object value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00007176 File Offset: 0x00005376
		public static void WriteLine(object value, Color color)
		{
			Console.smethod_2<object>(new Action<object>(Console.WriteLine), value, color);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0000718B File Offset: 0x0000538B
		public static void WriteLineAlternating(object value, ColorAlternator alternator)
		{
			Console.smethod_4<object>(new Action<object>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000071A0 File Offset: 0x000053A0
		public static void WriteLineStyled(StyledString value, StyleSheet styleSheet)
		{
			Console.smethod_7(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000071AE File Offset: 0x000053AE
		public static void WriteLine(string value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000071B6 File Offset: 0x000053B6
		public static void WriteLine(string value, Color color)
		{
			Console.smethod_2<string>(new Action<string>(Console.WriteLine), value, color);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000071CB File Offset: 0x000053CB
		public static void WriteLineAlternating(string value, ColorAlternator alternator)
		{
			Console.smethod_4<string>(new Action<string>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000071E0 File Offset: 0x000053E0
		public static void WriteLineStyled(string value, StyleSheet styleSheet)
		{
			Console.smethod_6<string>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000071EE File Offset: 0x000053EE
		public static void WriteLine(uint value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000071F6 File Offset: 0x000053F6
		public static void WriteLine(uint value, Color color)
		{
			Console.smethod_2<uint>(new Action<uint>(Console.WriteLine), value, color);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0000720B File Offset: 0x0000540B
		public static void WriteLineAlternating(uint value, ColorAlternator alternator)
		{
			Console.smethod_4<uint>(new Action<uint>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00007220 File Offset: 0x00005420
		public static void WriteLineStyled(uint value, StyleSheet styleSheet)
		{
			Console.smethod_6<uint>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0000722E File Offset: 0x0000542E
		public static void WriteLine(ulong value)
		{
			Console.WriteLine(value);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00007236 File Offset: 0x00005436
		public static void WriteLine(ulong value, Color color)
		{
			Console.smethod_2<ulong>(new Action<ulong>(Console.WriteLine), value, color);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0000724B File Offset: 0x0000544B
		public static void WriteLineAlternating(ulong value, ColorAlternator alternator)
		{
			Console.smethod_4<ulong>(new Action<ulong>(Console.WriteLine), value, alternator);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00007260 File Offset: 0x00005460
		public static void WriteLineStyled(ulong value, StyleSheet styleSheet)
		{
			Console.smethod_6<ulong>(Console.string_0, value, styleSheet);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0000726E File Offset: 0x0000546E
		public static void WriteLine(string format, object arg0)
		{
			Console.WriteLine(format, arg0);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00007277 File Offset: 0x00005477
		public static void WriteLine(string format, object arg0, Color color)
		{
			Console.smethod_10<string, object>(new Action<string, object>(Console.WriteLine), format, arg0, color);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0000728D File Offset: 0x0000548D
		public static void WriteLineAlternating(string format, object arg0, ColorAlternator alternator)
		{
			Console.smethod_11<string, object>(new Action<string, object>(Console.WriteLine), format, arg0, alternator);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000072A3 File Offset: 0x000054A3
		public static void WriteLineStyled(string format, object arg0, StyleSheet styleSheet)
		{
			Console.smethod_12<string, object>(Console.string_0, format, arg0, styleSheet);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000072B2 File Offset: 0x000054B2
		public static void WriteLineFormatted(string format, object arg0, Color styledColor, Color defaultColor)
		{
			Console.smethod_13<string, object>(Console.string_0, format, arg0, styledColor, defaultColor);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000072C2 File Offset: 0x000054C2
		public static void WriteLineFormatted(string format, Formatter arg0, Color defaultColor)
		{
			Console.smethod_14<string>(Console.string_0, format, arg0, defaultColor);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000072D1 File Offset: 0x000054D1
		public static void WriteLine(string format, params object[] args)
		{
			Console.WriteLine(format, args);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000072DA File Offset: 0x000054DA
		public static void WriteLine(string format, Color color, params object[] args)
		{
			Console.smethod_10<string, object[]>(new Action<string, object[]>(Console.WriteLine), format, args, color);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000072F0 File Offset: 0x000054F0
		public static void WriteLineAlternating(string format, ColorAlternator alternator, params object[] args)
		{
			Console.smethod_11<string, object[]>(new Action<string, object[]>(Console.WriteLine), format, args, alternator);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00007306 File Offset: 0x00005506
		public static void WriteLineStyled(StyleSheet styleSheet, string format, params object[] args)
		{
			Console.smethod_12<string, object[]>(Console.string_0, format, args, styleSheet);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00007315 File Offset: 0x00005515
		public static void WriteLineFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
		{
			Console.smethod_13<string, object[]>(Console.string_0, format, args, styledColor, defaultColor);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00007325 File Offset: 0x00005525
		public static void WriteLineFormatted(string format, Color defaultColor, params Formatter[] args)
		{
			Console.smethod_25<string>(Console.string_0, format, args, defaultColor);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00007334 File Offset: 0x00005534
		public static void WriteLine(char[] buffer, int index, int count)
		{
			Console.WriteLine(buffer, index, count);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0000733E File Offset: 0x0000553E
		public static void WriteLine(char[] buffer, int index, int count, Color color)
		{
			Console.smethod_3(new Action<string>(Console.WriteLine), buffer, index, count, color);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00007355 File Offset: 0x00005555
		public static void WriteLineAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
		{
			Console.smethod_5(new Action<string>(Console.WriteLine), buffer, index, count, alternator);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0000736C File Offset: 0x0000556C
		public static void WriteLineStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
		{
			Console.smethod_9(Console.string_0, buffer, index, count, styleSheet);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0000737C File Offset: 0x0000557C
		public static void WriteLine(string format, object arg0, object arg1)
		{
			Console.WriteLine(format, arg0, arg1);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00007386 File Offset: 0x00005586
		public static void WriteLine(string format, object arg0, object arg1, Color color)
		{
			Console.smethod_15<string, object>(new Action<string, object, object>(Console.WriteLine), format, arg0, arg1, color);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0000739D File Offset: 0x0000559D
		public static void WriteLineAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
		{
			Console.smethod_16<string, object>(new Action<string, object, object>(Console.WriteLine), format, arg0, arg1, alternator);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000073B4 File Offset: 0x000055B4
		public static void WriteLineStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
		{
			Console.smethod_17<string, object>(Console.string_0, format, arg0, arg1, styleSheet);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000073C4 File Offset: 0x000055C4
		public static void WriteLineFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
		{
			Console.smethod_18<string, object>(Console.string_0, format, arg0, arg1, styledColor, defaultColor);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000073D6 File Offset: 0x000055D6
		public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
		{
			Console.smethod_19<string>(Console.string_0, format, arg0, arg1, defaultColor);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000073E6 File Offset: 0x000055E6
		public static void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			Console.WriteLine(format, arg0, arg1, arg2);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000073F1 File Offset: 0x000055F1
		public static void WriteLine(string format, object arg0, object arg1, object arg2, Color color)
		{
			Console.smethod_20<string, object>(new Action<string, object, object, object>(Console.WriteLine), format, arg0, arg1, arg2, color);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000740A File Offset: 0x0000560A
		public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
		{
			Console.smethod_21<string, object>(new Action<string, object, object, object>(Console.WriteLine), format, arg0, arg1, arg2, alternator);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00007423 File Offset: 0x00005623
		public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
		{
			Console.smethod_22<string, object>(Console.string_0, format, arg0, arg1, arg2, styleSheet);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00007435 File Offset: 0x00005635
		public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
		{
			Console.smethod_23<string, object>(Console.string_0, format, arg0, arg1, arg2, styledColor, defaultColor);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00007449 File Offset: 0x00005649
		public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
		{
			Console.smethod_24<string>(Console.string_0, format, arg0, arg1, arg2, defaultColor);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0000745B File Offset: 0x0000565B
		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
		{
			Console.WriteLine(format, new object[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000747A File Offset: 0x0000567A
		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, Color color)
		{
			Console.smethod_10<string, object[]>(new Action<string, object[]>(Console.WriteLine), format, new object[] { arg0, arg1, arg2, arg3 }, color);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000074A7 File Offset: 0x000056A7
		public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
		{
			Console.smethod_11<string, object[]>(new Action<string, object[]>(Console.WriteLine), format, new object[] { arg0, arg1, arg2, arg3 }, alternator);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000074D4 File Offset: 0x000056D4
		public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
		{
			Console.smethod_12<string, object[]>(Console.string_0, format, new object[] { arg0, arg1, arg2, arg3 }, styleSheet);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000074FA File Offset: 0x000056FA
		public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
		{
			Console.smethod_13<string, object[]>(Console.string_0, format, new object[] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00007522 File Offset: 0x00005722
		public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
		{
			Console.smethod_25<string>(Console.string_0, format, new Formatter[] { arg0, arg1, arg2, arg3 }, defaultColor);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00007548 File Offset: 0x00005748
		public static void WriteAscii(string value)
		{
			Console.WriteAscii(value, null);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00007551 File Offset: 0x00005751
		public static void WriteAscii(string value, FigletFont font)
		{
			Console.WriteLine(Console.smethod_27(font).ToAscii(value).ConcreteValue);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00007569 File Offset: 0x00005769
		public static void WriteAscii(string value, Color color)
		{
			Console.WriteAscii(value, null, color);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00007573 File Offset: 0x00005773
		public static void WriteAscii(string value, FigletFont font, Color color)
		{
			Console.WriteLine(Console.smethod_27(font).ToAscii(value).ConcreteValue, color);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0000758C File Offset: 0x0000578C
		public static void WriteAsciiAlternating(string value, ColorAlternator alternator)
		{
			Console.WriteAsciiAlternating(value, null, alternator);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00020D3C File Offset: 0x0001EF3C
		public static void WriteAsciiAlternating(string value, FigletFont font, ColorAlternator alternator)
		{
			string[] array = Console.smethod_27(font).ToAscii(value).ConcreteValue.Split(new char[] { '\n' });
			for (int i = 0; i < array.Length; i++)
			{
				Console.WriteLineAlternating(array[i], alternator);
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00007596 File Offset: 0x00005796
		public static void WriteAsciiStyled(string value, StyleSheet styleSheet)
		{
			Console.WriteAsciiStyled(value, null, styleSheet);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000075A0 File Offset: 0x000057A0
		public static void WriteAsciiStyled(string value, FigletFont font, StyleSheet styleSheet)
		{
			Console.WriteLineStyled(Console.smethod_27(font).ToAscii(value), styleSheet);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000075B4 File Offset: 0x000057B4
		public static void WriteWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 16)
		{
			Console.smethod_26<T>(new Action<object, Color>(Console.Write), input, startColor, endColor, maxColorsInGradient);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000075CB File Offset: 0x000057CB
		public static void WriteLineWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 16)
		{
			Console.smethod_26<T>(new Action<object, Color>(Console.WriteLine), input, startColor, endColor, maxColorsInGradient);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000075E2 File Offset: 0x000057E2
		public static int Read()
		{
			return Console.Read();
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000075E9 File Offset: 0x000057E9
		public static ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey();
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000075F0 File Offset: 0x000057F0
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return Console.ReadKey(intercept);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000075F8 File Offset: 0x000057F8
		public static string ReadLine()
		{
			return Console.ReadLine();
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000075FF File Offset: 0x000057FF
		public static void ResetColor()
		{
			Console.ResetColor();
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00007606 File Offset: 0x00005806
		public static void SetBufferSize(int width, int height)
		{
			Console.SetBufferSize(width, height);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0000760F File Offset: 0x0000580F
		public static void SetCursorPosition(int left, int top)
		{
			Console.SetCursorPosition(left, top);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00007618 File Offset: 0x00005818
		public static void SetError(TextWriter newError)
		{
			Console.SetError(newError);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00007620 File Offset: 0x00005820
		public static void SetIn(TextReader newIn)
		{
			Console.SetIn(newIn);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00007628 File Offset: 0x00005828
		public static void SetOut(TextWriter newOut)
		{
			Console.SetOut(newOut);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00007630 File Offset: 0x00005830
		public static void SetWindowPosition(int left, int top)
		{
			Console.SetWindowPosition(left, top);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00007639 File Offset: 0x00005839
		public static void SetWindowSize(int width, int height)
		{
			Console.SetWindowSize(width, height);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00007642 File Offset: 0x00005842
		public static Stream OpenStandardError()
		{
			return Console.OpenStandardError();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00007649 File Offset: 0x00005849
		public static Stream OpenStandardError(int bufferSize)
		{
			return Console.OpenStandardError(bufferSize);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00007651 File Offset: 0x00005851
		public static Stream OpenStandardInput()
		{
			return Console.OpenStandardInput();
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00007658 File Offset: 0x00005858
		public static Stream OpenStandardInput(int bufferSize)
		{
			return Console.OpenStandardInput(bufferSize);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00007660 File Offset: 0x00005860
		public static Stream OpenStandardOutput()
		{
			return Console.OpenStandardOutput();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00007667 File Offset: 0x00005867
		public static Stream OpenStandardOutput(int bufferSize)
		{
			return Console.OpenStandardOutput(bufferSize);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0000766F File Offset: 0x0000586F
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00020D84 File Offset: 0x0001EF84
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000767E File Offset: 0x0000587E
		public static void Clear()
		{
			Console.Clear();
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		public static void ReplaceAllColorsWithDefaults()
		{
			Console.colorStore_0 = Console.smethod_28();
			Console.colorManagerFactory_0 = new ColorManagerFactory();
			Console.colorManager_0 = Console.colorManagerFactory_0.GetManager(Console.colorStore_0, 16, 1, Console.colorManager_0.IsInCompatibilityMode);
			if (!Console.colorManager_0.IsInCompatibilityMode)
			{
				new ColorMapper().SetBatchBufferColors(Console.dictionary_0);
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00007685 File Offset: 0x00005885
		public static void ReplaceColor(Color oldColor, Color newColor)
		{
			Console.colorManager_0.ReplaceColor(oldColor, newColor);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00007693 File Offset: 0x00005893
		public static void Beep(int frequency, int duration)
		{
			Console.Beep(frequency, duration);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0000769C File Offset: 0x0000589C
		private static void smethod_30(object sender, ConsoleCancelEventArgs e)
		{
			Console.consoleCancelEventHandler_0(sender, e);
		}

		// Token: 0x0400069A RID: 1690
		private static ColorStore colorStore_0;

		// Token: 0x0400069B RID: 1691
		private static ColorManagerFactory colorManagerFactory_0;

		// Token: 0x0400069C RID: 1692
		private static ColorManager colorManager_0;

		// Token: 0x0400069D RID: 1693
		private static Dictionary<string, COLORREF> dictionary_0;

		// Token: 0x0400069E RID: 1694
		private const int int_0 = 16;

		// Token: 0x0400069F RID: 1695
		private const int int_1 = 1;

		// Token: 0x040006A0 RID: 1696
		private static readonly string string_0 = "\r\n";

		// Token: 0x040006A1 RID: 1697
		private static readonly string string_1 = "";

		// Token: 0x040006A2 RID: 1698
		[CompilerGenerated]
		private static readonly TaskQueue taskQueue_0;

		// Token: 0x040006A3 RID: 1699
		private static readonly Color color_0 = Color.FromArgb(0, 0, 0);

		// Token: 0x040006A4 RID: 1700
		private static readonly Color color_1 = Color.FromArgb(0, 0, 255);

		// Token: 0x040006A5 RID: 1701
		private static readonly Color color_2 = Color.FromArgb(0, 255, 255);

		// Token: 0x040006A6 RID: 1702
		private static readonly Color color_3 = Color.FromArgb(0, 0, 128);

		// Token: 0x040006A7 RID: 1703
		private static readonly Color color_4 = Color.FromArgb(0, 128, 128);

		// Token: 0x040006A8 RID: 1704
		private static readonly Color color_5 = Color.FromArgb(128, 128, 128);

		// Token: 0x040006A9 RID: 1705
		private static readonly Color color_6 = Color.FromArgb(0, 128, 0);

		// Token: 0x040006AA RID: 1706
		private static readonly Color color_7 = Color.FromArgb(128, 0, 128);

		// Token: 0x040006AB RID: 1707
		private static readonly Color color_8 = Color.FromArgb(128, 0, 0);

		// Token: 0x040006AC RID: 1708
		private static readonly Color color_9 = Color.FromArgb(128, 128, 0);

		// Token: 0x040006AD RID: 1709
		private static readonly Color color_10 = Color.FromArgb(192, 192, 192);

		// Token: 0x040006AE RID: 1710
		private static readonly Color color_11 = Color.FromArgb(0, 255, 0);

		// Token: 0x040006AF RID: 1711
		private static readonly Color color_12 = Color.FromArgb(255, 0, 255);

		// Token: 0x040006B0 RID: 1712
		private static readonly Color color_13 = Color.FromArgb(255, 0, 0);

		// Token: 0x040006B1 RID: 1713
		private static readonly Color color_14 = Color.FromArgb(255, 255, 255);

		// Token: 0x040006B2 RID: 1714
		private static readonly Color color_15 = Color.FromArgb(255, 255, 0);

		// Token: 0x040006B3 RID: 1715
		[CompilerGenerated]
		private static ConsoleCancelEventHandler consoleCancelEventHandler_0 = new ConsoleCancelEventHandler(Console.Class15.<>9.method_0);

		// Token: 0x020000E8 RID: 232
		[CompilerGenerated]
		[Serializable]
		private sealed class Class15
		{
			// Token: 0x06000929 RID: 2345 RVA: 0x000076AA File Offset: 0x000058AA
			// Note: this type is marked as 'beforefieldinit'.
			static Class15()
			{
			}

			// Token: 0x0600092A RID: 2346 RVA: 0x00002116 File Offset: 0x00000316
			public Class15()
			{
			}

			// Token: 0x0600092B RID: 2347 RVA: 0x000076B6 File Offset: 0x000058B6
			internal void method_0(object sender, ConsoleCancelEventArgs e)
			{
			}

			// Token: 0x040006B4 RID: 1716
			public static readonly Console.Class15 <>9 = new Console.Class15();
		}

		// Token: 0x020000E9 RID: 233
		[CompilerGenerated]
		[Serializable]
		private sealed class Class16<T>
		{
			// Token: 0x0600092C RID: 2348 RVA: 0x000076B8 File Offset: 0x000058B8
			// Note: this type is marked as 'beforefieldinit'.
			static Class16()
			{
			}

			// Token: 0x0600092D RID: 2349 RVA: 0x00002116 File Offset: 0x00000316
			public Class16()
			{
			}

			// Token: 0x0600092E RID: 2350 RVA: 0x000076C4 File Offset: 0x000058C4
			internal object method_0(Formatter formatter_0)
			{
				return formatter_0.Target;
			}

			// Token: 0x0600092F RID: 2351 RVA: 0x000076CC File Offset: 0x000058CC
			internal Color method_1(Formatter formatter_0)
			{
				return formatter_0.Color;
			}

			// Token: 0x040006B5 RID: 1717
			public static readonly Console.Class16<T> <>9 = new Console.Class16<T>();

			// Token: 0x040006B6 RID: 1718
			public static Func<Formatter, object> <>9__36_0;

			// Token: 0x040006B7 RID: 1719
			public static Func<Formatter, Color> <>9__36_1;
		}

		// Token: 0x020000EA RID: 234
		[CompilerGenerated]
		private sealed class Class17
		{
			// Token: 0x06000930 RID: 2352 RVA: 0x00002116 File Offset: 0x00000316
			public Class17()
			{
			}

			// Token: 0x06000931 RID: 2353 RVA: 0x00020E04 File Offset: 0x0001F004
			internal Task method_0()
			{
				TaskFactory factory = Task.Factory;
				Action action;
				if ((action = this.action_0) == null)
				{
					action = (this.action_0 = new Action(this.method_1));
				}
				return factory.StartNew(action);
			}

			// Token: 0x06000932 RID: 2354 RVA: 0x00020E3C File Offset: 0x0001F03C
			internal void method_1()
			{
				ConsoleColor foregroundColor = Console.ForegroundColor;
				int num = 1;
				foreach (KeyValuePair<string, Color> keyValuePair in this.ienumerable_0)
				{
					Console.ForegroundColor = Console.colorManager_0.GetConsoleColor(keyValuePair.Value);
					if (num == this.ienumerable_0.Count<KeyValuePair<string, Color>>())
					{
						Console.Write(keyValuePair.Key + this.string_0);
					}
					else
					{
						Console.Write(keyValuePair.Key);
					}
					num++;
				}
				Console.ForegroundColor = foregroundColor;
			}

			// Token: 0x040006B8 RID: 1720
			public IEnumerable<KeyValuePair<string, Color>> ienumerable_0;

			// Token: 0x040006B9 RID: 1721
			public string string_0;

			// Token: 0x040006BA RID: 1722
			public Action action_0;
		}

		// Token: 0x020000EB RID: 235
		[CompilerGenerated]
		private static class Class18<T, U>
		{
			// Token: 0x040006BB RID: 1723
			public static CallSite<Func<CallSite, Type, string, object, object>> callSite_0;

			// Token: 0x040006BC RID: 1724
			public static CallSite<Func<CallSite, object, string>> callSite_1;
		}

		// Token: 0x020000EC RID: 236
		[CompilerGenerated]
		private static class Class19<T, U>
		{
			// Token: 0x040006BD RID: 1725
			public static CallSite<Func<CallSite, Type, string, object, object>> callSite_0;

			// Token: 0x040006BE RID: 1726
			public static CallSite<Func<CallSite, object, string>> callSite_1;
		}

		// Token: 0x020000ED RID: 237
		[CompilerGenerated]
		private static class Class20<T, U>
		{
			// Token: 0x040006BF RID: 1727
			public static CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>> callSite_0;

			// Token: 0x040006C0 RID: 1728
			public static CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>> callSite_1;
		}

		// Token: 0x020000EE RID: 238
		[CompilerGenerated]
		private static class Class21<T, U>
		{
			// Token: 0x040006C1 RID: 1729
			public static CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>> callSite_0;

			// Token: 0x040006C2 RID: 1730
			public static CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>> callSite_1;
		}

		// Token: 0x020000EF RID: 239
		[CompilerGenerated]
		private static class Class22<T, U>
		{
			// Token: 0x040006C3 RID: 1731
			public static CallSite<Func<CallSite, TextFormatter, string, object, Color[], object>> callSite_0;

			// Token: 0x040006C4 RID: 1732
			public static CallSite<Func<CallSite, object, List<KeyValuePair<string, Color>>>> callSite_1;
		}
	}
}
