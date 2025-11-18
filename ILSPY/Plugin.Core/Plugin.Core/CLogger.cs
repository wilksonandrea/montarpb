using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Plugin.Core.Colorful;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core;

public static class CLogger
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class1
	{
		public static readonly Class1 _003C_003E9 = new Class1();

		internal bool method_0(string string_0)
		{
			return !Directory.Exists(string_0);
		}
	}

	private static readonly string string_0;

	private static readonly string string_1;

	private static readonly string string_2;

	private static readonly string string_3;

	private static readonly string string_4;

	private static readonly string string_5;

	private static readonly string string_6;

	private static readonly object object_0;

	static CLogger()
	{
		string_0 = DateTimeUtil.Now("yyyy-MM-dd--HH-mm-ss");
		string_1 = $"Logs/{LoggerType.Command}";
		string_2 = $"Logs/{LoggerType.Console}";
		string_3 = $"Logs/{LoggerType.Debug}";
		string_4 = $"Logs/{LoggerType.Error}";
		string_5 = $"Logs/{LoggerType.Hack}";
		string_6 = $"Logs/{LoggerType.Opcode}";
		object_0 = new object();
		try
		{
			foreach (string item in new string[7] { "Logs/", string_1, string_2, string_3, string_4, string_5, string_6 }.Where((string string_0) => !Directory.Exists(string_0)))
			{
				Directory.CreateDirectory(item);
			}
		}
		catch (Exception ex)
		{
			Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static void Print(string Text, LoggerType Type, Exception Ex = null)
	{
		switch (Type)
		{
		case LoggerType.Info:
			smethod_1("{0}", Text, Ex, Type);
			break;
		case LoggerType.Debug:
			smethod_1("{2}", Text, Ex, Type);
			break;
		case LoggerType.Warning:
			smethod_1("{1}", Text, Ex, Type);
			break;
		case LoggerType.Error:
			smethod_1("{3}", Text, Ex, Type);
			break;
		case LoggerType.Hack:
			smethod_1("{4}", Text, Ex, Type);
			break;
		case LoggerType.Command:
			smethod_1("{5}", Text, Ex, Type);
			break;
		case LoggerType.Console:
			smethod_1("{5}", Text, Ex, Type);
			break;
		case LoggerType.Opcode:
			smethod_1("{-}", Text, Ex, Type);
			break;
		}
	}

	private static string[] smethod_0(Exception exception_0)
	{
		string[] array = new string[3] { "", "", "" };
		try
		{
			StackTrace stackTrace = new StackTrace(exception_0, fNeedFileInfo: true);
			if (stackTrace != null)
			{
				array[0] = stackTrace.GetFrame(0).GetMethod().ReflectedType.Name;
				array[1] = stackTrace.GetFrame(0).GetFileLineNumber().ToString();
				array[2] = stackTrace.GetFrame(0).GetFileColumnNumber().ToString();
				return array;
			}
			return array;
		}
		catch
		{
			return array;
		}
	}

	private static void smethod_1(string string_7, string string_8, Exception exception_0, LoggerType loggerType_0)
	{
		try
		{
			lock (object_0)
			{
				if (!loggerType_0.Equals(LoggerType.Opcode))
				{
					Formatter[] args = new Formatter[6]
					{
						new Formatter("[I]", ColorUtil.White),
						new Formatter("[W]", ColorUtil.Yellow),
						new Formatter("[D]", ColorUtil.Cyan),
						new Formatter("[E]", ColorUtil.Red),
						new Formatter("[H]", ColorUtil.Red),
						new Formatter("[C]", ColorUtil.Red)
					};
					Plugin.Core.Colorful.Console.WriteLineFormatted(DateTimeUtil.Now("HH:mm:ss") + " " + string_7 + " " + string_8, ColorUtil.LightGrey, args);
				}
				else
				{
					Plugin.Core.Colorful.Console.WriteLine(string_8, ColorUtil.Blue);
				}
				string string_9 = ((loggerType_0.Equals(LoggerType.Info) || loggerType_0.Equals(LoggerType.Warning)) ? ("Logs/" + string_0 + ".log") : (loggerType_0.Equals(LoggerType.Error) ? string.Format("Logs/{0}/{1}-{2}.log", loggerType_0, string_0, (exception_0 != null) ? smethod_0(exception_0)[0] : "NULL") : $"Logs/{loggerType_0}/{string_0}.log"));
				smethod_2(string_8, exception_0, string_9);
			}
		}
		catch
		{
		}
	}

	private static void smethod_2(string string_7, Exception exception_0, string string_8)
	{
		using FileStream fileStream = new FileStream(string_8, FileMode.Append);
		using StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
		try
		{
			string value = ((exception_0 != null) ? $"{string_7} \n{exception_0}" : string_7);
			streamWriter.WriteLine(value);
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
