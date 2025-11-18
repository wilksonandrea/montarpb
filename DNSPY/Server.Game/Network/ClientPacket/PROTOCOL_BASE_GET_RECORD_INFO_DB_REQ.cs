using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000142 RID: 322
	public class PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ : GameClientPacket
	{
		// Token: 0x06000324 RID: 804 RVA: 0x00004FA0 File Offset: 0x000031A0
		public override void Read()
		{
			this.long_0 = base.ReadQ();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00018FB4 File Offset: 0x000171B4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account account = AccountManager.GetAccount(this.long_0, 31);
					if (account != null && player.PlayerId != account.PlayerId)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(account));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ()
		{
		}

		// Token: 0x04000243 RID: 579
		private long long_0;
	}
}
