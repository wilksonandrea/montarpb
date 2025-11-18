namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_AUTH_ACCOUNT_KICK_ACK : AuthServerPacket
{
	private readonly int int_0;

	public PROTOCOL_AUTH_ACCOUNT_KICK_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(1989);
		WriteC((byte)int_0);
	}
}
