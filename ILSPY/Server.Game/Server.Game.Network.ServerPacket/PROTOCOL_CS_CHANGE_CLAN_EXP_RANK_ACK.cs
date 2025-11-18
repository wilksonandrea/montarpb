namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(880);
		WriteC((byte)int_0);
	}
}
