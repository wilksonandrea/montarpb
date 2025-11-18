// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.Event.RadioChat
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

public class RadioChat
{
  public static void WriteInfo(SyncServerPacket Action, MissionDataInfo C)
  {
    Action.WriteT(C.PlantTime);
    Action.WriteC((byte) C.Bomb);
  }

  public static PosRotationInfo ReadInfo([In] ActionModel obj0, [In] SyncClientPacket obj1, bool Time)
  {
    WeaponHost weaponHost = new WeaponHost();
    ((PosRotationInfo) weaponHost).CameraX = obj1.ReadUH();
    ((PosRotationInfo) weaponHost).CameraY = obj1.ReadUH();
    ((PosRotationInfo) weaponHost).Area = obj1.ReadUH();
    ((PosRotationInfo) weaponHost).RotationZ = obj1.ReadUH();
    ((PosRotationInfo) weaponHost).RotationX = obj1.ReadUH();
    ((PosRotationInfo) weaponHost).RotationY = obj1.ReadUH();
    PosRotationInfo posRotationInfo = (PosRotationInfo) weaponHost;
    if (Time)
      CLogger.Print($"PVP Slot: {obj0.Slot}; Camera: (X: {posRotationInfo.CameraX}, Y: {posRotationInfo.CameraY}); Area: {posRotationInfo.Area}; Rotation: (X: {posRotationInfo.RotationX}, Y: {posRotationInfo.RotationY}, Z: {posRotationInfo.RotationZ})", LoggerType.Warning, (Exception) null);
    return posRotationInfo;
  }
}
