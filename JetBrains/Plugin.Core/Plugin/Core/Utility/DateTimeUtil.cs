// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Utility.DateTimeUtil
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Utility;

public class DateTimeUtil
{
  public static uint Verificate(byte Text, byte int_1, [In] byte obj2, [In] byte obj3)
  {
    byte[] source = new byte[4]{ Text, int_1, obj2, obj3 };
    if (!((IEnumerable<byte>) source).Any<byte>())
      return 0;
    if (int_1 < (byte) 60)
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print($"Refresh Rate is below the minimum limit ({int_1})", LoggerType.Warning, (Exception) null);
      return 0;
    }
    if (obj2 >= (byte) 0 && obj2 <= (byte) 1)
      return BitConverter.ToUInt32(source, 0);
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Unknown Window State ({obj2})", LoggerType.Warning, (Exception) null);
    return 0;
  }

  static DateTimeUtil() => ComDiv.Class5.\u003C\u003E9 = (ComDiv.Class5) new DateTimeUtil();

  internal bool method_0([In] ItemsModel obj0) => obj0.Count > 0U;
}
