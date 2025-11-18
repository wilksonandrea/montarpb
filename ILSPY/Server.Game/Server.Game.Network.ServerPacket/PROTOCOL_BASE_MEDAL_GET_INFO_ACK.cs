using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_MEDAL_GET_INFO_ACK : GameServerPacket
{
	private readonly Account account_0;

	public PROTOCOL_BASE_MEDAL_GET_INFO_ACK(Account account_1)
	{
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(2363);
		WriteQ(account_0.PlayerId);
		WriteD(account_0.Ribbon);
		WriteD(account_0.Ensign);
		WriteD(account_0.Medal);
		WriteD(account_0.MasterMedal);
	}
}
