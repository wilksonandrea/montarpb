namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_MATCH_SERVER_IDX_ACK : AuthServerPacket
{
	private readonly short short_0;

	public PROTOCOL_MATCH_SERVER_IDX_ACK(short short_1)
	{
		short_0 = short_1;
	}

	public override void Write()
	{
		WriteH(7682);
		WriteH(0);
	}
}
