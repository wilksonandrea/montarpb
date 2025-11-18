using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ : GameClientPacket
	{
		private long long_0;

		public PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ()
		{
		}

		public override void Read()
		{
			this.long_0 = base.ReadQ();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account account = AccountManager.GetAccount(this.long_0, 31);
					if (account != null && player.PlayerId != account.PlayerId)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(account));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}