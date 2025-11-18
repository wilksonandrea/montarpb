// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.SoundPosRotation
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

public class SoundPosRotation
{
  public static void WriteInfo(SyncServerPacket S, RadioChatInfo Info)
  {
    S.WriteC(Info.RadioId);
    S.WriteC(Info.AreaId);
  }

  public static SeizeDataForClientInfo ReadInfo(
    ActionModel Action,
    SyncClientPacket C,
    bool GenLog)
  {
    CharaFireNHitDataInfo fireNhitDataInfo = new CharaFireNHitDataInfo();
    ((SeizeDataForClientInfo) fireNhitDataInfo).UseTime = C.ReadT();
    ((SeizeDataForClientInfo) fireNhitDataInfo).Flags = C.ReadC();
    SeizeDataForClientInfo dataForClientInfo = (SeizeDataForClientInfo) fireNhitDataInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; Use Time: {dataForClientInfo.UseTime}; Flags: {dataForClientInfo.Flags}", LoggerType.Warning, (Exception) null);
    return dataForClientInfo;
  }

  public static void WriteInfo(SyncServerPacket S, SeizeDataForClientInfo Info)
  {
    S.WriteT(Info.UseTime);
    S.WriteC(Info.Flags);
  }
}
