// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.DBQuery
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Utility;

public class DBQuery
{
  private readonly List<string> list_0;
  private readonly List<object> list_1;

  internal int method_1(ItemsModel A) => A.Id;

  public static DateTime Now()
  {
    try
    {
      DateTime now = DateTime.Now;
      return ConfigLoader.CustomYear ? now.AddYears(-ConfigLoader.BackYear) : now;
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return new DateTime();
    }
  }

  public static string Now([In] string obj0) => DBQuery.Now().ToString(obj0);

  public static DateTime Convert([In] string obj0)
  {
    string[] formats = new string[2]
    {
      "yyMMddHHmm",
      "yyMMdd"
    };
    try
    {
      if (obj0.Length < 6)
        obj0 = "101010";
      return DateTime.ParseExact(obj0, formats, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print(ex.Message, LoggerType.Error, ex);
      return new DateTime();
    }
  }
}
