using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000147 RID: 327
	public class PROTOCOL_GMCHAT_SEND_CHAT_REQ : GameClientPacket
	{
		// Token: 0x06000333 RID: 819 RVA: 0x00019318 File Offset: 0x00017518
		public override void Read()
		{
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
			this.string_2 = base.ReadU((int)(base.ReadH() * 2));
			this.string_1 = base.ReadU((int)(base.ReadC() * 2));
			this.long_0 = base.ReadQ();
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00019370 File Offset: 0x00017570
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
			catch (Exception ex)
			{
				CLogger.Print(base.GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_GMCHAT_SEND_CHAT_REQ()
		{
		}

		// Token: 0x0400024F RID: 591
		private long long_0;

		// Token: 0x04000250 RID: 592
		private string string_0;

		// Token: 0x04000251 RID: 593
		private string string_1;

		// Token: 0x04000252 RID: 594
		private string string_2;
	}
}
