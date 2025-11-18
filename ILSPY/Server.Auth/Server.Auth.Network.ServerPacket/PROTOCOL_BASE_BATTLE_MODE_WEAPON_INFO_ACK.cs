namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_BATTLE_MODE_WEAPON_INFO_ACK : AuthServerPacket
{
	public override void Write()
	{
		WriteH(2484);
		WriteC(0);
		WriteD(1);
		WriteD(1);
		WriteD(1);
		WriteD(33602800);
	}
}
