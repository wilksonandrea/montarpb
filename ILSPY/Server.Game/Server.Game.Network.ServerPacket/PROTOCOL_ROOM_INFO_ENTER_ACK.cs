namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_INFO_ENTER_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3672);
		WriteD(0);
		WriteC(68);
	}
}
