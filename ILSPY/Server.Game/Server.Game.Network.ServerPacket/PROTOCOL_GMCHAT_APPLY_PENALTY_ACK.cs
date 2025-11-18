namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_GMCHAT_APPLY_PENALTY_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(6664);
	}
}
