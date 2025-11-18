namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1042);
		WriteH(0);
	}
}
