// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK : GameServerPacket
{
  private readonly VoteKickModel voteKickModel_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 3407);
    this.WriteC((byte) ((PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK) this).voteKickModel_0.Accept);
    this.WriteC((byte) ((PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK) this).voteKickModel_0.Denie);
  }

  public PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK(int roomModel_1)
  {
    ((PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK) this).int_0 = roomModel_1;
  }
}
