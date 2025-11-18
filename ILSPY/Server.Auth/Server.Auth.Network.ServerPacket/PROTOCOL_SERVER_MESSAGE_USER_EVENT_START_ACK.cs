namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK : AuthServerPacket
{
	public override void Write()
	{
		WriteH(3086);
	}
}
