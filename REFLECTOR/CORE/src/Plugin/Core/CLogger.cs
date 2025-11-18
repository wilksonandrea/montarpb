namespace Plugin.Core
{
    using Plugin.Core.Colorful;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class CLogger
    {
        private static readonly string string_0 = DateTimeUtil.Now("yyyy-MM-dd--HH-mm-ss");
        private static readonly string string_1 = $"Logs/{LoggerType.Command}";
        private static readonly string string_2 = $"Logs/{LoggerType.Console}";
        private static readonly string string_3 = $"Logs/{LoggerType.Debug}";
        private static readonly string string_4 = $"Logs/{LoggerType.Error}";
        private static readonly string string_5 = $"Logs/{LoggerType.Hack}";
        private static readonly string string_6 = $"Logs/{LoggerType.Opcode}";
        private static readonly object object_0 = new object();

        static CLogger()
        {
            try
            {
                string[] source = new string[] { "Logs/", string_1, string_2, string_3, string_4, string_5, string_6 };
                using (IEnumerator<string> enumerator = source.Where<string>(new Func<string, bool>(Class1.<>9.method_0)).GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Directory.CreateDirectory(enumerator.Current);
                    }
                }
            }
            catch (Exception exception)
            {
                Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static void Print(string Text, LoggerType Type, Exception Ex = null)
        {
            switch (Type)
            {
                case LoggerType.Info:
                    smethod_1("{0}", Text, Ex, Type);
                    return;

                case LoggerType.Debug:
                    smethod_1("{2}", Text, Ex, Type);
                    return;

                case LoggerType.Warning:
                    smethod_1("{1}", Text, Ex, Type);
                    return;

                case LoggerType.Error:
                    smethod_1("{3}", Text, Ex, Type);
                    return;

                case LoggerType.Hack:
                    smethod_1("{4}", Text, Ex, Type);
                    return;

                case LoggerType.Command:
                    smethod_1("{5}", Text, Ex, Type);
                    return;

                case LoggerType.Console:
                    smethod_1("{5}", Text, Ex, Type);
                    return;

                case LoggerType.Opcode:
                    smethod_1("{-}", Text, Ex, Type);
                    return;
            }
        }

        private static string[] smethod_0(Exception exception_0)
        {
            string[] strArray = new string[] { "", "", "" };
            try
            {
                StackTrace trace = new StackTrace(exception_0, true);
                if (trace != null)
                {
                    strArray[0] = trace.GetFrame(0).GetMethod().ReflectedType.Name;
                    strArray[1] = trace.GetFrame(0).GetFileLineNumber().ToString();
                    strArray[2] = trace.GetFrame(0).GetFileColumnNumber().ToString();
                }
            }
            catch
            {
            }
            return strArray;
        }

        private static void smethod_1(string string_7, string string_8, Exception exception_0, LoggerType loggerType_0)
        {
            try
            {
                object obj2 = object_0;
                lock (obj2)
                {
                    if (loggerType_0.Equals(LoggerType.Opcode))
                    {
                        Plugin.Core.Colorful.Console.WriteLine(string_8, ColorUtil.Blue);
                    }
                    else
                    {
                        Formatter[] args = new Formatter[] { new Formatter("[I]", ColorUtil.White), new Formatter("[W]", ColorUtil.Yellow), new Formatter("[D]", ColorUtil.Cyan), new Formatter("[E]", ColorUtil.Red), new Formatter("[H]", ColorUtil.Red), new Formatter("[C]", ColorUtil.Red) };
                        string[] textArray1 = new string[] { DateTimeUtil.Now("HH:mm:ss"), " ", string_7, " ", string_8 };
                        Plugin.Core.Colorful.Console.WriteLineFormatted(string.Concat(textArray1), ColorUtil.LightGrey, args);
                    }
                    string str = (loggerType_0.Equals(LoggerType.Info) || loggerType_0.Equals(LoggerType.Warning)) ? ("Logs/" + string_0 + ".log") : (loggerType_0.Equals(LoggerType.Error) ? $"Logs/{loggerType_0}/{string_0}-{((exception_0 != null) ? smethod_0(exception_0)[0] : "NULL")}.log" : $"Logs/{loggerType_0}/{string_0}.log");
                    smethod_2(string_8, exception_0, str);
                }
            }
            catch
            {
            }
        }

        private static void smethod_2(string string_7, Exception exception_0, string string_8)
        {
            using (FileStream stream = new FileStream(string_8, FileMode.Append))
            {
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    try
                    {
                        writer.WriteLine((exception_0 != null) ? $"{string_7} 
{exception_0}" : string_7);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        writer.Flush();
                        writer.Close();
                        stream.Flush();
                        stream.Close();
                    }
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class Class1
        {
            public static readonly CLogger.Class1 <>9 = new CLogger.Class1();

            internal bool method_0(string string_0) => 
                !Directory.Exists(string_0);
        }
    }
}

