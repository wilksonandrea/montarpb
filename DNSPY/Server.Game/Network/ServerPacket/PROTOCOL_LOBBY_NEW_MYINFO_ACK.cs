using System;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000DE RID: 222
	public class PROTOCOL_LOBBY_NEW_MYINFO_ACK : GameServerPacket
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00004410 File Offset: 0x00002610
		public PROTOCOL_LOBBY_NEW_MYINFO_ACK(Account account_1)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
				this.statisticClan_0 = account_1.Statistic.Clan;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00010E94 File Offset: 0x0000F094
		public override void Write()
		{
			base.WriteH(977);
			base.WriteD(0);
			base.WriteQ(this.account_0.PlayerId);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(10101);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(20202);
			base.WriteD(0);
			base.WriteD(30303);
			base.WriteD(0);
			base.WriteD(40404);
			base.WriteD(50505);
			base.WriteD(60606);
			base.WriteD(0);
			base.WriteD(70707);
			base.WriteD(80808);
			base.WriteD(90909);
			base.WriteD(101010);
			base.WriteD(111111);
			base.WriteD(121212);
			base.WriteD(131313);
			base.WriteD(141414);
			base.WriteD(151515);
			base.WriteD(161616);
			base.WriteD(171717);
			base.WriteD(181818);
			base.WriteD(191919);
			base.WriteD(0);
		}

		// Token: 0x04000194 RID: 404
		private readonly Account account_0;

		// Token: 0x04000195 RID: 405
		private readonly ClanModel clanModel_0;

		// Token: 0x04000196 RID: 406
		private readonly StatisticClan statisticClan_0;
	}
}
