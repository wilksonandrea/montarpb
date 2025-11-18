using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLEBOX_AUTH_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly Account account_0;

	private readonly int int_0;

	public PROTOCOL_BATTLEBOX_AUTH_ACK(uint uint_1, Account account_1 = null, int int_1 = 0)
	{
		uint_0 = uint_1;
		account_0 = account_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(7430);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteD(int_0);
			WriteB(new byte[5]);
			WriteD(account_0.Tags);
		}
	}
}
