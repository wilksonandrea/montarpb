namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(5159);
		WriteD(int_0);
	}
}
