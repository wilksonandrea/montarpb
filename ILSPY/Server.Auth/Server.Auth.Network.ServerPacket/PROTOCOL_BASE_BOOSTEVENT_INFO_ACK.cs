namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_BOOSTEVENT_INFO_ACK : AuthServerPacket
{
	public override void Write()
	{
		WriteH(2469);
		WriteD(1);
		WriteD(0);
		WriteC(0);
	}
}
