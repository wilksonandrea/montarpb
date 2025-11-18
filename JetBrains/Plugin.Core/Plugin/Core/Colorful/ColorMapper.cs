// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.ColorMapper
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class ColorMapper
{
  private const int int_0 = -11;
  private static readonly IntPtr intptr_0;

  private ConsoleColor method_0([In] Color obj0)
  {
    if (this.method_1() && ((Class23) ((ColorManager) this).colorStore_0).RequiresUpdate(obj0))
    {
      ConsoleColor int0 = (ConsoleColor) ((ColorManager) this).int_0;
      ((ColorManager) this).colorMapper_0.MapColor(int0, obj0);
      ((DefaultFonts) ((ColorManager) this).colorStore_0).Update(int0, obj0);
      ((ColorManager) this).int_0 = ((ColorManager) this).int_0 + 1;
    }
    return ((ColorManager) this).colorStore_0.Colors.ContainsKey(obj0) ? ((ColorManager) this).colorStore_0.Colors[obj0] : ((ColorManager) this).colorStore_0.Colors.Last<KeyValuePair<Color, ConsoleColor>>().Value;
  }

  private bool method_1() => ((ColorManager) this).int_0 < ((ColorManager) this).int_1;

  public ColorManager GetManager(ColorStore color, int newColor, [In] int obj2, [In] bool obj3)
  {
    return new ColorManager(color, (ColorMapper) new COLORREF(), newColor, obj2, obj3);
  }

  public ColorManager GetManager(
    ConcurrentDictionary<Color, ConsoleColor> color_0,
    ConcurrentDictionary<ConsoleColor, Color> maxColorChanges,
    int initialColorChangeCountValue,
    int isInCompatibilityMode,
    [In] bool obj4)
  {
    return new ColorManager((ColorStore) new DefaultFonts(color_0, maxColorChanges), (ColorMapper) new COLORREF(), initialColorChangeCountValue, isInCompatibilityMode, obj4);
  }

  [DllImport("kernel32.dll", SetLastError = true)]
  private static extern IntPtr GetStdHandle(int colorMap);

  [DllImport("kernel32.dll", SetLastError = true)]
  private static extern bool GetConsoleScreenBufferInfoEx(
    [In] IntPtr obj0,
    ref ColorMapper.Struct3 consoleColorMap);

  [DllImport("kernel32.dll", SetLastError = true)]
  private static extern bool SetConsoleScreenBufferInfoEx([In] IntPtr obj0, [In] ref ColorMapper.Struct3 obj1);

  public void MapColor(ConsoleColor int_1, [In] Color obj1)
  {
    ((ColorMappingException) this).method_1(int_1, (uint) obj1.R, (uint) obj1.G, (uint) obj1.B);
  }

  public Dictionary<string, COLORREF> GetBufferColors()
  {
    Dictionary<string, COLORREF> bufferColors = new Dictionary<string, COLORREF>();
    ColorMapper.Struct3 struct3 = this.method_0(ColorMapper.GetStdHandle(-11));
    bufferColors.Add("black", struct3.colorref_0);
    bufferColors.Add("darkBlue", struct3.colorref_1);
    bufferColors.Add("darkGreen", struct3.colorref_2);
    bufferColors.Add("darkCyan", struct3.colorref_3);
    bufferColors.Add("darkRed", struct3.colorref_4);
    bufferColors.Add("darkMagenta", struct3.colorref_5);
    bufferColors.Add("darkYellow", struct3.colorref_6);
    bufferColors.Add("gray", struct3.colorref_7);
    bufferColors.Add("darkGray", struct3.colorref_8);
    bufferColors.Add("blue", struct3.colorref_9);
    bufferColors.Add("green", struct3.colorref_10);
    bufferColors.Add("cyan", struct3.colorref_11);
    bufferColors.Add("red", struct3.colorref_12);
    bufferColors.Add("magenta", struct3.colorref_13);
    bufferColors.Add("yellow", struct3.colorref_14);
    bufferColors.Add("white", struct3.colorref_15);
    return bufferColors;
  }

  public void SetBatchBufferColors([In] Dictionary<string, COLORREF> obj0)
  {
    IntPtr stdHandle = ColorMapper.GetStdHandle(-11);
    ColorMapper.Struct3 struct3 = this.method_0(stdHandle) with
    {
      colorref_0 = obj0["black"],
      colorref_1 = obj0["darkBlue"],
      colorref_2 = obj0["darkGreen"],
      colorref_3 = obj0["darkCyan"],
      colorref_4 = obj0["darkRed"],
      colorref_5 = obj0["darkMagenta"],
      colorref_6 = obj0["darkYellow"],
      colorref_7 = obj0["gray"],
      colorref_8 = obj0["darkGray"],
      colorref_9 = obj0["blue"],
      colorref_10 = obj0["green"],
      colorref_11 = obj0["cyan"],
      colorref_12 = obj0["red"],
      colorref_13 = obj0["magenta"],
      colorref_14 = obj0["yellow"],
      colorref_15 = obj0["white"]
    };
    ((ColorMappingException) this).method_2(stdHandle, struct3);
  }

  private ColorMapper.Struct3 method_0(IntPtr intptr_1)
  {
    ColorMapper.Struct3 consoleColorMap = new ColorMapper.Struct3();
    consoleColorMap.int_0 = Marshal.SizeOf<ColorMapper.Struct3>(consoleColorMap);
    if (intptr_1 == ColorMapper.intptr_0)
      throw ((ColorMappingException) this).method_3(Marshal.GetLastWin32Error());
    return ColorMapper.GetConsoleScreenBufferInfoEx(intptr_1, ref consoleColorMap) ? consoleColorMap : throw ((ColorMappingException) this).method_3(Marshal.GetLastWin32Error());
  }

  private struct Struct1
  {
    internal short short_0;
    internal short short_1;
  }

  private struct Struct2
  {
    internal short short_0;
    internal short short_1;
    internal short short_2;
    internal short short_3;
  }

  private struct Struct3
  {
    internal int int_0;
    internal ColorMapper.Struct1 struct1_0;
    internal ColorMapper.Struct1 struct1_1;
    internal ushort ushort_0;
    internal ColorMapper.Struct2 struct2_0;
    internal ColorMapper.Struct1 struct1_2;
    internal ushort ushort_1;
    internal bool bool_0;
    internal COLORREF colorref_0;
    internal COLORREF colorref_1;
    internal COLORREF colorref_2;
    internal COLORREF colorref_3;
    internal COLORREF colorref_4;
    internal COLORREF colorref_5;
    internal COLORREF colorref_6;
    internal COLORREF colorref_7;
    internal COLORREF colorref_8;
    internal COLORREF colorref_9;
    internal COLORREF colorref_10;
    internal COLORREF colorref_11;
    internal COLORREF colorref_12;
    internal COLORREF colorref_13;
    internal COLORREF colorref_14;
    internal COLORREF colorref_15;
  }
}
