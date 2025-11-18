namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_COMMISSION_MASTER_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_CS_COMMISSION_MASTER_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(834);
		WriteD(uint_0);
	}
}
