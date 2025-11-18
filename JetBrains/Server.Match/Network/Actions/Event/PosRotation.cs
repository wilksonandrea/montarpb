// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.PosRotation
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class PosRotation
{
  public static void WriteInfo(SyncServerPacket S, HPSyncInfo Hits) => S.WriteH(Hits.CharaLife);

  public static MissionDataInfo ReadInfo(
    ActionModel Action,
    SyncClientPacket C,
    float GenLog,
    [In] bool obj3,
    [In] bool obj4)
  {
    WeaponClient weaponClient = new WeaponClient();
    ((MissionDataInfo) weaponClient).PlantTime = C.ReadT();
    ((MissionDataInfo) weaponClient).Bomb = (int) C.ReadC();
    MissionDataInfo missionDataInfo = (MissionDataInfo) weaponClient;
    if (!obj4)
    {
      missionDataInfo.BombEnum = (BombFlag) (missionDataInfo.Bomb & 15);
      missionDataInfo.BombId = missionDataInfo.Bomb >> 4;
    }
    if (obj3)
      CLogger.Print($"PVP Slot: {Action.Slot}; Bomb: {missionDataInfo.BombEnum}; Id: {missionDataInfo.BombId}; PlantTime: {missionDataInfo.PlantTime}; Time: {GenLog}", LoggerType.Warning, (Exception) null);
    return missionDataInfo;
  }
}
