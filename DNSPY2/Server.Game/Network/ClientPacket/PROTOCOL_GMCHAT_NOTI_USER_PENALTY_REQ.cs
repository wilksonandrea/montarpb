using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000146 RID: 326
	public class PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ : GameClientPacket
	{
		// Token: 0x06000330 RID: 816 RVA: 0x00004FCC File Offset: 0x000031CC
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.byte_0 = base.ReadC();
			this.long_0 = base.ReadQ();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00019288 File Offset: 0x00017488
		public override void Run()
		{
			try
			{
				if (this.Client.Player != null)
				{
					Account account = AccountManager.GetAccount(this.long_0, 31);
					if (account != null)
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(0U, account));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_ACK(2147483648U, account));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(base.GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ()
		{
		}

		// Token: 0x0400024C RID: 588
		private long long_0;

		// Token: 0x0400024D RID: 589
		private int int_0;

		// Token: 0x0400024E RID: 590
		private byte byte_0;
	}
}
