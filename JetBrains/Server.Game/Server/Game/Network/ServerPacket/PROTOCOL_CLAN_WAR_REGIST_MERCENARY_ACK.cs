// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK : GameServerPacket
{
  private readonly MatchModel matchModel_0;

  public PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(uint uint_1, ClanModel matchModel_1 = null)
  {
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).uint_0 = uint_1;
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0 = matchModel_1;
    if (((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0 == null)
      return;
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).int_0 = DaoManagerSQL.GetClanPlayers(matchModel_1.Id);
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).account_0 = ClanManager.GetAccount(matchModel_1.OwnerId, 31 /*0x1F*/);
    if (((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).account_0 != null)
      return;
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).uint_0 = 2147483648U /*0x80000000*/;
  }

  public PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(uint uint_1)
  {
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).uint_0 = uint_1;
  }
}
