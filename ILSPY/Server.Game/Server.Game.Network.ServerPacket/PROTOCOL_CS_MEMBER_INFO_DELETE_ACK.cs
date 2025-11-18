namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_MEMBER_INFO_DELETE_ACK : GameServerPacket
{
	private readonly long long_0;

	public PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(long long_1)
	{
		long_0 = long_1;
	}

	public override void Write()
	{
		WriteH(849);
		WriteQ(long_0);
	}
}
