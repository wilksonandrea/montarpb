namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_REMOVE_MERCENARY_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(6941);
		WriteD(0);
	}
}
