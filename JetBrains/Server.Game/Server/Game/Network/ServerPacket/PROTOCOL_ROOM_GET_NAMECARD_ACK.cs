// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_NAMECARD_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_NAMECARD_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 3593);
    this.WriteD(((PROTOCOL_ROOM_CREATE_ACK) this).uint_0 == 0U ? (uint) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.RoomId : ((PROTOCOL_ROOM_CREATE_ACK) this).uint_0);
    if (((PROTOCOL_ROOM_CREATE_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.RoomId);
    this.WriteU(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.Name, 46);
    this.WriteC((byte) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.MapId);
    this.WriteC((byte) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.Rule);
    this.WriteC((byte) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.Stage);
    this.WriteC((byte) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.RoomType);
    this.WriteC((byte) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.State);
    this.WriteC((byte) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.GetCountPlayers());
    this.WriteC((byte) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.GetSlotCount());
    this.WriteC((byte) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.Ping);
    this.WriteH((ushort) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.WeaponsFlag);
    this.WriteD(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.GetFlag());
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.NewInt);
    this.WriteH((short) 0);
    this.WriteU(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.LeaderName, 66);
    this.WriteD(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.KillTime);
    this.WriteC(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.Limit);
    this.WriteC(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.WatchRuleFlag);
    this.WriteH((ushort) ((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.BalanceType);
    this.WriteB(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.RandomMaps);
    this.WriteC(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.CountdownIG);
    this.WriteB(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.LeaderAddr);
    this.WriteC(((PROTOCOL_ROOM_CREATE_ACK) this).roomModel_0.KillCam);
    this.WriteH((short) 0);
  }

  public PROTOCOL_ROOM_GET_NAMECARD_ACK([In] int obj0, [In] int obj1)
  {
    ((PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK) this).int_0 = obj0;
    ((PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK) this).int_1 = obj1;
  }
}
