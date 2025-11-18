// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 6919);
    this.WriteD(((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).uint_0);
    if (((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).uint_0 != 0U)
      return;
    this.WriteH((short) ((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).matchModel_0.MatchId);
    this.WriteH((short) ((RoomModel) ((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).matchModel_0).GetServerInfo());
    this.WriteH((short) ((RoomModel) ((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).matchModel_0).GetServerInfo());
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).matchModel_0.State);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).matchModel_0.FriendId);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).matchModel_0.Training);
    this.WriteC((byte) ((RoomModel) ((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).matchModel_0).GetCountPlayers());
    this.WriteD(((PROTOCOL_CLAN_WAR_CREATE_TEAM_ACK) this).matchModel_0.Leader);
    this.WriteC((byte) 0);
  }

  public PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(MatchModel matchModel_0)
  {
    ((PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK) this).match = matchModel_0;
  }
}
