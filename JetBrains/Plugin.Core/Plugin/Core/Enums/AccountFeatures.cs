// Decompiled with JetBrains decompiler
// Type: Plugin.Core.Enums.AccountFeatures
// Assembly: Plugin.Core, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 8661A0A0-7C0B-45D4-B139-86AB3E625616
// Assembly location: C:\Users\Administrator\Desktop\unpack\Plugin.Core.dll

#nullable disable
namespace Plugin.Core.Enums;

public enum AccountFeatures : uint
{
  PLAYTIME_ONLY = 256, // 0x00000100
  CLAN_ONLY = 4096, // 0x00001000
  TICKET_ONLY = 16384, // 0x00004000
  CLAN_COUPON = 30590, // 0x0000777E
  TAGS_ONLY = 67108864, // 0x04000000
  TOKEN_ONLY = 2121728000, // 0x7E770000
  TOKEN_CLAN = 2121767220, // 0x7E779934
  RATING_BOTH = 2147483384, // 0x7FFFFEF8
  TEST_MODE = 2147483385, // 0x7FFFFEF9
  FROM_SNIFF = 2389079930, // 0x8E66777A
  ALL = 2389079934, // 0x8E66777E
}
