// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK : GameServerPacket
{
  private readonly List<MatchModel> list_0;
  private readonly int int_0;

  public PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK(int int_2, ClanModel int_3)
  {
    ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).int_0 = int_2;
    ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).clanModel_0 = int_3;
    if (int_3 == null)
      return;
    ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).account_0 = ClanManager.GetAccount(int_3.OwnerId, 31 /*0x1F*/);
    ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).int_1 = DaoManagerSQL.GetClanPlayers(int_3.Id);
  }

  public virtual void Write()
  {
    this.WriteH((short) 1000);
    this.WriteH((short) 0);
    this.WriteD(0);
    this.WriteD((int) ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).clanModel_0.Points);
    this.WriteC((byte) 0);
    this.WriteD(0);
    this.WriteD(((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).clanModel_0.MatchLoses);
    this.WriteD(((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).clanModel_0.MatchWins);
    this.WriteD(((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).clanModel_0.Matches);
    this.WriteC((byte) ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).clanModel_0.MaxPlayers);
    this.WriteC((byte) ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).int_1);
    this.WriteC((byte) ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).clanModel_0.GetClanUnit());
    this.WriteB(((PROTOCOL_CS_CLAN_LIST_FILTER_ACK) this).method_0(((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).account_0));
    this.WriteD(((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).clanModel_0.Exp);
    this.WriteC((byte) ((PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK) this).clanModel_0.Rank);
    this.WriteQ(0L);
    this.WriteC((byte) 0);
  }
}
