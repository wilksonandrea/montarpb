// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_CHATTING_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHATTING_ACK : GameServerPacket
{
  private readonly string string_0;
  private readonly int int_0;
  private readonly int int_1;
  private readonly bool bool_0;

  public virtual void Write()
  {
    this.WriteH((short) 3601);
    this.WriteD(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.RoomId);
    this.WriteU(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.Name, 46);
    this.WriteC((byte) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.MapId);
    this.WriteC((byte) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.Rule);
    this.WriteC((byte) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.Stage);
    this.WriteC((byte) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.RoomType);
    this.WriteC((byte) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.State);
    this.WriteC((byte) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.GetCountPlayers());
    this.WriteC((byte) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.GetSlotCount());
    this.WriteC((byte) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.Ping);
    this.WriteH((ushort) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.WeaponsFlag);
    this.WriteD(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.GetFlag());
    this.WriteH((short) 0);
    this.WriteD(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.NewInt);
    this.WriteH((short) 0);
    this.WriteU(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.LeaderName, 66);
    this.WriteD(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.KillTime);
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.Limit);
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.WatchRuleFlag);
    this.WriteH((ushort) ((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.BalanceType);
    this.WriteB(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.RandomMaps);
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.CountdownIG);
    this.WriteB(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.LeaderAddr);
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.KillCam);
    this.WriteH((short) 0);
    if (!((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).bool_0)
      return;
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.AiCount);
    this.WriteC(((PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK) this).roomModel_0.AiLevel);
  }

  public PROTOCOL_ROOM_CHATTING_ACK(RoomModel slotModel_1)
  {
    ((PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK) this).roomModel_0 = slotModel_1;
  }
}
