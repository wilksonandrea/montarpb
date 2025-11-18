namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK : GameServerPacket
{
	private readonly int int_0;

	public PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK(int int_1)
	{
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(3413);
		WriteC((byte)int_0);
		WriteC(1);
		WriteD(1);
	}
}
