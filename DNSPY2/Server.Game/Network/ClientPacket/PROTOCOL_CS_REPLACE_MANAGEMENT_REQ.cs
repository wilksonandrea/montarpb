using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001A7 RID: 423
	public class PROTOCOL_CS_REPLACE_MANAGEMENT_REQ : GameClientPacket
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x000055BC File Offset: 0x000037BC
		public override void Read()
		{
			this.int_3 = (int)base.ReadC();
			this.int_0 = (int)base.ReadC();
			this.int_2 = (int)base.ReadC();
			this.int_1 = (int)base.ReadC();
			this.joinClanType_0 = (JoinClanType)base.ReadC();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00022254 File Offset: 0x00020454
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ClanModel clan = ClanManager.GetClan(player.ClanId);
					if (clan.Id > 0 && clan.OwnerId == player.PlayerId)
					{
						if (clan.Authority != this.int_3)
						{
							clan.Authority = this.int_3;
						}
						if (clan.RankLimit != this.int_0)
						{
							clan.RankLimit = this.int_0;
						}
						if (clan.MinAgeLimit != this.int_1)
						{
							clan.MinAgeLimit = this.int_1;
						}
						if (clan.MaxAgeLimit != this.int_2)
						{
							clan.MaxAgeLimit = this.int_2;
						}
						if (clan.JoinType != this.joinClanType_0)
						{
							clan.JoinType = this.joinClanType_0;
						}
						DaoManagerSQL.UpdateClanInfo(clan.Id, clan.Authority, clan.RankLimit, clan.MinAgeLimit, clan.MaxAgeLimit, (int)clan.JoinType);
					}
					else
					{
						this.uint_0 = 2147483648U;
					}
					this.Client.SendPacket(new PROTOCOL_CS_REPLACE_MANAGEMENT_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_REPLACE_MANAGEMENT_REQ()
		{
		}

		// Token: 0x04000310 RID: 784
		private int int_0;

		// Token: 0x04000311 RID: 785
		private int int_1;

		// Token: 0x04000312 RID: 786
		private int int_2;

		// Token: 0x04000313 RID: 787
		private int int_3;

		// Token: 0x04000314 RID: 788
		private JoinClanType joinClanType_0;

		// Token: 0x04000315 RID: 789
		private uint uint_0;
	}
}
