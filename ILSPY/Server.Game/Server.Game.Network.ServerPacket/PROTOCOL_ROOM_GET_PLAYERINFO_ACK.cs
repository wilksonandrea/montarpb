using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_PLAYERINFO_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly Account account_0;

	private readonly PlayerInventory playerInventory_0;

	private readonly PlayerEquipment playerEquipment_0;

	private readonly StatisticTotal statisticTotal_0;

	private readonly StatisticSeason statisticSeason_0;

	private readonly ClanModel clanModel_0;

	private readonly int[] int_0;

	public PROTOCOL_ROOM_GET_PLAYERINFO_ACK(uint uint_1, Account account_1, int[] int_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
		if (account_1 != null)
		{
			playerInventory_0 = account_1.Inventory;
			playerEquipment_0 = account_1.Equipment;
			statisticTotal_0 = account_1.Statistic.Basic;
			statisticSeason_0 = account_1.Statistic.Season;
			clanModel_0 = ClanManager.GetClan(account_1.ClanId);
		}
		int_0 = int_1;
	}

	public override void Write()
	{
		WriteH(3597);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteD(int_0[1]);
			WriteD(int_0[0]);
			WriteD(statisticSeason_0.Matches);
			WriteD(statisticSeason_0.MatchWins);
			WriteD(statisticSeason_0.MatchLoses);
			WriteD(statisticSeason_0.MatchDraws);
			WriteD(statisticSeason_0.KillsCount);
			WriteD(statisticSeason_0.HeadshotsCount);
			WriteD(statisticSeason_0.DeathsCount);
			WriteD(statisticSeason_0.TotalMatchesCount);
			WriteD(statisticSeason_0.TotalKillsCount);
			WriteD(statisticSeason_0.EscapesCount);
			WriteD(statisticSeason_0.AssistsCount);
			WriteD(statisticSeason_0.MvpCount);
			WriteD(statisticTotal_0.Matches);
			WriteD(statisticTotal_0.MatchWins);
			WriteD(statisticTotal_0.MatchLoses);
			WriteD(statisticTotal_0.MatchDraws);
			WriteD(statisticTotal_0.KillsCount);
			WriteD(statisticTotal_0.HeadshotsCount);
			WriteD(statisticTotal_0.DeathsCount);
			WriteD(statisticTotal_0.TotalMatchesCount);
			WriteD(statisticTotal_0.TotalKillsCount);
			WriteD(statisticTotal_0.EscapesCount);
			WriteD(statisticTotal_0.AssistsCount);
			WriteD(statisticTotal_0.MvpCount);
			WriteC(3);
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.DinoItem));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.SprayId));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.NameCardId));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponPrimary));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSecondary));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponMelee));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponExplosive));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSpecial));
			WriteB(playerInventory_0.EquipmentData(int_0[0]));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartHead));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartFace));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartJacket));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartPocket));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartGlove));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartBelt));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartHolster));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartSkin));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.BeretItem));
			WriteD(590851);
			WriteU(account_0.Nickname, 66);
			WriteD(account_0.GetRank());
			WriteD(account_0.Rank);
			WriteD(account_0.Gold);
			WriteD(account_0.Exp);
			WriteD(0);
			WriteC(0);
			WriteQ(0L);
			WriteC((byte)account_0.AuthLevel());
			WriteC(0);
			WriteD(account_0.Tags);
			WriteH(0);
			WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
			WriteH((ushort)account_0.InventoryPlus);
			WriteD(account_0.Cash);
			WriteD(clanModel_0.Id);
			WriteD(account_0.ClanAccess);
			WriteQ(account_0.StatusId());
			WriteC((byte)account_0.CafePC);
			WriteC((byte)account_0.Country);
			WriteU(clanModel_0.Name, 34);
			WriteC((byte)clanModel_0.Rank);
			WriteC((byte)clanModel_0.GetClanUnit());
			WriteD(clanModel_0.Logo);
			WriteC((byte)clanModel_0.NameColor);
			WriteC((byte)clanModel_0.Effect);
		}
	}
}
