using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000123 RID: 291
	public class PROTOCOL_AUTH_FIND_USER_REQ : GameClientPacket
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00004CDA File Offset: 0x00002EDA
		public override void Read()
		{
			this.string_0 = base.ReadU(34);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00014A5C File Offset: 0x00012C5C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account account = AccountManager.GetAccount(this.string_0, 1, 286);
					if (account != null && player.Nickname.Length > 0 && player.Nickname != this.string_0)
					{
						if (player.Nickname != account.Nickname)
						{
							player.FindPlayer = account.Nickname;
						}
					}
					else
					{
						this.uint_0 = 2147489795U;
					}
					this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(this.uint_0, account, int.MaxValue));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_FIND_USER_REQ()
		{
		}

		// Token: 0x0400020B RID: 523
		private string string_0;

		// Token: 0x0400020C RID: 524
		private uint uint_0;
	}
}
