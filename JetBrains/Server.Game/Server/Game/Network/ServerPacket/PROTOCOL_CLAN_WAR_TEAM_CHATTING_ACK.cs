// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK : GameServerPacket
{
  private readonly int int_0;
  private readonly int int_1;
  private readonly string string_0;
  private readonly string string_1;

  public virtual void Write()
  {
    this.WriteH((short) 6917);
    this.WriteH((ushort) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK) this).int_1);
    if (((PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK) this).int_1 <= 0)
      return;
    this.WriteH((short) 1);
    this.WriteH((short) 0);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK) this).int_1);
    foreach (MatchModel matchModel in ((PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK) this).list_0)
    {
      if (matchModel.MatchId != ((PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK) this).int_0)
      {
        this.WriteH((short) matchModel.MatchId);
        this.WriteH((short) ((RoomModel) matchModel).GetServerInfo());
        this.WriteH((short) ((RoomModel) matchModel).GetServerInfo());
        this.WriteC((byte) matchModel.State);
        this.WriteC((byte) matchModel.FriendId);
        this.WriteC((byte) matchModel.Training);
        this.WriteC((byte) ((RoomModel) matchModel).GetCountPlayers());
        this.WriteD(matchModel.Leader);
        this.WriteC((byte) 0);
        this.WriteD(matchModel.Clan.Id);
        this.WriteC((byte) matchModel.Clan.Rank);
        this.WriteD(matchModel.Clan.Logo);
        this.WriteS(matchModel.Clan.Name, 17);
        this.WriteT(matchModel.Clan.Points);
        this.WriteC((byte) matchModel.Clan.NameColor);
        Account leader = ((RoomModel) matchModel).GetLeader();
        if (leader != null)
        {
          this.WriteC((byte) leader.Rank);
          this.WriteS(leader.Nickname, 33);
          this.WriteQ(leader.PlayerId);
          this.WriteC((byte) matchModel.Slots[matchModel.Leader].State);
        }
        else
          this.WriteB(new byte[43]);
      }
    }
  }

  public PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(MatchModel uint_1)
  {
    ((PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) this).matchModel_0 = uint_1;
  }

  public virtual void Write()
  {
    this.WriteH((short) 6939);
    this.WriteH((short) ((RoomModel) ((PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) this).matchModel_0).GetServerInfo());
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) this).matchModel_0.State);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) this).matchModel_0.FriendId);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) this).matchModel_0.Training);
    this.WriteC((byte) ((RoomModel) ((PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) this).matchModel_0).GetCountPlayers());
    this.WriteD(((PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) this).matchModel_0.Leader);
    this.WriteC((byte) 0);
    foreach (SlotMatch slot in ((PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) this).matchModel_0.Slots)
    {
      Account playerBySlot = ((PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK) this).matchModel_0.GetPlayerBySlot(slot);
      if (playerBySlot != null)
      {
        this.WriteC((byte) playerBySlot.Rank);
        this.WriteS(playerBySlot.Nickname, 33);
        this.WriteQ(slot.PlayerId);
        this.WriteC((byte) slot.State);
      }
      else
        this.WriteB(new byte[43]);
    }
  }
}
