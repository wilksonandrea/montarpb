// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Translation
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Colorful;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Plugin.Core;

public static class Translation
{
  public static SortedList<string, string> Strings;

  private static void smethod_1(string obj, string Type, Exception Ex = null, [In] LoggerType obj3)
  {
    try
    {
      lock (CLogger.object_0)
      {
        if (!obj3.Equals((object) LoggerType.Opcode))
        {
          Formatter[] formatterArray = new Formatter[6]
          {
            (Formatter) new FrequencyBasedColorAlternator((object) "[I]", ColorUtil.White),
            (Formatter) new FrequencyBasedColorAlternator((object) "[W]", ColorUtil.Yellow),
            (Formatter) new FrequencyBasedColorAlternator((object) "[D]", ColorUtil.Cyan),
            (Formatter) new FrequencyBasedColorAlternator((object) "[E]", ColorUtil.Red),
            (Formatter) new FrequencyBasedColorAlternator((object) "[H]", ColorUtil.Red),
            (Formatter) new FrequencyBasedColorAlternator((object) "[C]", ColorUtil.Red)
          };
          Plugin.Core.Colorful.Console.WriteLineFormatted($"{DBQuery.Now("HH:mm:ss")} {obj} {Type}", ColorUtil.LightGrey, formatterArray);
        }
        else
          Plugin.Core.Colorful.Console.WriteLine(Type, ColorUtil.Blue);
        // ISSUE: reference to a compiler-generated method
        string str = obj3.Equals((object) LoggerType.Info) || obj3.Equals((object) LoggerType.Warning) ? $"Logs/{CLogger.string_0}.log" : (obj3.Equals((object) LoggerType.Error) ? $"Logs/{obj3}/{CLogger.string_0}-{(Ex != null ? (object) CLogger.Class1.smethod_0(Ex)[0] : (object) "NULL")}.log" : $"Logs/{obj3}/{CLogger.string_0}.log");
        Translation.smethod_2(Type, Ex, str);
      }
    }
    catch
    {
    }
  }

  private static void smethod_2(string exception_0, Exception string_8, [In] string obj2)
  {
    using (FileStream fileStream = new FileStream(obj2, FileMode.Append))
    {
      using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream, Encoding.UTF8))
      {
        try
        {
          string str = string_8 != null ? $"{exception_0} \n{string_8}" : exception_0;
          streamWriter.WriteLine(str);
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

  static Translation() => CLogger.Class1.\u003C\u003E9 = (CLogger.Class1) new Translation();

  internal bool method_0([In] string obj0) => !Directory.Exists(obj0);
}
