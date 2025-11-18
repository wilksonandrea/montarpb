// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using Server.Game.Data.Utils;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK : GameServerPacket
{
  private readonly RoomModel roomModel_0;

  public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(RoomModel roomModel_1)
  {
    ((PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) this).roomModel_0 = roomModel_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 5153);
    this.WriteC((byte) ((PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) this).roomModel_0.Rounds);
    this.WriteD(((PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) this).roomModel_0.GetInBattleTimeLeft());
    this.WriteD(AllUtils.GetSlotsFlag(((PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) this).roomModel_0, true, false));
    this.WriteC(((PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) this).roomModel_0.SwapRound ? (byte) 3 : (byte) 0);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) this).roomModel_0.FRRounds);
    this.WriteH((ushort) ((PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) this).roomModel_0.CTRounds);
    if (!((PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) this).roomModel_0.SwapRound)
      return;
    this.WriteB(((PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK) this).method_0(((PROTOCOL_BATTLE_MISSION_ROUND_START_ACK) this).roomModel_0));
  }
}
