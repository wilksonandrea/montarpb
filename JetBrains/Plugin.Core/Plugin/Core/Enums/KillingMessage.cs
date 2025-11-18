// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Enums.KillingMessage
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;

#nullable disable
namespace Plugin.Core.Enums;

[Flags]
public enum KillingMessage
{
  None = 0,
  PiercingShot = 1,
  MassKill = 2,
  ChainStopper = 4,
  Headshot = 8,
  ChainHeadshot = 16, // 0x00000010
  ChainSlugger = 32, // 0x00000020
  Suicide = 64, // 0x00000040
  ObjectDefense = 128, // 0x00000080
}
