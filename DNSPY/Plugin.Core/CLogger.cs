using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Plugin.Core.Colorful;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core
{
	// Token: 0x02000004 RID: 4
	public static class CLogger
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00008644 File Offset: 0x00006844
		static CLogger()
		{
			try
			{
				foreach (string text in new string[]
				{
					"Logs/",
					CLogger.string_1,
					CLogger.string_2,
					CLogger.string_3,
					CLogger.string_4,
					CLogger.string_5,
					CLogger.string_6
				}.Where(new Func<string, bool>(CLogger.Class1.<>9.method_0)))
				{
					Directory.CreateDirectory(text);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00008794 File Offset: 0x00006994
		public static void Print(string Text, LoggerType Type, Exception Ex = null)
		{
			switch (Type)
			{
			case LoggerType.Info:
				CLogger.smethod_1("{0}", Text, Ex, Type);
				return;
			case LoggerType.Debug:
				CLogger.smethod_1("{2}", Text, Ex, Type);
				return;
			case LoggerType.Warning:
				CLogger.smethod_1("{1}", Text, Ex, Type);
				return;
			case LoggerType.Error:
				CLogger.smethod_1("{3}", Text, Ex, Type);
				return;
			case LoggerType.Hack:
				CLogger.smethod_1("{4}", Text, Ex, Type);
				return;
			case LoggerType.Command:
				CLogger.smethod_1("{5}", Text, Ex, Type);
				return;
			case LoggerType.Console:
				CLogger.smethod_1("{5}", Text, Ex, Type);
				return;
			case LoggerType.Opcode:
				CLogger.smethod_1("{-}", Text, Ex, Type);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000883C File Offset: 0x00006A3C
		private static string[] smethod_0(Exception exception_0)
		{
			string[] array = new string[] { "", "", "" };
			try
			{
				StackTrace stackTrace = new StackTrace(exception_0, true);
				if (stackTrace != null)
				{
					array[0] = stackTrace.GetFrame(0).GetMethod().ReflectedType.Name;
					array[1] = stackTrace.GetFrame(0).GetFileLineNumber().ToString();
					array[2] = stackTrace.GetFrame(0).GetFileColumnNumber().ToString();
				}
			}
			catch
			{
			}
			return array;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000088D0 File Offset: 0x00006AD0
		private static void smethod_1(string string_7, string string_8, Exception exception_0, LoggerType loggerType_0)
		{
			try
			{
				object obj = CLogger.object_0;
				lock (obj)
				{
					if (!loggerType_0.Equals(LoggerType.Opcode))
					{
						Formatter[] array = new Formatter[]
						{
							new Formatter("[I]", ColorUtil.White),
							new Formatter("[W]", ColorUtil.Yellow),
							new Formatter("[D]", ColorUtil.Cyan),
							new Formatter("[E]", ColorUtil.Red),
							new Formatter("[H]", ColorUtil.Red),
							new Formatter("[C]", ColorUtil.Red)
						};
						Plugin.Core.Colorful.Console.WriteLineFormatted(string.Concat(new string[]
						{
							DateTimeUtil.Now("HH:mm:ss"),
							" ",
							string_7,
							" ",
							string_8
						}), ColorUtil.LightGrey, array);
					}
					else
					{
						Plugin.Core.Colorful.Console.WriteLine(string_8, ColorUtil.Blue);
					}
					string text = ((loggerType_0.Equals(LoggerType.Info) || loggerType_0.Equals(LoggerType.Warning)) ? ("Logs/" + CLogger.string_0 + ".log") : (loggerType_0.Equals(LoggerType.Error) ? string.Format("Logs/{0}/{1}-{2}.log", loggerType_0, CLogger.string_0, (exception_0 != null) ? CLogger.smethod_0(exception_0)[0] : "NULL") : string.Format("Logs/{0}/{1}.log", loggerType_0, CLogger.string_0)));
					CLogger.smethod_2(string_8, exception_0, text);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00008AA4 File Offset: 0x00006CA4
		private static void smethod_2(string string_7, Exception exception_0, string string_8)
		{
			using (FileStream fileStream = new FileStream(string_8, FileMode.Append))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
				{
					try
					{
						string text = ((exception_0 != null) ? string.Format("{0} \n{1}", string_7, exception_0) : string_7);
						streamWriter.WriteLine(text);
					}
					catch
					{
					}
					finally
					{
						streamWriter.Flush();
						streamWriter.Close();
						fileStream.Flush();
						fileStream.Close();
					}
				}
			}
		}

		// Token: 0x04000041 RID: 65
		private static readonly string string_0 = DateTimeUtil.Now("yyyy-MM-dd--HH-mm-ss");

		// Token: 0x04000042 RID: 66
		private static readonly string string_1 = string.Format("Logs/{0}", LoggerType.Command);

		// Token: 0x04000043 RID: 67
		private static readonly string string_2 = string.Format("Logs/{0}", LoggerType.Console);

		// Token: 0x04000044 RID: 68
		private static readonly string string_3 = string.Format("Logs/{0}", LoggerType.Debug);

		// Token: 0x04000045 RID: 69
		private static readonly string string_4 = string.Format("Logs/{0}", LoggerType.Error);

		// Token: 0x04000046 RID: 70
		private static readonly string string_5 = string.Format("Logs/{0}", LoggerType.Hack);

		// Token: 0x04000047 RID: 71
		private static readonly string string_6 = string.Format("Logs/{0}", LoggerType.Opcode);

		// Token: 0x04000048 RID: 72
		private static readonly object object_0 = new object();

		// Token: 0x02000005 RID: 5
		[CompilerGenerated]
		[Serializable]
		private sealed class Class1
		{
			// Token: 0x06000014 RID: 20 RVA: 0x0000210A File Offset: 0x0000030A
			// Note: this type is marked as 'beforefieldinit'.
			static Class1()
			{
			}

			// Token: 0x06000015 RID: 21 RVA: 0x00002116 File Offset: 0x00000316
			public Class1()
			{
			}

			// Token: 0x06000016 RID: 22 RVA: 0x0000211E File Offset: 0x0000031E
			internal bool method_0(string string_0)
			{
				return !Directory.Exists(string_0);
			}

			// Token: 0x04000049 RID: 73
			public static readonly CLogger.Class1 <>9 = new CLogger.Class1();
		}
	}
}
