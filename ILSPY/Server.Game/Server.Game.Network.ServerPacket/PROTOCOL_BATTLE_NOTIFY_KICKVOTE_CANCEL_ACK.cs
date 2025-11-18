namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3405);
	}
}
