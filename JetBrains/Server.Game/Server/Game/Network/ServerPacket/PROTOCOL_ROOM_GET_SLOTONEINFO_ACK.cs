// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_ROOM_GET_SLOTONEINFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_SLOTONEINFO_ACK : GameServerPacket
{
  private readonly Account account_0;
  private readonly RoomModel roomModel_0;
  private readonly ClanModel clanModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 3634);
    this.WriteD(((PROTOCOL_ROOM_GET_RANK_ACK) this).int_0);
    this.WriteD(((PROTOCOL_ROOM_GET_RANK_ACK) this).int_1);
  }

  public PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(RoomModel uint_1)
  {
    ((PROTOCOL_ROOM_GET_SLOTINFO_ACK) this).roomModel_0 = uint_1;
    if (uint_1 == null || uint_1.GetLeader() != null)
      return;
    uint_1.SetNewLeader(-1, SlotState.EMPTY, uint_1.LeaderSlot, false);
  }

  public virtual void Write()
  {
    this.WriteH((short) 3595);
    this.WriteC((byte) ((PROTOCOL_ROOM_GET_SLOTINFO_ACK) this).roomModel_0.LeaderSlot);
    this.WriteB(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).method_0(((PROTOCOL_ROOM_GET_SLOTINFO_ACK) this).roomModel_0));
    this.WriteB(((PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK) this).method_1(((PROTOCOL_ROOM_GET_SLOTINFO_ACK) this).roomModel_0));
  }
}
