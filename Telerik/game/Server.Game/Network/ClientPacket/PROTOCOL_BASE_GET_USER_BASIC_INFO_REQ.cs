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
	public class PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ : GameClientPacket
	{
		private uint uint_0;

		private long long_0;

		public PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ()
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
					Account accountDB = AccountManager.GetAccountDB(this.long_0, 2, 31);
					if (accountDB == null || player.Nickname.Length <= 0 || player.PlayerId == this.long_0)
					{
						this.uint_0 = -2147477501;
					}
					else
					{
						if (player.Nickname != accountDB.Nickname)
						{
							player.FindPlayer = accountDB.Nickname;
						}
						this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(this.uint_0, accountDB, 2147483647));
					}
					this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(this.uint_0));
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