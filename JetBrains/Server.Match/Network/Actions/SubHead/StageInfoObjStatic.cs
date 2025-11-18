// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.SubHead.StageInfoObjStatic
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Match.Data.Models.Event;
using Server.Match.Data.Models.SubHead;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.SubHead;

public class StageInfoObjStatic
{
  public static void WriteInfo([In] SyncServerPacket obj0, [In] ObjectAnimInfo obj1)
  {
    obj0.WriteH(obj1.Life);
    obj0.WriteC(obj1.Anim1);
    obj0.WriteC(obj1.Anim2);
    obj0.WriteT(obj1.SyncDate);
  }

  public static ObjectStaticInfo ReadInfo([In] SyncClientPacket obj0, bool Info)
  {
    SoundPosRotationInfo soundPosRotationInfo = new SoundPosRotationInfo();
    ((ObjectStaticInfo) soundPosRotationInfo).Type = obj0.ReadUH();
    ((ObjectStaticInfo) soundPosRotationInfo).Life = obj0.ReadUH();
    ((ObjectStaticInfo) soundPosRotationInfo).DestroyedBySlot = obj0.ReadC();
    ObjectStaticInfo objectStaticInfo = (ObjectStaticInfo) soundPosRotationInfo;
    if (Info)
      CLogger.Print($"Sub Head: ObjectStatic; Type: {objectStaticInfo.Type}; Life: {objectStaticInfo.Life}; Destroyed: {objectStaticInfo.DestroyedBySlot}", LoggerType.Warning, (Exception) null);
    return objectStaticInfo;
  }
}
