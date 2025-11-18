// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.Suicide
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models;
using Server.Match.Data.Models.Event;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.Event;

public class Suicide
{
  public static SoundPosRotationInfo ReadInfo(ActionModel Action, SyncClientPacket C, bool GenLog)
  {
    DropWeaponInfo dropWeaponInfo = new DropWeaponInfo();
    ((SoundPosRotationInfo) dropWeaponInfo).Time = C.ReadT();
    SoundPosRotationInfo soundPosRotationInfo = (SoundPosRotationInfo) dropWeaponInfo;
    if (GenLog)
      CLogger.Print($"PVP Slot: {Action.Slot}; Time: {soundPosRotationInfo.Time}", LoggerType.Warning, (Exception) null);
    return soundPosRotationInfo;
  }

  public static SoundPosRotationInfo ReadInfo(
    ActionModel S,
    SyncClientPacket Info,
    [In] float obj2,
    [In] bool obj3)
  {
    DropWeaponInfo dropWeaponInfo = new DropWeaponInfo();
    ((SoundPosRotationInfo) dropWeaponInfo).Time = obj2;
    SoundPosRotationInfo soundPosRotationInfo = (SoundPosRotationInfo) dropWeaponInfo;
    if (obj3)
      CLogger.Print($"PVP Slot: {S.Slot}; Time: {soundPosRotationInfo.Time}", LoggerType.Warning, (Exception) null);
    return soundPosRotationInfo;
  }
}
