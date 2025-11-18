namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_ROOM_INVITED_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_CS_ROOM_INVITED_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(1902);
		WriteD(int_0);
	}
}
