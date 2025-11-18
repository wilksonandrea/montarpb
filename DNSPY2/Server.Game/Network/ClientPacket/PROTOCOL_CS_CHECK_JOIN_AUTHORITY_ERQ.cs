using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200018E RID: 398
	public class PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ : GameClientPacket
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x000053F0 File Offset: 0x000035F0
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001FFC8 File Offset: 0x0001E1C8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(this.int_0);
					if (clan.Id == 0)
					{
						this.uint_0 = 2147483648U;
					}
					else if (clan.RankLimit > player.Rank)
					{
						this.uint_0 = 2147487867U;
					}
					this.Client.SendPacket(new PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ()
		{
		}

		// Token: 0x040002E7 RID: 743
		private int int_0;

		// Token: 0x040002E8 RID: 744
		private uint uint_0;
	}
}
