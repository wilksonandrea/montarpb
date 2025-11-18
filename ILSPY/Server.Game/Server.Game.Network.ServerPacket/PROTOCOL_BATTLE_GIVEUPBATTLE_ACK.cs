using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_GIVEUPBATTLE_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly int int_0;

	public PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Account account_1, int int_1)
	{
		account_0 = account_1;
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(5134);
		WriteD(account_0.SlotId);
		WriteC((byte)int_0);
		WriteD(account_0.Exp);
		WriteD(account_0.Rank);
		WriteD(account_0.Gold);
		WriteD(account_0.Statistic.Season.EscapesCount);
		WriteD(account_0.Statistic.Basic.EscapesCount);
		WriteD(0);
		WriteC(0);
	}
}
