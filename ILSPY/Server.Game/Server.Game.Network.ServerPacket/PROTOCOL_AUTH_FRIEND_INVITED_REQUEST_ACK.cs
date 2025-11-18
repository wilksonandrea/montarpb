namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_AUTH_FRIEND_INVITED_REQUEST_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(1813);
		WriteC((byte)int_0);
	}
}
