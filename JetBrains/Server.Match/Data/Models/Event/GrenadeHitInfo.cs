// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.Event.GrenadeHitInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;
using System.Collections.Generic;

#nullable disable
namespace Server.Match.Data.Models.Event;

public class GrenadeHitInfo
{
  public byte Extensions;
  public byte Accessory;
  public ushort BoomInfo;
  public ushort GrenadesCount;
  public ushort ObjectId;
  public uint HitInfo;
  public int WeaponId;
  public List<int> BoomPlayers;
  public CharaDeath DeathType;
  public Half3 FirePos;
  public Half3 HitPos;
  public Half3 PlayerPos;
  public HitType HitEnum;
  public ClassType WeaponClass;
}
