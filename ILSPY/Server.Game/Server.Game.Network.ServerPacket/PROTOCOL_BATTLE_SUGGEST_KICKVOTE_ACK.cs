namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(3397);
		WriteD(uint_0);
	}
}
