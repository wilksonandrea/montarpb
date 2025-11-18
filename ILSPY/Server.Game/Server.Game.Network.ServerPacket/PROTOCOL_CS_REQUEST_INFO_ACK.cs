using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REQUEST_INFO_ACK : GameServerPacket
{
	private readonly string string_0;

	private readonly uint uint_0;

	private readonly Account account_0;

	public PROTOCOL_CS_REQUEST_INFO_ACK(long long_0, string string_1)
	{
		string_0 = string_1;
		account_0 = AccountManager.GetAccount(long_0, 31);
		if (account_0 == null || string_1 == null)
		{
			uint_0 = 2147483648u;
		}
	}

	public override void Write()
	{
		WriteH(821);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteQ(account_0.PlayerId);
			WriteU(account_0.Nickname, 66);
			WriteC((byte)account_0.Rank);
			WriteD(account_0.Statistic.Basic.KillsCount);
			WriteD(account_0.Statistic.Basic.DeathsCount);
			WriteD(account_0.Statistic.Basic.Matches);
			WriteD(account_0.Statistic.Basic.MatchWins);
			WriteD(account_0.Statistic.Basic.MatchLoses);
			WriteN(string_0, string_0.Length + 2, "UTF-16LE");
		}
	}
}
