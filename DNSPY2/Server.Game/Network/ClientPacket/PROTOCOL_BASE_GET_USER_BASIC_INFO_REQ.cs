using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000143 RID: 323
	public class PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ : GameClientPacket
	{
		// Token: 0x06000327 RID: 807 RVA: 0x00004FAE File Offset: 0x000031AE
		public override void Read()
		{
			this.long_0 = base.ReadQ();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00019024 File Offset: 0x00017224
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account accountDB = AccountManager.GetAccountDB(this.long_0, 2, 31);
					if (accountDB != null && player.Nickname.Length > 0 && player.PlayerId != this.long_0)
					{
						if (player.Nickname != accountDB.Nickname)
						{
							player.FindPlayer = accountDB.Nickname;
						}
						this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(this.uint_0, accountDB, int.MaxValue));
					}
					else
					{
						this.uint_0 = 2147489795U;
					}
					this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ()
		{
		}

		// Token: 0x04000244 RID: 580
		private uint uint_0;

		// Token: 0x04000245 RID: 581
		private long long_0;
	}
}
