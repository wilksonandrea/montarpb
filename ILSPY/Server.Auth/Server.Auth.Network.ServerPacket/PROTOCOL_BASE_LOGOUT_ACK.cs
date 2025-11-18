namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_LOGOUT_ACK : AuthServerPacket
{
	public override void Write()
	{
		WriteH(2308);
		WriteH(0);
	}
}
