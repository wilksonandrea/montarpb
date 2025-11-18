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
	public class PROTOCOL_AUTH_FIND_USER_REQ : GameClientPacket
	{
		private string string_0;

		private uint uint_0;

		public PROTOCOL_AUTH_FIND_USER_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadU(34);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account account = AccountManager.GetAccount(this.string_0, 1, 286);
					if (account == null || player.Nickname.Length <= 0 || !(player.Nickname != this.string_0))
					{
						this.uint_0 = -2147477501;
					}
					else if (player.Nickname != account.Nickname)
					{
						player.FindPlayer = account.Nickname;
					}
					this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(this.uint_0, account, 2147483647));
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