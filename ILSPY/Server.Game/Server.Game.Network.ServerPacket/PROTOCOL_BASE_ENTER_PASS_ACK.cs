namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_ENTER_PASS_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_BASE_ENTER_PASS_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(2402);
		WriteH(0);
		WriteD(uint_0);
	}
}
