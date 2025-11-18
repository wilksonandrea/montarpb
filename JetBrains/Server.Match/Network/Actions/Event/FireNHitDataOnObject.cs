// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.FireNHitDataOnObject
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

public class FireNHitDataOnObject
{
  public static void WriteInfo(SyncServerPacket S, FireDataInfo Info)
  {
    S.WriteC(Info.Effect);
    S.WriteC(Info.Part);
    S.WriteH(Info.Index);
    S.WriteD(Info.WeaponId);
    S.WriteC(Info.Accessory);
    S.WriteC(Info.Extensions);
    S.WriteH(Info.X);
    S.WriteH(Info.Y);
    S.WriteH(Info.Z);
  }

  public static FireDataObjectInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
  {
    PosRotationInfo posRotationInfo = new PosRotationInfo();
    ((FireDataObjectInfo) posRotationInfo).DeathType = (CharaDeath) C.ReadC();
    ((FireDataObjectInfo) posRotationInfo).HitPart = C.ReadC();
    ((FireDataObjectInfo) posRotationInfo).Unk = C.ReadC();
    FireDataObjectInfo fireDataObjectInfo = (FireDataObjectInfo) posRotationInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; Death Type: {fireDataObjectInfo.DeathType}; Hit Part: {fireDataObjectInfo.HitPart}", LoggerType.Warning, (Exception) null);
    return fireDataObjectInfo;
  }
}
