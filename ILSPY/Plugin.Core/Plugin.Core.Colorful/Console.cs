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

namespace Plugin.Core.Colorful;

public static class Console
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class15
	{
		public static readonly Class15 _003C_003E9 = new Class15();

		internal void method_0(object sender, ConsoleCancelEventArgs e)
		{
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class Class16<T>
	{
		public static readonly Class16<T> _003C_003E9 = new Class16<T>();

		public static Func<Formatter, object> _003C_003E9__36_0;

		public static Func<Formatter, Color> _003C_003E9__36_1;

		internal object method_0(Formatter formatter_0)
		{
			return formatter_0.Target;
		}

		internal Color method_1(Formatter formatter_0)
		{
			return formatter_0.Color;
		}
	}

	[CompilerGenerated]
	private sealed class Class17
	{
		public IEnumerable<KeyValuePair<string, Color>> ienumerable_0;

		public string string_0;

		public Action action_0;

		internal Task method_0()
		{
			return Task.Factory.StartNew(delegate
			{
				ConsoleColor foregroundColor = System.Console.ForegroundColor;
				int num = 1;
				foreach (KeyValuePair<string, Color> item in ienumerable_0)
				{
					System.Console.ForegroundColor = colorManager_0.GetConsoleColor(item.Value);
					if (num == ienumerable_0.Count())
					{
						System.Console.Write(item.Key + string_0);
					}
					else
					{
						System.Console.Write(item.Key);
					}
					num++;
				}
				System.Console.ForegroundColor = foregroundColor;
			});
		}

		internal void method_1()
		{
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			int num = 1;
			foreach (KeyValuePair<string, Color> item in ienumerable_0)
			{
				System.Console.ForegroundColor = colorManager_0.GetConsoleColor(item.Value);
				if (num == ienumerable_0.Count())
				{
					System.Console.Write(item.Key + string_0);
				}
				else
				{
					System.Console.Write(item.Key);
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

	private static ColorStore colorStore_0;

	private static ColorManagerFactory colorManagerFactory_0;

	private static ColorManager colorManager_0;

	private static Dictionary<string, COLORREF> dictionary_0;

	private const int int_0 = 16;

	private const int int_1 = 1;

	private static readonly string string_0;

	private static readonly string string_1;

	[CompilerGenerated]
	private static readonly TaskQueue taskQueue_0;

	private static readonly Color color_0;

	private static readonly Color color_1;

	private static readonly Color color_2;

	private static readonly Color color_3;

	private static readonly Color color_4;

	private static readonly Color color_5;

	private static readonly Color color_6;

	private static readonly Color color_7;

	private static readonly Color color_8;

	private static readonly Color color_9;

	private static readonly Color color_10;

	private static readonly Color color_11;

	private static readonly Color color_12;

	private static readonly Color color_13;

	private static readonly Color color_14;

	private static readonly Color color_15;

	[CompilerGenerated]
	private static ConsoleCancelEventHandler consoleCancelEventHandler_0;

	private static TaskQueue TaskQueue_0
	{
		[CompilerGenerated]
		get
		{
			return taskQueue_0;
		}
	}

	public static Color BackgroundColor
	{
		get
		{
			return colorManager_0.GetColor(System.Console.BackgroundColor);
		}
		set
		{
			System.Console.BackgroundColor = colorManager_0.GetConsoleColor(value);
		}
	}

	public static int BufferHeight
	{
		get
		{
			return System.Console.BufferHeight;
		}
		set
		{
			System.Console.BufferHeight = value;
		}
	}

	public static int BufferWidth
	{
		get
		{
			return System.Console.BufferWidth;
		}
		set
		{
			System.Console.BufferWidth = value;
		}
	}

	public static bool CapsLock => System.Console.CapsLock;

	public static int CursorLeft
	{
		get
		{
			return System.Console.CursorLeft;
		}
		set
		{
			System.Console.CursorLeft = value;
		}
	}

	public static int CursorSize
	{
		get
		{
			return System.Console.CursorSize;
		}
		set
		{
			System.Console.CursorSize = value;
		}
	}

	public static int CursorTop
	{
		get
		{
			return System.Console.CursorTop;
		}
		set
		{
			System.Console.CursorTop = value;
		}
	}

	public static bool CursorVisible
	{
		get
		{
			return System.Console.CursorVisible;
		}
		set
		{
			System.Console.CursorVisible = value;
		}
	}

	public static TextWriter Error => System.Console.Error;

	public static Color ForegroundColor
	{
		get
		{
			return colorManager_0.GetColor(System.Console.ForegroundColor);
		}
		set
		{
			System.Console.ForegroundColor = colorManager_0.GetConsoleColor(value);
		}
	}

	public static TextReader In => System.Console.In;

	public static Encoding InputEncoding
	{
		get
		{
			return System.Console.InputEncoding;
		}
		set
		{
			System.Console.InputEncoding = value;
		}
	}

	public static bool IsErrorRedirected => System.Console.IsErrorRedirected;

	public static bool IsInputRedirected => System.Console.IsInputRedirected;

	public static bool IsOutputRedirected => System.Console.IsOutputRedirected;

	public static bool KeyAvailable => System.Console.KeyAvailable;

	public static int LargestWindowHeight => System.Console.LargestWindowHeight;

	public static int LargestWindowWidth => System.Console.LargestWindowWidth;

	public static bool NumberLock => System.Console.NumberLock;

	public static TextWriter Out => System.Console.Out;

	public static Encoding OutputEncoding
	{
		get
		{
			return System.Console.OutputEncoding;
		}
		set
		{
			System.Console.OutputEncoding = value;
		}
	}

	public static string Title
	{
		get
		{
			return System.Console.Title;
		}
		set
		{
			System.Console.Title = value;
		}
	}

	public static bool TreatControlCAsInput
	{
		get
		{
			return System.Console.TreatControlCAsInput;
		}
		set
		{
			System.Console.TreatControlCAsInput = value;
		}
	}

	public static int WindowHeight
	{
		get
		{
			return System.Console.WindowHeight;
		}
		set
		{
			System.Console.WindowHeight = value;
		}
	}

	public static int WindowLeft
	{
		get
		{
			return System.Console.WindowLeft;
		}
		set
		{
			System.Console.WindowLeft = value;
		}
	}

	public static int WindowTop
	{
		get
		{
			return System.Console.WindowTop;
		}
		set
		{
			System.Console.WindowTop = value;
		}
	}

	public static int WindowWidth
	{
		get
		{
			return System.Console.WindowWidth;
		}
		set
		{
			System.Console.WindowWidth = value;
		}
	}

	public static event ConsoleCancelEventHandler CancelKeyPress
	{
		[CompilerGenerated]
		add
		{
			ConsoleCancelEventHandler consoleCancelEventHandler = consoleCancelEventHandler_0;
			ConsoleCancelEventHandler consoleCancelEventHandler2;
			do
			{
				consoleCancelEventHandler2 = consoleCancelEventHandler;
				ConsoleCancelEventHandler value2 = (ConsoleCancelEventHandler)Delegate.Combine(consoleCancelEventHandler2, value);
				consoleCancelEventHandler = Interlocked.CompareExchange(ref consoleCancelEventHandler_0, value2, consoleCancelEventHandler2);
			}
			while ((object)consoleCancelEventHandler != consoleCancelEventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			ConsoleCancelEventHandler consoleCancelEventHandler = consoleCancelEventHandler_0;
			ConsoleCancelEventHandler consoleCancelEventHandler2;
			do
			{
				consoleCancelEventHandler2 = consoleCancelEventHandler;
				ConsoleCancelEventHandler value2 = (ConsoleCancelEventHandler)Delegate.Remove(consoleCancelEventHandler2, value);
				consoleCancelEventHandler = Interlocked.CompareExchange(ref consoleCancelEventHandler_0, value2, consoleCancelEventHandler2);
			}
			while ((object)consoleCancelEventHandler != consoleCancelEventHandler2);
		}
	}

	private static void smethod_0(IEnumerable<KeyValuePair<string, Color>> ienumerable_0, string string_2)
	{
		TaskQueue_0.Enqueue(() => Task.Factory.StartNew(delegate
		{
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			int num = 1;
			foreach (KeyValuePair<string, Color> item in ienumerable_0)
			{
				System.Console.ForegroundColor = colorManager_0.GetConsoleColor(item.Value);
				if (num == ienumerable_0.Count())
				{
					System.Console.Write(item.Key + string_2);
				}
				else
				{
					System.Console.Write(item.Key);
				}
				num++;
			}
			System.Console.ForegroundColor = foregroundColor;
		})).Wait();
	}

	private static void smethod_1(StyledString styledString_0, string string_2)
	{
		ConsoleColor foregroundColor = System.Console.ForegroundColor;
		int length = styledString_0.CharacterGeometry.GetLength(0);
		int length2 = styledString_0.CharacterGeometry.GetLength(1);
		for (int i = 0; i < length; i++)
		{
			for (int j = 0; j < length2; j++)
			{
				System.Console.ForegroundColor = colorManager_0.GetConsoleColor(styledString_0.ColorGeometry[i, j]);
				if (i == length - 1 && j == length2 - 1)
				{
					System.Console.Write(styledString_0.CharacterGeometry[i, j] + string_2);
				}
				else if (j == length2 - 1)
				{
					System.Console.Write(styledString_0.CharacterGeometry[i, j] + "\r\n");
				}
				else
				{
					System.Console.Write(styledString_0.CharacterGeometry[i, j]);
				}
			}
		}
		System.Console.ForegroundColor = foregroundColor;
	}

	private static void smethod_2<T>(Action<T> action_0, T gparam_0, Color color_16)
	{
		ConsoleColor foregroundColor = System.Console.ForegroundColor;
		System.Console.ForegroundColor = colorManager_0.GetConsoleColor(color_16);
		action_0(gparam_0);
		System.Console.ForegroundColor = foregroundColor;
	}

	private static void smethod_3(Action<string> action_0, char[] char_0, int int_2, int int_3, Color color_16)
	{
		string gparam_ = char_0.smethod_2().Substring(int_2, int_3);
		smethod_2(action_0, gparam_, color_16);
	}

	private static void smethod_4<T>(Action<T> action_0, T gparam_0, ColorAlternator colorAlternator_0)
	{
		Color nextColor = colorAlternator_0.GetNextColor(gparam_0.smethod_2());
		ConsoleColor foregroundColor = System.Console.ForegroundColor;
		System.Console.ForegroundColor = colorManager_0.GetConsoleColor(nextColor);
		action_0(gparam_0);
		System.Console.ForegroundColor = foregroundColor;
	}

	private static void smethod_5(Action<string> action_0, char[] char_0, int int_2, int int_3, ColorAlternator colorAlternator_0)
	{
		string gparam_ = char_0.smethod_2().Substring(int_2, int_3);
		smethod_4(action_0, gparam_, colorAlternator_0);
	}

	private static void smethod_6<T>(string string_2, T gparam_0, StyleSheet styleSheet_0)
	{
		smethod_0(new TextAnnotator(styleSheet_0).GetAnnotationMap(gparam_0.smethod_2()), string_2);
	}

	private static void smethod_7(string string_2, StyledString styledString_0, StyleSheet styleSheet_0)
	{
		smethod_8(new TextAnnotator(styleSheet_0).GetAnnotationMap(styledString_0.AbstractValue), styledString_0);
		smethod_1(styledString_0, string_2);
	}

	private static void smethod_8(IEnumerable<KeyValuePair<string, Color>> ienumerable_0, StyledString styledString_0)
	{
		int num = 0;
		foreach (KeyValuePair<string, Color> item in ienumerable_0)
		{
			for (int i = 0; i < item.Key.Length; i++)
			{
				int length = styledString_0.CharacterIndexGeometry.GetLength(0);
				int length2 = styledString_0.CharacterIndexGeometry.GetLength(1);
				for (int j = 0; j < length; j++)
				{
					for (int k = 0; k < length2; k++)
					{
						if (styledString_0.CharacterIndexGeometry[j, k] == num)
						{
							styledString_0.ColorGeometry[j, k] = item.Value;
						}
					}
				}
				num++;
			}
		}
	}

	private static void smethod_9(string string_2, char[] char_0, int int_2, int int_3, StyleSheet styleSheet_0)
	{
		string gparam_ = char_0.smethod_2().Substring(int_2, int_3);
		smethod_6(string_2, gparam_, styleSheet_0);
	}

	private static void smethod_10<T, U>(Action<T, U> action_0, T gparam_0, U gparam_1, Color color_16)
	{
		ConsoleColor foregroundColor = System.Console.ForegroundColor;
		System.Console.ForegroundColor = colorManager_0.GetConsoleColor(color_16);
		action_0(gparam_0, gparam_1);
		System.Console.ForegroundColor = foregroundColor;
	}

	private static void smethod_11<T, U>(Action<T, U> action_0, T gparam_0, U gparam_1, ColorAlternator colorAlternator_0)
	{
		string input = string.Format(gparam_0.ToString(), gparam_1.smethod_3());
		Color nextColor = colorAlternator_0.GetNextColor(input);
		ConsoleColor foregroundColor = System.Console.ForegroundColor;
		System.Console.ForegroundColor = colorManager_0.GetConsoleColor(nextColor);
		action_0(gparam_0, gparam_1);
		System.Console.ForegroundColor = foregroundColor;
	}

	private static void smethod_12<T, U>(string string_2, T gparam_0, U gparam_1, StyleSheet styleSheet_0)
	{
		TextAnnotator textAnnotator = new TextAnnotator(styleSheet_0);
		string input = string.Format(gparam_0.ToString(), gparam_1.smethod_3());
		smethod_0(textAnnotator.GetAnnotationMap(input), string_2);
	}

	private static void smethod_13<T, U>(string string_2, T gparam_0, U gparam_1, Color color_16, Color color_17)
	{
		TextFormatter textFormatter = new TextFormatter(color_17);
		smethod_0((List<KeyValuePair<string, Color>>)textFormatter.GetFormatMap(gparam_0.ToString(), gparam_1.smethod_3(), new Color[1] { color_16 }), string_2);
	}

	private static void smethod_14<T>(string string_2, T gparam_0, Formatter formatter_0, Color color_16)
	{
		smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), new object[1] { formatter_0.Target }, new Color[1] { formatter_0.Color }), string_2);
	}

	private static void smethod_15<T, U>(Action<T, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, Color color_16)
	{
		ConsoleColor foregroundColor = System.Console.ForegroundColor;
		System.Console.ForegroundColor = colorManager_0.GetConsoleColor(color_16);
		action_0(gparam_0, gparam_1, gparam_2);
		System.Console.ForegroundColor = foregroundColor;
	}

	private static void smethod_16<T, U>(Action<T, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, ColorAlternator colorAlternator_0)
	{
		string input = string.Format(gparam_0.ToString(), gparam_1, gparam_2);
		Color nextColor = colorAlternator_0.GetNextColor(input);
		ConsoleColor foregroundColor = System.Console.ForegroundColor;
		System.Console.ForegroundColor = colorManager_0.GetConsoleColor(nextColor);
		action_0(gparam_0, gparam_1, gparam_2);
		System.Console.ForegroundColor = foregroundColor;
	}

	private static void smethod_17<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, StyleSheet styleSheet_0)
	{
		TextAnnotator textAnnotator = new TextAnnotator(styleSheet_0);
		string input = string.Format(gparam_0.ToString(), gparam_1, gparam_2);
		smethod_0(textAnnotator.GetAnnotationMap(input), string_2);
	}

	private static void smethod_18<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, Color color_16, Color color_17)
	{
		TextFormatter textFormatter = new TextFormatter(color_17);
		smethod_0((List<KeyValuePair<string, Color>>)textFormatter.GetFormatMap(gparam_0.ToString(), new U[2] { gparam_1, gparam_2 }.smethod_3(), new Color[1] { color_16 }), string_2);
	}

	private static void smethod_19<T>(string string_2, T gparam_0, Formatter formatter_0, Formatter formatter_1, Color color_16)
	{
		smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), new object[2] { formatter_0.Target, formatter_1.Target }, new Color[2] { formatter_0.Color, formatter_1.Color }), string_2);
	}

	private static void smethod_20<T, U>(Action<T, U, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, U gparam_3, Color color_16)
	{
		ConsoleColor foregroundColor = System.Console.ForegroundColor;
		System.Console.ForegroundColor = colorManager_0.GetConsoleColor(color_16);
		action_0(gparam_0, gparam_1, gparam_2, gparam_3);
		System.Console.ForegroundColor = foregroundColor;
	}

	private static void smethod_21<T, U>(Action<T, U, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, U gparam_3, ColorAlternator colorAlternator_0)
	{
		string input = string.Format(gparam_0.ToString(), gparam_1, gparam_2, gparam_3);
		Color nextColor = colorAlternator_0.GetNextColor(input);
		ConsoleColor foregroundColor = System.Console.ForegroundColor;
		System.Console.ForegroundColor = colorManager_0.GetConsoleColor(nextColor);
		action_0(gparam_0, gparam_1, gparam_2, gparam_3);
		System.Console.ForegroundColor = foregroundColor;
	}

	private static void smethod_22<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, U gparam_3, StyleSheet styleSheet_0)
	{
		TextAnnotator textAnnotator = new TextAnnotator(styleSheet_0);
		string input = string.Format(gparam_0.ToString(), gparam_1, gparam_2, gparam_3);
		smethod_0(textAnnotator.GetAnnotationMap(input), string_2);
	}

	private static void smethod_23<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, U gparam_3, Color color_16, Color color_17)
	{
		TextFormatter textFormatter = new TextFormatter(color_17);
		smethod_0((List<KeyValuePair<string, Color>>)textFormatter.GetFormatMap(gparam_0.ToString(), new U[3] { gparam_1, gparam_2, gparam_3 }.smethod_3(), new Color[1] { color_16 }), string_2);
	}

	private static void smethod_24<T>(string string_2, T gparam_0, Formatter formatter_0, Formatter formatter_1, Formatter formatter_2, Color color_16)
	{
		smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), new object[3] { formatter_0.Target, formatter_1.Target, formatter_2.Target }, new Color[3] { formatter_0.Color, formatter_1.Color, formatter_2.Color }), string_2);
	}

	private static void smethod_25<T>(string string_2, T gparam_0, Formatter[] formatter_0, Color color_16)
	{
		smethod_0(new TextFormatter(color_16).GetFormatMap(gparam_0.ToString(), formatter_0.Select((Formatter formatter_0) => formatter_0.Target).ToArray(), formatter_0.Select((Formatter formatter_0) => formatter_0.Color).ToArray()), string_2);
	}

	private static void smethod_26<T>(Action<object, Color> action_0, IEnumerable<T> ienumerable_0, Color color_16, Color color_17, int int_2)
	{
		foreach (StyleClass<T> item in new GradientGenerator().GenerateGradient(ienumerable_0, color_16, color_17, int_2))
		{
			action_0(item.Target, item.Color);
		}
	}

	private static Figlet smethod_27(FigletFont figletFont_0 = null)
	{
		if (figletFont_0 == null)
		{
			return new Figlet();
		}
		return new Figlet(figletFont_0);
	}

	private static ColorStore smethod_28()
	{
		ConcurrentDictionary<Color, ConsoleColor> concurrentDictionary_ = new ConcurrentDictionary<Color, ConsoleColor>();
		ConcurrentDictionary<ConsoleColor, Color> concurrentDictionary = new ConcurrentDictionary<ConsoleColor, Color>();
		concurrentDictionary.TryAdd(ConsoleColor.Black, color_0);
		concurrentDictionary.TryAdd(ConsoleColor.Blue, color_1);
		concurrentDictionary.TryAdd(ConsoleColor.Cyan, color_2);
		concurrentDictionary.TryAdd(ConsoleColor.DarkBlue, color_3);
		concurrentDictionary.TryAdd(ConsoleColor.DarkCyan, color_4);
		concurrentDictionary.TryAdd(ConsoleColor.DarkGray, color_5);
		concurrentDictionary.TryAdd(ConsoleColor.DarkGreen, color_6);
		concurrentDictionary.TryAdd(ConsoleColor.DarkMagenta, color_7);
		concurrentDictionary.TryAdd(ConsoleColor.DarkRed, color_8);
		concurrentDictionary.TryAdd(ConsoleColor.DarkYellow, color_9);
		concurrentDictionary.TryAdd(ConsoleColor.Gray, color_10);
		concurrentDictionary.TryAdd(ConsoleColor.Green, color_11);
		concurrentDictionary.TryAdd(ConsoleColor.Magenta, color_12);
		concurrentDictionary.TryAdd(ConsoleColor.Red, color_13);
		concurrentDictionary.TryAdd(ConsoleColor.White, color_14);
		concurrentDictionary.TryAdd(ConsoleColor.Yellow, color_15);
		return new ColorStore(concurrentDictionary_, concurrentDictionary);
	}

	private static void smethod_29(bool bool_0)
	{
		colorStore_0 = smethod_28();
		colorManagerFactory_0 = new ColorManagerFactory();
		colorManager_0 = colorManagerFactory_0.GetManager(colorStore_0, 16, 1, bool_0);
		if (!colorManager_0.IsInCompatibilityMode)
		{
			new ColorMapper().SetBatchBufferColors(dictionary_0);
		}
	}

	static Console()
	{
		string_0 = "\r\n";
		string_1 = "";
		taskQueue_0 = new TaskQueue();
		color_0 = Color.FromArgb(0, 0, 0);
		color_1 = Color.FromArgb(0, 0, 255);
		color_2 = Color.FromArgb(0, 255, 255);
		color_3 = Color.FromArgb(0, 0, 128);
		color_4 = Color.FromArgb(0, 128, 128);
		color_5 = Color.FromArgb(128, 128, 128);
		color_6 = Color.FromArgb(0, 128, 0);
		color_7 = Color.FromArgb(128, 0, 128);
		color_8 = Color.FromArgb(128, 0, 0);
		color_9 = Color.FromArgb(128, 128, 0);
		color_10 = Color.FromArgb(192, 192, 192);
		color_11 = Color.FromArgb(0, 255, 0);
		color_12 = Color.FromArgb(255, 0, 255);
		color_13 = Color.FromArgb(255, 0, 0);
		color_14 = Color.FromArgb(255, 255, 255);
		color_15 = Color.FromArgb(255, 255, 0);
		consoleCancelEventHandler_0 = delegate
		{
		};
		bool bool_ = false;
		try
		{
			dictionary_0 = new ColorMapper().GetBufferColors();
		}
		catch
		{
			bool_ = true;
		}
		smethod_29(bool_);
		System.Console.CancelKeyPress += smethod_30;
	}

	public static void Write(bool value)
	{
		System.Console.Write(value);
	}

	public static void Write(bool value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(bool value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(bool value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(char value)
	{
		System.Console.Write(value);
	}

	public static void Write(char value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(char value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(char value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(char[] value)
	{
		System.Console.Write(value);
	}

	public static void Write(char[] value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(char[] value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(char[] value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(decimal value)
	{
		System.Console.Write(value);
	}

	public static void Write(decimal value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(decimal value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(decimal value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(double value)
	{
		System.Console.Write(value);
	}

	public static void Write(double value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(double value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(double value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(float value)
	{
		System.Console.Write(value);
	}

	public static void Write(float value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(float value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(float value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(int value)
	{
		System.Console.Write(value);
	}

	public static void Write(int value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(int value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(int value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(long value)
	{
		System.Console.Write(value);
	}

	public static void Write(long value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(long value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(long value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(object value)
	{
		System.Console.Write(value);
	}

	public static void Write(object value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(object value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(object value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(string value)
	{
		System.Console.Write(value);
	}

	public static void Write(string value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(string value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(string value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(uint value)
	{
		System.Console.Write(value);
	}

	public static void Write(uint value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(uint value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(uint value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(ulong value)
	{
		System.Console.Write(value);
	}

	public static void Write(ulong value, Color color)
	{
		smethod_2(System.Console.Write, value, color);
	}

	public static void WriteAlternating(ulong value, ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, value, alternator);
	}

	public static void WriteStyled(ulong value, StyleSheet styleSheet)
	{
		smethod_6(string_1, value, styleSheet);
	}

	public static void Write(string format, object arg0)
	{
		System.Console.Write(format, arg0);
	}

	public static void Write(string format, object arg0, Color color)
	{
		smethod_10(System.Console.Write, format, arg0, color);
	}

	public static void WriteAlternating(string format, object arg0, ColorAlternator alternator)
	{
		smethod_11(System.Console.Write, format, arg0, alternator);
	}

	public static void WriteStyled(string format, object arg0, StyleSheet styleSheet)
	{
		smethod_12(string_1, format, arg0, styleSheet);
	}

	public static void WriteFormatted(string format, object arg0, Color styledColor, Color defaultColor)
	{
		smethod_13(string_1, format, arg0, styledColor, defaultColor);
	}

	public static void WriteFormatted(string format, Formatter arg0, Color defaultColor)
	{
		smethod_14(string_1, format, arg0, defaultColor);
	}

	public static void Write(string format, params object[] args)
	{
		System.Console.Write(format, args);
	}

	public static void Write(string format, Color color, params object[] args)
	{
		smethod_10(System.Console.Write, format, args, color);
	}

	public static void WriteAlternating(string format, ColorAlternator alternator, params object[] args)
	{
		smethod_11(System.Console.Write, format, args, alternator);
	}

	public static void WriteStyled(StyleSheet styleSheet, string format, params object[] args)
	{
		smethod_12(string_1, format, args, styleSheet);
	}

	public static void WriteFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
	{
		smethod_13(string_1, format, args, styledColor, defaultColor);
	}

	public static void WriteFormatted(string format, Color defaultColor, params Formatter[] args)
	{
		smethod_25(string_1, format, args, defaultColor);
	}

	public static void Write(char[] buffer, int index, int count)
	{
		System.Console.Write(buffer, index, count);
	}

	public static void Write(char[] buffer, int index, int count, Color color)
	{
		smethod_3(System.Console.Write, buffer, index, count, color);
	}

	public static void WriteAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
	{
		smethod_5(System.Console.Write, buffer, index, count, alternator);
	}

	public static void WriteStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
	{
		smethod_9(string_1, buffer, index, count, styleSheet);
	}

	public static void Write(string format, object arg0, object arg1)
	{
		System.Console.Write(format, arg0, arg1);
	}

	public static void Write(string format, object arg0, object arg1, Color color)
	{
		smethod_15(System.Console.Write, format, arg0, arg1, color);
	}

	public static void WriteAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
	{
		smethod_16(System.Console.Write, format, arg0, arg1, alternator);
	}

	public static void WriteStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
	{
		smethod_17(string_1, format, arg0, arg1, styleSheet);
	}

	public static void WriteFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
	{
		smethod_18(string_1, format, arg0, arg1, styledColor, defaultColor);
	}

	public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
	{
		smethod_19(string_1, format, arg0, arg1, defaultColor);
	}

	public static void Write(string format, object arg0, object arg1, object arg2)
	{
		System.Console.Write(format, arg0, arg1, arg2);
	}

	public static void Write(string format, object arg0, object arg1, object arg2, Color color)
	{
		smethod_20(System.Console.Write, format, arg0, arg1, arg2, color);
	}

	public static void WriteAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
	{
		smethod_21(System.Console.Write, format, arg0, arg1, arg2, alternator);
	}

	public static void WriteStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
	{
		smethod_22(string_1, format, arg0, arg1, arg2, styleSheet);
	}

	public static void WriteFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
	{
		smethod_23(string_1, format, arg0, arg1, arg2, styledColor, defaultColor);
	}

	public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
	{
		smethod_24(string_1, format, arg0, arg1, arg2, defaultColor);
	}

	public static void Write(string format, object arg0, object arg1, object arg2, object arg3)
	{
		System.Console.Write(format, arg0, arg1, arg2, arg3);
	}

	public static void Write(string format, object arg0, object arg1, object arg2, object arg3, Color color)
	{
		smethod_10(System.Console.Write, format, new object[4] { arg0, arg1, arg2, arg3 }, color);
	}

	public static void WriteAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
	{
		smethod_11(System.Console.Write, format, new object[4] { arg0, arg1, arg2, arg3 }, alternator);
	}

	public static void WriteStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
	{
		smethod_12(string_1, format, new object[4] { arg0, arg1, arg2, arg3 }, styleSheet);
	}

	public static void WriteFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
	{
		smethod_13(string_1, format, new object[4] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
	}

	public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
	{
		smethod_25(string_1, format, new Formatter[4] { arg0, arg1, arg2, arg3 }, defaultColor);
	}

	public static void WriteLine()
	{
		System.Console.WriteLine();
	}

	public static void WriteLineAlternating(ColorAlternator alternator)
	{
		smethod_4(System.Console.Write, string_0, alternator);
	}

	public static void WriteLineStyled(StyleSheet styleSheet)
	{
		smethod_6(string_1, string_0, styleSheet);
	}

	public static void WriteLine(bool value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(bool value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(bool value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(bool value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(char value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(char value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(char value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(char value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(char[] value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(char[] value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(char[] value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(char[] value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(decimal value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(decimal value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(decimal value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(decimal value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(double value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(double value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(double value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(double value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(float value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(float value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(float value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(float value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(int value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(int value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(int value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(int value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(long value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(long value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(long value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(long value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(object value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(object value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(object value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(StyledString value, StyleSheet styleSheet)
	{
		smethod_7(string_0, value, styleSheet);
	}

	public static void WriteLine(string value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(string value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(string value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(string value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(uint value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(uint value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(uint value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(uint value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(ulong value)
	{
		System.Console.WriteLine(value);
	}

	public static void WriteLine(ulong value, Color color)
	{
		smethod_2(System.Console.WriteLine, value, color);
	}

	public static void WriteLineAlternating(ulong value, ColorAlternator alternator)
	{
		smethod_4(System.Console.WriteLine, value, alternator);
	}

	public static void WriteLineStyled(ulong value, StyleSheet styleSheet)
	{
		smethod_6(string_0, value, styleSheet);
	}

	public static void WriteLine(string format, object arg0)
	{
		System.Console.WriteLine(format, arg0);
	}

	public static void WriteLine(string format, object arg0, Color color)
	{
		smethod_10(System.Console.WriteLine, format, arg0, color);
	}

	public static void WriteLineAlternating(string format, object arg0, ColorAlternator alternator)
	{
		smethod_11(System.Console.WriteLine, format, arg0, alternator);
	}

	public static void WriteLineStyled(string format, object arg0, StyleSheet styleSheet)
	{
		smethod_12(string_0, format, arg0, styleSheet);
	}

	public static void WriteLineFormatted(string format, object arg0, Color styledColor, Color defaultColor)
	{
		smethod_13(string_0, format, arg0, styledColor, defaultColor);
	}

	public static void WriteLineFormatted(string format, Formatter arg0, Color defaultColor)
	{
		smethod_14(string_0, format, arg0, defaultColor);
	}

	public static void WriteLine(string format, params object[] args)
	{
		System.Console.WriteLine(format, args);
	}

	public static void WriteLine(string format, Color color, params object[] args)
	{
		smethod_10(System.Console.WriteLine, format, args, color);
	}

	public static void WriteLineAlternating(string format, ColorAlternator alternator, params object[] args)
	{
		smethod_11(System.Console.WriteLine, format, args, alternator);
	}

	public static void WriteLineStyled(StyleSheet styleSheet, string format, params object[] args)
	{
		smethod_12(string_0, format, args, styleSheet);
	}

	public static void WriteLineFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
	{
		smethod_13(string_0, format, args, styledColor, defaultColor);
	}

	public static void WriteLineFormatted(string format, Color defaultColor, params Formatter[] args)
	{
		smethod_25(string_0, format, args, defaultColor);
	}

	public static void WriteLine(char[] buffer, int index, int count)
	{
		System.Console.WriteLine(buffer, index, count);
	}

	public static void WriteLine(char[] buffer, int index, int count, Color color)
	{
		smethod_3(System.Console.WriteLine, buffer, index, count, color);
	}

	public static void WriteLineAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
	{
		smethod_5(System.Console.WriteLine, buffer, index, count, alternator);
	}

	public static void WriteLineStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
	{
		smethod_9(string_0, buffer, index, count, styleSheet);
	}

	public static void WriteLine(string format, object arg0, object arg1)
	{
		System.Console.WriteLine(format, arg0, arg1);
	}

	public static void WriteLine(string format, object arg0, object arg1, Color color)
	{
		smethod_15(System.Console.WriteLine, format, arg0, arg1, color);
	}

	public static void WriteLineAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
	{
		smethod_16(System.Console.WriteLine, format, arg0, arg1, alternator);
	}

	public static void WriteLineStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
	{
		smethod_17(string_0, format, arg0, arg1, styleSheet);
	}

	public static void WriteLineFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
	{
		smethod_18(string_0, format, arg0, arg1, styledColor, defaultColor);
	}

	public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
	{
		smethod_19(string_0, format, arg0, arg1, defaultColor);
	}

	public static void WriteLine(string format, object arg0, object arg1, object arg2)
	{
		System.Console.WriteLine(format, arg0, arg1, arg2);
	}

	public static void WriteLine(string format, object arg0, object arg1, object arg2, Color color)
	{
		smethod_20(System.Console.WriteLine, format, arg0, arg1, arg2, color);
	}

	public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
	{
		smethod_21(System.Console.WriteLine, format, arg0, arg1, arg2, alternator);
	}

	public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
	{
		smethod_22(string_0, format, arg0, arg1, arg2, styleSheet);
	}

	public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
	{
		smethod_23(string_0, format, arg0, arg1, arg2, styledColor, defaultColor);
	}

	public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
	{
		smethod_24(string_0, format, arg0, arg1, arg2, defaultColor);
	}

	public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
	{
		System.Console.WriteLine(format, arg0, arg1, arg2, arg3);
	}

	public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, Color color)
	{
		smethod_10(System.Console.WriteLine, format, new object[4] { arg0, arg1, arg2, arg3 }, color);
	}

	public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
	{
		smethod_11(System.Console.WriteLine, format, new object[4] { arg0, arg1, arg2, arg3 }, alternator);
	}

	public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
	{
		smethod_12(string_0, format, new object[4] { arg0, arg1, arg2, arg3 }, styleSheet);
	}

	public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
	{
		smethod_13(string_0, format, new object[4] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
	}

	public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
	{
		smethod_25(string_0, format, new Formatter[4] { arg0, arg1, arg2, arg3 }, defaultColor);
	}

	public static void WriteAscii(string value)
	{
		WriteAscii(value, null);
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
		string[] array = smethod_27(font).ToAscii(value).ConcreteValue.Split('\n');
		for (int i = 0; i < array.Length; i++)
		{
			WriteLineAlternating(array[i], alternator);
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

	public static void WriteWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 16)
	{
		smethod_26(Write, input, startColor, endColor, maxColorsInGradient);
	}

	public static void WriteLineWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 16)
	{
		smethod_26(WriteLine, input, startColor, endColor, maxColorsInGradient);
	}

	public static int Read()
	{
		return System.Console.Read();
	}

	public static ConsoleKeyInfo ReadKey()
	{
		return System.Console.ReadKey();
	}

	public static ConsoleKeyInfo ReadKey(bool intercept)
	{
		return System.Console.ReadKey(intercept);
	}

	public static string ReadLine()
	{
		return System.Console.ReadLine();
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

	public static Stream OpenStandardError()
	{
		return System.Console.OpenStandardError();
	}

	public static Stream OpenStandardError(int bufferSize)
	{
		return System.Console.OpenStandardError(bufferSize);
	}

	public static Stream OpenStandardInput()
	{
		return System.Console.OpenStandardInput();
	}

	public static Stream OpenStandardInput(int bufferSize)
	{
		return System.Console.OpenStandardInput(bufferSize);
	}

	public static Stream OpenStandardOutput()
	{
		return System.Console.OpenStandardOutput();
	}

	public static Stream OpenStandardOutput(int bufferSize)
	{
		return System.Console.OpenStandardOutput(bufferSize);
	}

	public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
	{
		System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
	}

	public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
	{
		System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
	}

	public static void Clear()
	{
		System.Console.Clear();
	}

	public static void ReplaceAllColorsWithDefaults()
	{
		colorStore_0 = smethod_28();
		colorManagerFactory_0 = new ColorManagerFactory();
		colorManager_0 = colorManagerFactory_0.GetManager(colorStore_0, 16, 1, colorManager_0.IsInCompatibilityMode);
		if (!colorManager_0.IsInCompatibilityMode)
		{
			new ColorMapper().SetBatchBufferColors(dictionary_0);
		}
	}

	public static void ReplaceColor(Color oldColor, Color newColor)
	{
		colorManager_0.ReplaceColor(oldColor, newColor);
	}

	public static void Beep(int frequency, int duration)
	{
		System.Console.Beep(frequency, duration);
	}

	private static void smethod_30(object sender, ConsoleCancelEventArgs e)
	{
		consoleCancelEventHandler_0(sender, e);
	}
}
