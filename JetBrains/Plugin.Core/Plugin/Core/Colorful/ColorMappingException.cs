// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.ColorMappingException
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class ColorMappingException : Exception
{
  private void method_1([In] ConsoleColor obj0, uint struct3_0, [In] uint obj2, [In] uint obj3)
  {
    IntPtr stdHandle = ColorMapper.GetStdHandle(-11);
    ColorMapper.Struct3 struct3 = ((ColorMapper) this).method_0(stdHandle);
    switch (obj0)
    {
      case ConsoleColor.Black:
        struct3.colorref_0 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.DarkBlue:
        struct3.colorref_1 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.DarkGreen:
        struct3.colorref_2 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.DarkCyan:
        struct3.colorref_3 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.DarkRed:
        struct3.colorref_4 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.DarkMagenta:
        struct3.colorref_5 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.DarkYellow:
        struct3.colorref_6 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.Gray:
        struct3.colorref_7 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.DarkGray:
        struct3.colorref_8 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.Blue:
        struct3.colorref_9 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.Green:
        struct3.colorref_10 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.Cyan:
        struct3.colorref_11 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.Red:
        struct3.colorref_12 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.Magenta:
        struct3.colorref_13 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.Yellow:
        struct3.colorref_14 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
      case ConsoleColor.White:
        struct3.colorref_15 = (COLORREF) new ColorStore(struct3_0, obj2, obj3);
        break;
    }
    this.method_2(stdHandle, struct3);
  }

  private void method_2(IntPtr intptr_1, [In] ColorMapper.Struct3 obj1)
  {
    ++obj1.struct2_0.short_3;
    ++obj1.struct2_0.short_2;
    if (!ColorMapper.SetConsoleScreenBufferInfoEx(intptr_1, ref obj1))
      throw this.method_3(Marshal.GetLastWin32Error());
  }

  private Exception method_3([In] int obj0)
  {
    return obj0 == 6 ? (Exception) new Class23() : (Exception) new ColorStore(obj0);
  }

  public int ErrorCode
  {
    [CompilerGenerated, SpecialName] get => (^(ColorMappingException&) ref this).int_0;
    [CompilerGenerated, SpecialName] [param: In] private set
    {
      ((ColorMappingException) this).int_0 = value;
    }
  }
}
