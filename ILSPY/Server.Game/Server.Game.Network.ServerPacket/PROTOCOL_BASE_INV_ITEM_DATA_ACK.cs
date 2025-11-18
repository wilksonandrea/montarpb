using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_INV_ITEM_DATA_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly Account account_0;

	public PROTOCOL_BASE_INV_ITEM_DATA_ACK(int int_1, Account account_1)
	{
		int_0 = int_1;
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(2395);
		WriteC((byte)int_0);
		WriteC((byte)account_0.NickColor);
		WriteD(account_0.Bonus.FakeRank);
		WriteD(account_0.Bonus.FakeRank);
		WriteU(account_0.Bonus.FakeNick, 66);
		WriteH((short)account_0.Bonus.CrosshairColor);
		WriteH((short)account_0.Bonus.MuzzleColor);
		WriteC((byte)account_0.Bonus.NickBorderColor);
	}
}
