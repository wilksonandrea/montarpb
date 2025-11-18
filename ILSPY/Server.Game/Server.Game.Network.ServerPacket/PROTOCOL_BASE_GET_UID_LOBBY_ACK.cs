namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_UID_LOBBY_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(2442);
	}
}
