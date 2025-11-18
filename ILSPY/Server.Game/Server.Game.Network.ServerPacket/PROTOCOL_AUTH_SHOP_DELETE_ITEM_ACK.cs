namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK : GameServerPacket
{
	private readonly long long_0;

	private readonly uint uint_0;

	public PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(uint uint_1, long long_1 = 0L)
	{
		uint_0 = uint_1;
		if (uint_1 == 1)
		{
			long_0 = long_1;
		}
	}

	public override void Write()
	{
		WriteH(1056);
		WriteD(uint_0);
		if (uint_0 == 1)
		{
			WriteD((int)long_0);
		}
	}
}
