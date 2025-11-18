namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GUIDE_COMPLETE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(2341);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
		WriteD(0);
	}
}
