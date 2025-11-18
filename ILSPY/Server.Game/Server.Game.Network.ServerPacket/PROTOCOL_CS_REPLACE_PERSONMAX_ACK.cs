namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_PERSONMAX_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_CS_REPLACE_PERSONMAX_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(873);
		WriteD(0);
		WriteC((byte)int_0);
	}
}
