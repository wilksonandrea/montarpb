namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(902);
		WriteC((byte)int_0);
	}
}
