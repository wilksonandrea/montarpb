namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_LOGOUT_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(2308);
		WriteH(0);
	}
}
