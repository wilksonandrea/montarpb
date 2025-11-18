// Decompiled with JetBrains decompiler
// Type: Server.Match.Network.Actions.SubHead.StageInfoObjControl
// Assembly: Server.Match, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: C407394F-F092-4178-B37A-7E152A148666
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Match.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Models.SubHead;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Match.Network.Actions.SubHead;

public class StageInfoObjControl
{
  public static byte[] GET_CODE(List<ObjectHitInfo> Data)
  {
    using (SyncServerPacket syncServerPacket = new SyncServerPacket())
    {
      foreach (ObjectHitInfo objectHitInfo in Data)
      {
        if (objectHitInfo.Type == 1)
        {
          if (objectHitInfo.ObjSyncId == 0)
          {
            syncServerPacket.WriteH((short) 10);
            syncServerPacket.WriteH((ushort) objectHitInfo.ObjId);
            syncServerPacket.WriteC((byte) 3);
            syncServerPacket.WriteH((short) 6);
            syncServerPacket.WriteH((ushort) objectHitInfo.ObjLife);
            syncServerPacket.WriteC((byte) objectHitInfo.KillerSlot);
          }
          else
          {
            syncServerPacket.WriteH((short) 13);
            syncServerPacket.WriteH((ushort) objectHitInfo.ObjId);
            syncServerPacket.WriteC((byte) 6);
            syncServerPacket.WriteH((ushort) objectHitInfo.ObjLife);
            syncServerPacket.WriteC((byte) objectHitInfo.AnimId1);
            syncServerPacket.WriteC((byte) objectHitInfo.AnimId2);
            syncServerPacket.WriteT(objectHitInfo.SpecialUse);
          }
        }
        else if (objectHitInfo.Type == 2)
        {
          ushort num = 11;
          UdpGameEvent udpGameEvent = UdpGameEvent.HpSync;
          if (objectHitInfo.ObjLife == 0)
          {
            num += (ushort) 13;
            udpGameEvent |= UdpGameEvent.GetWeaponForHost;
          }
          syncServerPacket.WriteH(num);
          syncServerPacket.WriteH((ushort) objectHitInfo.ObjId);
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteD((uint) udpGameEvent);
          syncServerPacket.WriteH((ushort) objectHitInfo.ObjLife);
          if (udpGameEvent.HasFlag((Enum) UdpGameEvent.GetWeaponForHost))
          {
            syncServerPacket.WriteC((byte) ((ObjectInfo) objectHitInfo).get_DeathType());
            syncServerPacket.WriteC((byte) objectHitInfo.KillerSlot);
            syncServerPacket.WriteHV(objectHitInfo.Position);
            syncServerPacket.WriteD(0);
            syncServerPacket.WriteC((byte) ((ObjectInfo) objectHitInfo).get_HitPart());
          }
        }
        else if (objectHitInfo.Type == 3)
        {
          if (objectHitInfo.ObjSyncId == 0)
          {
            syncServerPacket.WriteH((short) 8);
            syncServerPacket.WriteH((ushort) objectHitInfo.ObjId);
            syncServerPacket.WriteC((byte) 9);
            syncServerPacket.WriteH((short) 1);
            syncServerPacket.WriteC((byte) (objectHitInfo.ObjLife == 0));
          }
          else
          {
            syncServerPacket.WriteH((short) 14);
            syncServerPacket.WriteH((ushort) objectHitInfo.ObjId);
            syncServerPacket.WriteC((byte) 12);
            syncServerPacket.WriteC((byte) objectHitInfo.DestroyState);
            syncServerPacket.WriteH((ushort) objectHitInfo.ObjLife);
            syncServerPacket.WriteT(objectHitInfo.SpecialUse);
            syncServerPacket.WriteC((byte) objectHitInfo.AnimId1);
            syncServerPacket.WriteC((byte) objectHitInfo.AnimId2);
          }
        }
        else if (objectHitInfo.Type == 4)
        {
          syncServerPacket.WriteH((short) 11);
          syncServerPacket.WriteH((ushort) objectHitInfo.ObjId);
          syncServerPacket.WriteC((byte) 8);
          syncServerPacket.WriteD(8U);
          syncServerPacket.WriteH((ushort) objectHitInfo.ObjLife);
        }
        else if (objectHitInfo.Type == 5)
        {
          syncServerPacket.WriteH((short) 12);
          syncServerPacket.WriteH((ushort) objectHitInfo.ObjId);
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteD(1073741824U /*0x40000000*/);
          syncServerPacket.WriteC((byte) ((uint) ((ObjectInfo) objectHitInfo).get_DeathType() * 16U /*0x10*/));
          syncServerPacket.WriteC((byte) ((ObjectInfo) objectHitInfo).get_HitPart());
          syncServerPacket.WriteC((byte) objectHitInfo.KillerSlot);
        }
        else if (objectHitInfo.Type == 6)
        {
          syncServerPacket.WriteH((short) 15);
          syncServerPacket.WriteH((ushort) objectHitInfo.ObjId);
          syncServerPacket.WriteC((byte) 0);
          syncServerPacket.WriteD(67108864U /*0x04000000*/);
          syncServerPacket.WriteD(objectHitInfo.WeaponId);
          syncServerPacket.WriteC(objectHitInfo.Accessory);
          syncServerPacket.WriteC(objectHitInfo.Extensions);
        }
      }
      return syncServerPacket.ToArray();
    }
  }

  public static ObjectMoveInfo ReadInfo([In] SyncClientPacket obj0, bool Address)
  {
    ObjectStaticInfo objectStaticInfo = new ObjectStaticInfo();
    ((ObjectMoveInfo) objectStaticInfo).Unk = obj0.ReadB(16 /*0x10*/);
    ObjectMoveInfo objectMoveInfo = (ObjectMoveInfo) objectStaticInfo;
    if (Address)
      CLogger.Print(Bitwise.ToHexData("UDP_SUB_HEAD: OBJECT_MOVE", objectMoveInfo.Unk), LoggerType.Opcode, (Exception) null);
    return objectMoveInfo;
  }
}
