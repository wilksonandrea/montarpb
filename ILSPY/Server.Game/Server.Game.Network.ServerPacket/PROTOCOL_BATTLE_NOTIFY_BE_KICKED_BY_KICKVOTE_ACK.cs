namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3409);
	}
}
