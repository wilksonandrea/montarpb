namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_LOGIN_WAIT_ACK : AuthServerPacket
{
	private readonly int int_0;

	public PROTOCOL_BASE_LOGIN_WAIT_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(2313);
		WriteC(3);
		WriteH(68);
		WriteD(int_0);
	}
}
