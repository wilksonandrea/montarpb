namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_USER_LEAVE_ACK : AuthServerPacket
{
	private readonly int int_0;

	public PROTOCOL_BASE_USER_LEAVE_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(2329);
		WriteD(int_0);
		WriteH(0);
	}
}
