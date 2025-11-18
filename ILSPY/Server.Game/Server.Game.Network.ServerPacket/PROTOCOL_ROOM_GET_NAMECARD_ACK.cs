namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_NAMECARD_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3637);
	}
}
