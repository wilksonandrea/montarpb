// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.Event.CharaFireNHitDataInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Enums;

#nullable disable
namespace Server.Match.Data.Models.Event;

public class CharaFireNHitDataInfo
{
  public byte Extensions;
  public byte Accessory;
  public ushort X;
  public ushort Y;
  public ushort Z;
  public uint HitInfo;
  public int WeaponId;
  public short Unk;
  public ClassType WeaponClass;
}
