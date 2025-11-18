// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ServerPacket.PROTOCOL_CLAN_WAR_RESULT_ACK
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Server.Game.Data.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_RESULT_ACK : GameServerPacket
{
  public virtual void Write()
  {
    this.WriteH((short) 1570);
    this.WriteD(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).uint_0);
    if (((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).uint_0 != 0U)
      return;
    this.WriteD(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.Id);
    this.WriteS(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.Name, 17);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.Rank);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).int_0);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.MaxPlayers);
    this.WriteD(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.CreationDate);
    this.WriteD(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.Logo);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.NameColor);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.GetClanUnit());
    this.WriteD(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.Exp);
    this.WriteD(0);
    this.WriteQ(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).clanModel_0.OwnerId);
    this.WriteS(((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).account_0.Nickname, 33);
    this.WriteC((byte) ((PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK) this).account_0.Rank);
    this.WriteS("", (int) byte.MaxValue);
  }

  public PROTOCOL_CLAN_WAR_RESULT_ACK(List<MatchModel> uint_1, [In] int obj1)
  {
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK) this).int_0 = obj1;
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK) this).list_0 = uint_1;
    ((PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK) this).int_1 = uint_1.Count - 1;
  }
}
