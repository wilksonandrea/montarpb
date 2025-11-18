// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Colorful.COLORREF
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Plugin.Core.Colorful;

public struct COLORREF
{
  private uint uint_0;

  public COLORREF() => ((object) ref this).\u002Ector();

  static COLORREF() => ColorMapper.intptr_0 = new IntPtr(-1);

  [CompilerGenerated]
  [SpecialName]
  public int get_ErrorCode() => (^(ColorMappingException&) ref this).int_0;
}
