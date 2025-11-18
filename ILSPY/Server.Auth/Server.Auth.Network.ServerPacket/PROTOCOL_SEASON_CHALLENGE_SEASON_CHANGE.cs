namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE : AuthServerPacket
{
	public override void Write()
	{
		WriteH(8451);
		WriteH(0);
		WriteC(1);
	}
}
