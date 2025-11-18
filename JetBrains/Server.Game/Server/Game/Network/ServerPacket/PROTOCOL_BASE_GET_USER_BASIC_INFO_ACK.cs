// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK : GameServerPacket
{
  private readonly uint uint_0;

  public virtual void Write()
  {
    this.WriteH((short) 2371);
    this.WriteU(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Nickname, 66);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.GetRank());
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.GetRank());
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Gold);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Exp);
    this.WriteD(0);
    this.WriteC((byte) 0);
    this.WriteQ(0L);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.AuthLevel());
    this.WriteC((byte) 0);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Tags);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Cash);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).clanModel_0.Id);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.ClanAccess);
    this.WriteQ(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.StatusId());
    this.WriteC((byte) ((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.CafePC);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Country);
    this.WriteU(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).clanModel_0.Name, 34);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).clanModel_0.Rank);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).clanModel_0.GetClanUnit());
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).clanModel_0.Logo);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).clanModel_0.NameColor);
    this.WriteC((byte) ((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).clanModel_0.Effect);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.Matches);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.MatchWins);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.MatchLoses);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.MatchDraws);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.KillsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.HeadshotsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.DeathsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.TotalMatchesCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.TotalKillsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.EscapesCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.AssistsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Season.MvpCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.Matches);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.MatchWins);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.MatchLoses);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.MatchDraws);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.KillsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.HeadshotsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.DeathsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.get_TotalMatchesCount());
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.get_TotalKillsCount());
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.EscapesCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.AssistsCount);
    this.WriteD(((PROTOCOL_BASE_GET_MYINFO_BASIC_ACK) this).account_0.Statistic.Basic.MvpCount);
  }

  public PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(PlayerStatistic int_1)
  {
    ((PROTOCOL_BASE_GET_MYINFO_RECORD_ACK) this).playerStatistic_0 = int_1;
  }
}
