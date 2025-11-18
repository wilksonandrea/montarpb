using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly ulong ulong_0;

	public PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK(Account account_1)
	{
		account_0 = account_1;
		ulong_0 = ComDiv.GetClanStatus(account_1.Status, account_1.IsOnline);
	}

	public override void Write()
	{
		WriteH(876);
		WriteQ(account_0.PlayerId);
		WriteU(account_0.Nickname, 66);
		WriteC((byte)account_0.Rank);
		WriteC((byte)account_0.ClanAccess);
		WriteQ(ulong_0);
		WriteD(account_0.ClanDate);
		WriteC((byte)account_0.NickColor);
		WriteD(0);
		WriteD(0);
	}
}
