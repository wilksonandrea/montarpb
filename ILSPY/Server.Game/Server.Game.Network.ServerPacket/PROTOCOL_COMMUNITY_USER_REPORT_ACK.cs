namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_COMMUNITY_USER_REPORT_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_COMMUNITY_USER_REPORT_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteD(3851);
		WriteD(uint_0);
	}
}
