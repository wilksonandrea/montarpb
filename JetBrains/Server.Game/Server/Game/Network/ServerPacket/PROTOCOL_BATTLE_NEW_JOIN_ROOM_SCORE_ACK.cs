// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public virtual void Write()
  {
    this.WriteH((short) 5179);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK) this).roomModel_0.FRDino);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK) this).roomModel_0.CTDino);
    this.WriteD(((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK) this).slotModel_0.Id);
    this.WriteH((short) ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK) this).slotModel_0.PassSequence);
  }

  public PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(RoomModel roomModel_1)
  {
    ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK) this).roomModel_0 = roomModel_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5185);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK) this).roomModel_0.FRDino);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK) this).roomModel_0.CTDino);
  }

  public PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(RoomModel roomModel_1)
  {
    ((PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK) this).roomModel_0 = roomModel_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5189);
    this.WriteC((byte) 3);
    this.WriteH((short) (((PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK) this).roomModel_0.GetTimeByMask() * 60 - ((PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK) this).roomModel_0.GetInBattleTime()));
  }
}
