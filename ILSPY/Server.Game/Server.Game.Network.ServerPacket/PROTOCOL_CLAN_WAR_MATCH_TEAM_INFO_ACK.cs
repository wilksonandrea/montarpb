using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly ClanModel clanModel_0;

	private readonly Account account_0;

	private readonly int int_0;

	public PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(uint uint_1, ClanModel clanModel_1)
	{
		uint_0 = uint_1;
		clanModel_0 = clanModel_1;
		if (clanModel_0 != null)
		{
			int_0 = DaoManagerSQL.GetClanPlayers(clanModel_1.Id);
			account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 31);
			if (account_0 == null)
			{
				uint_0 = 2147483648u;
			}
		}
	}

	public PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1570);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteD(clanModel_0.Id);
			WriteS(clanModel_0.Name, 17);
			WriteC((byte)clanModel_0.Rank);
			WriteC((byte)int_0);
			WriteC((byte)clanModel_0.MaxPlayers);
			WriteD(clanModel_0.CreationDate);
			WriteD(clanModel_0.Logo);
			WriteC((byte)clanModel_0.NameColor);
			WriteC((byte)clanModel_0.GetClanUnit());
			WriteD(clanModel_0.Exp);
			WriteD(0);
			WriteQ(clanModel_0.OwnerId);
			WriteS(account_0.Nickname, 33);
			WriteC((byte)account_0.Rank);
			WriteS("", 255);
		}
	}
}
