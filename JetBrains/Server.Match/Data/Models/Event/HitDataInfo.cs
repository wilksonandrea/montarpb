// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.Event.HitDataInfo
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;
using System.Collections.Generic;

#nullable disable
namespace Server.Match.Data.Models.Event;

public class HitDataInfo
{
  public byte Extensions;
  public byte Accessory;
  public ushort BoomInfo;
  public ushort ObjectId;
  public uint HitIndex;
  public int WeaponId;
  public Half3 StartBullet;
  public Half3 EndBullet;
  public Half3 BulletPos;
  public List<int> BoomPlayers;
  public HitType HitEnum;
  public ClassType WeaponClass;
}
