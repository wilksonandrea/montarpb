namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_COMMISSION_STAFF_RESULT_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(838);
	}
}
