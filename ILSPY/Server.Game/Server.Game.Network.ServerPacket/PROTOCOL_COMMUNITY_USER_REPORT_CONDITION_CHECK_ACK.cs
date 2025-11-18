namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteD(3853);
		WriteD(int_0);
	}
}
