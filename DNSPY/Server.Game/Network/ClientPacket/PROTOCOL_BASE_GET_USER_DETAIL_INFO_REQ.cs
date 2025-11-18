using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001BC RID: 444
	public class PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ : GameClientPacket
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x000056EE File Offset: 0x000038EE
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00024618 File Offset: 0x00022818
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					PlayerSession player2 = player.GetChannel().GetPlayer(this.int_0);
					if (player2 != null)
					{
						Account account = AccountManager.GetAccount(player2.PlayerId, true);
						if (account != null)
						{
							if (player.Nickname != account.Nickname)
							{
								player.FindPlayer = account.Nickname;
							}
							this.Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(0U, account, int.MaxValue));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_LOBBY_USER_STATISTICS_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ()
		{
		}

		// Token: 0x04000332 RID: 818
		private int int_0;
	}
}
