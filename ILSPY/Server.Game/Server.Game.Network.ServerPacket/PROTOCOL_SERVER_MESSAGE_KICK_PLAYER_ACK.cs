namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3075);
		WriteC(0);
	}
}
