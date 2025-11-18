namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1062);
		WriteD(uint_0);
	}
}
