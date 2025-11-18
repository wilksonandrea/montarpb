using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000E0 RID: 224
	public class PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00011150 File Offset: 0x0000F350
		public PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(uint uint_1, Account account_1, int int_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.playerInventory_0 = account_1.Inventory;
				this.playerEquipment_0 = account_1.Equipment;
				this.statisticSeason_0 = account_1.Statistic.Season;
				this.statisticWeapon_0 = account_1.Statistic.Weapon;
				this.statisticAcemode_0 = account_1.Statistic.Acemode;
				this.statisticBattlecup_0 = account_1.Statistic.Battlecup;
				this.int_0 = ((int_1 == int.MaxValue) ? this.playerEquipment_0.CharaRedId : int_1);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000111EC File Offset: 0x0000F3EC
		public override void Write()
		{
			base.WriteH(2438);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteQ(this.account_0.PlayerId);
				base.WriteU(this.account_0.Nickname, 66);
				base.WriteB(this.method_0(this.account_0));
				if (this.account_0.GetRank() != 53)
				{
					if (this.account_0.GetRank() != 54)
					{
						base.WriteC((byte)this.account_0.GetRank());
						goto IL_9B;
					}
				}
				base.WriteC(55);
				IL_9B:
				base.WriteD(this.account_0.Exp);
				base.WriteD(this.statisticSeason_0.Matches);
				base.WriteD(this.statisticSeason_0.MatchWins);
				base.WriteD(this.statisticSeason_0.MatchDraws);
				base.WriteD(this.statisticSeason_0.MatchLoses);
				base.WriteD(this.statisticSeason_0.EscapesCount);
				base.WriteD(this.statisticSeason_0.KillsCount);
				base.WriteD(this.statisticSeason_0.DeathsCount);
				base.WriteD(this.statisticSeason_0.HeadshotsCount);
				base.WriteD(this.statisticSeason_0.AssistsCount);
				base.WriteD(this.statisticSeason_0.MvpCount);
				base.WriteB(new byte[45]);
				base.WriteD(1000);
				base.WriteB(new byte[82]);
				base.WriteD(0);
				base.WriteB(new byte[128]);
				base.WriteC(5);
				base.WriteC(1);
				base.WriteC(0);
				base.WriteD(ComDiv.GetPlayerStatus(this.account_0.Status, this.account_0.IsOnline));
				base.WriteB(this.method_1(this.account_0));
				base.WriteC(8);
				base.WriteC(0);
				base.WriteC(7);
				base.WriteC(0);
				base.WriteD(52);
				base.WriteC(1);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteC(0);
				base.WriteC((byte)this.account_0.NickColor);
				base.WriteD(this.account_0.Bonus.FakeRank);
				base.WriteD(this.account_0.Bonus.FakeRank);
				base.WriteU(this.account_0.Bonus.FakeNick, 66);
				base.WriteH((short)this.account_0.Bonus.CrosshairColor);
				base.WriteH((short)this.account_0.Bonus.MuzzleColor);
				base.WriteC((byte)this.account_0.Bonus.NickBorderColor);
				base.WriteC(2);
				base.WriteC(0);
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponPrimary));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSecondary));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponMelee));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponExplosive));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSpecial));
				base.WriteB(this.playerInventory_0.EquipmentData(this.int_0));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHead));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartFace));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartJacket));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartPocket));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartGlove));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartBelt));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHolster));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartSkin));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.BeretItem));
				base.WriteC(0);
				base.WriteD(this.playerEquipment_0.CharaRedId);
				base.WriteD(this.playerEquipment_0.CharaBlueId);
				base.WriteB(new byte[631]);
				base.WriteD(this.statisticWeapon_0.AssaultKills);
				base.WriteD(this.statisticWeapon_0.AssaultDeaths);
				base.WriteD(this.statisticWeapon_0.SmgKills);
				base.WriteD(this.statisticWeapon_0.SmgDeaths);
				base.WriteD(this.statisticWeapon_0.SniperKills);
				base.WriteD(this.statisticWeapon_0.SniperDeaths);
				base.WriteD(this.statisticWeapon_0.MachinegunKills);
				base.WriteD(this.statisticWeapon_0.MachinegunDeaths);
				base.WriteD(this.statisticWeapon_0.ShotgunKills);
				base.WriteD(this.statisticWeapon_0.ShotgunDeaths);
				base.WriteD(this.statisticWeapon_0.ShieldKills);
				base.WriteD(this.statisticWeapon_0.ShieldDeaths);
				base.WriteD(this.playerEquipment_0.CharaRedId);
				base.WriteD(this.playerEquipment_0.CharaBlueId);
				base.WriteC(0);
				base.WriteD(16);
				base.WriteD(this.playerEquipment_0.NameCardId);
				base.WriteC(0);
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.SprayId));
				base.WriteD(this.statisticAcemode_0.Matches);
				base.WriteD(this.statisticAcemode_0.MatchWins);
				base.WriteD(this.statisticAcemode_0.MatchLoses);
				base.WriteD(this.statisticAcemode_0.Kills);
				base.WriteD(this.statisticAcemode_0.Deaths);
				base.WriteD(this.statisticAcemode_0.Headshots);
				base.WriteD(this.statisticAcemode_0.Assists);
				base.WriteD(this.statisticAcemode_0.Escapes);
				base.WriteD(this.statisticAcemode_0.Winstreaks);
				base.WriteD(this.statisticBattlecup_0.Matches);
				base.WriteD(this.account_0.Statistic.GetBCWinRatio());
				base.WriteD(this.statisticBattlecup_0.MatchLoses);
				base.WriteD(this.statisticBattlecup_0.KillsCount);
				base.WriteD(this.statisticBattlecup_0.DeathsCount);
				base.WriteD(this.statisticBattlecup_0.HeadshotsCount);
				base.WriteD(this.statisticBattlecup_0.AssistsCount);
				base.WriteD(this.statisticBattlecup_0.EscapesCount);
				base.WriteD(this.account_0.Statistic.GetBCKDRatio());
				base.WriteD(this.statisticBattlecup_0.MatchWins);
				base.WriteD(this.statisticBattlecup_0.AverageDamage);
				base.WriteD(this.statisticBattlecup_0.PlayTime);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0001193C File Offset: 0x0000FB3C
		private byte[] method_0(Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				ClanModel clan = ClanManager.GetClan(account_1.ClanId);
				syncServerPacket.WriteU(clan.Name, 34);
				syncServerPacket.WriteD(clan.Logo);
				syncServerPacket.WriteC((byte)clan.Effect);
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000119A8 File Offset: 0x0000FBA8
		private byte[] method_1(Account account_1)
		{
			if (((account_1 != null) ? account_1.Status : null) == null)
			{
				byte[] array = new byte[4];
				array[0] = 3;
				return array;
			}
			FriendState friendState;
			int num;
			int num2;
			int num3;
			ComDiv.GetPlayerLocation(account_1.Status, account_1.IsOnline, out friendState, out num, out num2, out num3);
			byte[] array2;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)friendState);
				syncServerPacket.WriteC((byte)num3);
				syncServerPacket.WriteC((byte)num2);
				syncServerPacket.WriteC((byte)num);
				array2 = syncServerPacket.ToArray();
			}
			return array2;
		}

		// Token: 0x0400019B RID: 411
		private readonly uint uint_0;

		// Token: 0x0400019C RID: 412
		private readonly Account account_0;

		// Token: 0x0400019D RID: 413
		private readonly PlayerInventory playerInventory_0;

		// Token: 0x0400019E RID: 414
		private readonly PlayerEquipment playerEquipment_0;

		// Token: 0x0400019F RID: 415
		private readonly StatisticSeason statisticSeason_0;

		// Token: 0x040001A0 RID: 416
		private readonly StatisticWeapon statisticWeapon_0;

		// Token: 0x040001A1 RID: 417
		private readonly StatisticAcemode statisticAcemode_0;

		// Token: 0x040001A2 RID: 418
		private readonly StatisticBattlecup statisticBattlecup_0;

		// Token: 0x040001A3 RID: 419
		private readonly int int_0;
	}
}
