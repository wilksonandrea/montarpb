namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(1559);
		WriteD(uint_0);
	}
}
