using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_MESSENGER_NOTE_SEND_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		private string string_0;

		private string string_1;

		private uint uint_0;

		public PROTOCOL_MESSENGER_NOTE_SEND_REQ()
		{
		}

		private MessageModel method_0(string string_2, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15)
			{
				SenderName = string_2,
				SenderId = long_1,
				Text = this.string_1,
				State = NoteMessageState.Unreaded
			};
			if (DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return messageModel;
			}
			this.uint_0 = -2147483648;
			return null;
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
			this.int_1 = base.ReadC();
			this.string_0 = base.ReadU(this.int_0 * 2);
			this.string_1 = base.ReadU(this.int_1 * 2);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Nickname.Length != 0 && !(player.Nickname == this.string_0))
				{
					Account account = AccountManager.GetAccount(this.string_0, 1, 0);
					if (account == null)
					{
						this.uint_0 = -2147479426;
					}
					else if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
					{
						MessageModel messageModel = this.method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
						if (messageModel != null)
						{
							account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
						}
					}
					else
					{
						this.uint_0 = -2147479425;
					}
					this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(this.uint_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_MESSENGER_NOTE_SEND_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}