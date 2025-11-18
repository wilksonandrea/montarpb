namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_USER_EVENT_SYNC_ACK : AuthServerPacket
{
	public override void Write()
	{
		WriteH(2473);
		WriteH(0);
	}
}
