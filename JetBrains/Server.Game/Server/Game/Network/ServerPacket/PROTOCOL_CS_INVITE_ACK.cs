// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_INVITE_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_INVITE_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public PROTOCOL_CS_INVITE_ACK(int uint_1, ClanModel clanModel_1)
  {
    ((PROTOCOL_CS_DETAIL_INFO_ACK) this).int_0 = uint_1;
    ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0 = clanModel_1;
    if (clanModel_1 == null)
      return;
    ((PROTOCOL_CS_DETAIL_INFO_ACK) this).account_0 = ClanManager.GetAccount(clanModel_1.OwnerId, 31 /*0x1F*/);
    ((PROTOCOL_CS_DETAIL_INFO_ACK) this).int_1 = DaoManagerSQL.GetClanPlayers(clanModel_1.Id);
  }

  public virtual void Write()
  {
    this.WriteH((short) 801);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).int_0);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.Id);
    this.WriteU(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.Name, 34);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.Rank);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).int_1);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.MaxPlayers);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.CreationDate);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.Logo);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.NameColor);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.Effect);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.GetClanUnit());
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.Exp);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.OwnerId);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteB(((PROTOCOL_CS_JOIN_REQUEST_ACK) this).method_0(((PROTOCOL_CS_DETAIL_INFO_ACK) this).account_0));
    this.WriteU(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.Info, 510);
    this.WriteB(new byte[41]);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.JoinType);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.RankLimit);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.MaxAgeLimit);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.MinAgeLimit);
    this.WriteC((byte) ((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.Authority);
    this.WriteU(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.News, 510);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.Matches);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.MatchWins);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.MatchLoses);
    this.WriteD(6666);
    this.WriteD(7777);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.TotalKills);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.TotalAssists);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.TotalDeaths);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.TotalHeadshots);
    this.WriteD(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.TotalEscapes);
    this.WriteD(8888);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(9999);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Exp.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Exp.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Wins.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Wins.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Kills.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Kills.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Headshots.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Headshots.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Participation.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Participation.PlayerId);
    this.WriteC((byte) 0);
    this.WriteC((byte) 0);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Exp.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Wins.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Kills.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Headshots.PlayerId);
    this.WriteQ(((PROTOCOL_CS_DETAIL_INFO_ACK) this).clanModel_0.BestPlayers.Participation.PlayerId);
    this.WriteQ(0L);
    this.WriteQ(0L);
    this.WriteQ(0L);
  }
}
