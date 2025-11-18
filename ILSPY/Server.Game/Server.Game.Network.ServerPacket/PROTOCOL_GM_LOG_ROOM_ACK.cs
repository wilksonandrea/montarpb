using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GM_LOG_ROOM_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly uint uint_0;

	public PROTOCOL_GM_LOG_ROOM_ACK(uint uint_1, Account account_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(2687);
		WriteD(uint_0);
		WriteQ(account_0.PlayerId);
	}
}
