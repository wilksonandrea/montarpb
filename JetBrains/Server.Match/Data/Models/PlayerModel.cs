// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.PlayerModel
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;
using System;
using System.Net;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class PlayerModel
{
  public int Slot;
  public TeamEnum Team;
  public int Life;
  public int MaxLife;
  public int PlayerIdByUser;
  public int PlayerIdByServer;
  public int RespawnByUser;
  public int RespawnByServer;
  public int Ping;
  public int Latency;
  public int WeaponId;
  public byte Accessory;
  public byte Extensions;
  public float PlantDuration;
  public float DefuseDuration;
  public float C4Time;
  public Half3 Position;
  public IPEndPoint Client;
  public DateTime StartTime;
  public DateTime LastPing;
  public DateTime LastDie;
  public DateTime C4First;
  public Equipment Equip;
  public ClassType WeaponClass;
  public CharaResId CharaRes;
  public bool Dead;
  public bool NeverRespawn;
  public bool Integrity;
  public bool Immortal;

  [CompilerGenerated]
  [SpecialName]
  public byte[] get_WithoutEndData() => ((PacketModel) this).byte_2;

  [CompilerGenerated]
  [SpecialName]
  public void set_WithoutEndData(byte[] value) => ((PacketModel) this).byte_2 = value;

  [CompilerGenerated]
  [SpecialName]
  public DateTime get_ReceiveDate() => ((PacketModel) this).dateTime_0;

  [CompilerGenerated]
  [SpecialName]
  public void set_ReceiveDate(DateTime value) => ((PacketModel) this).dateTime_0 = value;

  public PlayerModel()
  {
  }

  public PlayerModel(int value)
  {
    this.Slot = value;
    this.Team = (TeamEnum) (value % 2);
  }

  public bool CompareIp(IPEndPoint value)
  {
    return this.Client != null && value != null && this.Client.Address.Equals((object) value.Address) && this.Client.Port == value.Port;
  }

  public bool RespawnIsValid() => this.RespawnByServer == this.RespawnByUser;

  public bool AccountIdIsValid() => this.PlayerIdByServer == this.PlayerIdByUser;
}
