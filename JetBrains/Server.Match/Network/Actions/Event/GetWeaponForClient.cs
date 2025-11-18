// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.GetWeaponForClient
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

public class GetWeaponForClient
{
  public static void WriteInfo(SyncServerPacket S, FireDataObjectInfo Info)
  {
    S.WriteC((byte) Info.DeathType);
    S.WriteC(Info.HitPart);
    S.WriteC(Info.Unk);
  }

  public static FireNHitDataObjectInfo ReadInfo(
    ActionModel Action,
    SyncClientPacket C,
    bool GenLog)
  {
    RadioChatInfo radioChatInfo = new RadioChatInfo();
    ((FireNHitDataObjectInfo) radioChatInfo).Portal = C.ReadUH();
    FireNHitDataObjectInfo nhitDataObjectInfo = (FireNHitDataObjectInfo) radioChatInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; Passed on the portal [{nhitDataObjectInfo.Portal}]", LoggerType.Warning, (Exception) null);
    return nhitDataObjectInfo;
  }
}
