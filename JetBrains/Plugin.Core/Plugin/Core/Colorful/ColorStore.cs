// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.ColorStore
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Plugin.Core.Colorful;

public sealed class ColorStore
{
  public ColorStore([In] int obj0)
    : this($"Color conversion failed with system error code {obj0}!")
  {
    // ISSUE: reference to a compiler-generated method
    this.set_ErrorCode(obj0);
  }

  internal ColorStore(Color intptr_1)
  {
    ((COLORREF) this).uint_0 = (uint) ((int) intptr_1.R + ((int) intptr_1.G << 8) + ((int) intptr_1.B << 16 /*0x10*/));
  }

  internal ColorStore([In] uint obj0, uint struct3_0, [In] uint obj2)
  {
    ((COLORREF) this).uint_0 = (uint) ((int) obj0 + ((int) struct3_0 << 8) + ((int) obj2 << 16 /*0x10*/));
  }

  public override string ToString() => ((COLORREF) this).uint_0.ToString();

  public ConcurrentDictionary<Color, ConsoleColor> Colors { get; private set; }

  public ConcurrentDictionary<ConsoleColor, Color> ConsoleColors
  {
    get => this.concurrentDictionary_1;
    [CompilerGenerated, SpecialName] private set
    {
      ((ColorStore) this).concurrentDictionary_1 = value;
    }
  }
}
