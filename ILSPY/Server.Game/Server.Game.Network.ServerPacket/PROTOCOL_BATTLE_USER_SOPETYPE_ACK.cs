using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_USER_SOPETYPE_ACK : GameServerPacket
{
	private readonly Account account_0;

	public PROTOCOL_BATTLE_USER_SOPETYPE_ACK(Account account_1)
	{
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(5277);
		WriteD(account_0.SlotId);
		WriteC((byte)account_0.Sight);
	}
}
