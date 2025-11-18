// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.UseObject
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Models.Event.Event;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class UseObject
{
  public static void WriteInfo([In] SyncServerPacket obj0, [In] SoundPosRotationInfo obj1)
  {
    obj0.WriteT(obj1.Time);
  }

  public static List<SuicideInfo> ReadInfo(
    [In] ActionModel obj0,
    SyncClientPacket C,
    bool Time,
    bool GenLog)
  {
    List<SuicideInfo> suicideInfoList = new List<SuicideInfo>();
    int num = (int) C.ReadC();
    for (int index = 0; index < num; ++index)
    {
      ActionObjectInfo actionObjectInfo = new ActionObjectInfo();
      ((SuicideInfo) actionObjectInfo).PlayerPos = C.ReadUHV();
      ((SuicideInfo) actionObjectInfo).WeaponId = C.ReadD();
      ((SuicideInfo) actionObjectInfo).Accessory = C.ReadC();
      ((SuicideInfo) actionObjectInfo).Extensions = C.ReadC();
      ((SuicideInfo) actionObjectInfo).HitInfo = C.ReadUD();
      SuicideInfo suicideInfo = (SuicideInfo) actionObjectInfo;
      if (!GenLog)
        suicideInfo.WeaponClass = (ClassType) ComDiv.GetIdStatics(suicideInfo.WeaponId, 2);
      if (Time)
        CLogger.Print($"PVP Slot: {obj0.Slot}; Weapon Id: {suicideInfo.WeaponId}; Ext: {suicideInfo.Extensions}; Acc: {suicideInfo.Accessory}; Suicide Hit: {suicideInfo.HitInfo}; X: {suicideInfo.PlayerPos.X}; Y: {suicideInfo.PlayerPos.Y}; Z: {suicideInfo.PlayerPos.Z}", LoggerType.Warning, (Exception) null);
      suicideInfoList.Add(suicideInfo);
    }
    return suicideInfoList;
  }
}
