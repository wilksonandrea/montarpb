// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.DefaultFonts
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class DefaultFonts
{
  public static readonly string SmallSlant;

  public DefaultFonts(
    ConcurrentDictionary<Color, ConsoleColor> uint_1,
    ConcurrentDictionary<ConsoleColor, Color> uint_2)
  {
    ((ColorStore) this).Colors = uint_1;
    // ISSUE: reference to a compiler-generated method
    ((ConsoleAccessException) this).set_ConsoleColors(uint_2);
  }

  public void Update([In] ConsoleColor obj0, [In] Color obj1)
  {
    ((ColorStore) this).Colors.TryAdd(obj1, obj0);
    ((ColorStore) this).ConsoleColors[obj0] = obj1;
  }
}
