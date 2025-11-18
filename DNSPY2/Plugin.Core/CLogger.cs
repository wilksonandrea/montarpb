using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Plugin.Core.Colorful;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Console = Plugin.Core.Colorful.Console;

namespace Plugin.Core
{
    public static class CLogger
    {
        private static readonly string Date = DateTimeUtil.Now("yyyy-MM-dd--HH-mm-ss");
        private static readonly string CommandPath = $"Logs/{LoggerType.Command}";
        private static readonly string ConsolePath = $"Logs/{LoggerType.Console}";
        private static readonly string DebugPath = $"Logs/{LoggerType.Debug}";
        private static readonly string ErrorPath = $"Logs/{LoggerType.Error}";
        private static readonly string HackPath = $"Logs/{LoggerType.Hack}";
        private static readonly string OpcodePath = $"Logs/{LoggerType.Opcode}";
        private static readonly object Sync = new object();
        public static DateTime LastAuthException;
        public static DateTime LatchAuthSession;
        public static DateTime LastGameException;
        public static DateTime LastGameSession;
        public static DateTime LastMatchSocket;
        public static DateTime LastMatchBuffer;
        static CLogger()
        {
            try
            {
                DateTime Date = new DateTime();
                LastAuthException = Date;
                LatchAuthSession = Date;
                LastGameException = Date;
                LastGameSession = Date;
                LastMatchSocket = Date;
                LastMatchBuffer = Date;
                string[] Directorys = new string[] { "Logs/", CommandPath, ConsolePath, DebugPath, ErrorPath, HackPath, OpcodePath };
                foreach (string Local in Directorys)
                {
                    if (!Directory.Exists(Local))
                    {
                        Directory.CreateDirectory(Local);
                    }
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
                case LoggerType.Info: Execute("{0}", Text, Ex, Type); break;
                case LoggerType.Warning: Execute("{1}", Text, Ex, Type); break;
                case LoggerType.Debug: Execute("{2}", Text, Ex, Type); break;
                case LoggerType.Error: Execute("{3}", Text, Ex, Type); break;
                case LoggerType.Hack: Execute("{4}", Text, Ex, Type); break;
                case LoggerType.Command: Execute("{5}", Text, Ex, Type); break;
                case LoggerType.Console: Execute("{5}", Text, Ex, Type); break;
                case LoggerType.Opcode: Execute("{-}", Text, Ex, Type); break;
                default: break;
            }
        }
        private static string[] StackTraces(Exception Ex)
        {
            string[] Traces = new string[3] { "", "", "" };
            try
            {
                StackTrace Trace = new StackTrace(Ex, true);
                if (Trace != null)
                {
                    Traces[0] = Trace.GetFrame(0).GetMethod().ReflectedType.Name;
                    Traces[1] = Trace.GetFrame(0).GetFileLineNumber().ToString();
                    Traces[2] = Trace.GetFrame(0).GetFileColumnNumber().ToString();
                }
            }
            catch
            {
            }
            return Traces;
        }
        private static void Execute(string Code, string Text, Exception Ex, LoggerType PathGroup)
        {
            try
            {
                lock (Sync)
                {
                    if (!PathGroup.Equals(LoggerType.Opcode))
                    {
                        Formatter[] TitleFormat = new Formatter[]
                        {
                            new Formatter("[I]", ColorUtil.White),
                            new Formatter("[W]", ColorUtil.Yellow),
                            new Formatter("[D]", ColorUtil.Cyan),
                            new Formatter("[E]", ColorUtil.Red),
                            new Formatter("[H]", ColorUtil.Red),
                            new Formatter("[C]", ColorUtil.Red)
                        };
                        Console.WriteLineFormatted($"{DateTimeUtil.Now("HH:mm:ss")} {Code} {Text}", ColorUtil.LightGrey, TitleFormat);
                    }
                    else
                    {
                        Console.WriteLine(Text, ColorUtil.Blue);
                    }
                    string FinalPath = ((PathGroup.Equals(LoggerType.Info) || PathGroup.Equals(LoggerType.Warning)) ? $"Logs/{Date}.log" : PathGroup.Equals(LoggerType.Error) ? $"Logs/{PathGroup}/{Date}-{(Ex != null ? StackTraces(Ex)[0] : "NULL")}.log" : $"Logs/{PathGroup}/{Date}.log");
                    LOG(Text, Ex, FinalPath);
                }
            }
            catch
            {
            }
        }
        private static void LOG(string Text, Exception Ex, string FinalPath)
        {
            using (FileStream File = new FileStream(FinalPath, FileMode.Append))
            using (StreamWriter Stream = new StreamWriter(File, Encoding.UTF8))
            {
                try
                {
                    string TextFinal = (Ex != null ? $"{Text} \n{Ex}" : Text);
                    Stream.WriteLine(TextFinal);
                }
                catch
                {
                }
                finally
                {
                    Stream.Flush();
                    Stream.Close();
                    File.Flush();
                    File.Close();
                }
            }
        }
    }
}
