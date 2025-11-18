// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.ColorManager
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class ColorManager
{
  private ColorMapper colorMapper_0;
  private ColorStore colorStore_0;
  private int int_0;
  private int int_1;

  internal object method_0([In] Formatter obj0)
  {
    return ((FrequencyBasedColorAlternator) obj0).get_Target();
  }

  internal Color method_1(Formatter sender) => ((FrequencyBasedColorAlternator) sender).get_Color();

  public ColorManager()
  {
  }

  internal Task method_0()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return Task.Factory.StartNew(((Console.Class17) this).action_0 ?? (((Console.Class17) this).action_0 = new Action(this.method_1)));
  }

  internal void method_1()
  {
    ConsoleColor foregroundColor = System.Console.ForegroundColor;
    int num = 1;
    // ISSUE: reference to a compiler-generated field
    foreach (KeyValuePair<string, Color> keyValuePair in ((Console.Class17) this).ienumerable_0)
    {
      System.Console.ForegroundColor = ((ColorManagerFactory) Console.colorManager_0).GetConsoleColor(keyValuePair.Value);
      // ISSUE: reference to a compiler-generated field
      if (num == ((Console.Class17) this).ienumerable_0.Count<KeyValuePair<string, Color>>())
      {
        // ISSUE: reference to a compiler-generated field
        System.Console.Write(keyValuePair.Key + ((Console.Class17) this).string_0);
      }
      else
        System.Console.Write(keyValuePair.Key);
      ++num;
    }
    System.Console.ForegroundColor = foregroundColor;
  }

  public bool IsInCompatibilityMode { get; [param: In] private set; }

  public ColorManager(ColorStore sender, ColorMapper e, [In] int obj2, [In] int obj3, [In] bool obj4)
  {
    this.colorStore_0 = sender;
    this.colorMapper_0 = e;
    this.int_0 = obj3;
    this.int_1 = obj2;
    this.IsInCompatibilityMode = obj4;
  }
}
