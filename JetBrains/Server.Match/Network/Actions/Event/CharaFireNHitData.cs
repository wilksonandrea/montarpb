// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.CharaFireNHitData
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

public class CharaFireNHitData
{
  public static void WriteInfo(SyncServerPacket S, ActionStateInfo Info)
  {
    S.WriteH((ushort) Info.Action);
    S.WriteC(Info.Value);
    S.WriteC((byte) Info.Flag);
  }

  public static AnimationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
  {
    FireNHitDataObjectInfo nhitDataObjectInfo = new FireNHitDataObjectInfo();
    ((AnimationInfo) nhitDataObjectInfo).Animation = C.ReadUH();
    AnimationInfo animationInfo = (AnimationInfo) nhitDataObjectInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; POV: {animationInfo.Animation}", LoggerType.Warning, (Exception) null);
    return animationInfo;
  }
}
