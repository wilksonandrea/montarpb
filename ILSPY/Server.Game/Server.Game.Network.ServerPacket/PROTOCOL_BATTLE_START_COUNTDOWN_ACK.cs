using Plugin.Core.Enums;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_START_COUNTDOWN_ACK : GameServerPacket
{
	private readonly CountDownEnum countDownEnum_0;

	public PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum countDownEnum_1)
	{
		countDownEnum_0 = countDownEnum_1;
	}

	public override void Write()
	{
		WriteH(5126);
		WriteC((byte)countDownEnum_0);
	}
}
