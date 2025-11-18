namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_TICKET_UPDATE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(2509);
		WriteH(0);
	}
}
