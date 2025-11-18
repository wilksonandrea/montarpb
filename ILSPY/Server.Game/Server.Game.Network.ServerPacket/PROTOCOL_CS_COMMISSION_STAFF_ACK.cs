namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_COMMISSION_STAFF_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_CS_COMMISSION_STAFF_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(837);
		WriteD(uint_0);
	}
}
