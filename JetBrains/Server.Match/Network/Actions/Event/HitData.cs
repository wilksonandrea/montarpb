// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.HitData
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class HitData
{
  public static void WriteInfo(SyncServerPacket S, WeaponHost Info)
  {
    S.WriteC(Info.DeathType);
    S.WriteH(Info.X);
    S.WriteH(Info.Y);
    S.WriteH(Info.Z);
    S.WriteC(Info.Unks);
    S.WriteD(Info.UNK);
    S.WriteC(Info.HitPart);
  }

  public static List<GrenadeHitInfo> ReadInfo(
    ActionModel Action,
    SyncClientPacket C,
    bool GenLog,
    [In] bool obj3)
  {
    List<GrenadeHitInfo> grenadeHitInfoList = new List<GrenadeHitInfo>();
    int num1 = (int) C.ReadC();
    for (int index1 = 0; index1 < num1; ++index1)
    {
      SuicideInfo suicideInfo = new SuicideInfo();
      ((GrenadeHitInfo) suicideInfo).WeaponId = C.ReadD();
      ((GrenadeHitInfo) suicideInfo).Accessory = C.ReadC();
      ((GrenadeHitInfo) suicideInfo).Extensions = C.ReadC();
      ((GrenadeHitInfo) suicideInfo).BoomInfo = C.ReadUH();
      ((GrenadeHitInfo) suicideInfo).ObjectId = C.ReadUH();
      ((GrenadeHitInfo) suicideInfo).HitInfo = C.ReadUD();
      ((GrenadeHitInfo) suicideInfo).PlayerPos = C.ReadUHV();
      ((GrenadeHitInfo) suicideInfo).FirePos = C.ReadUHV();
      ((GrenadeHitInfo) suicideInfo).HitPos = C.ReadUHV();
      ((GrenadeHitInfo) suicideInfo).GrenadesCount = C.ReadUH();
      ((GrenadeHitInfo) suicideInfo).DeathType = (CharaDeath) C.ReadC();
      GrenadeHitInfo grenadeHitInfo = (GrenadeHitInfo) suicideInfo;
      if (!obj3)
      {
        grenadeHitInfo.HitEnum = (HitType) AllUtils.GetHitHelmet(grenadeHitInfo.HitInfo);
        if (grenadeHitInfo.BoomInfo > (ushort) 0)
        {
          grenadeHitInfo.BoomPlayers = new List<int>();
          for (int index2 = 0; index2 < 18; ++index2)
          {
            int num2 = 1 << index2;
            if (((int) grenadeHitInfo.BoomInfo & num2) == num2)
              grenadeHitInfo.BoomPlayers.Add(index2);
          }
        }
        grenadeHitInfo.WeaponClass = (ClassType) ComDiv.GetIdStatics(grenadeHitInfo.WeaponId, 2);
      }
      if (GenLog)
      {
        CLogger.Print($"PVP Slot: {Action.Slot}; Weapon Id: {grenadeHitInfo.WeaponId}; Ext: {grenadeHitInfo.Extensions}; Acc: {grenadeHitInfo.Accessory}", LoggerType.Warning, (Exception) null);
        CLogger.Print($"PVP Slot: {Action.Slot}; Grenade Hit: {grenadeHitInfo.HitInfo}; [Object Postion] X: {grenadeHitInfo.HitPos.X}; Y: {grenadeHitInfo.HitPos.Y}; Z: {grenadeHitInfo.HitPos.Z}", LoggerType.Warning, (Exception) null);
        CLogger.Print($"PVP Slot: {Action.Slot}; Grenade Hit: {grenadeHitInfo.HitInfo}; [Player Postion] X: {grenadeHitInfo.FirePos.X}; Y: {grenadeHitInfo.FirePos.Y}; Z: {grenadeHitInfo.FirePos.Z}", LoggerType.Warning, (Exception) null);
      }
      grenadeHitInfoList.Add(grenadeHitInfo);
    }
    return grenadeHitInfoList;
  }
}
