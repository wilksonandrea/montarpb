// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.ColorManagerFactory
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class ColorManagerFactory
{
  public Color GetColor(ConsoleColor colorStore_1)
  {
    return ((ColorManager) this).colorStore_0.ConsoleColors[colorStore_1];
  }

  public void ReplaceColor([In] Color obj0, Color colorMapper_1)
  {
    if (((ColorManager) this).IsInCompatibilityMode)
      return;
    ((ColorManager) this).colorMapper_0.MapColor(((Class23) ((ColorManager) this).colorStore_0).Replace(obj0, colorMapper_1), colorMapper_1);
  }

  public ConsoleColor GetConsoleColor([In] Color obj0)
  {
    if (((ColorManager) this).IsInCompatibilityMode)
      return obj0.ToNearestConsoleColor();
    try
    {
      return ((ColorMapper) this).method_0(obj0);
    }
    catch
    {
      return obj0.ToNearestConsoleColor();
    }
  }
}
