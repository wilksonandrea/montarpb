namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_POINT_RESET_RESULT_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(908);
	}
}
