using Plugin.Core.Colorful;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Plugin.Core
{
	public static class CLogger
	{
		private readonly static string string_0;

		private readonly static string string_1;

		private readonly static string string_2;

		private readonly static string string_3;

		private readonly static string string_4;

		private readonly static string string_5;

		private readonly static string string_6;

		private readonly static object object_0;

		static CLogger()
		{
			CLogger.string_0 = DateTimeUtil.Now("yyyy-MM-dd--HH-mm-ss");
			CLogger.string_1 = string.Format("Logs/{0}", LoggerType.Command);
			CLogger.string_2 = string.Format("Logs/{0}", LoggerType.Console);
			CLogger.string_3 = string.Format("Logs/{0}", LoggerType.Debug);
			CLogger.string_4 = string.Format("Logs/{0}", LoggerType.Error);
			CLogger.string_5 = string.Format("Logs/{0}", LoggerType.Hack);
			CLogger.string_6 = string.Format("Logs/{0}", LoggerType.Opcode);
			CLogger.object_0 = new object();
			try
			{
				foreach (string str in 
					from string_0 in new string[] { "Logs/", CLogger.string_1, CLogger.string_2, CLogger.string_3, CLogger.string_4, CLogger.string_5, CLogger.string_6 }
					where !Directory.Exists(string_0)
					select string_0)
				{
					Directory.CreateDirectory(str);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public static void Print(string Text, LoggerType Type, Exception Ex = null)
		{
			switch (Type)
			{
				case LoggerType.Info:
				{
					CLogger.smethod_1("{0}", Text, Ex, Type);
					return;
				}
				case LoggerType.Debug:
				{
					CLogger.smethod_1("{2}", Text, Ex, Type);
					return;
				}
				case LoggerType.Warning:
				{
					CLogger.smethod_1("{1}", Text, Ex, Type);
					return;
				}
				case LoggerType.Error:
				{
					CLogger.smethod_1("{3}", Text, Ex, Type);
					return;
				}
				case LoggerType.Hack:
				{
					CLogger.smethod_1("{4}", Text, Ex, Type);
					return;
				}
				case LoggerType.Command:
				{
					CLogger.smethod_1("{5}", Text, Ex, Type);
					return;
				}
				case LoggerType.Console:
				{
					CLogger.smethod_1("{5}", Text, Ex, Type);
					return;
				}
				case LoggerType.Opcode:
				{
					CLogger.smethod_1("{-}", Text, Ex, Type);
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private static string[] smethod_0(Exception exception_0)
		{
			string[] name = new string[] { "", "", "" };
			try
			{
				StackTrace stackTrace = new StackTrace(exception_0, true);
				if (stackTrace != null)
				{
					name[0] = stackTrace.GetFrame(0).GetMethod().ReflectedType.Name;
					int fileLineNumber = stackTrace.GetFrame(0).GetFileLineNumber();
					name[1] = fileLineNumber.ToString();
					fileLineNumber = stackTrace.GetFrame(0).GetFileColumnNumber();
					name[2] = fileLineNumber.ToString();
				}
			}
			catch
			{
			}
			return name;
		}

		private static void smethod_1(string string_7, string string_8, Exception exception_0, LoggerType loggerType_0)
		{
			string str;
			try
			{
				lock (CLogger.object_0)
				{
					if (loggerType_0.Equals(LoggerType.Opcode))
					{
						Plugin.Core.Colorful.Console.WriteLine(string_8, ColorUtil.Blue);
					}
					else
					{
						Formatter[] formatter = new Formatter[] { new Formatter("[I]", ColorUtil.White), new Formatter("[W]", ColorUtil.Yellow), new Formatter("[D]", ColorUtil.Cyan), new Formatter("[E]", ColorUtil.Red), new Formatter("[H]", ColorUtil.Red), new Formatter("[C]", ColorUtil.Red) };
						Plugin.Core.Colorful.Console.WriteLineFormatted(string.Concat(new string[] { DateTimeUtil.Now("HH:mm:ss"), " ", string_7, " ", string_8 }), ColorUtil.LightGrey, formatter);
					}
					if (loggerType_0.Equals(LoggerType.Info) || loggerType_0.Equals(LoggerType.Warning))
					{
						str = string.Concat("Logs/", CLogger.string_0, ".log");
					}
					else
					{
						str = (loggerType_0.Equals(LoggerType.Error) ? string.Format("Logs/{0}/{1}-{2}.log", loggerType_0, CLogger.string_0, (exception_0 != null ? CLogger.smethod_0(exception_0)[0] : "NULL")) : string.Format("Logs/{0}/{1}.log", loggerType_0, CLogger.string_0));
					}
					CLogger.smethod_2(string_8, exception_0, str);
				}
			}
			catch
			{
			}
		}

		private static void smethod_2(string string_7, Exception exception_0, string string_8)
		{
			using (FileStream fileStream = new FileStream(string_8, FileMode.Append))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
				{
					try
					{
						try
						{
							streamWriter.WriteLine((exception_0 != null ? string.Format("{0} \n{1}", string_7, exception_0) : string_7));
						}
						catch
						{
						}
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
	}
}