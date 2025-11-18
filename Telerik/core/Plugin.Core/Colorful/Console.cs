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

namespace Plugin.Core.Colorful
{
	public static class Console
	{
		private static ColorStore colorStore_0;

		private static ColorManagerFactory colorManagerFactory_0;

		private static ColorManager colorManager_0;

		private static Dictionary<string, COLORREF> dictionary_0;

		private const int int_0 = 16;

		private const int int_1 = 1;

		private readonly static string string_0;

		private readonly static string string_1;

		private readonly static Color color_0;

		private readonly static Color color_1;

		private readonly static Color color_2;

		private readonly static Color color_3;

		private readonly static Color color_4;

		private readonly static Color color_5;

		private readonly static Color color_6;

		private readonly static Color color_7;

		private readonly static Color color_8;

		private readonly static Color color_9;

		private readonly static Color color_10;

		private readonly static Color color_11;

		private readonly static Color color_12;

		private readonly static Color color_13;

		private readonly static Color color_14;

		private readonly static Color color_15;

		public static Color BackgroundColor
		{
			get
			{
				return Plugin.Core.Colorful.Console.colorManager_0.GetColor(System.Console.BackgroundColor);
			}
			set
			{
				System.Console.BackgroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(value);
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

		public static bool CapsLock
		{
			get
			{
				return System.Console.CapsLock;
			}
		}

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

		public static TextWriter Error
		{
			get
			{
				return System.Console.Error;
			}
		}

		public static Color ForegroundColor
		{
			get
			{
				return Plugin.Core.Colorful.Console.colorManager_0.GetColor(System.Console.ForegroundColor);
			}
			set
			{
				System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(value);
			}
		}

		public static TextReader In
		{
			get
			{
				return System.Console.In;
			}
		}

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

		public static bool IsErrorRedirected
		{
			get
			{
				return System.Console.IsErrorRedirected;
			}
		}

		public static bool IsInputRedirected
		{
			get
			{
				return System.Console.IsInputRedirected;
			}
		}

		public static bool IsOutputRedirected
		{
			get
			{
				return System.Console.IsOutputRedirected;
			}
		}

		public static bool KeyAvailable
		{
			get
			{
				return System.Console.KeyAvailable;
			}
		}

		public static int LargestWindowHeight
		{
			get
			{
				return System.Console.LargestWindowHeight;
			}
		}

		public static int LargestWindowWidth
		{
			get
			{
				return System.Console.LargestWindowWidth;
			}
		}

		public static bool NumberLock
		{
			get
			{
				return System.Console.NumberLock;
			}
		}

		public static TextWriter Out
		{
			get
			{
				return System.Console.Out;
			}
		}

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

		private static TaskQueue TaskQueue_0
		{
			get;
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

		static Console()
		{
			Plugin.Core.Colorful.Console.string_0 = "\r\n";
			Plugin.Core.Colorful.Console.string_1 = "";
			Plugin.Core.Colorful.Console.TaskQueue_0 = new TaskQueue();
			Plugin.Core.Colorful.Console.color_0 = Color.FromArgb(0, 0, 0);
			Plugin.Core.Colorful.Console.color_1 = Color.FromArgb(0, 0, 255);
			Plugin.Core.Colorful.Console.color_2 = Color.FromArgb(0, 255, 255);
			Plugin.Core.Colorful.Console.color_3 = Color.FromArgb(0, 0, 128);
			Plugin.Core.Colorful.Console.color_4 = Color.FromArgb(0, 128, 128);
			Plugin.Core.Colorful.Console.color_5 = Color.FromArgb(128, 128, 128);
			Plugin.Core.Colorful.Console.color_6 = Color.FromArgb(0, 128, 0);
			Plugin.Core.Colorful.Console.color_7 = Color.FromArgb(128, 0, 128);
			Plugin.Core.Colorful.Console.color_8 = Color.FromArgb(128, 0, 0);
			Plugin.Core.Colorful.Console.color_9 = Color.FromArgb(128, 128, 0);
			Plugin.Core.Colorful.Console.color_10 = Color.FromArgb(192, 192, 192);
			Plugin.Core.Colorful.Console.color_11 = Color.FromArgb(0, 255, 0);
			Plugin.Core.Colorful.Console.color_12 = Color.FromArgb(255, 0, 255);
			Plugin.Core.Colorful.Console.color_13 = Color.FromArgb(255, 0, 0);
			Plugin.Core.Colorful.Console.color_14 = Color.FromArgb(255, 255, 255);
			Plugin.Core.Colorful.Console.color_15 = Color.FromArgb(255, 255, 0);
			Plugin.Core.Colorful.Console.consoleCancelEventHandler_0 = (object sender, ConsoleCancelEventArgs e) => {
			};
			bool flag = false;
			try
			{
				Plugin.Core.Colorful.Console.dictionary_0 = (new ColorMapper()).GetBufferColors();
			}
			catch
			{
				flag = true;
			}
			Plugin.Core.Colorful.Console.smethod_29(flag);
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

		public static void ReplaceAllColorsWithDefaults()
		{
			Plugin.Core.Colorful.Console.colorStore_0 = Plugin.Core.Colorful.Console.smethod_28();
			Plugin.Core.Colorful.Console.colorManagerFactory_0 = new ColorManagerFactory();
			Plugin.Core.Colorful.Console.colorManager_0 = Plugin.Core.Colorful.Console.colorManagerFactory_0.GetManager(Plugin.Core.Colorful.Console.colorStore_0, 16, 1, Plugin.Core.Colorful.Console.colorManager_0.IsInCompatibilityMode);
			if (!Plugin.Core.Colorful.Console.colorManager_0.IsInCompatibilityMode)
			{
				(new ColorMapper()).SetBatchBufferColors(Plugin.Core.Colorful.Console.dictionary_0);
			}
		}

		public static void ReplaceColor(Color oldColor, Color newColor)
		{
			Plugin.Core.Colorful.Console.colorManager_0.ReplaceColor(oldColor, newColor);
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
			Action action2 = null;
			Plugin.Core.Colorful.Console.TaskQueue_0.Enqueue(() => {
				TaskFactory factory = Task.Factory;
				Action action0 = action2;
				if (action0 == null)
				{
					Action action = () => {
						ConsoleColor foregroundColor = System.Console.ForegroundColor;
						int ınt32 = 1;
						foreach (KeyValuePair<string, Color> ienumerable0 in ienumerable_0)
						{
							System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(ienumerable0.Value);
							if (ınt32 != ienumerable_0.Count<KeyValuePair<string, Color>>())
							{
								System.Console.Write(ienumerable0.Key);
							}
							else
							{
								System.Console.Write(string.Concat(ienumerable0.Key, string_2));
							}
							ınt32++;
						}
						System.Console.ForegroundColor = foregroundColor;
					};
					Action action1 = action;
					action2 = action;
					action0 = action1;
				}
				return factory.StartNew(action0);
			}).Wait();
		}

		private static void smethod_1(StyledString styledString_0, string string_2)
		{
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			int length = styledString_0.CharacterGeometry.GetLength(0);
			int ınt32 = styledString_0.CharacterGeometry.GetLength(1);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < ınt32; j++)
				{
					System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(styledString_0.ColorGeometry[i, j]);
					if (i == length - 1 && j == ınt32 - 1)
					{
						System.Console.Write(string.Concat(styledString_0.CharacterGeometry[i, j].ToString(), string_2));
					}
					else if (j != ınt32 - 1)
					{
						System.Console.Write(styledString_0.CharacterGeometry[i, j]);
					}
					else
					{
						System.Console.Write(string.Concat(styledString_0.CharacterGeometry[i, j].ToString(), "\r\n"));
					}
				}
			}
			System.Console.ForegroundColor = foregroundColor;
		}

		private static void smethod_10<T, U>(Action<T, U> action_0, T gparam_0, U gparam_1, Color color_16)
		{
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(color_16);
			action_0(gparam_0, gparam_1);
			System.Console.ForegroundColor = foregroundColor;
		}

		private static void smethod_11<T, U>(Action<T, U> action_0, T gparam_0, U gparam_1, ColorAlternator colorAlternator_0)
		{
			// 
			// Current member / type: System.Void Plugin.Core.Colorful.Console::smethod_11(System.Action`2<T,U>,T,U,Plugin.Core.Colorful.ColorAlternator)
			// File path: C:\Users\Administrator\Desktop\DUMP TO SRC\Plugin.Core.dll
			// 
			// Product version: 2024.1.131.0
			// Exception in: System.Void smethod_11(System.Action<T,U>,T,U,Plugin.Core.Colorful.ColorAlternator)
			// 
			// Nesne başvurusu bir nesnenin örneğine ayarlanmadı.
			//    konum: Mono.Cecil.MethodReference.get_GenericParameterReturnType() C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil\MethodReference.cs içinde: satır 250
			//    konum: ..(Expression ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\DynamicElementAnalyzer.cs içinde: satır 50
			//    konum: ..(Expression ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\DynamicElementAnalyzer.cs içinde: satır 19
			//    konum: ..( , IList`1 ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 132
			//    konum: ..( , IEnumerable`1 , IEnumerable`1 ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 99
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 67
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 87
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..Visit[,]( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 286
			//    konum: ..Visit( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 322
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 499
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 92
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 87
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..(BinaryExpression ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 529
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 97
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 383
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 325
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 59
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..Visit[,]( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 286
			//    konum: ..Visit( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 317
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 337
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 49
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..( , Dictionary`2 , Dictionary`2 , HashSet`1 , TypeSystem ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 29
			//    konum: ..(DecompilationContext ,  ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\ResolveDynamicVariablesStep.cs içinde: satır 31
			//    konum: ..(MethodBody ,  , ILanguage ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs içinde: satır 88
			//    konum: ..(MethodBody , ILanguage ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs içinde: satır 70
			//    konum: Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs içinde: satır 95
			//    konum: Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs içinde: satır 58
			//    konum: ..(ILanguage , MethodDefinition ,  ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs içinde: satır 117
			// 
			// mailto: JustDecompilePublicFeedback@telerik.com

		}

		private static void smethod_12<T, U>(string string_2, T gparam_0, U gparam_1, StyleSheet styleSheet_0)
		{
			// 
			// Current member / type: System.Void Plugin.Core.Colorful.Console::smethod_12(System.String,T,U,Plugin.Core.Colorful.StyleSheet)
			// File path: C:\Users\Administrator\Desktop\DUMP TO SRC\Plugin.Core.dll
			// 
			// Product version: 2024.1.131.0
			// Exception in: System.Void smethod_12(System.String,T,U,Plugin.Core.Colorful.StyleSheet)
			// 
			// Nesne başvurusu bir nesnenin örneğine ayarlanmadı.
			//    konum: Mono.Cecil.MethodReference.get_GenericParameterReturnType() C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Mono.Cecil\Mono.Cecil\MethodReference.cs içinde: satır 250
			//    konum: ..(Expression ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\DynamicElementAnalyzer.cs içinde: satır 50
			//    konum: ..(Expression ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\DynamicElementAnalyzer.cs içinde: satır 19
			//    konum: ..( , IList`1 ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 132
			//    konum: ..( , IEnumerable`1 , IEnumerable`1 ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 99
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 67
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 87
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..Visit[,]( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 286
			//    konum: ..Visit( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 322
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 499
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 92
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 87
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..(BinaryExpression ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 529
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 97
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 383
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 325
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 59
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..Visit[,]( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 286
			//    konum: ..Visit( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 317
			//    konum: ..( ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 337
			//    konum: ..(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 49
			//    konum: ..Visit(ICodeNode ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Ast\BaseCodeTransformer.cs içinde: satır 276
			//    konum: ..( , Dictionary`2 , Dictionary`2 , HashSet`1 , TypeSystem ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\CallSiteInvocationReplacer.cs içinde: satır 29
			//    konum: ..(DecompilationContext ,  ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Steps\DynamicVariables\ResolveDynamicVariablesStep.cs içinde: satır 31
			//    konum: ..(MethodBody ,  , ILanguage ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs içinde: satır 88
			//    konum: ..(MethodBody , ILanguage ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs içinde: satır 70
			//    konum: Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs içinde: satır 95
			//    konum: Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs içinde: satır 58
			//    konum: ..(ILanguage , MethodDefinition ,  ) C:\DeveloperTooling_JD_Agent1\_work\15\s\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs içinde: satır 117
			// 
			// mailto: JustDecompilePublicFeedback@telerik.com

		}

		private static void smethod_13<T, U>(string string_2, T gparam_0, U gparam_1, Color color_16, Color color_17)
		{
			TextFormatter textFormatter = new TextFormatter(color_17);
			Plugin.Core.Colorful.Console.smethod_0((List<KeyValuePair<string, Color>>)textFormatter.GetFormatMap(gparam_0.ToString(), gparam_1.smethod_3<U>(), new Color[] { color_16 }), string_2);
		}

		private static void smethod_14<T>(string string_2, T gparam_0, Formatter formatter_0, Color color_16)
		{
			Plugin.Core.Colorful.Console.smethod_0((new TextFormatter(color_16)).GetFormatMap(gparam_0.ToString(), new object[] { formatter_0.Target }, new Color[] { formatter_0.Color }), string_2);
		}

		private static void smethod_15<T, U>(Action<T, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, Color color_16)
		{
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(color_16);
			action_0(gparam_0, gparam_1, gparam_2);
			System.Console.ForegroundColor = foregroundColor;
		}

		private static void smethod_16<T, U>(Action<T, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, ColorAlternator colorAlternator_0)
		{
			string str = string.Format(gparam_0.ToString(), gparam_1, gparam_2);
			Color nextColor = colorAlternator_0.GetNextColor(str);
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(nextColor);
			action_0(gparam_0, gparam_1, gparam_2);
			System.Console.ForegroundColor = foregroundColor;
		}

		private static void smethod_17<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, StyleSheet styleSheet_0)
		{
			TextAnnotator textAnnotator = new TextAnnotator(styleSheet_0);
			string str = string.Format(gparam_0.ToString(), gparam_1, gparam_2);
			Plugin.Core.Colorful.Console.smethod_0(textAnnotator.GetAnnotationMap(str), string_2);
		}

		private static void smethod_18<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, Color color_16, Color color_17)
		{
			TextFormatter textFormatter = new TextFormatter(color_17);
			Plugin.Core.Colorful.Console.smethod_0((List<KeyValuePair<string, Color>>)textFormatter.GetFormatMap(gparam_0.ToString(), (new U[] { gparam_1, gparam_2 }).smethod_3<U[]>(), new Color[] { color_16 }), string_2);
		}

		private static void smethod_19<T>(string string_2, T gparam_0, Formatter formatter_0, Formatter formatter_1, Color color_16)
		{
			Plugin.Core.Colorful.Console.smethod_0((new TextFormatter(color_16)).GetFormatMap(gparam_0.ToString(), new object[] { formatter_0.Target, formatter_1.Target }, new Color[] { formatter_0.Color, formatter_1.Color }), string_2);
		}

		private static void smethod_2<T>(Action<T> action_0, T gparam_0, Color color_16)
		{
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(color_16);
			action_0(gparam_0);
			System.Console.ForegroundColor = foregroundColor;
		}

		private static void smethod_20<T, U>(Action<T, U, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, U gparam_3, Color color_16)
		{
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(color_16);
			action_0(gparam_0, gparam_1, gparam_2, gparam_3);
			System.Console.ForegroundColor = foregroundColor;
		}

		private static void smethod_21<T, U>(Action<T, U, U, U> action_0, T gparam_0, U gparam_1, U gparam_2, U gparam_3, ColorAlternator colorAlternator_0)
		{
			string str = string.Format(gparam_0.ToString(), gparam_1, gparam_2, gparam_3);
			Color nextColor = colorAlternator_0.GetNextColor(str);
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(nextColor);
			action_0(gparam_0, gparam_1, gparam_2, gparam_3);
			System.Console.ForegroundColor = foregroundColor;
		}

		private static void smethod_22<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, U gparam_3, StyleSheet styleSheet_0)
		{
			TextAnnotator textAnnotator = new TextAnnotator(styleSheet_0);
			string str = string.Format(gparam_0.ToString(), gparam_1, gparam_2, gparam_3);
			Plugin.Core.Colorful.Console.smethod_0(textAnnotator.GetAnnotationMap(str), string_2);
		}

		private static void smethod_23<T, U>(string string_2, T gparam_0, U gparam_1, U gparam_2, U gparam_3, Color color_16, Color color_17)
		{
			TextFormatter textFormatter = new TextFormatter(color_17);
			Plugin.Core.Colorful.Console.smethod_0((List<KeyValuePair<string, Color>>)textFormatter.GetFormatMap(gparam_0.ToString(), (new U[] { gparam_1, gparam_2, gparam_3 }).smethod_3<U[]>(), new Color[] { color_16 }), string_2);
		}

		private static void smethod_24<T>(string string_2, T gparam_0, Formatter formatter_0, Formatter formatter_1, Formatter formatter_2, Color color_16)
		{
			Plugin.Core.Colorful.Console.smethod_0((new TextFormatter(color_16)).GetFormatMap(gparam_0.ToString(), new object[] { formatter_0.Target, formatter_1.Target, formatter_2.Target }, new Color[] { formatter_0.Color, formatter_1.Color, formatter_2.Color }), string_2);
		}

		private static void smethod_25<T>(string string_2, T gparam_0, Formatter[] formatter_0, Color color_16)
		{
			Plugin.Core.Colorful.Console.smethod_0((new TextFormatter(color_16)).GetFormatMap(gparam_0.ToString(), (
				from  in (IEnumerable<Formatter>)formatter_0
				select argument0.Target).ToArray<object>(), (
				from  in (IEnumerable<Formatter>)formatter_0
				select argument1.Color).ToArray<Color>()), string_2);
		}

		private static void smethod_26<T>(Action<object, Color> action_0, IEnumerable<T> ienumerable_0, Color color_16, Color color_17, int int_2)
		{
			foreach (StyleClass<T> styleClass in (new GradientGenerator()).GenerateGradient<T>(ienumerable_0, color_16, color_17, int_2))
			{
				action_0(styleClass.Target, styleClass.Color);
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
			ConcurrentDictionary<Color, ConsoleColor> colors = new ConcurrentDictionary<Color, ConsoleColor>();
			ConcurrentDictionary<ConsoleColor, Color> consoleColors = new ConcurrentDictionary<ConsoleColor, Color>();
			consoleColors.TryAdd(ConsoleColor.Black, Plugin.Core.Colorful.Console.color_0);
			consoleColors.TryAdd(ConsoleColor.Blue, Plugin.Core.Colorful.Console.color_1);
			consoleColors.TryAdd(ConsoleColor.Cyan, Plugin.Core.Colorful.Console.color_2);
			consoleColors.TryAdd(ConsoleColor.DarkBlue, Plugin.Core.Colorful.Console.color_3);
			consoleColors.TryAdd(ConsoleColor.DarkCyan, Plugin.Core.Colorful.Console.color_4);
			consoleColors.TryAdd(ConsoleColor.DarkGray, Plugin.Core.Colorful.Console.color_5);
			consoleColors.TryAdd(ConsoleColor.DarkGreen, Plugin.Core.Colorful.Console.color_6);
			consoleColors.TryAdd(ConsoleColor.DarkMagenta, Plugin.Core.Colorful.Console.color_7);
			consoleColors.TryAdd(ConsoleColor.DarkRed, Plugin.Core.Colorful.Console.color_8);
			consoleColors.TryAdd(ConsoleColor.DarkYellow, Plugin.Core.Colorful.Console.color_9);
			consoleColors.TryAdd(ConsoleColor.Gray, Plugin.Core.Colorful.Console.color_10);
			consoleColors.TryAdd(ConsoleColor.Green, Plugin.Core.Colorful.Console.color_11);
			consoleColors.TryAdd(ConsoleColor.Magenta, Plugin.Core.Colorful.Console.color_12);
			consoleColors.TryAdd(ConsoleColor.Red, Plugin.Core.Colorful.Console.color_13);
			consoleColors.TryAdd(ConsoleColor.White, Plugin.Core.Colorful.Console.color_14);
			consoleColors.TryAdd(ConsoleColor.Yellow, Plugin.Core.Colorful.Console.color_15);
			return new ColorStore(colors, consoleColors);
		}

		private static void smethod_29(bool bool_0)
		{
			Plugin.Core.Colorful.Console.colorStore_0 = Plugin.Core.Colorful.Console.smethod_28();
			Plugin.Core.Colorful.Console.colorManagerFactory_0 = new ColorManagerFactory();
			Plugin.Core.Colorful.Console.colorManager_0 = Plugin.Core.Colorful.Console.colorManagerFactory_0.GetManager(Plugin.Core.Colorful.Console.colorStore_0, 16, 1, bool_0);
			if (!Plugin.Core.Colorful.Console.colorManager_0.IsInCompatibilityMode)
			{
				(new ColorMapper()).SetBatchBufferColors(Plugin.Core.Colorful.Console.dictionary_0);
			}
		}

		private static void smethod_3(Action<string> action_0, char[] char_0, int int_2, int int_3, Color color_16)
		{
			string str = char_0.smethod_2<char[]>().Substring(int_2, int_3);
			Plugin.Core.Colorful.Console.smethod_2<string>(action_0, str, color_16);
		}

		private static void smethod_30(object sender, ConsoleCancelEventArgs e)
		{
			Plugin.Core.Colorful.Console.consoleCancelEventHandler_0(sender, e);
		}

		private static void smethod_4<T>(Action<T> action_0, T gparam_0, ColorAlternator colorAlternator_0)
		{
			Color nextColor = colorAlternator_0.GetNextColor(gparam_0.smethod_2<T>());
			ConsoleColor foregroundColor = System.Console.ForegroundColor;
			System.Console.ForegroundColor = Plugin.Core.Colorful.Console.colorManager_0.GetConsoleColor(nextColor);
			action_0(gparam_0);
			System.Console.ForegroundColor = foregroundColor;
		}

		private static void smethod_5(Action<string> action_0, char[] char_0, int int_2, int int_3, ColorAlternator colorAlternator_0)
		{
			string str = char_0.smethod_2<char[]>().Substring(int_2, int_3);
			Plugin.Core.Colorful.Console.smethod_4<string>(action_0, str, colorAlternator_0);
		}

		private static void smethod_6<T>(string string_2, T gparam_0, StyleSheet styleSheet_0)
		{
			Plugin.Core.Colorful.Console.smethod_0((new TextAnnotator(styleSheet_0)).GetAnnotationMap(gparam_0.smethod_2<T>()), string_2);
		}

		private static void smethod_7(string string_2, StyledString styledString_0, StyleSheet styleSheet_0)
		{
			Plugin.Core.Colorful.Console.smethod_8((new TextAnnotator(styleSheet_0)).GetAnnotationMap(styledString_0.AbstractValue), styledString_0);
			Plugin.Core.Colorful.Console.smethod_1(styledString_0, string_2);
		}

		private static void smethod_8(IEnumerable<KeyValuePair<string, Color>> ienumerable_0, StyledString styledString_0)
		{
			int ınt32 = 0;
			foreach (KeyValuePair<string, Color> ienumerable0 in ienumerable_0)
			{
				for (int i = 0; i < ienumerable0.Key.Length; i++)
				{
					int length = styledString_0.CharacterIndexGeometry.GetLength(0);
					int length1 = styledString_0.CharacterIndexGeometry.GetLength(1);
					for (int j = 0; j < length; j++)
					{
						for (int k = 0; k < length1; k++)
						{
							if (styledString_0.CharacterIndexGeometry[j, k] == ınt32)
							{
								styledString_0.ColorGeometry[j, k] = ienumerable0.Value;
							}
						}
					}
					ınt32++;
				}
			}
		}

		private static void smethod_9(string string_2, char[] char_0, int int_2, int int_3, StyleSheet styleSheet_0)
		{
			string str = char_0.smethod_2<char[]>().Substring(int_2, int_3);
			Plugin.Core.Colorful.Console.smethod_6<string>(string_2, str, styleSheet_0);
		}

		public static void Write(bool value)
		{
			System.Console.Write(value);
		}

		public static void Write(bool value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<bool>(new Action<bool>(System.Console.Write), value, color);
		}

		public static void Write(char value)
		{
			System.Console.Write(value);
		}

		public static void Write(char value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<char>(new Action<char>(System.Console.Write), value, color);
		}

		public static void Write(char[] value)
		{
			System.Console.Write(value);
		}

		public static void Write(char[] value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<char[]>(new Action<char[]>(System.Console.Write), value, color);
		}

		public static void Write(decimal value)
		{
			System.Console.Write(value);
		}

		public static void Write(decimal value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<decimal>(new Action<decimal>(System.Console.Write), value, color);
		}

		public static void Write(double value)
		{
			System.Console.Write(value);
		}

		public static void Write(double value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<double>(new Action<double>(System.Console.Write), value, color);
		}

		public static void Write(float value)
		{
			System.Console.Write(value);
		}

		public static void Write(float value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<float>(new Action<float>(System.Console.Write), value, color);
		}

		public static void Write(int value)
		{
			System.Console.Write(value);
		}

		public static void Write(int value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<int>(new Action<int>(System.Console.Write), value, color);
		}

		public static void Write(long value)
		{
			System.Console.Write(value);
		}

		public static void Write(long value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<long>(new Action<long>(System.Console.Write), value, color);
		}

		public static void Write(object value)
		{
			System.Console.Write(value);
		}

		public static void Write(object value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<object>(new Action<object>(System.Console.Write), value, color);
		}

		public static void Write(string value)
		{
			System.Console.Write(value);
		}

		public static void Write(string value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<string>(new Action<string>(System.Console.Write), value, color);
		}

		public static void Write(uint value)
		{
			System.Console.Write(value);
		}

		public static void Write(uint value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<uint>(new Action<uint>(System.Console.Write), value, color);
		}

		public static void Write(ulong value)
		{
			System.Console.Write(value);
		}

		public static void Write(ulong value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<ulong>(new Action<ulong>(System.Console.Write), value, color);
		}

		public static void Write(string format, object arg0)
		{
			System.Console.Write(format, arg0);
		}

		public static void Write(string format, object arg0, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_10<string, object>(new Action<string, object>(System.Console.Write), format, arg0, color);
		}

		public static void Write(string format, params object[] args)
		{
			System.Console.Write(format, args);
		}

		public static void Write(string format, Color color, params object[] args)
		{
			Plugin.Core.Colorful.Console.smethod_10<string, object[]>(new Action<string, object[]>(System.Console.Write), format, args, color);
		}

		public static void Write(char[] buffer, int index, int count)
		{
			System.Console.Write(buffer, index, count);
		}

		public static void Write(char[] buffer, int index, int count, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_3(new Action<string>(System.Console.Write), buffer, index, count, color);
		}

		public static void Write(string format, object arg0, object arg1)
		{
			System.Console.Write(format, arg0, arg1);
		}

		public static void Write(string format, object arg0, object arg1, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_15<string, object>(new Action<string, object, object>(System.Console.Write), format, arg0, arg1, color);
		}

		public static void Write(string format, object arg0, object arg1, object arg2)
		{
			System.Console.Write(format, arg0, arg1, arg2);
		}

		public static void Write(string format, object arg0, object arg1, object arg2, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_20<string, object>(new Action<string, object, object, object>(System.Console.Write), format, arg0, arg1, arg2, color);
		}

		public static void Write(string format, object arg0, object arg1, object arg2, object arg3)
		{
			System.Console.Write(format, new object[] { arg0, arg1, arg2, arg3 });
		}

		public static void Write(string format, object arg0, object arg1, object arg2, object arg3, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_10<string, object[]>(new Action<string, object[]>(System.Console.Write), format, new object[] { arg0, arg1, arg2, arg3 }, color);
		}

		public static void WriteAlternating(bool value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<bool>(new Action<bool>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(char value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<char>(new Action<char>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(char[] value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<char[]>(new Action<char[]>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(decimal value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<decimal>(new Action<decimal>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(double value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<double>(new Action<double>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(float value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<float>(new Action<float>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(int value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<int>(new Action<int>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(long value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<long>(new Action<long>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(object value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<object>(new Action<object>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(string value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<string>(new Action<string>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(uint value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<uint>(new Action<uint>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(ulong value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<ulong>(new Action<ulong>(System.Console.Write), value, alternator);
		}

		public static void WriteAlternating(string format, object arg0, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_11<string, object>(new Action<string, object>(System.Console.Write), format, arg0, alternator);
		}

		public static void WriteAlternating(string format, ColorAlternator alternator, params object[] args)
		{
			Plugin.Core.Colorful.Console.smethod_11<string, object[]>(new Action<string, object[]>(System.Console.Write), format, args, alternator);
		}

		public static void WriteAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_5(new Action<string>(System.Console.Write), buffer, index, count, alternator);
		}

		public static void WriteAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_16<string, object>(new Action<string, object, object>(System.Console.Write), format, arg0, arg1, alternator);
		}

		public static void WriteAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_21<string, object>(new Action<string, object, object, object>(System.Console.Write), format, arg0, arg1, arg2, alternator);
		}

		public static void WriteAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_11<string, object[]>(new Action<string, object[]>(System.Console.Write), format, new object[] { arg0, arg1, arg2, arg3 }, alternator);
		}

		public static void WriteAscii(string value)
		{
			Plugin.Core.Colorful.Console.WriteAscii(value, null);
		}

		public static void WriteAscii(string value, FigletFont font)
		{
			Plugin.Core.Colorful.Console.WriteLine(Plugin.Core.Colorful.Console.smethod_27(font).ToAscii(value).ConcreteValue);
		}

		public static void WriteAscii(string value, Color color)
		{
			Plugin.Core.Colorful.Console.WriteAscii(value, null, color);
		}

		public static void WriteAscii(string value, FigletFont font, Color color)
		{
			Plugin.Core.Colorful.Console.WriteLine(Plugin.Core.Colorful.Console.smethod_27(font).ToAscii(value).ConcreteValue, color);
		}

		public static void WriteAsciiAlternating(string value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.WriteAsciiAlternating(value, null, alternator);
		}

		public static void WriteAsciiAlternating(string value, FigletFont font, ColorAlternator alternator)
		{
			string[] strArrays = Plugin.Core.Colorful.Console.smethod_27(font).ToAscii(value).ConcreteValue.Split(new char[] { '\n' });
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				Plugin.Core.Colorful.Console.WriteLineAlternating(strArrays[i], alternator);
			}
		}

		public static void WriteAsciiStyled(string value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.WriteAsciiStyled(value, null, styleSheet);
		}

		public static void WriteAsciiStyled(string value, FigletFont font, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.WriteLineStyled(Plugin.Core.Colorful.Console.smethod_27(font).ToAscii(value), styleSheet);
		}

		public static void WriteFormatted(string format, object arg0, Color styledColor, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_13<string, object>(Plugin.Core.Colorful.Console.string_1, format, arg0, styledColor, defaultColor);
		}

		public static void WriteFormatted(string format, Formatter arg0, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_14<string>(Plugin.Core.Colorful.Console.string_1, format, arg0, defaultColor);
		}

		public static void WriteFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
		{
			Plugin.Core.Colorful.Console.smethod_13<string, object[]>(Plugin.Core.Colorful.Console.string_1, format, args, styledColor, defaultColor);
		}

		public static void WriteFormatted(string format, Color defaultColor, params Formatter[] args)
		{
			Plugin.Core.Colorful.Console.smethod_25<string>(Plugin.Core.Colorful.Console.string_1, format, args, defaultColor);
		}

		public static void WriteFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_18<string, object>(Plugin.Core.Colorful.Console.string_1, format, arg0, arg1, styledColor, defaultColor);
		}

		public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_19<string>(Plugin.Core.Colorful.Console.string_1, format, arg0, arg1, defaultColor);
		}

		public static void WriteFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_23<string, object>(Plugin.Core.Colorful.Console.string_1, format, arg0, arg1, arg2, styledColor, defaultColor);
		}

		public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_24<string>(Plugin.Core.Colorful.Console.string_1, format, arg0, arg1, arg2, defaultColor);
		}

		public static void WriteFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_13<string, object[]>(Plugin.Core.Colorful.Console.string_1, format, new object[] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
		}

		public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_25<string>(Plugin.Core.Colorful.Console.string_1, format, new Formatter[] { arg0, arg1, arg2, arg3 }, defaultColor);
		}

		public static void WriteLine()
		{
			System.Console.WriteLine();
		}

		public static void WriteLine(bool value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(bool value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<bool>(new Action<bool>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(char value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(char value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<char>(new Action<char>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(char[] value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(char[] value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<char[]>(new Action<char[]>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(decimal value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(decimal value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<decimal>(new Action<decimal>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(double value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(double value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<double>(new Action<double>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(float value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(float value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<float>(new Action<float>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(int value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(int value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<int>(new Action<int>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(long value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(long value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<long>(new Action<long>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(object value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(object value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<object>(new Action<object>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(string value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(string value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<string>(new Action<string>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(uint value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(uint value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<uint>(new Action<uint>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(ulong value)
		{
			System.Console.WriteLine(value);
		}

		public static void WriteLine(ulong value, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_2<ulong>(new Action<ulong>(System.Console.WriteLine), value, color);
		}

		public static void WriteLine(string format, object arg0)
		{
			System.Console.WriteLine(format, arg0);
		}

		public static void WriteLine(string format, object arg0, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_10<string, object>(new Action<string, object>(System.Console.WriteLine), format, arg0, color);
		}

		public static void WriteLine(string format, params object[] args)
		{
			System.Console.WriteLine(format, args);
		}

		public static void WriteLine(string format, Color color, params object[] args)
		{
			Plugin.Core.Colorful.Console.smethod_10<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, args, color);
		}

		public static void WriteLine(char[] buffer, int index, int count)
		{
			System.Console.WriteLine(buffer, index, count);
		}

		public static void WriteLine(char[] buffer, int index, int count, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_3(new Action<string>(System.Console.WriteLine), buffer, index, count, color);
		}

		public static void WriteLine(string format, object arg0, object arg1)
		{
			System.Console.WriteLine(format, arg0, arg1);
		}

		public static void WriteLine(string format, object arg0, object arg1, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_15<string, object>(new Action<string, object, object>(System.Console.WriteLine), format, arg0, arg1, color);
		}

		public static void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			System.Console.WriteLine(format, arg0, arg1, arg2);
		}

		public static void WriteLine(string format, object arg0, object arg1, object arg2, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_20<string, object>(new Action<string, object, object, object>(System.Console.WriteLine), format, arg0, arg1, arg2, color);
		}

		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
		{
			System.Console.WriteLine(format, new object[] { arg0, arg1, arg2, arg3 });
		}

		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, Color color)
		{
			Plugin.Core.Colorful.Console.smethod_10<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, new object[] { arg0, arg1, arg2, arg3 }, color);
		}

		public static void WriteLineAlternating(ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<string>(new Action<string>(System.Console.Write), Plugin.Core.Colorful.Console.string_0, alternator);
		}

		public static void WriteLineAlternating(bool value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<bool>(new Action<bool>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(char value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<char>(new Action<char>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(char[] value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<char[]>(new Action<char[]>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(decimal value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<decimal>(new Action<decimal>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(double value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<double>(new Action<double>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(float value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<float>(new Action<float>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(int value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<int>(new Action<int>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(long value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<long>(new Action<long>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(object value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<object>(new Action<object>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(string value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<string>(new Action<string>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(uint value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<uint>(new Action<uint>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(ulong value, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_4<ulong>(new Action<ulong>(System.Console.WriteLine), value, alternator);
		}

		public static void WriteLineAlternating(string format, object arg0, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_11<string, object>(new Action<string, object>(System.Console.WriteLine), format, arg0, alternator);
		}

		public static void WriteLineAlternating(string format, ColorAlternator alternator, params object[] args)
		{
			Plugin.Core.Colorful.Console.smethod_11<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, args, alternator);
		}

		public static void WriteLineAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_5(new Action<string>(System.Console.WriteLine), buffer, index, count, alternator);
		}

		public static void WriteLineAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_16<string, object>(new Action<string, object, object>(System.Console.WriteLine), format, arg0, arg1, alternator);
		}

		public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_21<string, object>(new Action<string, object, object, object>(System.Console.WriteLine), format, arg0, arg1, arg2, alternator);
		}

		public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
		{
			Plugin.Core.Colorful.Console.smethod_11<string, object[]>(new Action<string, object[]>(System.Console.WriteLine), format, new object[] { arg0, arg1, arg2, arg3 }, alternator);
		}

		public static void WriteLineFormatted(string format, object arg0, Color styledColor, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_13<string, object>(Plugin.Core.Colorful.Console.string_0, format, arg0, styledColor, defaultColor);
		}

		public static void WriteLineFormatted(string format, Formatter arg0, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_14<string>(Plugin.Core.Colorful.Console.string_0, format, arg0, defaultColor);
		}

		public static void WriteLineFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
		{
			Plugin.Core.Colorful.Console.smethod_13<string, object[]>(Plugin.Core.Colorful.Console.string_0, format, args, styledColor, defaultColor);
		}

		public static void WriteLineFormatted(string format, Color defaultColor, params Formatter[] args)
		{
			Plugin.Core.Colorful.Console.smethod_25<string>(Plugin.Core.Colorful.Console.string_0, format, args, defaultColor);
		}

		public static void WriteLineFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_18<string, object>(Plugin.Core.Colorful.Console.string_0, format, arg0, arg1, styledColor, defaultColor);
		}

		public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_19<string>(Plugin.Core.Colorful.Console.string_0, format, arg0, arg1, defaultColor);
		}

		public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_23<string, object>(Plugin.Core.Colorful.Console.string_0, format, arg0, arg1, arg2, styledColor, defaultColor);
		}

		public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_24<string>(Plugin.Core.Colorful.Console.string_0, format, arg0, arg1, arg2, defaultColor);
		}

		public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_13<string, object[]>(Plugin.Core.Colorful.Console.string_0, format, new object[] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
		}

		public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
		{
			Plugin.Core.Colorful.Console.smethod_25<string>(Plugin.Core.Colorful.Console.string_0, format, new Formatter[] { arg0, arg1, arg2, arg3 }, defaultColor);
		}

		public static void WriteLineStyled(StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<string>(Plugin.Core.Colorful.Console.string_1, Plugin.Core.Colorful.Console.string_0, styleSheet);
		}

		public static void WriteLineStyled(bool value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<bool>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(char value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<char>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(char[] value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<char[]>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(decimal value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<decimal>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(double value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<double>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(float value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<float>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(int value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<int>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(long value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<long>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(StyledString value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_7(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(string value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<string>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(uint value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<uint>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(ulong value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<ulong>(Plugin.Core.Colorful.Console.string_0, value, styleSheet);
		}

		public static void WriteLineStyled(string format, object arg0, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_12<string, object>(Plugin.Core.Colorful.Console.string_0, format, arg0, styleSheet);
		}

		public static void WriteLineStyled(StyleSheet styleSheet, string format, params object[] args)
		{
			Plugin.Core.Colorful.Console.smethod_12<string, object[]>(Plugin.Core.Colorful.Console.string_0, format, args, styleSheet);
		}

		public static void WriteLineStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_9(Plugin.Core.Colorful.Console.string_0, buffer, index, count, styleSheet);
		}

		public static void WriteLineStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_17<string, object>(Plugin.Core.Colorful.Console.string_0, format, arg0, arg1, styleSheet);
		}

		public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_22<string, object>(Plugin.Core.Colorful.Console.string_0, format, arg0, arg1, arg2, styleSheet);
		}

		public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_12<string, object[]>(Plugin.Core.Colorful.Console.string_0, format, new object[] { arg0, arg1, arg2, arg3 }, styleSheet);
		}

		public static void WriteLineWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 16)
		{
			Plugin.Core.Colorful.Console.smethod_26<T>(new Action<object, Color>(Plugin.Core.Colorful.Console.WriteLine), input, startColor, endColor, maxColorsInGradient);
		}

		public static void WriteStyled(bool value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<bool>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(char value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<char>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(char[] value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<char[]>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(decimal value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<decimal>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(double value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<double>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(float value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<float>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(int value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<int>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(long value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<long>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(object value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<object>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(string value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<string>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(uint value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<uint>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(ulong value, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_6<ulong>(Plugin.Core.Colorful.Console.string_1, value, styleSheet);
		}

		public static void WriteStyled(string format, object arg0, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_12<string, object>(Plugin.Core.Colorful.Console.string_1, format, arg0, styleSheet);
		}

		public static void WriteStyled(StyleSheet styleSheet, string format, params object[] args)
		{
			Plugin.Core.Colorful.Console.smethod_12<string, object[]>(Plugin.Core.Colorful.Console.string_1, format, args, styleSheet);
		}

		public static void WriteStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_9(Plugin.Core.Colorful.Console.string_1, buffer, index, count, styleSheet);
		}

		public static void WriteStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_17<string, object>(Plugin.Core.Colorful.Console.string_1, format, arg0, arg1, styleSheet);
		}

		public static void WriteStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_22<string, object>(Plugin.Core.Colorful.Console.string_1, format, arg0, arg1, arg2, styleSheet);
		}

		public static void WriteStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
		{
			Plugin.Core.Colorful.Console.smethod_12<string, object[]>(Plugin.Core.Colorful.Console.string_1, format, new object[] { arg0, arg1, arg2, arg3 }, styleSheet);
		}

		public static void WriteWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 16)
		{
			Plugin.Core.Colorful.Console.smethod_26<T>(new Action<object, Color>(Plugin.Core.Colorful.Console.Write), input, startColor, endColor, maxColorsInGradient);
		}

		public static event ConsoleCancelEventHandler CancelKeyPress
		{
			add
			{
				ConsoleCancelEventHandler consoleCancelEventHandler;
				ConsoleCancelEventHandler consoleCancelEventHandler0 = Plugin.Core.Colorful.Console.consoleCancelEventHandler_0;
				do
				{
					consoleCancelEventHandler = consoleCancelEventHandler0;
					ConsoleCancelEventHandler consoleCancelEventHandler1 = (ConsoleCancelEventHandler)Delegate.Combine(consoleCancelEventHandler, value);
					consoleCancelEventHandler0 = Interlocked.CompareExchange<ConsoleCancelEventHandler>(ref Plugin.Core.Colorful.Console.consoleCancelEventHandler_0, consoleCancelEventHandler1, consoleCancelEventHandler);
				}
				while ((object)consoleCancelEventHandler0 != (object)consoleCancelEventHandler);
			}
			remove
			{
				ConsoleCancelEventHandler consoleCancelEventHandler;
				ConsoleCancelEventHandler consoleCancelEventHandler0 = Plugin.Core.Colorful.Console.consoleCancelEventHandler_0;
				do
				{
					consoleCancelEventHandler = consoleCancelEventHandler0;
					ConsoleCancelEventHandler consoleCancelEventHandler1 = (ConsoleCancelEventHandler)Delegate.Remove(consoleCancelEventHandler, value);
					consoleCancelEventHandler0 = Interlocked.CompareExchange<ConsoleCancelEventHandler>(ref Plugin.Core.Colorful.Console.consoleCancelEventHandler_0, consoleCancelEventHandler1, consoleCancelEventHandler);
				}
				while ((object)consoleCancelEventHandler0 != (object)consoleCancelEventHandler);
			}
		}
	}
}