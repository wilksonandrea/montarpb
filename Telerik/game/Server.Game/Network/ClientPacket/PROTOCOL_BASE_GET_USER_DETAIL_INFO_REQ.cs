using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					PlayerSession playerSession = player.GetChannel().GetPlayer(this.int_0);
					if (playerSession != null)
					{
						Account account = AccountManager.GetAccount(playerSession.PlayerId, true);
						if (account != null)
						{
							if (player.Nickname != account.Nickname)
							{
								player.FindPlayer = account.Nickname;
							}
							this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(0, account, 2147483647));
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_LOBBY_USER_STATISTICS_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}