// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Enums.StageOptions
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

#nullable disable
namespace Plugin.Core.Enums;

public enum StageOptions : byte
{
  None = 0,
  Default = 1,
  AI = 2,
  DieHard = 4,
  Infection = 6,
  Sniper = 96, // 0x60
  Shotgun = 128, // 0x80
  Knuckle = 224, // 0xE0
}
