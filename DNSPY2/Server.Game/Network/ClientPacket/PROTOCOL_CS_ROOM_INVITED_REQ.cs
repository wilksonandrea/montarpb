using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001AC RID: 428
	public class PROTOCOL_CS_ROOM_INVITED_REQ : GameClientPacket
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x0000562C File Offset: 0x0000382C
		public override void Read()
		{
			this.long_0 = base.ReadQ();
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0002274C File Offset: 0x0002094C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ClanId != 0)
				{
					Account account = AccountManager.GetAccount(this.long_0, 31);
					if (account != null && account.ClanId == player.ClanId)
					{
						account.SendPacket(new PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(this.Client.PlayerId), false);
					}
					player.SendPacket(new PROTOCOL_CS_ROOM_INVITED_ACK(0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_ROOM_INVITED_REQ()
		{
		}

		// Token: 0x0400031A RID: 794
		private long long_0;
	}
}
