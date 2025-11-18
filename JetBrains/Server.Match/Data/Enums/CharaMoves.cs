// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Enums.CharaMoves
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using System;

#nullable disable
namespace Server.Match.Data.Enums;

[Flags]
public enum CharaMoves
{
  Stop = 0,
  Left = 1,
  Back = 2,
  Right = 4,
  Front = 8,
  HeliInMove = 16, // 0x00000010
  HeliUnknown = 32, // 0x00000020
  HeliLeave = 64, // 0x00000040
  HeliStopped = 128, // 0x00000080
}
