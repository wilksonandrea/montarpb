using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly Account account_0;

	private readonly PlayerInventory playerInventory_0;

	private readonly PlayerEquipment playerEquipment_0;

	private readonly StatisticSeason statisticSeason_0;

	private readonly StatisticWeapon statisticWeapon_0;

	private readonly StatisticAcemode statisticAcemode_0;

	private readonly StatisticBattlecup statisticBattlecup_0;

	private readonly int int_0;

	public PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(uint uint_1, Account account_1, int int_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
		if (account_1 != null)
		{
			playerInventory_0 = account_1.Inventory;
			playerEquipment_0 = account_1.Equipment;
			statisticSeason_0 = account_1.Statistic.Season;
			statisticWeapon_0 = account_1.Statistic.Weapon;
			statisticAcemode_0 = account_1.Statistic.Acemode;
			statisticBattlecup_0 = account_1.Statistic.Battlecup;
			int_0 = ((int_1 == int.MaxValue) ? playerEquipment_0.CharaRedId : int_1);
		}
	}

	public override void Write()
	{
		WriteH(2438);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteQ(account_0.PlayerId);
			WriteU(account_0.Nickname, 66);
			WriteB(method_0(account_0));
			if (account_0.GetRank() != 53 && account_0.GetRank() != 54)
			{
				WriteC((byte)account_0.GetRank());
			}
			else
			{
				WriteC(55);
			}
			WriteD(account_0.Exp);
			WriteD(statisticSeason_0.Matches);
			WriteD(statisticSeason_0.MatchWins);
			WriteD(statisticSeason_0.MatchDraws);
			WriteD(statisticSeason_0.MatchLoses);
			WriteD(statisticSeason_0.EscapesCount);
			WriteD(statisticSeason_0.KillsCount);
			WriteD(statisticSeason_0.DeathsCount);
			WriteD(statisticSeason_0.HeadshotsCount);
			WriteD(statisticSeason_0.AssistsCount);
			WriteD(statisticSeason_0.MvpCount);
			WriteB(new byte[45]);
			WriteD(1000);
			WriteB(new byte[82]);
			WriteD(0);
			WriteB(new byte[128]);
			WriteC(5);
			WriteC(1);
			WriteC(0);
			WriteD(ComDiv.GetPlayerStatus(account_0.Status, account_0.IsOnline));
			WriteB(method_1(account_0));
			WriteC(8);
			WriteC(0);
			WriteC(7);
			WriteC(0);
			WriteD(52);
			WriteC(1);
			WriteD(0);
			WriteD(0);
			WriteD(0);
			WriteC(0);
			WriteC((byte)account_0.NickColor);
			WriteD(account_0.Bonus.FakeRank);
			WriteD(account_0.Bonus.FakeRank);
			WriteU(account_0.Bonus.FakeNick, 66);
			WriteH((short)account_0.Bonus.CrosshairColor);
			WriteH((short)account_0.Bonus.MuzzleColor);
			WriteC((byte)account_0.Bonus.NickBorderColor);
			WriteC(2);
			WriteC(0);
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponPrimary));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSecondary));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponMelee));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponExplosive));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSpecial));
			WriteB(playerInventory_0.EquipmentData(int_0));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartHead));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartFace));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartJacket));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartPocket));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartGlove));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartBelt));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartHolster));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartSkin));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.BeretItem));
			WriteC(0);
			WriteD(playerEquipment_0.CharaRedId);
			WriteD(playerEquipment_0.CharaBlueId);
			WriteB(new byte[631]);
			WriteD(statisticWeapon_0.AssaultKills);
			WriteD(statisticWeapon_0.AssaultDeaths);
			WriteD(statisticWeapon_0.SmgKills);
			WriteD(statisticWeapon_0.SmgDeaths);
			WriteD(statisticWeapon_0.SniperKills);
			WriteD(statisticWeapon_0.SniperDeaths);
			WriteD(statisticWeapon_0.MachinegunKills);
			WriteD(statisticWeapon_0.MachinegunDeaths);
			WriteD(statisticWeapon_0.ShotgunKills);
			WriteD(statisticWeapon_0.ShotgunDeaths);
			WriteD(statisticWeapon_0.ShieldKills);
			WriteD(statisticWeapon_0.ShieldDeaths);
			WriteD(playerEquipment_0.CharaRedId);
			WriteD(playerEquipment_0.CharaBlueId);
			WriteC(0);
			WriteD(16);
			WriteD(playerEquipment_0.NameCardId);
			WriteC(0);
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.SprayId));
			WriteD(statisticAcemode_0.Matches);
			WriteD(statisticAcemode_0.MatchWins);
			WriteD(statisticAcemode_0.MatchLoses);
			WriteD(statisticAcemode_0.Kills);
			WriteD(statisticAcemode_0.Deaths);
			WriteD(statisticAcemode_0.Headshots);
			WriteD(statisticAcemode_0.Assists);
			WriteD(statisticAcemode_0.Escapes);
			WriteD(statisticAcemode_0.Winstreaks);
			WriteD(statisticBattlecup_0.Matches);
			WriteD(account_0.Statistic.GetBCWinRatio());
			WriteD(statisticBattlecup_0.MatchLoses);
			WriteD(statisticBattlecup_0.KillsCount);
			WriteD(statisticBattlecup_0.DeathsCount);
			WriteD(statisticBattlecup_0.HeadshotsCount);
			WriteD(statisticBattlecup_0.AssistsCount);
			WriteD(statisticBattlecup_0.EscapesCount);
			WriteD(account_0.Statistic.GetBCKDRatio());
			WriteD(statisticBattlecup_0.MatchWins);
			WriteD(statisticBattlecup_0.AverageDamage);
			WriteD(statisticBattlecup_0.PlayTime);
		}
	}

	private byte[] method_0(Account account_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		ClanModel clan = ClanManager.GetClan(account_1.ClanId);
		syncServerPacket.WriteU(clan.Name, 34);
		syncServerPacket.WriteD(clan.Logo);
		syncServerPacket.WriteC((byte)clan.Effect);
		return syncServerPacket.ToArray();
	}

	private byte[] method_1(Account account_1)
	{
		if (account_1?.Status == null)
		{
			return new byte[4] { 3, 0, 0, 0 };
		}
		ComDiv.GetPlayerLocation(account_1.Status, account_1.IsOnline, out var state, out var roomId, out var channelId, out var serverId);
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)state);
		syncServerPacket.WriteC((byte)serverId);
		syncServerPacket.WriteC((byte)channelId);
		syncServerPacket.WriteC((byte)roomId);
		return syncServerPacket.ToArray();
	}
}
