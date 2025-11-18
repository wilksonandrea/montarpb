namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_ROOM_INVITED_RESULT_ACK : GameServerPacket
{
	private readonly long long_0;

	public PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(long long_1)
	{
		long_0 = long_1;
	}

	public override void Write()
	{
		WriteH(1903);
		WriteQ(long_0);
	}
}
