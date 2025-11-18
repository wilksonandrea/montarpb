using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_INFO_INSERT_ACK : GameServerPacket
{
	private readonly Account account_0;

	public PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(Account account_1)
	{
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(847);
		WriteC((byte)(account_0.Nickname.Length + 1));
		WriteN(account_0.Nickname, account_0.Nickname.Length + 2, "UTF-16LE");
		WriteQ(account_0.PlayerId);
		WriteQ(ComDiv.GetClanStatus(account_0.Status, account_0.IsOnline));
		WriteC((byte)account_0.Rank);
		WriteC((byte)account_0.NickColor);
		WriteD(account_0.Statistic.Clan.MatchWins);
		WriteD(account_0.Statistic.Clan.MatchLoses);
		WriteD(account_0.Equipment.NameCardId);
		WriteC((byte)account_0.Bonus.NickBorderColor);
		WriteD(10);
		WriteD(20);
		WriteD(30);
	}
}
