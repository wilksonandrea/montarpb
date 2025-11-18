// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Enums.ResultIcon
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;

#nullable disable
namespace Plugin.Core.Enums;

[Flags]
public enum ResultIcon
{
  None = 0,
  Pc = 1,
  PcPlus = 2,
  Item = 4,
  Event = 8,
  Unk = 16, // 0x00000010
}
