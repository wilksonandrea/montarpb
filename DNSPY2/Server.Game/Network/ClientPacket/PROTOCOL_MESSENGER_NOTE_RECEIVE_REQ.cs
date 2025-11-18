using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001C1 RID: 449
	public class PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ : GameClientPacket
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x00005737 File Offset: 0x00003937
		public override void Read()
		{
			this.long_0 = base.ReadQ();
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00024944 File Offset: 0x00022B44
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (this.Client.PlayerId != this.long_0)
					{
						Account account = AccountManager.GetAccount(this.long_0, 31);
						if (account != null)
						{
							if (DaoManagerSQL.GetMessagesCount(account.PlayerId) >= 100)
							{
								this.uint_0 = 2147487871U;
							}
							else
							{
								MessageModel messageModel = this.method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
								if (messageModel != null)
								{
									account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
								}
							}
						}
						else
						{
							this.uint_0 = 2147487870U;
						}
						this.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(this.uint_0));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00024A28 File Offset: 0x00022C28
		private MessageModel method_0(string string_1, long long_1, long long_2)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = string_1,
				SenderId = long_2,
				Text = this.string_0,
				State = NoteMessageState.Unreaded
			};
			if (!DaoManagerSQL.CreateMessage(long_1, messageModel))
			{
				this.uint_0 = 2147483648U;
				return null;
			}
			return messageModel;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ()
		{
		}

		// Token: 0x04000337 RID: 823
		private long long_0;

		// Token: 0x04000338 RID: 824
		private string string_0;

		// Token: 0x04000339 RID: 825
		private uint uint_0;
	}
}
