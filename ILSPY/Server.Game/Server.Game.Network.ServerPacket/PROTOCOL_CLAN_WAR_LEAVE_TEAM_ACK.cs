namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK : GameServerPacket
{
	private readonly uint uint_0;

	public PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK(uint uint_1)
	{
		uint_0 = uint_1;
	}

	public override void Write()
	{
		WriteH(6923);
		WriteD(uint_0);
	}
}
