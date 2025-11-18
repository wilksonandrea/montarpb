using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Sync.Client
{
	public class AuthLogin
	{
		public AuthLogin()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			Account account = AccountManager.GetAccount(C.ReadQ(), true);
			if (account != null)
			{
				account.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(1));
				account.SendPacket(new PROTOCOL_SERVER_MESSAGE_ERROR_ACK(-2147479552));
				account.Close(1000, false);
			}
		}
	}
}