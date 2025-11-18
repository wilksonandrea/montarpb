using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001AF RID: 431
	public class PROTOCOL_GMCHAT_START_CHAT_REQ : GameClientPacket
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00005648 File Offset: 0x00003848
		public override void Read()
		{
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
			this.int_0 = base.ReadD();
			this.byte_0 = base.ReadC();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00022AD0 File Offset: 0x00020CD0
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
						this.Client.SendPacket(new PROTOCOL_GMCHAT_START_CHAT_ACK(0U, account));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_START_CHAT_ACK(2147483648U, null));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_GMCHAT_START_CHAT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_GMCHAT_START_CHAT_REQ()
		{
		}

		// Token: 0x0400031D RID: 797
		private string string_0;

		// Token: 0x0400031E RID: 798
		private int int_0;

		// Token: 0x0400031F RID: 799
		private byte byte_0;
	}
}
