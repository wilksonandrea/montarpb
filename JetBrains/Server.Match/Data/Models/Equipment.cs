// Decompiled with JetBrains decompiler
// Type: Server.Match.Data.Models.Equipment
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using System;
using System.Net;
using System.Runtime.CompilerServices;

#nullable disable
namespace Server.Match.Data.Models;

public class Equipment
{
  public void CheckLifeValue()
  {
    if (((PlayerModel) this).Life <= ((PlayerModel) this).MaxLife)
      return;
    ((PlayerModel) this).Life = ((PlayerModel) this).MaxLife;
  }

  public void ResetAllInfos()
  {
    ((PlayerModel) this).Client = (IPEndPoint) null;
    ((PlayerModel) this).StartTime = new DateTime();
    ((PlayerModel) this).PlayerIdByUser = -2;
    ((PlayerModel) this).PlayerIdByServer = -1;
    ((PlayerModel) this).Integrity = true;
    this.ResetBattleInfos();
  }

  public void ResetBattleInfos()
  {
    ((PlayerModel) this).RespawnByUser = -2;
    ((PlayerModel) this).RespawnByServer = -1;
    ((PlayerModel) this).Immortal = false;
    ((PlayerModel) this).Dead = true;
    ((PlayerModel) this).NeverRespawn = true;
    ((PlayerModel) this).WeaponId = 0;
    ((PlayerModel) this).Accessory = (byte) 0;
    ((PlayerModel) this).Extensions = (byte) 0;
    ((PlayerModel) this).WeaponClass = ClassType.Unknown;
    ((PlayerModel) this).LastPing = new DateTime();
    ((PlayerModel) this).LastDie = new DateTime();
    ((PlayerModel) this).C4First = new DateTime();
    ((PlayerModel) this).C4Time = 0.0f;
    ((PlayerModel) this).Position = new Half3();
    ((PlayerModel) this).Life = 100;
    ((PlayerModel) this).MaxLife = 100;
    ((PlayerModel) this).Ping = 5;
    ((PlayerModel) this).Latency = 0;
    ((PlayerModel) this).PlantDuration = ConfigLoader.PlantDuration;
    ((PlayerModel) this).DefuseDuration = ConfigLoader.DefuseDuration;
  }

  public void ResetLife() => ((PlayerModel) this).Life = ((PlayerModel) this).MaxLife;

  public void LogPlayerPos(Half3 value)
  {
    CLogger.Print($"Player Position X: {((PlayerModel) this).Position.X} Y: {((PlayerModel) this).Position.Y} Z: {((PlayerModel) this).Position.Z}", LoggerType.Warning, (Exception) null);
    CLogger.Print($"End Bullet Position X: {value.X} Y: {value.Y} Z: {value.Z}", LoggerType.Warning, (Exception) null);
  }

  public int WpnPrimary { get; set; }

  public int WpnSecondary { get; set; }

  public int WpnMelee { get; set; }

  public int WpnExplosive { get; set; }

  public int WpnSpecial
  {
    [CompilerGenerated, SpecialName] get => ((Equipment) this).int_4;
    [CompilerGenerated, SpecialName] set => ((Equipment) this).int_4 = value;
  }

  public int Accessory
  {
    [CompilerGenerated, SpecialName] get => ((Equipment) this).int_5;
    [CompilerGenerated, SpecialName] set => ((Equipment) this).int_5 = value;
  }
}
