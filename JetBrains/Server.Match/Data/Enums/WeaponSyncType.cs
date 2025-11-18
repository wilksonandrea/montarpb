// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Enums.WeaponSyncType
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

#nullable disable
namespace Server.Match.Data.Enums;

public enum WeaponSyncType
{
  Primary = 0,
  Secondary = 16, // 0x00000010
  Melee = 32, // 0x00000020
  Explosive = 48, // 0x00000030
  Special = 64, // 0x00000040
  Mission = 80, // 0x00000050
  Dual = 128, // 0x00000080
  Ext = 144, // 0x00000090
}
