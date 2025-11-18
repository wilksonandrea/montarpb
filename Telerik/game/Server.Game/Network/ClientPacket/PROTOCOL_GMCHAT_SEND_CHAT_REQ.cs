using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_GMCHAT_SEND_CHAT_REQ : GameClientPacket
	{
		private long long_0;

		private string string_0;

		private string string_1;

		private string string_2;

		public PROTOCOL_GMCHAT_SEND_CHAT_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadU(base.ReadC() * 2);
			this.string_2 = base.ReadU(base.ReadH() * 2);
			this.string_1 = base.ReadU(base.ReadC() * 2);
			this.long_0 = base.ReadQ();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account account = AccountManager.GetAccount(this.string_0, 1, 31);
					if (account != null && player.Nickname != account.Nickname)
					{
						account.SendPacket(new PROTOCOL_GMCHAT_SEND_CHAT_ACK(this.string_0, this.string_2, this.string_1, account));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat(base.GetType().Name, ": ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}