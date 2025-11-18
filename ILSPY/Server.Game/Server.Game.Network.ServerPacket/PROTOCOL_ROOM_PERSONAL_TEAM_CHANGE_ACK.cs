namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3610);
		WriteC(2);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(2);
		WriteC(0);
		WriteC(8);
	}
}
