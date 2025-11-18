namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(5144);
		WriteH(0);
	}
}
