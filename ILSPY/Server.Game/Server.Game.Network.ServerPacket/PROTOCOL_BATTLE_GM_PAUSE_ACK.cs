namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_GM_PAUSE_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_BATTLE_GM_PAUSE_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(5206);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteD(1);
		}
	}
}
