namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_COMMISSION_MASTER_RESULT_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(835);
	}
}
