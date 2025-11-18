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
	public class PROTOCOL_AUTH_SEND_WHISPER_REQ : GameClientPacket
	{
		private long long_0;

		private string string_0;

		private string string_1;

		public PROTOCOL_AUTH_SEND_WHISPER_REQ()
		{
		}

		public override void Read()
		{
			this.long_0 = base.ReadQ();
			this.string_0 = base.ReadU(66);
			this.string_1 = base.ReadU(base.ReadH() * 2);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && !(player.Nickname == this.string_0))
				{
					Account account = AccountManager.GetAccount(this.long_0, 31);
					if (account == null || !account.IsOnline)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SEND_WHISPER_ACK(this.string_0, this.string_1, -2147483648));
					}
					else
					{
						account.SendPacket(new PROTOCOL_AUTH_RECV_WHISPER_ACK(player.Nickname, this.string_1, player.UseChatGM()), false);
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