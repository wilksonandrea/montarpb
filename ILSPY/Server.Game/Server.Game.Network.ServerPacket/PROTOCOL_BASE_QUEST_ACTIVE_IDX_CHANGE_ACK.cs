using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly uint uint_0;

	private readonly int int_0;

	public PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(uint uint_1, int int_1, Account account_1)
	{
		uint_0 = uint_1;
		int_0 = int_1;
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(2361);
		WriteD(uint_0);
		WriteC((byte)int_0);
		if ((uint_0 & 1) == 1)
		{
			WriteD(account_0.Exp);
			WriteD(account_0.Gold);
			WriteD(account_0.Ribbon);
			WriteD(account_0.Ensign);
			WriteD(account_0.Medal);
			WriteD(account_0.MasterMedal);
			WriteD(account_0.Rank);
		}
	}
}
