// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.ActionState
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event.Event;
using Server.Match.Data.Models.SubHead;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class ActionState
{
  public static void WriteInfo([In] SyncServerPacket obj0, StageAnimInfo GenLog)
  {
    obj0.WriteC(GenLog.Unk);
    obj0.WriteH(GenLog.Life);
    obj0.WriteT(GenLog.SyncDate);
    obj0.WriteC(GenLog.Anim1);
    obj0.WriteC(GenLog.Anim2);
  }

  public static ActionObjectInfo ReadInfo([In] ActionModel obj0, SyncClientPacket Info, [In] bool obj2)
  {
    RoomsManager roomsManager = new RoomsManager();
    ((ActionObjectInfo) roomsManager).Unk1 = Info.ReadC();
    ((ActionObjectInfo) roomsManager).Unk2 = Info.ReadC();
    ActionObjectInfo actionObjectInfo = (ActionObjectInfo) roomsManager;
    if (obj2)
      CLogger.Print($"PVP Slot: {obj0.Slot} Unk1: {actionObjectInfo.Unk1}; Unk2: {actionObjectInfo.Unk2}", LoggerType.Warning, (Exception) null);
    return actionObjectInfo;
  }
}
