// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK : GameServerPacket
{
  private readonly int int_0;

  public virtual void Write()
  {
    this.WriteH((short) 6921);
    this.WriteD(((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).uint_0);
    if (((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).uint_0 != 0U)
      return;
    this.WriteH((short) ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.MatchId);
    this.WriteH((ushort) ((RoomModel) ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0).GetServerInfo());
    this.WriteH((ushort) ((RoomModel) ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0).GetServerInfo());
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.State);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.FriendId);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Training);
    this.WriteC((byte) ((RoomModel) ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0).GetCountPlayers());
    this.WriteD(((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Leader);
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Clan.Id);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Clan.Rank);
    this.WriteD(((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Clan.Logo);
    this.WriteS(((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Clan.Name, 17);
    this.WriteT(((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Clan.Points);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Clan.NameColor);
    for (int index = 0; index < ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Training; ++index)
    {
      SlotMatch slot = ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.Slots[index];
      Account playerBySlot = ((PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK) this).matchModel_0.GetPlayerBySlot(slot);
      if (playerBySlot != null)
      {
        this.WriteC((byte) playerBySlot.Rank);
        this.WriteS(playerBySlot.Nickname, 33);
        this.WriteQ(playerBySlot.PlayerId);
        this.WriteC((byte) slot.State);
      }
      else
        this.WriteB(new byte[43]);
    }
  }

  public PROTOCOL_CLAN_WAR_MATCH_TEAM_COUNT_ACK(uint matchModel_1)
  {
    ((PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK) this).uint_0 = matchModel_1;
  }
}
