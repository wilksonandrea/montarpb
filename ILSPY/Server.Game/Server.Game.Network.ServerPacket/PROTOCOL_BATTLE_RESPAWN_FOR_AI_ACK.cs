namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(5175);
		WriteD(int_0);
	}
}
