// Decompiled with JetBrains decompiler
// Type: Plugin.Core.JSON.CommandHelperJSON
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.JSON;

public class CommandHelperJSON
{
  public static List<CommandHelper> Helpers;

  internal Class0<T0, int> method_0(T0 ItemId, [In] int obj1) => new Class0<T0, int>(ItemId, obj1);

  internal IEnumerable<T0> method_1([In] IGrouping<int, Class0<T0, int>> obj0); // Unable to render the method body

  internal T0 method_2([In] Class0<T0, int> obj0) => obj0.item;

  internal int method_0([In] Class0<T0, int> obj0)
  {
    // ISSUE: reference to a compiler-generated field
    return obj0.inx / ((ShopManager.Class14<T0>) this).int_0;
  }

  public static void Load()
  {
    string path = "Data/CommandHelper.json";
    if (File.Exists(path))
    {
      ResolutionJSON.smethod_0(path);
    }
    else
    {
      // ISSUE: reference to a compiler-generated method
      CLogger.Class1.Print("File not found: " + path, LoggerType.Warning, (Exception) null);
    }
    // ISSUE: reference to a compiler-generated method
    CLogger.Class1.Print($"Plugin Loaded: {CommandHelperJSON.Helpers.Count} Command Helpers", LoggerType.Info, (Exception) null);
  }
}
