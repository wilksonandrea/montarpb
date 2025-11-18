using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001AE RID: 430
	public class PROTOCOL_GMCHAT_END_CHAT_REQ : GameClientPacket
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0000563A File Offset: 0x0000383A
		public override void Read()
		{
			this.long_0 = base.ReadQ();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00022A4C File Offset: 0x00020C4C
		public override void Run()
		{
			try
			{
				if (this.Client.Player != null)
				{
					Account account = AccountManager.GetAccount(this.long_0, 31);
					if (account != null)
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_END_CHAT_ACK(0U, account));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_END_CHAT_ACK(2147483648U, null));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_GMCHAT_START_CHAT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_GMCHAT_END_CHAT_REQ()
		{
		}

		// Token: 0x0400031C RID: 796
		private long long_0;
	}
}
