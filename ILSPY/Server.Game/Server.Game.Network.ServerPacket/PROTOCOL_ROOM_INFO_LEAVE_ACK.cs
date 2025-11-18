namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_INFO_LEAVE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3674);
		WriteD(0);
		WriteC(68);
	}
}
