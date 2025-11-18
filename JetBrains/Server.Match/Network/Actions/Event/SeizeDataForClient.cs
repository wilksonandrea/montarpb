// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.SeizeDataForClient
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class SeizeDataForClient
{
  public static void WriteInfo(SyncServerPacket S, PosRotationInfo Info)
  {
    S.WriteH(Info.CameraX);
    S.WriteH(Info.CameraY);
    S.WriteH(Info.Area);
    S.WriteH(Info.RotationZ);
    S.WriteH(Info.RotationX);
    S.WriteH(Info.RotationY);
  }

  public static RadioChatInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
  {
    WeaponSyncInfo weaponSyncInfo = new WeaponSyncInfo();
    ((RadioChatInfo) weaponSyncInfo).RadioId = C.ReadC();
    ((RadioChatInfo) weaponSyncInfo).AreaId = C.ReadC();
    RadioChatInfo radioChatInfo = (RadioChatInfo) weaponSyncInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot} Radio: {radioChatInfo.RadioId} Area: {radioChatInfo.AreaId}", LoggerType.Warning, (Exception) null);
    return radioChatInfo;
  }
}
