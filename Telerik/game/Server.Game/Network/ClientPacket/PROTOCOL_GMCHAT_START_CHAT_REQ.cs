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
	public class PROTOCOL_GMCHAT_START_CHAT_REQ : GameClientPacket
	{
		private string string_0;

		private int int_0;

		private byte byte_0;

		public PROTOCOL_GMCHAT_START_CHAT_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadU(base.ReadC() * 2);
			this.int_0 = base.ReadD();
			this.byte_0 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account account = AccountManager.GetAccount(this.string_0, 1, 31);
					if (account == null || !(player.Nickname != account.Nickname))
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_START_CHAT_ACK(-2147483648, null));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_START_CHAT_ACK(0, account));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_GMCHAT_START_CHAT_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}