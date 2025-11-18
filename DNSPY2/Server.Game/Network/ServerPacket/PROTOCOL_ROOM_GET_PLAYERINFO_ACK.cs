using System;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F5 RID: 245
	public class PROTOCOL_ROOM_GET_PLAYERINFO_ACK : GameServerPacket
	{
		// Token: 0x06000254 RID: 596 RVA: 0x000124D4 File Offset: 0x000106D4
		public PROTOCOL_ROOM_GET_PLAYERINFO_ACK(uint uint_1, Account account_1, int[] int_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.playerInventory_0 = account_1.Inventory;
				this.playerEquipment_0 = account_1.Equipment;
				this.statisticTotal_0 = account_1.Statistic.Basic;
				this.statisticSeason_0 = account_1.Statistic.Season;
				this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
			}
			this.int_0 = int_1;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0001254C File Offset: 0x0001074C
		public override void Write()
		{
			base.WriteH(3597);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteD(this.int_0[1]);
				base.WriteD(this.int_0[0]);
				base.WriteD(this.statisticSeason_0.Matches);
				base.WriteD(this.statisticSeason_0.MatchWins);
				base.WriteD(this.statisticSeason_0.MatchLoses);
				base.WriteD(this.statisticSeason_0.MatchDraws);
				base.WriteD(this.statisticSeason_0.KillsCount);
				base.WriteD(this.statisticSeason_0.HeadshotsCount);
				base.WriteD(this.statisticSeason_0.DeathsCount);
				base.WriteD(this.statisticSeason_0.TotalMatchesCount);
				base.WriteD(this.statisticSeason_0.TotalKillsCount);
				base.WriteD(this.statisticSeason_0.EscapesCount);
				base.WriteD(this.statisticSeason_0.AssistsCount);
				base.WriteD(this.statisticSeason_0.MvpCount);
				base.WriteD(this.statisticTotal_0.Matches);
				base.WriteD(this.statisticTotal_0.MatchWins);
				base.WriteD(this.statisticTotal_0.MatchLoses);
				base.WriteD(this.statisticTotal_0.MatchDraws);
				base.WriteD(this.statisticTotal_0.KillsCount);
				base.WriteD(this.statisticTotal_0.HeadshotsCount);
				base.WriteD(this.statisticTotal_0.DeathsCount);
				base.WriteD(this.statisticTotal_0.TotalMatchesCount);
				base.WriteD(this.statisticTotal_0.TotalKillsCount);
				base.WriteD(this.statisticTotal_0.EscapesCount);
				base.WriteD(this.statisticTotal_0.AssistsCount);
				base.WriteD(this.statisticTotal_0.MvpCount);
				base.WriteC(3);
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.DinoItem));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.SprayId));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.NameCardId));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponPrimary));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSecondary));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponMelee));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponExplosive));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSpecial));
				base.WriteB(this.playerInventory_0.EquipmentData(this.int_0[0]));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHead));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartFace));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartJacket));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartPocket));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartGlove));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartBelt));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHolster));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartSkin));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.BeretItem));
				base.WriteD(590851);
				base.WriteU(this.account_0.Nickname, 66);
				base.WriteD(this.account_0.GetRank());
				base.WriteD(this.account_0.Rank);
				base.WriteD(this.account_0.Gold);
				base.WriteD(this.account_0.Exp);
				base.WriteD(0);
				base.WriteC(0);
				base.WriteQ(0L);
				base.WriteC((byte)this.account_0.AuthLevel());
				base.WriteC(0);
				base.WriteD(this.account_0.Tags);
				base.WriteH(0);
				base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
				base.WriteH((ushort)this.account_0.InventoryPlus);
				base.WriteD(this.account_0.Cash);
				base.WriteD(this.clanModel_0.Id);
				base.WriteD(this.account_0.ClanAccess);
				base.WriteQ(this.account_0.StatusId());
				base.WriteC((byte)this.account_0.CafePC);
				base.WriteC((byte)this.account_0.Country);
				base.WriteU(this.clanModel_0.Name, 34);
				base.WriteC((byte)this.clanModel_0.Rank);
				base.WriteC((byte)this.clanModel_0.GetClanUnit());
				base.WriteD(this.clanModel_0.Logo);
				base.WriteC((byte)this.clanModel_0.NameColor);
				base.WriteC((byte)this.clanModel_0.Effect);
			}
		}

		// Token: 0x040001C2 RID: 450
		private readonly uint uint_0;

		// Token: 0x040001C3 RID: 451
		private readonly Account account_0;

		// Token: 0x040001C4 RID: 452
		private readonly PlayerInventory playerInventory_0;

		// Token: 0x040001C5 RID: 453
		private readonly PlayerEquipment playerEquipment_0;

		// Token: 0x040001C6 RID: 454
		private readonly StatisticTotal statisticTotal_0;

		// Token: 0x040001C7 RID: 455
		private readonly StatisticSeason statisticSeason_0;

		// Token: 0x040001C8 RID: 456
		private readonly ClanModel clanModel_0;

		// Token: 0x040001C9 RID: 457
		private readonly int[] int_0;
	}
}
