namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_DEPORTATION_RESULT_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(832);
	}
}
