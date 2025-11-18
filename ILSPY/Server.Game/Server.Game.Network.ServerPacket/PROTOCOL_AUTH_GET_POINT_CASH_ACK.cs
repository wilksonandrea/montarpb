using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_GET_POINT_CASH_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly Account account_0;

	public PROTOCOL_AUTH_GET_POINT_CASH_ACK(uint uint_1, Account account_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(1058);
		WriteD(uint_0);
		WriteD(account_0.Gold);
		WriteD(account_0.Cash);
		WriteD(account_0.Tags);
		WriteD(0);
	}
}
