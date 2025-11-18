namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_GM_RESUME_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_BATTLE_GM_RESUME_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(5270);
		WriteD(uint_0);
	}
}
