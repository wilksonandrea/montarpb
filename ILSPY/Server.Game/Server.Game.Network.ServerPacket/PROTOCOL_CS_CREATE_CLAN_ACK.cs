using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CREATE_CLAN_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly ClanModel clanModel_0;

	private readonly uint uint_0;

	public PROTOCOL_CS_CREATE_CLAN_ACK(uint uint_1, ClanModel clanModel_1, Account account_1)
	{
		uint_0 = uint_1;
		clanModel_0 = clanModel_1;
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(807);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteD(clanModel_0.Id);
			WriteU(clanModel_0.Name, 34);
			WriteC((byte)clanModel_0.Rank);
			WriteC((byte)DaoManagerSQL.GetClanPlayers(clanModel_0.Id));
			WriteC((byte)clanModel_0.MaxPlayers);
			WriteD(clanModel_0.CreationDate);
			WriteD(clanModel_0.Logo);
			WriteB(new byte[11]);
			WriteQ(clanModel_0.OwnerId);
			WriteS(account_0.Nickname, 66);
			WriteC((byte)account_0.NickColor);
			WriteC((byte)account_0.Rank);
			WriteU(clanModel_0.Info, 510);
			WriteU("Temp", 42);
			WriteC((byte)clanModel_0.RankLimit);
			WriteC((byte)clanModel_0.MinAgeLimit);
			WriteC((byte)clanModel_0.MaxAgeLimit);
			WriteC((byte)clanModel_0.Authority);
			WriteU("", 510);
			WriteB(new byte[44]);
			WriteF(clanModel_0.Points);
			WriteF(60.0);
			WriteB(new byte[16]);
			WriteF(clanModel_0.Points);
			WriteF(60.0);
			WriteB(new byte[80]);
			WriteB(new byte[66]);
			WriteD(account_0.Gold);
		}
	}
}
