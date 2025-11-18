using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK : GameServerPacket
{
	private readonly ClanModel clanModel_0;

	private readonly Account account_0;

	private readonly int int_0;

	public PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(ClanModel clanModel_1, Account account_1, int int_1)
	{
		clanModel_0 = clanModel_1;
		account_0 = account_1;
		int_0 = int_1;
	}

	public PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(ClanModel clanModel_1, int int_1)
	{
		clanModel_0 = clanModel_1;
		account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 31);
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(824);
		WriteD(clanModel_0.Id);
		WriteU(clanModel_0.Name, 34);
		WriteC((byte)clanModel_0.Rank);
		WriteC((byte)int_0);
		WriteC((byte)clanModel_0.MaxPlayers);
		WriteD(clanModel_0.CreationDate);
		WriteD(clanModel_0.Logo);
		WriteC((byte)clanModel_0.NameColor);
		WriteC((byte)clanModel_0.Effect);
		WriteC((byte)clanModel_0.GetClanUnit());
		WriteD(clanModel_0.Exp);
		WriteD(10);
		WriteQ(clanModel_0.OwnerId);
		WriteU((account_0 != null) ? account_0.Nickname : "", 66);
		WriteC((byte)((account_0 != null) ? ((uint)account_0.NickColor) : 0u));
		WriteC((byte)((account_0 != null) ? ((uint)account_0.Rank) : 0u));
		WriteU(clanModel_0.Info, 510);
		WriteU("Temp", 42);
		WriteC((byte)clanModel_0.RankLimit);
		WriteC((byte)clanModel_0.MinAgeLimit);
		WriteC((byte)clanModel_0.MaxAgeLimit);
		WriteC((byte)clanModel_0.Authority);
		WriteU(clanModel_0.News, 510);
		WriteD(clanModel_0.Matches);
		WriteD(clanModel_0.MatchWins);
		WriteD(clanModel_0.MatchLoses);
		WriteD(clanModel_0.Matches);
		WriteD(clanModel_0.MatchWins);
		WriteD(clanModel_0.MatchLoses);
	}
}
