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
	// Token: 0x020001C2 RID: 450
	public class PROTOCOL_MESSENGER_NOTE_SEND_REQ : GameClientPacket
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00024A7C File Offset: 0x00022C7C
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
			this.int_1 = (int)base.ReadC();
			this.string_0 = base.ReadU(this.int_0 * 2);
			this.string_1 = base.ReadU(this.int_1 * 2);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00024ACC File Offset: 0x00022CCC
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.Nickname.Length != 0 && !(player.Nickname == this.string_0))
				{
					Account account = AccountManager.GetAccount(this.string_0, 1, 0);
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
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_MESSENGER_NOTE_SEND_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00024BC0 File Offset: 0x00022DC0
		private MessageModel method_0(string string_2, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = string_2,
				SenderId = long_1,
				Text = this.string_1,
				State = NoteMessageState.Unreaded
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				this.uint_0 = 2147483648U;
				return null;
			}
			return messageModel;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_MESSENGER_NOTE_SEND_REQ()
		{
		}

		// Token: 0x0400033A RID: 826
		private int int_0;

		// Token: 0x0400033B RID: 827
		private int int_1;

		// Token: 0x0400033C RID: 828
		private string string_0;

		// Token: 0x0400033D RID: 829
		private string string_1;

		// Token: 0x0400033E RID: 830
		private uint uint_0;
	}
}
