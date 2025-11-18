// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.Animation
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Models.Event.Event;
using System;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class Animation
{
  public static void WriteInfo(SyncServerPacket S, ActionObjectInfo Info)
  {
    S.WriteC(Info.Unk1);
    S.WriteC(Info.Unk2);
  }

  public static ActionStateInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
  {
    FireDataObjectInfo fireDataObjectInfo = new FireDataObjectInfo();
    ((ActionStateInfo) fireDataObjectInfo).Action = (ActionFlag) C.ReadUH();
    ((ActionStateInfo) fireDataObjectInfo).Value = C.ReadC();
    ((ActionStateInfo) fireDataObjectInfo).Flag = (WeaponSyncType) C.ReadC();
    ActionStateInfo actionStateInfo = (ActionStateInfo) fireDataObjectInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; Action {actionStateInfo.Action}; Value: {actionStateInfo.Value}; Flag: {actionStateInfo.Flag}", LoggerType.Warning, (Exception) null);
    return actionStateInfo;
  }
}
