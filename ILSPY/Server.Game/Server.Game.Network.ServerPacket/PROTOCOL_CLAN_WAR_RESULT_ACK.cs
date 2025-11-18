namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_RESULT_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(6966);
		WriteH(0);
	}
}
