// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK : GameServerPacket
{
  public PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK(RoomModel roomModel_1)
  {
    ((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0 = roomModel_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5263);
    this.WriteD(((PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK) this).method_1());
    this.WriteD(((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0.GetTimeByMask() * 60 - ((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0.GetInBattleTime());
    this.WriteB(((PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK) this).method_0(((PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK) this).roomModel_0));
  }
}
