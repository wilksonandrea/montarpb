using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_NEW_MYINFO_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly ClanModel clanModel_0;

	private readonly StatisticClan statisticClan_0;

	public PROTOCOL_LOBBY_NEW_MYINFO_ACK(Account account_1)
	{
		account_0 = account_1;
		if (account_1 != null)
		{
			clanModel_0 = ClanManager.GetClan(account_1.ClanId);
			statisticClan_0 = account_1.Statistic.Clan;
		}
	}

	public override void Write()
	{
		WriteH(977);
		WriteD(0);
		WriteQ(account_0.PlayerId);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(10101);
		WriteD(0);
		WriteD(0);
		WriteD(20202);
		WriteD(0);
		WriteD(30303);
		WriteD(0);
		WriteD(40404);
		WriteD(50505);
		WriteD(60606);
		WriteD(0);
		WriteD(70707);
		WriteD(80808);
		WriteD(90909);
		WriteD(101010);
		WriteD(111111);
		WriteD(121212);
		WriteD(131313);
		WriteD(141414);
		WriteD(151515);
		WriteD(161616);
		WriteD(171717);
		WriteD(181818);
		WriteD(191919);
		WriteD(0);
	}
}
