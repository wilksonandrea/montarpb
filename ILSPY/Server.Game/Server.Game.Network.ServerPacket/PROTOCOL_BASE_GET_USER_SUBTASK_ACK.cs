using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_USER_SUBTASK_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly int int_0;

	public PROTOCOL_BASE_GET_USER_SUBTASK_ACK(PlayerSession playerSession_0)
	{
		account_0 = AccountManager.GetAccount(playerSession_0.PlayerId, noUseDB: true);
		int_0 = playerSession_0.SessionId;
	}

	public override void Write()
	{
		WriteH(2448);
		WriteD(int_0);
		WriteH(0);
		WriteC(0);
		WriteD(int_0);
		WriteC(0);
	}
}
