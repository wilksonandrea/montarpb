// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Enums.BombFlag
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using System;

#nullable disable
namespace Server.Match.Data.Enums;

[Flags]
public enum BombFlag
{
  Start = 1,
  Stop = 2,
  Defuse = 4,
  Unknown = 8,
}
