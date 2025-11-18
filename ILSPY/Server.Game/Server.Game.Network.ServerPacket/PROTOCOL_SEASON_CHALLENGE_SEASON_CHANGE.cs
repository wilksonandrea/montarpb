namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : GameServerPacket
{
	public override void Write()
	{
		WriteH(8451);
		WriteH(0);
		WriteC(1);
	}
}
