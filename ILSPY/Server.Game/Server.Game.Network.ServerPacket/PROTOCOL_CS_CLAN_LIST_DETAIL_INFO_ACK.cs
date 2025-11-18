using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK : GameServerPacket
{
	private readonly ClanModel clanModel_0;

	private readonly int int_0;

	private readonly Account account_0;

	private readonly int int_1;

	public PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK(int int_2, ClanModel clanModel_1)
	{
		int_0 = int_2;
		clanModel_0 = clanModel_1;
		if (clanModel_1 != null)
		{
			account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 31);
			int_1 = DaoManagerSQL.GetClanPlayers(clanModel_1.Id);
		}
	}

	public override void Write()
	{
		WriteH(1000);
		WriteH(0);
		WriteD(0);
		WriteD((int)clanModel_0.Points);
		WriteC(0);
		WriteD(0);
		WriteD(clanModel_0.MatchLoses);
		WriteD(clanModel_0.MatchWins);
		WriteD(clanModel_0.Matches);
		WriteC((byte)clanModel_0.MaxPlayers);
		WriteC((byte)int_1);
		WriteC((byte)clanModel_0.GetClanUnit());
		WriteB(method_0(account_0));
		WriteD(clanModel_0.Exp);
		WriteC((byte)clanModel_0.Rank);
		WriteQ(0L);
		WriteC(0);
	}

	private byte[] method_0(Account account_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		if (account_1 != null)
		{
			syncServerPacket.WriteC((byte)(account_1.Nickname.Length + 1));
			syncServerPacket.WriteN(account_1.Nickname, account_1.Nickname.Length + 2, "UTF-16LE");
		}
		else
		{
			syncServerPacket.WriteC(0);
		}
		return syncServerPacket.ToArray();
	}
}
