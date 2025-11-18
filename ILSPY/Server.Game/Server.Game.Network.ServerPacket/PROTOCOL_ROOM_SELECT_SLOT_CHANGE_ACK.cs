namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_SELECT_SLOT_CHANGE_ACK : GameServerPacket
{
	public override void Write()
	{
		WriteH(3683);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteC(0);
	}
}
