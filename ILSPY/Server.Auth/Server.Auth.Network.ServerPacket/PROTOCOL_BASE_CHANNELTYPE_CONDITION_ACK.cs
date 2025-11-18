namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_CHANNELTYPE_CONDITION_ACK : AuthServerPacket
{
	public override void Write()
	{
		WriteH(2486);
		WriteB(new byte[888]);
	}
}
