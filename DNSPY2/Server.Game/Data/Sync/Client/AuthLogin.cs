using System;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001EF RID: 495
	public class AuthLogin
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x0002F778 File Offset: 0x0002D978
		public static void Load(SyncClientPacket C)
		{
			Account account = AccountManager.GetAccount(C.ReadQ(), true);
			if (account != null)
			{
				account.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(1));
				account.SendPacket(new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(2147487744U));
				account.Close(1000, false);
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000025DF File Offset: 0x000007DF
		public AuthLogin()
		{
		}
	}
}
