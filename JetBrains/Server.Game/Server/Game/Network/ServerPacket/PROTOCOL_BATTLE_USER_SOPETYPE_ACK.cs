// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_USER_SOPETYPE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_USER_SOPETYPE_ACK : GameServerPacket
{
  private readonly Account account_0;

  public virtual void Write()
  {
    this.WriteH((short) 3399);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_KICKVOTE_ACK) this).voteKickModel_0.CreatorIdx);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_KICKVOTE_ACK) this).voteKickModel_0.VictimIdx);
    this.WriteC((byte) ((PROTOCOL_BATTLE_START_KICKVOTE_ACK) this).voteKickModel_0.Motive);
  }

  public PROTOCOL_BATTLE_USER_SOPETYPE_ACK(uint roomModel_1)
  {
    ((PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK) this).uint_0 = roomModel_1;
  }
}
