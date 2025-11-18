// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Enums.RoomStageFlag
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

using System;

#nullable disable
namespace Plugin.Core.Enums;

[Flags]
public enum RoomStageFlag
{
  NONE = 0,
  TEAM_SWAP = 1,
  RANDOM_MAP = 2,
  PASSWORD = 4,
  OBSERVER_MODE = 8,
  REAL_IP = 16, // 0x00000010
  TEAM_BALANCE = 32, // 0x00000020
  OBSERVER = 64, // 0x00000040
  INTER_ENTER = 128, // 0x00000080
}
