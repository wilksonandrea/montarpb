namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_RECORD_RESET_RESULT_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(907);
	}
}
