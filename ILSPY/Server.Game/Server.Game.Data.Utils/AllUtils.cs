using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using Npgsql;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.RAW;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Utils;

public static class AllUtils
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class5
	{
		public static readonly Class5 _003C_003E9 = new Class5();

		public static Func<SlotModel, bool> _003C_003E9__125_0;

		public static Func<SlotModel, int> _003C_003E9__125_1;

		public static Func<SlotModel, bool> _003C_003E9__125_2;

		public static Func<SlotModel, int> _003C_003E9__125_3;

		public static Func<SlotModel, bool> _003C_003E9__125_4;

		public static Func<SlotModel, int> _003C_003E9__125_5;

		internal bool method_0(SlotModel slotModel_0)
		{
			return slotModel_0.Score > 0;
		}

		internal int method_1(SlotModel slotModel_0)
		{
			return slotModel_0.Score;
		}

		internal bool method_2(SlotModel slotModel_0)
		{
			return slotModel_0.AllAssists > 0;
		}

		internal int method_3(SlotModel slotModel_0)
		{
			return slotModel_0.AllAssists;
		}

		internal bool method_4(SlotModel slotModel_0)
		{
			return slotModel_0.KillsOnLife > 0;
		}

		internal int method_5(SlotModel slotModel_0)
		{
			return slotModel_0.KillsOnLife;
		}
	}

	[CompilerGenerated]
	private sealed class Class6
	{
		public PlayerCompetitive playerCompetitive_0;

		internal bool method_0(CompetitiveRank competitiveRank_0)
		{
			return playerCompetitive_0.Points <= competitiveRank_0.Points;
		}
	}

	[CompilerGenerated]
	private sealed class Class7
	{
		public SlotModel slotModel_0;

		internal bool method_0(SlotModel slotModel_1)
		{
			if (slotModel_1.Team == slotModel_0.Team)
			{
				return slotModel_1.State != SlotState.CLOSE;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class Class8
	{
		public SlotModel slotModel_0;

		internal bool method_0(SlotModel slotModel_1)
		{
			if (slotModel_1.Team == slotModel_0.Team)
			{
				return slotModel_1.State != SlotState.CLOSE;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class Class9
	{
		public RoomModel roomModel_0;

		public TeamEnum teamEnum_0;

		public bool bool_0;

		public FragInfos fragInfos_0;

		public SlotModel slotModel_0;

		internal void method_0(object object_0)
		{
			smethod_2(roomModel_0, teamEnum_0, bool_0, fragInfos_0, slotModel_0);
			lock (object_0)
			{
				roomModel_0.MatchEndTime.StopJob();
			}
		}
	}

	public static void ValidateAuthLevel(Account Player)
	{
		if (!Enum.IsDefined(typeof(AccessLevel), Player.Access))
		{
			AccessLevel accessLevel = Player.AuthLevel();
			if (ComDiv.UpdateDB("accounts", "access_level", (int)accessLevel, "player_id", Player.PlayerId))
			{
				Player.Access = accessLevel;
			}
		}
	}

	public static void LoadPlayerInventory(Account Player)
	{
		lock (Player.Inventory.Items)
		{
			Player.Inventory.Items.AddRange(DaoManagerSQL.GetPlayerInventoryItems(Player.PlayerId));
		}
	}

	public static void LoadPlayerMissions(Account Player)
	{
		PlayerMissions playerMissionsDB = DaoManagerSQL.GetPlayerMissionsDB(Player.PlayerId, Player.Mission.Mission1, Player.Mission.Mission2, Player.Mission.Mission3, Player.Mission.Mission4);
		if (playerMissionsDB != null)
		{
			Player.Mission = playerMissionsDB;
		}
		else if (!DaoManagerSQL.CreatePlayerMissionsDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Missions!", LoggerType.Warning);
		}
	}

	public static void ValidatePlayerInventoryStatus(Account Player)
	{
		Player.Inventory.LoadBasicItems();
		if (Player.Rank >= 46)
		{
			Player.Inventory.LoadGeneralBeret();
		}
		if (Player.IsGM())
		{
			Player.Inventory.LoadHatForGM();
		}
		if (!string.IsNullOrEmpty(Player.Nickname))
		{
			smethod_21(Player);
		}
		if (smethod_0(Player, out var string_))
		{
			List<ItemsModel> pCCafeRewards = TemplatePackXML.GetPCCafeRewards(Player.CafePC);
			lock (Player.Inventory.Items)
			{
				Player.Inventory.Items.AddRange(pCCafeRewards);
			}
			{
				foreach (ItemsModel item in pCCafeRewards)
				{
					if (ComDiv.GetIdStatics(item.Id, 1) == 6 && Player.Character.GetCharacter(item.Id) == null)
					{
						CreateCharacter(Player, item);
					}
					if (ComDiv.GetIdStatics(item.Id, 1) == 16)
					{
						CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(item.Id);
						if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && !Player.Effects.HasFlag(couponEffect.EffectFlag))
						{
							Player.Effects |= couponEffect.EffectFlag;
							DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
						}
					}
				}
				return;
			}
		}
		foreach (ItemsModel pCCafeReward in TemplatePackXML.GetPCCafeRewards(Player.CafePC))
		{
			if (ComDiv.GetIdStatics(pCCafeReward.Id, 1) == 6 && Player.Character.GetCharacter(pCCafeReward.Id) != null)
			{
				smethod_3(Player, pCCafeReward.Id);
			}
			if (ComDiv.GetIdStatics(pCCafeReward.Id, 1) == 16)
			{
				CouponFlag couponEffect2 = CouponEffectXML.GetCouponEffect(pCCafeReward.Id);
				if (couponEffect2 != null && couponEffect2.EffectFlag > (CouponEffects)0L && Player.Effects.HasFlag(couponEffect2.EffectFlag))
				{
					Player.Effects -= (long)couponEffect2.EffectFlag;
					DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
				}
			}
		}
		if (Player.CafePC > CafeEnum.None && ComDiv.UpdateDB("accounts", "pc_cafe", 0, "player_id", Player.PlayerId))
		{
			Player.CafePC = CafeEnum.None;
			if (!string.IsNullOrEmpty(string_) && ComDiv.DeleteDB("player_vip", "owner_id", Player.PlayerId))
			{
				CLogger.Print($"VIP for UID: {Player.PlayerId} Nick: {Player.Nickname} Deleted Due To {string_}", LoggerType.Info);
			}
			CLogger.Print($"Player PC Cafe was resetted by default into '{Player.CafePC}'; (UID: {Player.PlayerId} Nick: {Player.Nickname})", LoggerType.Info);
		}
	}

	private static bool smethod_0(Account account_0, out string string_0)
	{
		if (account_0.IsGM())
		{
			string_0 = "GM Special Access";
			return true;
		}
		PlayerVip playerVIP = DaoManagerSQL.GetPlayerVIP(account_0.PlayerId);
		if (playerVIP != null)
		{
			if (playerVIP.Expirate < uint.Parse(DateTimeUtil.Now("yyMMddHHmm")))
			{
				string_0 = "The Time Has Expired!";
				return false;
			}
			if (!InternetCafeXML.IsValidAddress(DaoManagerSQL.GetPlayerIP4Address(account_0.PlayerId), playerVIP.Address) && ConfigLoader.ICafeSystem)
			{
				string_0 = "Invalid Configuration!";
				return false;
			}
			string text = $"{account_0.CafePC}";
			if (!playerVIP.Benefit.Equals(text) && ComDiv.UpdateDB("player_vip", "last_benefit", text, "owner_id", account_0.PlayerId))
			{
				playerVIP.Benefit = text;
			}
			string_0 = "Valid Access";
			return true;
		}
		string_0 = "Database Not Found!";
		return false;
	}

	public static void LoadPlayerEquipments(Account Player)
	{
		PlayerEquipment playerEquipmentsDB = DaoManagerSQL.GetPlayerEquipmentsDB(Player.PlayerId);
		if (playerEquipmentsDB != null)
		{
			Player.Equipment = playerEquipmentsDB;
		}
		else if (!DaoManagerSQL.CreatePlayerEquipmentsDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Equipment!", LoggerType.Warning);
		}
	}

	public static void LoadPlayerCharacters(Account Player)
	{
		List<CharacterModel> playerCharactersDB = DaoManagerSQL.GetPlayerCharactersDB(Player.PlayerId);
		if (playerCharactersDB.Count > 0)
		{
			Player.Character.Characters = playerCharactersDB;
		}
	}

	public static void LoadPlayerStatistic(Account Player)
	{
		StatisticTotal playerStatBasicDB = DaoManagerSQL.GetPlayerStatBasicDB(Player.PlayerId);
		if (playerStatBasicDB != null)
		{
			Player.Statistic.Basic = playerStatBasicDB;
		}
		else if (!DaoManagerSQL.CreatePlayerStatBasicDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Basic Statistic!", LoggerType.Warning);
		}
		StatisticSeason playerStatSeasonDB = DaoManagerSQL.GetPlayerStatSeasonDB(Player.PlayerId);
		if (playerStatSeasonDB != null)
		{
			Player.Statistic.Season = playerStatSeasonDB;
		}
		else if (!DaoManagerSQL.CreatePlayerStatSeasonDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Season Statistic!", LoggerType.Warning);
		}
		StatisticClan playerStatClanDB = DaoManagerSQL.GetPlayerStatClanDB(Player.PlayerId);
		if (playerStatClanDB != null)
		{
			Player.Statistic.Clan = playerStatClanDB;
		}
		else if (!DaoManagerSQL.CreatePlayerStatClanDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Clan Statistic!", LoggerType.Warning);
		}
		StatisticDaily playerStatDailiesDB = DaoManagerSQL.GetPlayerStatDailiesDB(Player.PlayerId);
		if (playerStatDailiesDB != null)
		{
			Player.Statistic.Daily = playerStatDailiesDB;
		}
		else if (!DaoManagerSQL.CreatePlayerStatDailiesDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Daily Statistic!", LoggerType.Warning);
		}
		StatisticWeapon playerStatWeaponsDB = DaoManagerSQL.GetPlayerStatWeaponsDB(Player.PlayerId);
		if (playerStatWeaponsDB != null)
		{
			Player.Statistic.Weapon = playerStatWeaponsDB;
		}
		else if (!DaoManagerSQL.CreatePlayerStatWeaponsDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Weapon Statistic!", LoggerType.Warning);
		}
		StatisticAcemode playerStatAcemodesDB = DaoManagerSQL.GetPlayerStatAcemodesDB(Player.PlayerId);
		if (playerStatAcemodesDB != null)
		{
			Player.Statistic.Acemode = playerStatAcemodesDB;
		}
		else if (!DaoManagerSQL.CreatePlayerStatAcemodesDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Acemode Statistic!", LoggerType.Warning);
		}
		StatisticBattlecup playerStatBattlecupDB = DaoManagerSQL.GetPlayerStatBattlecupDB(Player.PlayerId);
		if (playerStatBattlecupDB != null)
		{
			Player.Statistic.Battlecup = playerStatBattlecupDB;
		}
		else if (!DaoManagerSQL.CreatePlayerStatBattlecupsDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Battlecup Statistic!", LoggerType.Warning);
		}
	}

	public static void LoadPlayerTitles(Account Player)
	{
		PlayerTitles playerTitlesDB = DaoManagerSQL.GetPlayerTitlesDB(Player.PlayerId);
		if (playerTitlesDB != null)
		{
			Player.Title = playerTitlesDB;
		}
		else if (!DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Title!", LoggerType.Warning);
		}
	}

	public static void LoadPlayerBattlepass(Account Player)
	{
		PlayerBattlepass playerBattlepassDB = DaoManagerSQL.GetPlayerBattlepassDB(Player.PlayerId);
		if (playerBattlepassDB != null)
		{
			Player.Battlepass = playerBattlepassDB;
		}
		else if (!DaoManagerSQL.CreatePlayerBattlepassDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Battlepass!", LoggerType.Warning);
		}
	}

	public static void LoadPlayerCompetitive(Account Player)
	{
		PlayerCompetitive playerCompetitiveDB = DaoManagerSQL.GetPlayerCompetitiveDB(Player.PlayerId);
		if (playerCompetitiveDB != null)
		{
			Player.Competitive = playerCompetitiveDB;
		}
		else if (!DaoManagerSQL.CreatePlayerCompetitiveDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Competitive!", LoggerType.Warning);
		}
	}

	public static void LoadPlayerBonus(Account Player)
	{
		PlayerBonus playerBonusDB = DaoManagerSQL.GetPlayerBonusDB(Player.PlayerId);
		if (playerBonusDB != null)
		{
			Player.Bonus = playerBonusDB;
		}
		else if (!DaoManagerSQL.CreatePlayerBonusDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Bonus!", LoggerType.Warning);
		}
	}

	public static void LoadPlayerFriend(Account Player, bool LoadFulLDatabase)
	{
		List<FriendModel> playerFriendsDB = DaoManagerSQL.GetPlayerFriendsDB(Player.PlayerId);
		if (playerFriendsDB.Count > 0)
		{
			Player.Friend.Friends = playerFriendsDB;
			if (LoadFulLDatabase)
			{
				AccountManager.GetFriendlyAccounts(Player.Friend);
			}
		}
	}

	public static void LoadPlayerEvent(Account Player)
	{
		PlayerEvent playerEventDB = DaoManagerSQL.GetPlayerEventDB(Player.PlayerId);
		if (playerEventDB != null)
		{
			Player.Event = playerEventDB;
		}
		else if (!DaoManagerSQL.CreatePlayerEventDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Event!", LoggerType.Warning);
		}
	}

	public static void LoadPlayerConfig(Account Player)
	{
		PlayerConfig playerConfigDB = DaoManagerSQL.GetPlayerConfigDB(Player.PlayerId);
		if (playerConfigDB != null)
		{
			Player.Config = playerConfigDB;
		}
		else if (!DaoManagerSQL.CreatePlayerConfigDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Config!", LoggerType.Warning);
		}
	}

	public static void LoadPlayerQuickstarts(Account Player)
	{
		List<QuickstartModel> playerQuickstartsDB = DaoManagerSQL.GetPlayerQuickstartsDB(Player.PlayerId);
		if (playerQuickstartsDB.Count > 0)
		{
			Player.Quickstart.Quickjoins = playerQuickstartsDB;
		}
		else if (!DaoManagerSQL.CreatePlayerQuickstartsDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Quickstarts!", LoggerType.Warning);
		}
	}

	public static void LoadPlayerReport(Account Player)
	{
		PlayerReport playerReportDB = DaoManagerSQL.GetPlayerReportDB(Player.PlayerId);
		if (playerReportDB != null)
		{
			Player.Report = playerReportDB;
		}
		else if (!DaoManagerSQL.CreatePlayerReportDB(Player.PlayerId))
		{
			CLogger.Print("There was an error when creating Player Report!", LoggerType.Warning);
		}
	}

	public static int GetKillScore(KillingMessage msg)
	{
		int num = 0;
		switch (msg)
		{
		case KillingMessage.ChainStopper:
			num += 8;
			break;
		case KillingMessage.Headshot:
			num += 10;
			break;
		case KillingMessage.ChainHeadshot:
			num += 14;
			break;
		case KillingMessage.ChainSlugger:
			num += 6;
			break;
		case KillingMessage.ObjectDefense:
			num += 7;
			break;
		default:
			num += 5;
			break;
		case KillingMessage.PiercingShot:
		case KillingMessage.MassKill:
			num += 6;
			break;
		case KillingMessage.Suicide:
			break;
		}
		return num;
	}

	private static ClassType smethod_1(ClassType classType_0)
	{
		switch (classType_0)
		{
		case ClassType.DualSMG:
			return ClassType.SMG;
		case ClassType.DualHandGun:
			return ClassType.HandGun;
		case ClassType.DualShotgun:
			return ClassType.Shotgun;
		default:
			return classType_0;
		case ClassType.DualKnife:
		case ClassType.Knuckle:
			return ClassType.Knife;
		}
	}

	public static TeamEnum GetWinnerTeam(RoomModel room)
	{
		if (room == null)
		{
			return TeamEnum.TEAM_DRAW;
		}
		TeamEnum result = TeamEnum.TEAM_DRAW;
		if (room.RoomType != RoomCondition.Bomb && room.RoomType != RoomCondition.Destroy && room.RoomType != RoomCondition.Annihilation && room.RoomType != RoomCondition.Defense && room.RoomType != RoomCondition.Destroy)
		{
			if (room.IsDinoMode("DE"))
			{
				if (room.CTDino == room.FRDino)
				{
					result = TeamEnum.TEAM_DRAW;
				}
				else if (room.CTDino > room.FRDino)
				{
					result = TeamEnum.CT_TEAM;
				}
				else if (room.CTDino < room.FRDino)
				{
					result = TeamEnum.FR_TEAM;
				}
			}
			else if (room.CTKills == room.FRKills)
			{
				result = TeamEnum.TEAM_DRAW;
			}
			else if (room.CTKills > room.FRKills)
			{
				result = TeamEnum.CT_TEAM;
			}
			else if (room.CTKills < room.FRKills)
			{
				result = TeamEnum.FR_TEAM;
			}
		}
		else if (room.CTRounds == room.FRRounds)
		{
			result = TeamEnum.TEAM_DRAW;
		}
		else if (room.CTRounds > room.FRRounds)
		{
			result = TeamEnum.CT_TEAM;
		}
		else if (room.CTRounds < room.FRRounds)
		{
			result = TeamEnum.FR_TEAM;
		}
		return result;
	}

	public static TeamEnum GetWinnerTeam(RoomModel room, int RedPlayers, int BluePlayers)
	{
		if (room == null)
		{
			return TeamEnum.TEAM_DRAW;
		}
		TeamEnum result = TeamEnum.TEAM_DRAW;
		if (RedPlayers == 0)
		{
			result = TeamEnum.CT_TEAM;
		}
		else if (BluePlayers == 0)
		{
			result = TeamEnum.FR_TEAM;
		}
		return result;
	}

	public static void UpdateMatchCount(bool WonTheMatch, Account Player, int WinnerTeam, DBQuery TotalQuery, DBQuery SeasonQuery)
	{
		if (WinnerTeam == 2)
		{
			TotalQuery.AddQuery("match_draws", ++Player.Statistic.Basic.MatchDraws);
			SeasonQuery.AddQuery("match_draws", ++Player.Statistic.Season.MatchDraws);
		}
		else if (WonTheMatch)
		{
			TotalQuery.AddQuery("match_wins", ++Player.Statistic.Basic.MatchWins);
			SeasonQuery.AddQuery("match_wins", ++Player.Statistic.Season.MatchWins);
		}
		else
		{
			TotalQuery.AddQuery("match_loses", ++Player.Statistic.Basic.MatchLoses);
			SeasonQuery.AddQuery("match_loses", ++Player.Statistic.Season.MatchLoses);
		}
		TotalQuery.AddQuery("matches", ++Player.Statistic.Basic.Matches);
		TotalQuery.AddQuery("total_matches", ++Player.Statistic.Basic.TotalMatchesCount);
		SeasonQuery.AddQuery("matches", ++Player.Statistic.Season.Matches);
		SeasonQuery.AddQuery("total_matches", ++Player.Statistic.Season.TotalMatchesCount);
	}

	public static void UpdateDailyRecord(bool WonTheMatch, Account Player, int winnerTeam, DBQuery query)
	{
		if (winnerTeam == 2)
		{
			query.AddQuery("match_draws", ++Player.Statistic.Daily.MatchDraws);
		}
		else if (WonTheMatch)
		{
			query.AddQuery("match_wins", ++Player.Statistic.Daily.MatchWins);
		}
		else
		{
			query.AddQuery("match_loses", ++Player.Statistic.Daily.MatchLoses);
		}
		query.AddQuery("matches", ++Player.Statistic.Daily.Matches);
	}

	public static void UpdateMatchCountFFA(RoomModel Room, Account Player, int SlotWin, DBQuery TotalQuery, DBQuery SeasonQuery)
	{
		int[] array = new int[18];
		for (int i = 0; i < array.Length; i++)
		{
			SlotModel slotModel = Room.Slots[i];
			if (slotModel.PlayerId != 0L)
			{
				array[i] = slotModel.AllKills;
			}
			else
			{
				array[i] = 0;
			}
		}
		int num = 0;
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] > array[num])
			{
				num = j;
			}
		}
		if (array[num] == SlotWin)
		{
			TotalQuery.AddQuery("match_wins", ++Player.Statistic.Basic.MatchWins);
			SeasonQuery.AddQuery("match_wins", ++Player.Statistic.Season.MatchWins);
		}
		else
		{
			TotalQuery.AddQuery("match_loses", ++Player.Statistic.Basic.MatchLoses);
			SeasonQuery.AddQuery("match_loses", ++Player.Statistic.Season.MatchLoses);
		}
		TotalQuery.AddQuery("matches", ++Player.Statistic.Basic.Matches);
		TotalQuery.AddQuery("total_matches", ++Player.Statistic.Basic.TotalMatchesCount);
		SeasonQuery.AddQuery("matches", ++Player.Statistic.Season.Matches);
		SeasonQuery.AddQuery("total_matches", ++Player.Statistic.Season.TotalMatchesCount);
	}

	public static void UpdateMatchDailyRecordFFA(RoomModel Room, Account Player, int SlotWin, DBQuery Query)
	{
		int[] array = new int[18];
		for (int i = 0; i < array.Length; i++)
		{
			SlotModel slotModel = Room.Slots[i];
			if (slotModel.PlayerId != 0L)
			{
				array[i] = slotModel.AllKills;
			}
			else
			{
				array[i] = 0;
			}
		}
		int num = 0;
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] > array[num])
			{
				num = j;
			}
		}
		if (array[num] == SlotWin)
		{
			Query.AddQuery("match_wins", ++Player.Statistic.Daily.MatchWins);
		}
		else
		{
			Query.AddQuery("match_loses", ++Player.Statistic.Daily.MatchLoses);
		}
		Query.AddQuery("matches", ++Player.Statistic.Daily.Matches);
	}

	public static void UpdateWeaponRecord(Account Player, SlotModel Slot, DBQuery Query)
	{
		StatisticWeapon weapon = Player.Statistic.Weapon;
		if (Slot.AR[0] > 0)
		{
			Query.AddQuery("assault_rifle_kills", ++weapon.AssaultKills);
		}
		if (Slot.AR[1] > 0)
		{
			Query.AddQuery("assault_rifle_deaths", ++weapon.AssaultDeaths);
		}
		if (Slot.SMG[0] > 0)
		{
			Query.AddQuery("sub_machine_gun_kills", ++weapon.SmgKills);
		}
		if (Slot.SMG[1] > 0)
		{
			Query.AddQuery("sub_machine_gun_deaths", ++weapon.SmgDeaths);
		}
		if (Slot.SR[0] > 0)
		{
			Query.AddQuery("sniper_rifle_kills", ++weapon.SniperKills);
		}
		if (Slot.SR[1] > 0)
		{
			Query.AddQuery("sniper_rifle_deaths", ++weapon.SniperDeaths);
		}
		if (Slot.SG[0] > 0)
		{
			Query.AddQuery("shot_gun_kills", ++weapon.ShotgunKills);
		}
		if (Slot.SG[1] > 0)
		{
			Query.AddQuery("shot_gun_deaths", ++weapon.ShotgunDeaths);
		}
		if (Slot.MG[0] > 0)
		{
			Query.AddQuery("machine_gun_kills", ++weapon.MachinegunKills);
		}
		if (Slot.MG[1] > 0)
		{
			Query.AddQuery("machine_gun_deaths", ++weapon.MachinegunDeaths);
		}
		if (Slot.SHD[0] > 0)
		{
			Query.AddQuery("shield_kills", ++weapon.ShieldKills);
		}
		if (Slot.SHD[1] > 0)
		{
			Query.AddQuery("shield_deaths", ++weapon.ShieldDeaths);
		}
	}

	public static void GenerateMissionAwards(Account Player, DBQuery query)
	{
		try
		{
			PlayerMissions mission = Player.Mission;
			int actualMission = mission.ActualMission;
			int currentMissionId = mission.GetCurrentMissionId();
			int currentCard = mission.GetCurrentCard();
			if (currentMissionId <= 0 || mission.SelectedCard)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			byte[] currentMissionList = mission.GetCurrentMissionList();
			foreach (MissionCardModel card in MissionCardRAW.GetCards(currentMissionId, -1))
			{
				if (currentMissionList[card.ArrayIdx] >= card.MissionLimit)
				{
					num2++;
					if (card.CardBasicId == currentCard)
					{
						num++;
					}
				}
			}
			if (num2 >= 40)
			{
				int masterMedal = Player.MasterMedal;
				int ribbon = Player.Ribbon;
				int medal = Player.Medal;
				int ensign = Player.Ensign;
				MissionCardAwards award = MissionCardRAW.GetAward(currentMissionId, currentCard);
				if (award != null)
				{
					Player.Ribbon += award.Ribbon;
					Player.Medal += award.Medal;
					Player.Ensign += award.Ensign;
					Player.Gold += award.Gold;
					Player.Exp += award.Exp;
				}
				MissionAwards award2 = MissionAwardXML.GetAward(currentMissionId);
				if (award2 != null)
				{
					Player.MasterMedal += award2.MasterMedal;
					Player.Exp += award2.Exp;
					Player.Gold += award2.Gold;
				}
				List<ItemsModel> missionAwards = MissionCardRAW.GetMissionAwards(currentMissionId);
				if (missionAwards.Count > 0)
				{
					Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, missionAwards));
				}
				Player.SendPacket(new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(273u, 4, Player));
				if (Player.Ribbon != ribbon)
				{
					query.AddQuery("ribbon", Player.Ribbon);
				}
				if (Player.Ensign != ensign)
				{
					query.AddQuery("ensign", Player.Ensign);
				}
				if (Player.Medal != medal)
				{
					query.AddQuery("medal", Player.Medal);
				}
				if (Player.MasterMedal != masterMedal)
				{
					query.AddQuery("master_medal", Player.MasterMedal);
				}
				query.AddQuery($"mission_id{actualMission + 1}", 0);
				ComDiv.UpdateDB("player_missions", "owner_id", Player.PlayerId, new string[2]
				{
					$"card{actualMission + 1}",
					$"mission{actualMission + 1}_raw"
				}, 0, new byte[0]);
				switch (actualMission)
				{
				case 0:
					mission.Mission1 = 0;
					mission.Card1 = 0;
					mission.List1 = new byte[40];
					break;
				case 1:
					mission.Mission2 = 0;
					mission.Card2 = 0;
					mission.List2 = new byte[40];
					break;
				case 2:
					mission.Mission3 = 0;
					mission.Card3 = 0;
					mission.List3 = new byte[40];
					break;
				case 3:
					mission.Mission4 = 0;
					mission.Card3 = 0;
					mission.List4 = new byte[40];
					if (Player.Event != null)
					{
						Player.Event.LastQuestFinish = 1;
						ComDiv.UpdateDB("player_events", "last_quest_finish", 1, "owner_id", Player.PlayerId);
					}
					break;
				}
			}
			else
			{
				if (num != 4 || mission.SelectedCard)
				{
					return;
				}
				MissionCardAwards award3 = MissionCardRAW.GetAward(currentMissionId, currentCard);
				if (award3 != null)
				{
					int ribbon2 = Player.Ribbon;
					int medal2 = Player.Medal;
					int ensign2 = Player.Ensign;
					Player.Ribbon += award3.Ribbon;
					Player.Medal += award3.Medal;
					Player.Ensign += award3.Ensign;
					Player.Gold += award3.Gold;
					Player.Exp += award3.Exp;
					if (Player.Ribbon != ribbon2)
					{
						query.AddQuery("ribbon", Player.Ribbon);
					}
					if (Player.Ensign != ensign2)
					{
						query.AddQuery("ensign", Player.Ensign);
					}
					if (Player.Medal != medal2)
					{
						query.AddQuery("medal", Player.Medal);
					}
				}
				mission.SelectedCard = true;
				Player.SendPacket(new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(1u, 1, Player));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("AllUtils.GenerateMissionAwards: " + ex.Message, LoggerType.Error, ex);
		}
	}

	public static void ResetSlotInfo(RoomModel Room, SlotModel Slot, bool UpdateInfo)
	{
		if (Slot.State >= SlotState.LOAD)
		{
			Room.ChangeSlotState(Slot, SlotState.NORMAL, UpdateInfo);
			Slot.ResetSlot();
		}
	}

	public static void EndMatchMission(RoomModel Room, Account Player, SlotModel Slot, TeamEnum WinnerTeam)
	{
		if (WinnerTeam != TeamEnum.TEAM_DRAW)
		{
			CompleteMission(Room, Player, Slot, (Slot.Team == WinnerTeam) ? MissionType.WIN : MissionType.DEFEAT, 0);
		}
	}

	public static void VotekickResult(RoomModel Room)
	{
		VoteKickModel voteKick = Room.VoteKick;
		if (voteKick != null)
		{
			int ınGamePlayers = voteKick.GetInGamePlayers();
			if (voteKick.Accept > voteKick.Denie && voteKick.Enemies > 0 && voteKick.Allies > 0 && voteKick.Votes.Count >= ınGamePlayers / 2)
			{
				Account playerBySlot = Room.GetPlayerBySlot(voteKick.VictimIdx);
				if (playerBySlot != null)
				{
					playerBySlot.SendPacket(new PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK());
					Room.KickedPlayersVote.Add(playerBySlot.PlayerId);
					Room.RemovePlayer(playerBySlot, WarnAllPlayers: true, 2);
				}
			}
			uint uint_ = 0u;
			if (voteKick.Allies == 0)
			{
				uint_ = 2147488001u;
			}
			else if (voteKick.Enemies == 0)
			{
				uint_ = 2147488002u;
			}
			else if (voteKick.Denie < voteKick.Accept || voteKick.Votes.Count < ınGamePlayers / 2)
			{
				uint_ = 2147488000u;
			}
			using PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK packet = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK(uint_, voteKick);
			Room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
		}
		Room.VoteKick = null;
	}

	public static void ResetBattleInfo(RoomModel Room)
	{
		SlotModel[] slots = Room.Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel.PlayerId > 0L && slotModel.State >= SlotState.LOAD)
			{
				slotModel.State = SlotState.NORMAL;
				slotModel.ResetSlot();
			}
			Room.CheckGhostSlot(slotModel);
		}
		Room.PreMatchCD = false;
		Room.BlockedClan = false;
		Room.SwapRound = false;
		Room.Rounds = 1;
		Room.SpawnsCount = 0;
		Room.FRKills = 0;
		Room.FRAssists = 0;
		Room.FRDeaths = 0;
		Room.CTKills = 0;
		Room.CTAssists = 0;
		Room.CTDeaths = 0;
		Room.FRDino = 0;
		Room.CTDino = 0;
		Room.FRRounds = 0;
		Room.CTRounds = 0;
		Room.BattleStart = default(DateTime);
		Room.TimeRoom = 0u;
		Room.Bar1 = 0;
		Room.Bar2 = 0;
		Room.IngameAiLevel = 0;
		Room.State = RoomState.READY;
		Room.UpdateRoomInfo();
		Room.VoteKick = null;
		Room.UdpServer = null;
		if (Room.RoundTime.IsTimer())
		{
			Room.RoundTime.StopJob();
		}
		if (Room.VoteTime.IsTimer())
		{
			Room.VoteTime.StopJob();
		}
		if (Room.BombTime.IsTimer())
		{
			Room.BombTime.StopJob();
		}
		Room.UpdateSlotsInfo();
	}

	public static List<int> GetDinossaurs(RoomModel Room, bool ForceNewTRex, int ForceRexIdx)
	{
		List<int> list = new List<int>();
		if (Room.IsDinoMode())
		{
			TeamEnum team = ((Room.Rounds != 1) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
			int[] teamArray = Room.GetTeamArray(team);
			foreach (int num in teamArray)
			{
				SlotModel slotModel = Room.Slots[num];
				if (slotModel.State == SlotState.BATTLE && !slotModel.SpecGM)
				{
					list.Add(num);
				}
			}
			if ((Room.TRex == -1 || Room.Slots[Room.TRex].State <= SlotState.BATTLE_LOAD || ForceNewTRex) && list.Count > 1 && Room.IsDinoMode("DE"))
			{
				if (ForceRexIdx >= 0 && list.Contains(ForceRexIdx))
				{
					Room.TRex = ForceRexIdx;
				}
				else if (ForceRexIdx == -2)
				{
					Room.TRex = list[new Random().Next(0, list.Count)];
				}
			}
		}
		return list;
	}

	public static void BattleEndPlayersCount(RoomModel Room, bool IsBotMode)
	{
		if (Room == null || IsBotMode || !Room.IsPreparing())
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		SlotModel[] slots = Room.Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel.State == SlotState.BATTLE)
			{
				if (slotModel.Team == TeamEnum.FR_TEAM)
				{
					num2++;
				}
				else
				{
					num++;
				}
			}
			else if (slotModel.State >= SlotState.LOAD)
			{
				if (slotModel.Team == TeamEnum.FR_TEAM)
				{
					num4++;
				}
				else
				{
					num3++;
				}
			}
		}
		if (((num2 == 0 || num == 0) && Room.State == RoomState.BATTLE) || ((num4 == 0 || num3 == 0) && Room.State <= RoomState.PRE_BATTLE))
		{
			EndBattle(Room, IsBotMode);
		}
	}

	public static void EndBattle(RoomModel Room)
	{
		EndBattle(Room, Room.IsBotMode());
	}

	public static void EndBattle(RoomModel Room, bool isBotMode)
	{
		EndBattle(Room, isBotMode, GetWinnerTeam(Room));
	}

	public static void EndBattleNoPoints(RoomModel Room)
	{
		List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
		if (allPlayers.Count > 0)
		{
			GetBattleResult(Room, out var MissionFlag, out var SlotFlag, out var Data);
			bool bool_ = Room.IsBotMode();
			foreach (Account item in allPlayers)
			{
				item.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(item, TeamEnum.TEAM_DRAW, SlotFlag, MissionFlag, bool_, Data));
				UpdateSeasonPass(item);
			}
		}
		ResetBattleInfo(Room);
	}

	public static void EndBattle(RoomModel Room, bool IsBotMode, TeamEnum WinnerTeam)
	{
		List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
		if (allPlayers.Count > 0)
		{
			Room.CalculateResult(WinnerTeam, IsBotMode);
			GetBattleResult(Room, out var MissionFlag, out var SlotFlag, out var Data);
			foreach (Account item in allPlayers)
			{
				item.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(item, WinnerTeam, SlotFlag, MissionFlag, IsBotMode, Data));
				UpdateSeasonPass(item);
			}
		}
		ResetBattleInfo(Room);
	}

	public static void BattleEndRound(RoomModel Room, TeamEnum Winner, bool ForceRestart, FragInfos Kills, SlotModel Killer)
	{
		Room.MatchEndTime.StartJob(1250, delegate(object object_0)
		{
			smethod_2(Room, Winner, ForceRestart, Kills, Killer);
			lock (object_0)
			{
				Room.MatchEndTime.StopJob();
			}
		});
	}

	private static void smethod_2(RoomModel roomModel_0, TeamEnum teamEnum_0, bool bool_0, FragInfos fragInfos_0, SlotModel slotModel_0)
	{
		int roundsByMask = roomModel_0.GetRoundsByMask();
		if (roomModel_0.FRRounds < roundsByMask && roomModel_0.CTRounds < roundsByMask)
		{
			if (!(!roomModel_0.ActiveC4 || bool_0))
			{
				return;
			}
			roomModel_0.StopBomb();
			roomModel_0.ChangeRounds();
			RoundSync.SendUDPRoundSync(roomModel_0);
			using (PROTOCOL_BATTLE_WINNING_CAM_ACK packet = new PROTOCOL_BATTLE_WINNING_CAM_ACK(fragInfos_0))
			{
				using PROTOCOL_BATTLE_MISSION_ROUND_END_ACK packet2 = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, teamEnum_0, RoundEndType.AllDeath);
				roomModel_0.SendPacketToPlayers(packet, packet2, SlotState.BATTLE, 0);
			}
			roomModel_0.RoundRestart();
			return;
		}
		roomModel_0.StopBomb();
		using (PROTOCOL_BATTLE_WINNING_CAM_ACK packet3 = new PROTOCOL_BATTLE_WINNING_CAM_ACK(fragInfos_0))
		{
			using PROTOCOL_BATTLE_MISSION_ROUND_END_ACK packet4 = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, teamEnum_0, RoundEndType.AllDeath);
			roomModel_0.SendPacketToPlayers(packet3, packet4, SlotState.BATTLE, 0);
		}
		EndBattle(roomModel_0, roomModel_0.IsBotMode(), teamEnum_0);
	}

	public static void BattleEndRound(RoomModel Room, TeamEnum Winner, RoundEndType Motive)
	{
		using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK packet = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(Room, Winner, Motive))
		{
			Room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
		}
		Room.StopBomb();
		int roundsByMask = Room.GetRoundsByMask();
		if (Room.FRRounds < roundsByMask && Room.CTRounds < roundsByMask)
		{
			Room.ChangeRounds();
			RoundSync.SendUDPRoundSync(Room);
			Room.RoundRestart();
		}
		else
		{
			EndBattle(Room, Room.IsBotMode(), Winner);
		}
	}

	public static int AddFriend(Account Owner, Account Friend, int state)
	{
		if (Owner != null && Friend != null)
		{
			try
			{
				FriendModel friend = Owner.Friend.GetFriend(Friend.PlayerId);
				if (friend == null)
				{
					NpgsqlConnection val = ConnectionSQL.GetInstance().Conn();
					try
					{
						NpgsqlCommand obj = val.CreateCommand();
						((DbConnection)(object)val).Open();
						((DbCommand)(object)obj).CommandType = CommandType.Text;
						obj.Parameters.AddWithValue("@friend", (object)Friend.PlayerId);
						obj.Parameters.AddWithValue("@owner", (object)Owner.PlayerId);
						obj.Parameters.AddWithValue("@state", (object)state);
						((DbCommand)(object)obj).CommandText = "INSERT INTO player_friends (id, owner_id, state) VALUES (@friend, @owner, @state)";
						((DbCommand)(object)obj).ExecuteNonQuery();
						((Component)(object)obj).Dispose();
						((Component)(object)val).Dispose();
						((DbConnection)(object)val).Close();
					}
					finally
					{
						((IDisposable)val)?.Dispose();
					}
					lock (Owner.Friend.Friends)
					{
						FriendModel friendModel = new FriendModel(Friend.PlayerId, Friend.Rank, Friend.NickColor, Friend.Nickname, Friend.IsOnline, Friend.Status)
						{
							State = state,
							Removed = false
						};
						Owner.Friend.Friends.Add(friendModel);
						SendFriendInfo.Load(Owner, friendModel, 0);
					}
					return 0;
				}
				if (friend.Removed)
				{
					friend.Removed = false;
					DaoManagerSQL.UpdatePlayerFriendBlock(Owner.PlayerId, friend);
					SendFriendInfo.Load(Owner, friend, 1);
				}
				return 1;
			}
			catch (Exception ex)
			{
				CLogger.Print("AllUtils.AddFriend: " + ex.Message, LoggerType.Error, ex);
				return -1;
			}
		}
		return -1;
	}

	public static void SyncPlayerToFriends(Account p, bool all)
	{
		if (p == null || p.Friend.Friends.Count == 0)
		{
			return;
		}
		PlayerInfo ınfo = new PlayerInfo(p.PlayerId, p.Rank, p.NickColor, p.Nickname, p.IsOnline, p.Status);
		for (int i = 0; i < p.Friend.Friends.Count; i++)
		{
			FriendModel friendModel = p.Friend.Friends[i];
			if (!all && (friendModel.State != 0 || friendModel.Removed))
			{
				continue;
			}
			Account account = AccountManager.GetAccount(friendModel.PlayerId, 287);
			if (account != null)
			{
				int index = -1;
				FriendModel friend = account.Friend.GetFriend(p.PlayerId, out index);
				if (friend != null)
				{
					friend.Info = ınfo;
					account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, index), OnlyInServer: false);
				}
			}
		}
	}

	public static void SyncPlayerToClanMembers(Account player)
	{
		if (player != null && player.ClanId > 0)
		{
			using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK packet = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(player))
			{
				ClanManager.SendPacket(packet, player.ClanId, player.PlayerId, UseCache: true, IsOnline: true);
			}
		}
	}

	public static void UpdateSlotEquips(Account Player)
	{
		RoomModel room = Player.Room;
		if (room != null)
		{
			UpdateSlotEquips(Player, room);
		}
	}

	public static void UpdateSlotEquips(Account Player, RoomModel Room)
	{
		if (Room.GetSlot(Player.SlotId, out var Slot))
		{
			Slot.Equipment = Player.Equipment;
		}
		Room.UpdateSlotsInfo();
	}

	public static int GetSlotsFlag(RoomModel Room, bool OnlyNoSpectators, bool MissionSuccess)
	{
		if (Room == null)
		{
			return 0;
		}
		int num = 0;
		SlotModel[] slots = Room.Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel.State >= SlotState.LOAD && ((MissionSuccess && slotModel.MissionsCompleted) || (!MissionSuccess && (!OnlyNoSpectators || !slotModel.Spectator))))
			{
				num += slotModel.Flag;
			}
		}
		return num;
	}

	public static void GetBattleResult(RoomModel Room, out int MissionFlag, out int SlotFlag, out byte[] Data)
	{
		MissionFlag = 0;
		SlotFlag = 0;
		Data = new byte[306];
		if (Room == null)
		{
			return;
		}
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		SlotModel[] slots = Room.Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel.State >= SlotState.LOAD)
			{
				int flag = slotModel.Flag;
				if (slotModel.MissionsCompleted)
				{
					MissionFlag += flag;
				}
				SlotFlag += flag;
			}
		}
		slots = Room.Slots;
		foreach (SlotModel slotModel2 in slots)
		{
			syncServerPacket.WriteH((ushort)slotModel2.Exp);
		}
		slots = Room.Slots;
		foreach (SlotModel slotModel3 in slots)
		{
			syncServerPacket.WriteH((ushort)slotModel3.Gold);
		}
		slots = Room.Slots;
		foreach (SlotModel slotModel4 in slots)
		{
			syncServerPacket.WriteC((byte)slotModel4.BonusFlags);
		}
		slots = Room.Slots;
		foreach (SlotModel slotModel5 in slots)
		{
			syncServerPacket.WriteH((ushort)slotModel5.BonusCafeExp);
			syncServerPacket.WriteH((ushort)slotModel5.BonusItemExp);
			syncServerPacket.WriteH((ushort)slotModel5.BonusEventExp);
		}
		slots = Room.Slots;
		foreach (SlotModel slotModel6 in slots)
		{
			syncServerPacket.WriteH((ushort)slotModel6.BonusCafePoint);
			syncServerPacket.WriteH((ushort)slotModel6.BonusItemPoint);
			syncServerPacket.WriteH((ushort)slotModel6.BonusEventPoint);
		}
		Data = syncServerPacket.ToArray();
	}

	public static bool DiscountPlayerItems(SlotModel Slot, Account Player)
	{
		try
		{
			bool flag = false;
			bool flag2 = false;
			uint num = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
			List<ItemsModel> list = new List<ItemsModel>();
			List<object> list2 = new List<object>();
			int num2 = ((Player.Bonus != null) ? Player.Bonus.Bonuses : 0);
			int num3 = ((Player.Bonus != null) ? Player.Bonus.FreePass : 0);
			lock (Player.Inventory.Items)
			{
				for (int i = 0; i < Player.Inventory.Items.Count; i++)
				{
					ItemsModel ıtemsModel = Player.Inventory.Items[i];
					if (ıtemsModel.Equip == ItemEquipType.Durable && Slot.ItemUsages.Contains(ıtemsModel.Id) && !Slot.SpecGM)
					{
						if (ıtemsModel.Count <= num && (ıtemsModel.Id == 800216 || ıtemsModel.Id == 2700013 || ıtemsModel.Id == 800169))
						{
							DaoManagerSQL.DeletePlayerInventoryItem(ıtemsModel.ObjectId, Player.PlayerId);
						}
						if (--ıtemsModel.Count < 1)
						{
							list2.Add(ıtemsModel.ObjectId);
							Player.Inventory.Items.RemoveAt(i--);
						}
						else
						{
							list.Add(ıtemsModel);
							ComDiv.UpdateDB("player_items", "count", (long)ıtemsModel.Count, "object_id", ıtemsModel.ObjectId, "owner_id", Player.PlayerId);
						}
					}
					else if (ıtemsModel.Count <= num && ıtemsModel.Equip == ItemEquipType.Temporary)
					{
						if (ıtemsModel.Category == ItemCategory.Coupon)
						{
							if (Player.Bonus == null)
							{
								continue;
							}
							if (!Player.Bonus.RemoveBonuses(ıtemsModel.Id))
							{
								if (ıtemsModel.Id == 1600014)
								{
									ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", Player.PlayerId);
									Player.Bonus.CrosshairColor = 4;
									flag = true;
								}
								else if (ıtemsModel.Id == 1600006)
								{
									ComDiv.UpdateDB("accounts", "nick_color", 0, "player_id", Player.PlayerId);
									Player.NickColor = 0;
									if (Player.Room != null)
									{
										using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK packet = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(Player.SlotId, Player.NickColor))
										{
											Player.Room.SendPacketToPlayers(packet);
										}
										Player.Room.UpdateSlotsInfo();
									}
									flag = true;
								}
								else if (ıtemsModel.Id == 1600009)
								{
									ComDiv.UpdateDB("player_bonus", "fake_rank", 55, "owner_id", Player.PlayerId);
									Player.Bonus.FakeRank = 55;
									if (Player.Room != null)
									{
										using (PROTOCOL_ROOM_GET_RANK_ACK packet2 = new PROTOCOL_ROOM_GET_RANK_ACK(Player.SlotId, Player.Rank))
										{
											Player.Room.SendPacketToPlayers(packet2);
										}
										Player.Room.UpdateSlotsInfo();
									}
									flag = true;
								}
								else if (ıtemsModel.Id == 1600010)
								{
									ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", Player.PlayerId);
									ComDiv.UpdateDB("accounts", "nickname", Player.Bonus.FakeNick, "player_id", Player.PlayerId);
									Player.Nickname = Player.Bonus.FakeNick;
									Player.Bonus.FakeNick = "";
									if (Player.Room != null)
									{
										using (PROTOCOL_ROOM_GET_NICKNAME_ACK packet3 = new PROTOCOL_ROOM_GET_NICKNAME_ACK(Player.SlotId, Player.Nickname))
										{
											Player.Room.SendPacketToPlayers(packet3);
										}
										Player.Room.UpdateSlotsInfo();
									}
									flag = true;
								}
								else if (ıtemsModel.Id == 1600187)
								{
									ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", Player.PlayerId);
									Player.Bonus.MuzzleColor = 0;
									if (Player.Room != null)
									{
										using PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK packet4 = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(Player.SlotId, Player.Bonus.MuzzleColor);
										Player.Room.SendPacketToPlayers(packet4);
									}
									flag = true;
								}
								else if (ıtemsModel.Id == 1600205)
								{
									ComDiv.UpdateDB("player_bonus", "nick_border_color", 0, "owner_id", Player.PlayerId);
									Player.Bonus.NickBorderColor = 0;
									if (Player.Room != null)
									{
										using PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK packet5 = new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(Player.SlotId, Player.Bonus.NickBorderColor);
										Player.Room.SendPacketToPlayers(packet5);
									}
									flag = true;
								}
							}
							CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtemsModel.Id);
							if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && Player.Effects.HasFlag(couponEffect.EffectFlag))
							{
								Player.Effects -= (long)couponEffect.EffectFlag;
								flag2 = true;
							}
						}
						list2.Add(ıtemsModel.ObjectId);
						Player.Inventory.Items.RemoveAt(i--);
					}
					else if (ıtemsModel.Count == 0)
					{
						list2.Add(ıtemsModel.ObjectId);
						Player.Inventory.Items.RemoveAt(i--);
					}
				}
			}
			if (list2.Count > 0)
			{
				for (int j = 0; j < list2.Count; j++)
				{
					ItemsModel ıtem = Player.Inventory.GetItem((long)list2[j]);
					if (ıtem != null && ıtem.Category == ItemCategory.Character && ComDiv.GetIdStatics(ıtem.Id, 1) == 6)
					{
						smethod_3(Player, ıtem.Id);
					}
					Player.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(1u, (long)list2[j]));
				}
				ComDiv.DeleteDB("player_items", "object_id", list2.ToArray(), "owner_id", Player.PlayerId);
			}
			list2.Clear();
			list2 = null;
			if (Player.Bonus != null && (Player.Bonus.Bonuses != num2 || Player.Bonus.FreePass != num3))
			{
				DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
			}
			if (Player.Effects < (CouponEffects)0L)
			{
				Player.Effects = (CouponEffects)0L;
			}
			if (list.Count > 0)
			{
				Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, Player, list));
			}
			list.Clear();
			list = null;
			if (flag2)
			{
				ComDiv.UpdateDB("accounts", "coupon_effect", (long)Player.Effects, "player_id", Player.PlayerId);
			}
			if (flag)
			{
				Player.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, Player));
			}
			int num4 = ComDiv.CheckEquipedItems(Player.Equipment, Player.Inventory.Items, BattleRules: false);
			if (num4 > 0)
			{
				DBQuery dBQuery = new DBQuery();
				if ((num4 & 2) == 2)
				{
					ComDiv.UpdateWeapons(Player.Equipment, dBQuery);
				}
				if ((num4 & 1) == 1)
				{
					ComDiv.UpdateChars(Player.Equipment, dBQuery);
				}
				if ((num4 & 3) == 3)
				{
					ComDiv.UpdateItems(Player.Equipment, dBQuery);
				}
				ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
				Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK(Player, Slot));
				Slot.Equipment = Player.Equipment;
				dBQuery = null;
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	private static void smethod_3(Account account_0, int int_0)
	{
		CharacterModel character = account_0.Character.GetCharacter(int_0);
		if (character == null)
		{
			return;
		}
		int num = 0;
		foreach (CharacterModel character2 in account_0.Character.Characters)
		{
			if (character2.Slot != character.Slot)
			{
				character2.Slot = num;
				DaoManagerSQL.UpdatePlayerCharacter(num, character2.ObjectId, account_0.PlayerId);
				num++;
			}
		}
		if (DaoManagerSQL.DeletePlayerCharacter(character.ObjectId, account_0.PlayerId))
		{
			account_0.Character.RemoveCharacter(character);
		}
	}

	public static void TryBalancePlayer(RoomModel Room, Account Player, bool InBattle, ref SlotModel MySlot)
	{
		SlotModel slot = Room.GetSlot(Player.SlotId);
		if (slot == null)
		{
			return;
		}
		TeamEnum team = slot.Team;
		TeamEnum balanceTeamIdx = GetBalanceTeamIdx(Room, InBattle, team);
		if (team == balanceTeamIdx || balanceTeamIdx == TeamEnum.ALL_TEAM)
		{
			return;
		}
		SlotModel slotModel = null;
		int[] array = ((team == TeamEnum.CT_TEAM) ? Room.FR_TEAM : Room.CT_TEAM);
		for (int i = 0; i < array.Length; i++)
		{
			SlotModel slotModel2 = Room.Slots[array[i]];
			if (slotModel2.State != SlotState.CLOSE && slotModel2.PlayerId == 0L)
			{
				slotModel = slotModel2;
				break;
			}
		}
		if (slotModel == null)
		{
			return;
		}
		List<SlotChange> list = new List<SlotChange>();
		lock (Room.Slots)
		{
			Room.SwitchSlots(list, slotModel.Id, slot.Id, ChangeReady: false);
		}
		if (list.Count > 0)
		{
			Player.SlotId = slot.Id;
			MySlot = slot;
			using (PROTOCOL_ROOM_TEAM_BALANCE_ACK packet = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(list, Room.LeaderSlot, 1))
			{
				Room.SendPacketToPlayers(packet);
			}
			Room.UpdateSlotsInfo();
		}
	}

	public static TeamEnum GetBalanceTeamIdx(RoomModel Room, bool InBattle, TeamEnum PlayerTeamIdx)
	{
		int num = ((InBattle && PlayerTeamIdx == TeamEnum.FR_TEAM) ? 1 : 0);
		int num2 = ((InBattle && PlayerTeamIdx == TeamEnum.CT_TEAM) ? 1 : 0);
		SlotModel[] slots = Room.Slots;
		foreach (SlotModel slotModel in slots)
		{
			if ((slotModel.State == SlotState.NORMAL && !InBattle) || (slotModel.State >= SlotState.LOAD && InBattle))
			{
				if (slotModel.Team == TeamEnum.FR_TEAM)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
		}
		if (num + 1 >= num2)
		{
			if (num2 + 1 >= num + 1)
			{
				return TeamEnum.ALL_TEAM;
			}
			return TeamEnum.CT_TEAM;
		}
		return TeamEnum.FR_TEAM;
	}

	public static int GetNewSlotId(int SlotIdx)
	{
		if (SlotIdx % 2 != 0)
		{
			return SlotIdx - 1;
		}
		return SlotIdx + 1;
	}

	public static void GetXmasReward(Account Player)
	{
		EventXmasModel runningEvent = EventXmasXML.GetRunningEvent();
		if (runningEvent == null)
		{
			return;
		}
		PlayerEvent @event = Player.Event;
		uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
		if (@event == null || (@event.LastXmasDate > runningEvent.BeginDate && @event.LastXmasDate <= runningEvent.EndedDate) || !ComDiv.UpdateDB("player_events", "last_xmas_date", (long)num, "owner_id", Player.PlayerId))
		{
			return;
		}
		@event.LastXmasDate = num;
		GoodsItem good = ShopManager.GetGood(runningEvent.GoodId);
		if (good != null)
		{
			if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && Player.Character.GetCharacter(good.Item.Id) == null)
			{
				CreateCharacter(Player, good.Item);
			}
			else
			{
				Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, good.Item));
			}
			Player.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Player, good.Item));
		}
	}

	private static void smethod_4(RoomModel roomModel_0, ref int int_0, ref int int_1, ref int int_2, ref int int_3)
	{
		if (roomModel_0.SwapRound)
		{
			int num = int_0;
			int num2 = int_1;
			int_1 = num;
			int_0 = num2;
			num2 = int_2;
			num = int_3;
			int_3 = num2;
			int_2 = num;
		}
	}

	public static void BattleEndRoundPlayersCount(RoomModel Room)
	{
		if (Room.RoundTime.IsTimer() || (Room.RoomType != RoomCondition.Bomb && Room.RoomType != RoomCondition.Annihilation && Room.RoomType != RoomCondition.Destroy && Room.RoomType != RoomCondition.Ace))
		{
			return;
		}
		Room.GetPlayingPlayers(InBattle: true, out var PlayerFR, out var PlayerCT, out var DeathFR, out var DeathCT);
		smethod_4(Room, ref PlayerFR, ref PlayerCT, ref DeathFR, ref DeathCT);
		if (DeathFR == PlayerFR)
		{
			if (!Room.ActiveC4)
			{
				if (Room.SwapRound)
				{
					Room.FRRounds++;
				}
				else
				{
					Room.CTRounds++;
				}
			}
			BattleEndRound(Room, (!Room.SwapRound) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, ForceRestart: false, null, null);
		}
		else if (DeathCT == PlayerCT)
		{
			if (Room.SwapRound)
			{
				Room.CTRounds++;
			}
			else
			{
				Room.FRRounds++;
			}
			BattleEndRound(Room, Room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, ForceRestart: true, null, null);
		}
	}

	public static void BattleEndKills(RoomModel room)
	{
		smethod_5(room, room.IsBotMode());
	}

	public static void BattleEndKills(RoomModel room, bool isBotMode)
	{
		smethod_5(room, isBotMode);
	}

	private static void smethod_5(RoomModel roomModel_0, bool bool_0)
	{
		int killsByMask = roomModel_0.GetKillsByMask();
		if (roomModel_0.FRKills < killsByMask && roomModel_0.CTKills < killsByMask)
		{
			return;
		}
		List<Account> allPlayers = roomModel_0.GetAllPlayers(SlotState.READY, 1);
		if (allPlayers.Count > 0)
		{
			TeamEnum winnerTeam = GetWinnerTeam(roomModel_0);
			roomModel_0.CalculateResult(winnerTeam, bool_0);
			GetBattleResult(roomModel_0, out var MissionFlag, out var SlotFlag, out var Data);
			using PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOL_BATTLE_MISSION_ROUND_END_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, winnerTeam, RoundEndType.TimeOut);
			byte[] completeBytes = pROTOCOL_BATTLE_MISSION_ROUND_END_ACK.GetCompleteBytes("AllUtils.BaseEndByKills");
			foreach (Account item in allPlayers)
			{
				SlotModel slot = roomModel_0.GetSlot(item.SlotId);
				if (slot != null)
				{
					if (slot.State == SlotState.BATTLE)
					{
						item.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_MISSION_ROUND_END_ACK.GetType().Name);
					}
					item.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(item, winnerTeam, SlotFlag, MissionFlag, bool_0, Data));
					UpdateSeasonPass(item);
				}
			}
		}
		ResetBattleInfo(roomModel_0);
	}

	public static void BattleEndKillsFreeForAll(RoomModel room)
	{
		smethod_6(room);
	}

	private static void smethod_6(RoomModel roomModel_0)
	{
		int killsByMask = roomModel_0.GetKillsByMask();
		int[] array = new int[18];
		for (int i = 0; i < array.Length; i++)
		{
			SlotModel slotModel = roomModel_0.Slots[i];
			if (slotModel.PlayerId != 0L)
			{
				array[i] = slotModel.AllKills;
			}
			else
			{
				array[i] = 0;
			}
		}
		int num = 0;
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] > array[num])
			{
				num = j;
			}
		}
		if (array[num] < killsByMask)
		{
			return;
		}
		List<Account> allPlayers = roomModel_0.GetAllPlayers(SlotState.READY, 1);
		if (allPlayers.Count > 0)
		{
			roomModel_0.CalculateResultFreeForAll(num);
			GetBattleResult(roomModel_0, out var MissionFlag, out var SlotFlag, out var Data);
			using PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOL_BATTLE_MISSION_ROUND_END_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, num, RoundEndType.FreeForAll);
			byte[] completeBytes = pROTOCOL_BATTLE_MISSION_ROUND_END_ACK.GetCompleteBytes("AllUtils.BaseEndByKills");
			foreach (Account item in allPlayers)
			{
				SlotModel slot = roomModel_0.GetSlot(item.SlotId);
				if (slot != null)
				{
					if (slot.State == SlotState.BATTLE)
					{
						item.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_MISSION_ROUND_END_ACK.GetType().Name);
					}
					item.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(item, num, SlotFlag, MissionFlag, bool_1: false, Data));
					UpdateSeasonPass(item);
				}
			}
		}
		ResetBattleInfo(roomModel_0);
	}

	public static bool CheckClanMatchRestrict(RoomModel room)
	{
		if (room.ChannelType == ChannelType.Clan)
		{
			foreach (ClanTeam value in smethod_7(room).Values)
			{
				if (value.PlayersFR >= 1 && value.PlayersCT >= 1)
				{
					room.BlockedClan = true;
					return true;
				}
			}
		}
		return false;
	}

	public static bool Have2ClansToClanMatch(RoomModel room)
	{
		return smethod_7(room).Count == 2;
	}

	public static bool HavePlayersToClanMatch(RoomModel room)
	{
		SortedList<int, ClanTeam> sortedList = smethod_7(room);
		bool flag = false;
		bool flag2 = false;
		foreach (ClanTeam value in sortedList.Values)
		{
			if (value.PlayersFR >= 4)
			{
				flag = true;
			}
			else if (value.PlayersCT >= 4)
			{
				flag2 = true;
			}
		}
		return flag && flag2;
	}

	private static SortedList<int, ClanTeam> smethod_7(RoomModel roomModel_0)
	{
		SortedList<int, ClanTeam> sortedList = new SortedList<int, ClanTeam>();
		for (int i = 0; i < roomModel_0.GetAllPlayers().Count; i++)
		{
			Account account = roomModel_0.GetAllPlayers()[i];
			if (account.ClanId == 0)
			{
				continue;
			}
			if (sortedList.TryGetValue(account.ClanId, out var value) && value != null)
			{
				if (account.SlotId % 2 == 0)
				{
					value.PlayersFR++;
				}
				else
				{
					value.PlayersCT++;
				}
				continue;
			}
			value = new ClanTeam
			{
				ClanId = account.ClanId
			};
			if (account.SlotId % 2 == 0)
			{
				value.PlayersFR++;
			}
			else
			{
				value.PlayersCT++;
			}
			sortedList.Add(account.ClanId, value);
		}
		return sortedList;
	}

	public static void PlayTimeEvent(Account Player, EventPlaytimeModel EvPlaytime, bool IsBotMode, SlotModel Slot, long PlayedTime)
	{
		try
		{
			RoomModel room = Player.Room;
			PlayerEvent @event = Player.Event;
			if (room == null || @event == null)
			{
				return;
			}
			int minutes = EvPlaytime.Minutes1;
			int minutes2 = EvPlaytime.Minutes2;
			int minutes3 = EvPlaytime.Minutes3;
			if (minutes == 0 && minutes2 == 0 && minutes3 == 0)
			{
				CLogger.Print($"Event Playtime Disabled Due To: 0 Value! (Minutes1: {minutes}; Minutes2: {minutes2}; Minutes3: {minutes3}", LoggerType.Warning);
				return;
			}
			long lastPlaytimeValue = @event.LastPlaytimeValue;
			long num = @event.LastPlaytimeFinish;
			long num2 = @event.LastPlaytimeDate;
			if (@event.LastPlaytimeFinish >= 0 && @event.LastPlaytimeFinish <= 2)
			{
				@event.LastPlaytimeValue += PlayedTime;
				int num3 = ((@event.LastPlaytimeFinish == 0) ? EvPlaytime.Minutes1 : ((@event.LastPlaytimeFinish == 1) ? EvPlaytime.Minutes2 : ((@event.LastPlaytimeFinish == 2) ? EvPlaytime.Minutes3 : 0)));
				if (num3 == 0)
				{
					return;
				}
				int num4 = num3 * 60;
				if (@event.LastPlaytimeValue >= num4)
				{
					Random random = new Random();
					List<int> list = ((@event.LastPlaytimeFinish == 0) ? EvPlaytime.Goods1 : ((@event.LastPlaytimeFinish == 1) ? EvPlaytime.Goods2 : ((@event.LastPlaytimeFinish == 2) ? EvPlaytime.Goods3 : new List<int>())));
					if (list.Count > 0)
					{
						GoodsItem good = ShopManager.GetGood(list[random.Next(0, list.Count)]);
						if (good != null)
						{
							if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && Player.Character.GetCharacter(good.Item.Id) == null)
							{
								CreateCharacter(Player, good.Item);
							}
							else
							{
								Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, good.Item));
							}
							Player.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Player, good.Item));
						}
					}
					@event.LastPlaytimeFinish++;
					@event.LastPlaytimeValue = 0L;
				}
				@event.LastPlaytimeDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			}
			if (@event.LastPlaytimeValue != lastPlaytimeValue || @event.LastPlaytimeFinish != num || @event.LastPlaytimeDate != num2)
			{
				EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, @event);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("[AllUtils.PlayTimeEvent] " + ex.Message, LoggerType.Error, ex);
		}
	}

	public static void CompleteMission(RoomModel Room, SlotModel Slot, FragInfos Kills, MissionType AutoComplete, int MoreInfo)
	{
		try
		{
			Account playerBySlot = Room.GetPlayerBySlot(Slot);
			if (playerBySlot != null)
			{
				smethod_8(Room, playerBySlot, Slot, Kills, AutoComplete, MoreInfo);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("[AllUtils.CompleteMission1] " + ex.Message, LoggerType.Error, ex);
		}
	}

	public static void CompleteMission(RoomModel room, SlotModel slot, MissionType autoComplete, int moreInfo)
	{
		try
		{
			Account playerBySlot = room.GetPlayerBySlot(slot);
			if (playerBySlot != null)
			{
				smethod_9(room, playerBySlot, slot, autoComplete, moreInfo);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("[AllUtils.CompleteMission2] " + ex.Message, LoggerType.Error, ex);
		}
	}

	public static void CompleteMission(RoomModel room, Account player, SlotModel slot, FragInfos kills, MissionType autoComplete, int moreInfo)
	{
		smethod_8(room, player, slot, kills, autoComplete, moreInfo);
	}

	public static void CompleteMission(RoomModel room, Account player, SlotModel slot, MissionType autoComplete, int moreInfo)
	{
		smethod_9(room, player, slot, autoComplete, moreInfo);
	}

	private static void smethod_8(RoomModel roomModel_0, Account account_0, SlotModel slotModel_0, FragInfos fragInfos_0, MissionType missionType_0, int int_0)
	{
		try
		{
			PlayerMissions missions = slotModel_0.Missions;
			if (missions == null)
			{
				return;
			}
			int currentMissionId = missions.GetCurrentMissionId();
			int currentCard = missions.GetCurrentCard();
			if (currentMissionId <= 0 || missions.SelectedCard)
			{
				return;
			}
			List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
			if (cards.Count == 0)
			{
				return;
			}
			KillingMessage allKillFlags = fragInfos_0.GetAllKillFlags();
			byte[] currentMissionList = missions.GetCurrentMissionList();
			ClassType ıdStatics = (ClassType)ComDiv.GetIdStatics(fragInfos_0.WeaponId, 2);
			ClassType classType_ = smethod_1(ıdStatics);
			int ıdStatics2 = ComDiv.GetIdStatics(fragInfos_0.WeaponId, 3);
			ClassType classType_2 = ((int_0 > 0) ? ((ClassType)ComDiv.GetIdStatics(fragInfos_0.WeaponId, 2)) : ClassType.Unknown);
			ClassType classType_3 = ((int_0 > 0) ? smethod_1(classType_2) : ClassType.Unknown);
			int int_ = ((int_0 > 0) ? ComDiv.GetIdStatics(int_0, 3) : 0);
			foreach (MissionCardModel item in cards)
			{
				int num = 0;
				if (item.MapId == 0 || item.MapId == (int)roomModel_0.MapId)
				{
					if (fragInfos_0.Frags.Count > 0)
					{
						if (item.MissionType != MissionType.KILL && (item.MissionType != MissionType.CHAINSTOPPER || !allKillFlags.HasFlag(KillingMessage.ChainStopper)) && (item.MissionType != MissionType.CHAINSLUGGER || !allKillFlags.HasFlag(KillingMessage.ChainSlugger)) && (item.MissionType != MissionType.CHAINKILLER || slotModel_0.KillsOnLife < 4) && (item.MissionType != MissionType.TRIPLE_KILL || slotModel_0.KillsOnLife != 3) && (item.MissionType != MissionType.DOUBLE_KILL || slotModel_0.KillsOnLife != 2) && (item.MissionType != MissionType.HEADSHOT || (!allKillFlags.HasFlag(KillingMessage.Headshot) && !allKillFlags.HasFlag(KillingMessage.ChainHeadshot))) && (item.MissionType != MissionType.CHAINHEADSHOT || !allKillFlags.HasFlag(KillingMessage.ChainHeadshot)) && (item.MissionType != MissionType.PIERCING || !allKillFlags.HasFlag(KillingMessage.PiercingShot)) && (item.MissionType != MissionType.MASS_KILL || !allKillFlags.HasFlag(KillingMessage.MassKill)) && (item.MissionType != MissionType.KILL_MAN || !roomModel_0.IsDinoMode() || ((slotModel_0.Team != TeamEnum.CT_TEAM || roomModel_0.Rounds != 2) && (slotModel_0.Team != 0 || roomModel_0.Rounds != 1))))
						{
							if (item.MissionType == MissionType.KILL_WEAPONCLASS || (item.MissionType == MissionType.DOUBLE_KILL_WEAPONCLASS && slotModel_0.KillsOnLife == 2) || (item.MissionType == MissionType.TRIPLE_KILL_WEAPONCLASS && slotModel_0.KillsOnLife == 3))
							{
								num = smethod_11(item, fragInfos_0);
							}
						}
						else
						{
							num = smethod_10(item, ıdStatics, classType_, ıdStatics2, fragInfos_0);
						}
					}
					else if (item.MissionType == MissionType.DEATHBLOW && missionType_0 == MissionType.DEATHBLOW)
					{
						num = smethod_13(item, classType_2, classType_3, int_);
					}
					else if (item.MissionType == missionType_0)
					{
						num = 1;
					}
				}
				if (num == 0)
				{
					continue;
				}
				int arrayIdx = item.ArrayIdx;
				if (currentMissionList[arrayIdx] + 1 <= item.MissionLimit)
				{
					slotModel_0.MissionsCompleted = true;
					currentMissionList[arrayIdx] += (byte)num;
					if (currentMissionList[arrayIdx] > item.MissionLimit)
					{
						currentMissionList[arrayIdx] = (byte)item.MissionLimit;
					}
					int int_2 = currentMissionList[arrayIdx];
					account_0.SendPacket(new PROTOCOL_BASE_QUEST_CHANGE_ACK(int_2, item));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private static void smethod_9(RoomModel roomModel_0, Account account_0, SlotModel slotModel_0, MissionType missionType_0, int int_0)
	{
		try
		{
			PlayerMissions missions = slotModel_0.Missions;
			if (missions == null)
			{
				return;
			}
			int currentMissionId = missions.GetCurrentMissionId();
			int currentCard = missions.GetCurrentCard();
			if (currentMissionId <= 0 || missions.SelectedCard)
			{
				return;
			}
			List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
			if (cards.Count == 0)
			{
				return;
			}
			byte[] currentMissionList = missions.GetCurrentMissionList();
			ClassType classType_ = ((int_0 > 0) ? ((ClassType)ComDiv.GetIdStatics(int_0, 2)) : ClassType.Unknown);
			ClassType classType_2 = ((int_0 > 0) ? smethod_1(classType_) : ClassType.Unknown);
			int int_ = ((int_0 > 0) ? ComDiv.GetIdStatics(int_0, 3) : 0);
			foreach (MissionCardModel item in cards)
			{
				int num = 0;
				if (item.MapId == 0 || item.MapId == (int)roomModel_0.MapId)
				{
					if (item.MissionType == MissionType.DEATHBLOW && missionType_0 == MissionType.DEATHBLOW)
					{
						num = smethod_13(item, classType_, classType_2, int_);
					}
					else if (item.MissionType == missionType_0)
					{
						num = 1;
					}
				}
				if (num == 0)
				{
					continue;
				}
				int arrayIdx = item.ArrayIdx;
				if (currentMissionList[arrayIdx] + 1 <= item.MissionLimit)
				{
					slotModel_0.MissionsCompleted = true;
					currentMissionList[arrayIdx] += (byte)num;
					if (currentMissionList[arrayIdx] > item.MissionLimit)
					{
						currentMissionList[arrayIdx] = (byte)item.MissionLimit;
					}
					int int_2 = currentMissionList[arrayIdx];
					account_0.SendPacket(new PROTOCOL_BASE_QUEST_CHANGE_ACK(int_2, item));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private static int smethod_10(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0, FragInfos fragInfos_0)
	{
		int num = 0;
		if ((missionCardModel_0.WeaponReqId == 0 || missionCardModel_0.WeaponReqId == int_0) && (missionCardModel_0.WeaponReq == ClassType.Unknown || missionCardModel_0.WeaponReq == classType_0 || missionCardModel_0.WeaponReq == classType_1))
		{
			foreach (FragModel frag in fragInfos_0.Frags)
			{
				if ((int)frag.VictimSlot % 2 != (int)fragInfos_0.KillerSlot % 2)
				{
					num++;
				}
			}
			return num;
		}
		return num;
	}

	private static int smethod_11(MissionCardModel missionCardModel_0, FragInfos fragInfos_0)
	{
		int num = 0;
		foreach (FragModel frag in fragInfos_0.Frags)
		{
			if ((int)frag.VictimSlot % 2 != (int)fragInfos_0.KillerSlot % 2 && (missionCardModel_0.WeaponReq == ClassType.Unknown || missionCardModel_0.WeaponReq == (ClassType)frag.WeaponClass || missionCardModel_0.WeaponReq == smethod_1((ClassType)frag.WeaponClass)))
			{
				num++;
			}
		}
		return num;
	}

	private static int smethod_12(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0, int int_1, FragModel fragModel_0)
	{
		if ((missionCardModel_0.WeaponReqId == 0 || missionCardModel_0.WeaponReqId == int_0) && (missionCardModel_0.WeaponReq == ClassType.Unknown || missionCardModel_0.WeaponReq == classType_0 || missionCardModel_0.WeaponReq == classType_1) && (int)fragModel_0.VictimSlot % 2 != int_1 % 2)
		{
			return 1;
		}
		return 0;
	}

	private static int smethod_13(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0)
	{
		if ((missionCardModel_0.WeaponReqId != 0 && missionCardModel_0.WeaponReqId != int_0) || (missionCardModel_0.WeaponReq != 0 && missionCardModel_0.WeaponReq != classType_0 && missionCardModel_0.WeaponReq != classType_1))
		{
			return 0;
		}
		return 1;
	}

	public static void EnableQuestMission(Account Player)
	{
		PlayerEvent @event = Player.Event;
		if (@event != null && @event.LastQuestFinish == 0 && EventQuestXML.GetRunningEvent() != null)
		{
			Player.Mission.Mission4 = 13;
		}
	}

	public static void GetReadyPlayers(RoomModel Room, ref int FRPlayers, ref int CTPlayers, ref int TotalEnemys)
	{
		int num = 0;
		for (int i = 0; i < Room.Slots.Length; i++)
		{
			SlotModel slotModel = Room.Slots[i];
			if (slotModel.State == SlotState.READY)
			{
				if (Room.RoomType == RoomCondition.FreeForAll && i > 0)
				{
					num++;
				}
				else if (slotModel.Team == TeamEnum.FR_TEAM)
				{
					FRPlayers++;
				}
				else
				{
					CTPlayers++;
				}
			}
		}
		if (Room.RoomType == RoomCondition.FreeForAll)
		{
			TotalEnemys = num;
		}
		else if (Room.LeaderSlot % 2 == 0)
		{
			TotalEnemys = CTPlayers;
		}
		else
		{
			TotalEnemys = FRPlayers;
		}
	}

	public static bool CompetitiveMatchCheck(Account Player, RoomModel Room, out uint Error)
	{
		if (Room.Competitive)
		{
			SlotModel[] slots = Room.Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel != null && slotModel.State != SlotState.CLOSE && slotModel.State < SlotState.READY)
				{
					Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, bool_1: true, Translation.GetLabel("CompetitiveFullSlot")));
					Error = 2147487858u;
					return true;
				}
			}
		}
		Error = 0u;
		return false;
	}

	public static bool ClanMatchCheck(RoomModel Room, ChannelType Type, int TotalEnemys, out uint Error)
	{
		if (!ConfigLoader.IsTestMode && Type == ChannelType.Clan)
		{
			if (!Have2ClansToClanMatch(Room))
			{
				Error = 2147487857u;
				return true;
			}
			if (TotalEnemys > 0 && !HavePlayersToClanMatch(Room))
			{
				Error = 2147487858u;
				return true;
			}
			Error = 0u;
			return false;
		}
		Error = 0u;
		return false;
	}

	public static void TryBalanceTeams(RoomModel Room)
	{
		if (Room.BalanceType != TeamBalance.Count || Room.IsBotMode())
		{
			return;
		}
		int[] array;
		switch (GetBalanceTeamIdx(Room, InBattle: false, TeamEnum.ALL_TEAM))
		{
		case TeamEnum.ALL_TEAM:
			return;
		default:
			array = Room.CT_TEAM;
			break;
		case TeamEnum.CT_TEAM:
			array = Room.FR_TEAM;
			break;
		}
		int[] array2 = array;
		SlotModel MySlot = null;
		int num = array2.Length - 1;
		while (num >= 0)
		{
			SlotModel slotModel = Room.Slots[array2[num]];
			if (slotModel.State != SlotState.READY || Room.LeaderSlot == slotModel.Id)
			{
				num--;
				continue;
			}
			MySlot = slotModel;
			break;
		}
		if (MySlot != null && Room.GetPlayerBySlot(MySlot, out var Player))
		{
			TryBalancePlayer(Room, Player, InBattle: false, ref MySlot);
		}
	}

	public static void FreepassEffect(Account Player, SlotModel Slot, RoomModel Room, bool IsBotMode)
	{
		DBQuery dBQuery = new DBQuery();
		if (Player.Bonus.FreePass != 0 && (Player.Bonus.FreePass != 1 || Room.ChannelType != ChannelType.Clan))
		{
			if (Room.State != RoomState.BATTLE)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			if (IsBotMode)
			{
				int num3 = Room.IngameAiLevel * (150 + Slot.AllDeaths);
				if (num3 == 0)
				{
					num3++;
				}
				int num4 = Slot.Score / num3;
				num2 += num4;
				num += num4;
			}
			else
			{
				int num5 = ((Slot.AllKills != 0 || Slot.AllDeaths != 0) ? ((int)Slot.InBattleTime(DateTimeUtil.Now())) : 0);
				if (Room.RoomType != RoomCondition.Bomb && Room.RoomType != RoomCondition.FreeForAll && Room.RoomType != RoomCondition.Destroy)
				{
					num = (int)((double)Slot.Score + (double)num5 / 2.5 + (double)Slot.AllDeaths * 1.8 + (double)(Slot.Objects * 20));
					num2 = (int)((double)Slot.Score + (double)num5 / 3.0 + (double)Slot.AllDeaths * 1.8 + (double)(Slot.Objects * 20));
				}
				else
				{
					num = (int)((double)Slot.Score + (double)num5 / 2.5 + (double)Slot.AllDeaths * 2.2 + (double)(Slot.Objects * 20));
					num2 = (int)((double)Slot.Score + (double)num5 / 3.0 + (double)Slot.AllDeaths * 2.2 + (double)(Slot.Objects * 20));
				}
			}
			Player.Exp += ((ConfigLoader.MaxExpReward < num) ? ConfigLoader.MaxExpReward : num);
			Player.Gold += ((ConfigLoader.MaxGoldReward < num2) ? ConfigLoader.MaxGoldReward : num2);
			if (num2 > 0)
			{
				dBQuery.AddQuery("gold", Player.Gold);
			}
			if (num > 0)
			{
				dBQuery.AddQuery("experience", Player.Exp);
			}
		}
		else
		{
			if (IsBotMode || Slot.State < SlotState.BATTLE_READY)
			{
				return;
			}
			if (Player.Gold > 0)
			{
				Player.Gold -= 200;
				if (Player.Gold < 0)
				{
					Player.Gold = 0;
				}
				dBQuery.AddQuery("gold", Player.Gold);
			}
			ComDiv.UpdateDB("player_stat_basics", "owner_id", Player.PlayerId, "escapes_count", ++Player.Statistic.Basic.EscapesCount);
			ComDiv.UpdateDB("player_stat_seasons", "owner_id", Player.PlayerId, "escapes_count", ++Player.Statistic.Season.EscapesCount);
		}
		ComDiv.UpdateDB("accounts", "player_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
	}

	public static void LeaveHostGiveBattlePVE(RoomModel Room, Account Player)
	{
		List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
		if (allPlayers.Count == 0)
		{
			return;
		}
		int leaderSlot = Room.LeaderSlot;
		Room.SetNewLeader(-1, SlotState.BATTLE_READY, leaderSlot, UpdateInfo: true);
		using PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOL_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0);
		using PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK pROTOCOL_BATTLE_LEAVEP2PSERVER_ACK = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(Room);
		byte[] completeBytes = pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-1");
		byte[] completeBytes2 = pROTOCOL_BATTLE_LEAVEP2PSERVER_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-2");
		foreach (Account item in allPlayers)
		{
			SlotModel slot = Room.GetSlot(item.SlotId);
			if (slot != null)
			{
				if (slot.State >= SlotState.PRESTART)
				{
					item.SendCompletePacket(completeBytes2, pROTOCOL_BATTLE_LEAVEP2PSERVER_ACK.GetType().Name);
				}
				item.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
			}
		}
	}

	public static void LeaveHostEndBattlePVE(RoomModel Room, Account Player)
	{
		List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
		if (allPlayers.Count > 0)
		{
			using PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOL_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0);
			byte[] completeBytes = pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-3");
			TeamEnum winnerTeam = GetWinnerTeam(Room);
			GetBattleResult(Room, out var MissionFlag, out var SlotFlag, out var Data);
			foreach (Account item in allPlayers)
			{
				item.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
				item.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(item, winnerTeam, SlotFlag, MissionFlag, bool_1: true, Data));
				UpdateSeasonPass(item);
			}
		}
		ResetBattleInfo(Room);
	}

	public static void LeaveHostEndBattlePVP(RoomModel Room, Account Player, int TeamFR, int TeamCT, out bool IsFinished)
	{
		IsFinished = true;
		List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
		if (allPlayers.Count > 0)
		{
			TeamEnum winnerTeam = GetWinnerTeam(Room, TeamFR, TeamCT);
			if (Room.State == RoomState.BATTLE)
			{
				Room.CalculateResult(winnerTeam, isBotMode: false);
			}
			using PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOL_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0);
			byte[] completeBytes = pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-4");
			GetBattleResult(Room, out var MissionFlag, out var SlotFlag, out var Data);
			foreach (Account item in allPlayers)
			{
				item.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
				item.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(item, winnerTeam, SlotFlag, MissionFlag, bool_1: false, Data));
				UpdateSeasonPass(item);
			}
		}
		ResetBattleInfo(Room);
	}

	public static void LeaveHostGiveBattlePVP(RoomModel Room, Account Player)
	{
		List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
		if (allPlayers.Count == 0)
		{
			return;
		}
		int leaderSlot = Room.LeaderSlot;
		SlotState state = ((Room.State == RoomState.BATTLE) ? SlotState.BATTLE_READY : SlotState.READY);
		Room.SetNewLeader(-1, state, leaderSlot, UpdateInfo: true);
		using PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK pROTOCOL_BATTLE_LEAVEP2PSERVER_ACK = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(Room);
		using PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOL_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0);
		byte[] completeBytes = pROTOCOL_BATTLE_LEAVEP2PSERVER_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-6");
		byte[] completeBytes2 = pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-7");
		foreach (Account item in allPlayers)
		{
			if (Room.Slots[item.SlotId].State >= SlotState.PRESTART)
			{
				item.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_LEAVEP2PSERVER_ACK.GetType().Name);
			}
			item.SendCompletePacket(completeBytes2, pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
		}
	}

	public static void LeavePlayerEndBattlePVP(RoomModel Room, Account Player, int TeamFR, int TeamCT, out bool IsFinished)
	{
		IsFinished = true;
		TeamEnum winnerTeam = GetWinnerTeam(Room, TeamFR, TeamCT);
		List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
		if (allPlayers.Count > 0)
		{
			if (Room.State == RoomState.BATTLE)
			{
				Room.CalculateResult(winnerTeam, isBotMode: false);
			}
			using PROTOCOL_BATTLE_GIVEUPBATTLE_ACK pROTOCOL_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0);
			byte[] completeBytes = pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-8");
			GetBattleResult(Room, out var MissionFlag, out var SlotFlag, out var Data);
			foreach (Account item in allPlayers)
			{
				item.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
				item.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(item, winnerTeam, SlotFlag, MissionFlag, bool_1: false, Data));
				UpdateSeasonPass(item);
			}
		}
		ResetBattleInfo(Room);
	}

	public static void LeavePlayerQuitBattle(RoomModel Room, Account Player)
	{
		using PROTOCOL_BATTLE_GIVEUPBATTLE_ACK packet = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0);
		Room.SendPacketToPlayers(packet, SlotState.READY, 1);
	}

	private static int smethod_14(int int_0, SortedList<int, int> sortedList_0)
	{
		if (sortedList_0.TryGetValue(int_0, out var value))
		{
			return value;
		}
		return 0;
	}

	private static int smethod_15(int int_0, SortedList<int, int> sortedList_0)
	{
		if (sortedList_0.TryGetValue(int_0, out var value))
		{
			return value;
		}
		return 0;
	}

	private static int smethod_16(Account account_0, int int_0)
	{
		return account_0.Inventory.GetItem(int_0)?.Id ?? 0;
	}

	public static void ValidateAccesoryEquipment(Account Player, int AccessoryId)
	{
		if (Player.Equipment.AccessoryId != AccessoryId)
		{
			Player.Equipment.AccessoryId = smethod_16(Player, AccessoryId);
			ComDiv.UpdateDB("player_equipments", "accesory_id", Player.Equipment.AccessoryId, "owner_id", Player.PlayerId);
		}
	}

	public static void ValidateDisabledCoupon(Account Player, SortedList<int, int> Coupons)
	{
		for (int i = 0; i < Coupons.Keys.Count; i++)
		{
			ItemsModel ıtem = Player.Inventory.GetItem(smethod_14(i, Coupons));
			if (ıtem != null)
			{
				CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtem.Id);
				if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && Player.Effects.HasFlag(couponEffect.EffectFlag))
				{
					Player.Effects -= (long)couponEffect.EffectFlag;
					DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
				}
			}
		}
	}

	public static void ValidateEnabledCoupon(Account Player, SortedList<int, int> Coupons)
	{
		for (int i = 0; i < Coupons.Keys.Count; i++)
		{
			ItemsModel ıtem = Player.Inventory.GetItem(smethod_14(i, Coupons));
			if (ıtem != null)
			{
				bool num = Player.Bonus.AddBonuses(ıtem.Id);
				CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtem.Id);
				if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && !Player.Effects.HasFlag(couponEffect.EffectFlag))
				{
					Player.Effects |= couponEffect.EffectFlag;
					DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
				}
				if (num)
				{
					DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
				}
			}
		}
	}

	private static bool smethod_17(int int_0, CouponEffects couponEffects_0, (int, CouponEffects, bool) valueTuple_0)
	{
		if (int_0 != valueTuple_0.Item1)
		{
			return false;
		}
		if (valueTuple_0.Item3)
		{
			return (couponEffects_0 & valueTuple_0.Item2) > (CouponEffects)0L;
		}
		return couponEffects_0.HasFlag(valueTuple_0.Item2);
	}

	public static bool CheckDuplicateCouponEffects(Account Player, int CouponId)
	{
		bool result = false;
		foreach (var item in new List<(int, CouponEffects, bool)>
		{
			(1600065, CouponEffects.Defense20 | CouponEffects.Defense10 | CouponEffects.Defense5, true),
			(1600079, CouponEffects.Defense90 | CouponEffects.Defense10 | CouponEffects.Defense5, true),
			(1600044, CouponEffects.Defense90 | CouponEffects.Defense20 | CouponEffects.Defense5, true),
			(1600030, CouponEffects.Defense90 | CouponEffects.Defense20 | CouponEffects.Defense10, true),
			(1600078, CouponEffects.JackHollowPoint | CouponEffects.HollowPoint | CouponEffects.FullMetalJack, true),
			(1600032, CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint | CouponEffects.FullMetalJack, true),
			(1600031, CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint | CouponEffects.HollowPoint, true),
			(1600036, CouponEffects.HollowPointPlus | CouponEffects.HollowPoint | CouponEffects.FullMetalJack, true),
			(1600028, CouponEffects.HP5, false),
			(1600040, CouponEffects.HP10, false),
			(1600209, CouponEffects.Camoflage50, false),
			(1600208, CouponEffects.Camoflage99, false)
		})
		{
			if (smethod_17(CouponId, Player.Effects, item))
			{
				return true;
			}
		}
		return result;
	}

	public static void ValidateCharacterEquipment(Account Player, PlayerEquipment Equip, int[] EquipmentList, int CharacterId, int[] CharaSlots)
	{
		DBQuery dBQuery = new DBQuery();
		CharacterModel character = Player.Character.GetCharacter(CharacterId);
		if (character != null)
		{
			int ıdStatics = ComDiv.GetIdStatics(character.Id, 1);
			int ıdStatics2 = ComDiv.GetIdStatics(character.Id, 2);
			int ıdStatics3 = ComDiv.GetIdStatics(character.Id, 5);
			if (ıdStatics == 6 && (ıdStatics2 == 1 || ıdStatics3 == 632) && CharaSlots[0] == character.Slot)
			{
				if (Equip.CharaRedId != character.Id)
				{
					Equip.CharaRedId = character.Id;
					dBQuery.AddQuery("chara_red_side", Equip.CharaRedId);
				}
			}
			else if (ıdStatics == 6 && (ıdStatics2 == 2 || ıdStatics3 == 664) && CharaSlots[1] == character.Slot && Equip.CharaBlueId != character.Id)
			{
				Equip.CharaBlueId = character.Id;
				dBQuery.AddQuery("chara_blue_side", Equip.CharaBlueId);
			}
		}
		for (int i = 0; i < EquipmentList.Length; i++)
		{
			int num = smethod_16(Player, EquipmentList[i]);
			switch (i)
			{
			case 0:
				if (num != 0 && Equip.WeaponPrimary != num)
				{
					Equip.WeaponPrimary = num;
					dBQuery.AddQuery("weapon_primary", Equip.WeaponPrimary);
				}
				break;
			case 1:
				if (num != 0 && Equip.WeaponSecondary != num)
				{
					Equip.WeaponSecondary = num;
					dBQuery.AddQuery("weapon_secondary", Equip.WeaponSecondary);
				}
				break;
			case 2:
				if (num != 0 && Equip.WeaponMelee != num)
				{
					Equip.WeaponMelee = num;
					dBQuery.AddQuery("weapon_melee", Equip.WeaponMelee);
				}
				break;
			case 3:
				if (num != 0 && Equip.WeaponExplosive != num)
				{
					Equip.WeaponExplosive = num;
					dBQuery.AddQuery("weapon_explosive", Equip.WeaponExplosive);
				}
				break;
			case 4:
				if (num != 0 && Equip.WeaponSpecial != num)
				{
					Equip.WeaponSpecial = num;
					dBQuery.AddQuery("weapon_special", Equip.WeaponSpecial);
				}
				break;
			case 5:
				if (Equip.PartHead != num)
				{
					Equip.PartHead = num;
					dBQuery.AddQuery("part_head", Equip.PartHead);
				}
				break;
			case 6:
				if (num != 0 && Equip.PartFace != num)
				{
					Equip.PartFace = num;
					dBQuery.AddQuery("part_face", Equip.PartFace);
				}
				break;
			case 7:
				if (num != 0 && Equip.PartJacket != num)
				{
					Equip.PartJacket = num;
					dBQuery.AddQuery("part_jacket", Equip.PartJacket);
				}
				break;
			case 8:
				if (num != 0 && Equip.PartPocket != num)
				{
					Equip.PartPocket = num;
					dBQuery.AddQuery("part_pocket", Equip.PartPocket);
				}
				break;
			case 9:
				if (num != 0 && Equip.PartGlove != num)
				{
					Equip.PartGlove = num;
					dBQuery.AddQuery("part_glove", Equip.PartGlove);
				}
				break;
			case 10:
				if (num != 0 && Equip.PartBelt != num)
				{
					Equip.PartBelt = num;
					dBQuery.AddQuery("part_belt", Equip.PartBelt);
				}
				break;
			case 11:
				if (num != 0 && Equip.PartHolster != num)
				{
					Equip.PartHolster = num;
					dBQuery.AddQuery("part_holster", Equip.PartHolster);
				}
				break;
			case 12:
				if (num != 0 && Equip.PartSkin != num)
				{
					Equip.PartSkin = num;
					dBQuery.AddQuery("part_skin", Equip.PartSkin);
				}
				break;
			case 13:
				if (Equip.BeretItem != num)
				{
					Equip.BeretItem = num;
					dBQuery.AddQuery("beret_item_part", Equip.BeretItem);
				}
				break;
			}
		}
		ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
	}

	public static void ValidateItemEquipment(Account Player, SortedList<int, int> Items)
	{
		for (int i = 0; i < Items.Keys.Count; i++)
		{
			int num = smethod_15(i, Items);
			switch (i)
			{
			case 0:
				if (num != 0 && Player.Equipment.DinoItem != num)
				{
					Player.Equipment.DinoItem = smethod_16(Player, num);
					ComDiv.UpdateDB("player_equipments", "dino_item_chara", Player.Equipment.DinoItem, "owner_id", Player.PlayerId);
				}
				break;
			case 1:
				if (Player.Equipment.SprayId != num)
				{
					Player.Equipment.SprayId = smethod_16(Player, num);
					ComDiv.UpdateDB("player_equipments", "spray_id", Player.Equipment.SprayId, "owner_id", Player.PlayerId);
				}
				break;
			case 2:
				if (Player.Equipment.NameCardId != num)
				{
					Player.Equipment.NameCardId = smethod_16(Player, num);
					ComDiv.UpdateDB("player_equipments", "namecard_id", Player.Equipment.NameCardId, "owner_id", Player.PlayerId);
				}
				break;
			}
		}
	}

	public static void ValidateCharacterSlot(Account Player, PlayerEquipment Equip, int[] Slots)
	{
		DBQuery dBQuery = new DBQuery();
		CharacterModel characterSlot = Player.Character.GetCharacterSlot(Slots[0]);
		if (characterSlot != null && Equip.CharaRedId != characterSlot.Id)
		{
			Equip.CharaRedId = smethod_16(Player, characterSlot.Id);
			dBQuery.AddQuery("chara_red_side", Equip.CharaRedId);
		}
		CharacterModel characterSlot2 = Player.Character.GetCharacterSlot(Slots[1]);
		if (characterSlot2 != null && Equip.CharaBlueId != characterSlot2.Id)
		{
			Equip.CharaBlueId = smethod_16(Player, characterSlot2.Id);
			dBQuery.AddQuery("chara_blue_side", Equip.CharaBlueId);
		}
		ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
	}

	public static PlayerEquipment ValidateRespawnEQ(SlotModel Slot, int[] ItemIds)
	{
		PlayerEquipment playerEquipment = new PlayerEquipment
		{
			WeaponPrimary = ItemIds[0],
			WeaponSecondary = ItemIds[1],
			WeaponMelee = ItemIds[2],
			WeaponExplosive = ItemIds[3],
			WeaponSpecial = ItemIds[4],
			PartHead = ItemIds[6],
			PartFace = ItemIds[7],
			PartJacket = ItemIds[8],
			PartPocket = ItemIds[9],
			PartGlove = ItemIds[10],
			PartBelt = ItemIds[11],
			PartHolster = ItemIds[12],
			PartSkin = ItemIds[13],
			BeretItem = ItemIds[14],
			AccessoryId = ItemIds[15],
			CharaRedId = Slot.Equipment.CharaRedId,
			CharaBlueId = Slot.Equipment.CharaBlueId,
			DinoItem = Slot.Equipment.DinoItem
		};
		int ıdStatics = ComDiv.GetIdStatics(ItemIds[5], 1);
		int ıdStatics2 = ComDiv.GetIdStatics(ItemIds[5], 2);
		int ıdStatics3 = ComDiv.GetIdStatics(ItemIds[5], 5);
		switch (ıdStatics)
		{
		case 6:
			if (ıdStatics2 != 1 && ıdStatics3 != 632)
			{
				if (ıdStatics2 == 2 || ıdStatics3 == 664)
				{
					playerEquipment.CharaBlueId = ItemIds[5];
				}
			}
			else
			{
				playerEquipment.CharaRedId = ItemIds[5];
			}
			break;
		case 15:
			playerEquipment.DinoItem = ItemIds[5];
			break;
		}
		return playerEquipment;
	}

	public static void InsertItem(int ItemId, SlotModel Slot)
	{
		lock (Slot.ItemUsages)
		{
			if (!Slot.ItemUsages.Contains(ItemId))
			{
				Slot.ItemUsages.Add(ItemId);
			}
		}
	}

	public static void ValidateBanPlayer(Account Player, string Message)
	{
		if (ConfigLoader.AutoBan && DaoManagerSQL.SaveAutoBan(Player.PlayerId, Player.Username, Player.Nickname, "Cheat " + Message + ")", DateTimeUtil.Now("dd -MM-yyyy HH:mm:ss"), Player.PublicIP.ToString(), "Illegal Program"))
		{
			using (PROTOCOL_LOBBY_CHATTING_ACK packet = new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 1, bool_1: false, "Permanently ban player [" + Player.Nickname + "], " + Message))
			{
				GameXender.Client.SendPacketToAllClients(packet);
			}
			Player.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), OnlyInServer: false);
			Player.Close(1000, kicked: true);
		}
		CLogger.Print($"Player: {Player.Nickname}; Id: {Player.PlayerId}; User: {Player.Username}; Reason: {Message}", LoggerType.Hack);
	}

	public static bool ServerCommands(Account Player, string Text)
	{
		try
		{
			bool num = CommandManager.TryParse(Text, Player);
			if (num)
			{
				CLogger.Print($"Player '{Player.Nickname}' (UID: {Player.PlayerId}) Running Command '{Text}'", LoggerType.Command);
			}
			return num;
		}
		catch
		{
			Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, bool_1: false, Translation.GetLabel("CommandsExceptionError")));
			return true;
		}
	}

	public static bool SlotValidMessage(SlotModel Sender, SlotModel Receiver)
	{
		if ((Sender.State != SlotState.NORMAL && Sender.State != SlotState.READY) || (Receiver.State != SlotState.NORMAL && Receiver.State != SlotState.READY))
		{
			if (Sender.State >= SlotState.LOAD && Receiver.State >= SlotState.LOAD)
			{
				if (!Receiver.SpecGM && !Sender.SpecGM && !Sender.DeathState.HasFlag(DeadEnum.UseChat) && (!Sender.DeathState.HasFlag(DeadEnum.Dead) || !Receiver.DeathState.HasFlag(DeadEnum.Dead)) && (!Sender.Spectator || !Receiver.Spectator))
				{
					if (Sender.DeathState.HasFlag(DeadEnum.Alive) && Receiver.DeathState.HasFlag(DeadEnum.Alive))
					{
						if (Sender.Spectator && Receiver.Spectator)
						{
							return true;
						}
						if (!Sender.Spectator)
						{
							return !Receiver.Spectator;
						}
						return false;
					}
					return false;
				}
				return true;
			}
			return false;
		}
		return true;
	}

	public static bool PlayerIsBattle(Account Player)
	{
		RoomModel room = Player.Room;
		if (room != null && room.GetSlot(Player.SlotId, out var Slot))
		{
			return Slot.State >= SlotState.READY;
		}
		return false;
	}

	public static void RoomPingSync(RoomModel Room)
	{
		if (!(ComDiv.GetDuration(Room.LastPingSync) < (double)ConfigLoader.PingUpdateTimeSeconds))
		{
			byte[] array = new byte[18];
			for (int i = 0; i < 18; i++)
			{
				array[i] = (byte)Room.Slots[i].Ping;
			}
			using (PROTOCOL_BATTLE_SENDPING_ACK packet = new PROTOCOL_BATTLE_SENDPING_ACK(array))
			{
				Room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
			}
			Room.LastPingSync = DateTimeUtil.Now();
		}
	}

	public static List<ItemsModel> RepairableItems(Account Player, List<long> ObjectIds, out int Gold, out int Cash, out uint Error)
	{
		Gold = 0;
		Cash = 0;
		Error = 0u;
		List<ItemsModel> list = new List<ItemsModel>();
		if (ObjectIds.Count > 0)
		{
			foreach (long ObjectId in ObjectIds)
			{
				ItemsModel ıtem = Player.Inventory.GetItem(ObjectId);
				if (ıtem != null)
				{
					uint[] array = smethod_18(Player, ıtem);
					Gold += (int)array[0];
					Cash += (int)array[1];
					Error = array[2];
					list.Add(ıtem);
				}
				else
				{
					Error = 2147483920u;
				}
			}
			return list;
		}
		return list;
	}

	private static uint[] smethod_18(Account account_0, ItemsModel itemsModel_0)
	{
		uint[] array = new uint[3];
		ItemsRepair repairItem = ShopManager.GetRepairItem(itemsModel_0.Id);
		if (repairItem != null)
		{
			uint percent = repairItem.Quantity - itemsModel_0.Count;
			if (repairItem.Point > repairItem.Cash)
			{
				uint num = (uint)ComDiv.Percentage(repairItem.Point, (int)percent);
				if (account_0.Gold < num)
				{
					array[2] = 2147483920u;
					return array;
				}
				array[0] = num;
			}
			else
			{
				if (repairItem.Cash <= repairItem.Point)
				{
					array[2] = 2147483920u;
					return array;
				}
				uint num2 = (uint)ComDiv.Percentage(repairItem.Cash, (int)percent);
				if (account_0.Cash < num2)
				{
					array[2] = 2147483920u;
					return array;
				}
				array[1] = num2;
			}
			itemsModel_0.Count = repairItem.Quantity;
			ComDiv.UpdateDB("player_items", "count", (long)itemsModel_0.Count, "owner_id", account_0.PlayerId, "id", itemsModel_0.Id);
			array[2] = 1u;
			return array;
		}
		array[2] = 2147483920u;
		return array;
	}

	public static bool ChannelRequirementCheck(Account Player, ChannelModel Channel)
	{
		if (Player.IsGM())
		{
			return false;
		}
		if (Channel.Type == ChannelType.Clan && Player.ClanId == 0)
		{
			return true;
		}
		if (Channel.Type == ChannelType.Novice && Player.Statistic.GetKDRatio() > 40 && Player.Statistic.GetSeasonKDRatio() > 40)
		{
			return true;
		}
		if (Channel.Type == ChannelType.Training && Player.Rank >= 4)
		{
			return true;
		}
		if (Channel.Type == ChannelType.Special && Player.Rank <= 25)
		{
			return true;
		}
		if (Channel.Type == ChannelType.Blocked)
		{
			return true;
		}
		return false;
	}

	public static bool ChangeCostume(SlotModel Slot, TeamEnum CostumeTeam)
	{
		if (Slot.CostumeTeam != CostumeTeam)
		{
			Slot.CostumeTeam = CostumeTeam;
		}
		return Slot.CostumeTeam == CostumeTeam;
	}

	public static void ClassicModeCheck(RoomModel Room, PlayerEquipment Equip)
	{
		if (!ConfigLoader.TournamentRule)
		{
			return;
		}
		TRuleModel tRuleModel = GameRuleXML.CheckTRuleByRoomName(Room.Name);
		if (tRuleModel == null || tRuleModel.BanIndexes.Count <= 0)
		{
			return;
		}
		foreach (int banIndex in tRuleModel.BanIndexes)
		{
			if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponPrimary))
			{
				Equip.WeaponPrimary = 103004;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponSecondary))
			{
				Equip.WeaponSecondary = 202003;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponMelee))
			{
				Equip.WeaponMelee = 301001;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponExplosive))
			{
				Equip.WeaponExplosive = 407001;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.WeaponSpecial))
			{
				Equip.WeaponSpecial = 508001;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.PartHead))
			{
				Equip.PartHead = 1000700000;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.PartFace))
			{
				Equip.PartFace = 1000800000;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.PartJacket))
			{
				Equip.PartJacket = 1000900000;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.PartPocket))
			{
				Equip.PartPocket = 1001000000;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.PartGlove))
			{
				Equip.PartGlove = 1001100000;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.PartBelt))
			{
				Equip.PartBelt = 1001200000;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.PartHolster))
			{
				Equip.PartHolster = 1001300000;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.PartSkin))
			{
				Equip.PartSkin = 1001400000;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.BeretItem))
			{
				Equip.BeretItem = 0;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.DinoItem))
			{
				Equip.DinoItem = 1500511;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.AccessoryId))
			{
				Equip.AccessoryId = 0;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.SprayId))
			{
				Equip.SprayId = 0;
			}
			else if (GameRuleXML.IsBlocked(banIndex, Equip.NameCardId))
			{
				Equip.NameCardId = 0;
			}
		}
	}

	public static bool ClassicModeCheck(Account Player, RoomModel Room)
	{
		TRuleModel tRuleModel = GameRuleXML.CheckTRuleByRoomName(Room.Name);
		if (tRuleModel == null)
		{
			return false;
		}
		PlayerEquipment equipment = Player.Equipment;
		if (equipment == null)
		{
			CLogger.Print("Player '" + Player.Nickname + "' has invalid equipment (Error) on " + (ConfigLoader.TournamentRule ? "Enabled" : "Disabled") + " Tournament Rules!", LoggerType.Warning);
			return false;
		}
		List<string> List = new List<string>();
		if (tRuleModel.BanIndexes.Count > 0)
		{
			foreach (int banIndex in tRuleModel.BanIndexes)
			{
				if (!GameRuleXML.IsBlocked(banIndex, equipment.WeaponPrimary, ref List, Translation.GetLabel("Primary")) && !GameRuleXML.IsBlocked(banIndex, equipment.WeaponSecondary, ref List, Translation.GetLabel("Secondary")) && !GameRuleXML.IsBlocked(banIndex, equipment.WeaponMelee, ref List, Translation.GetLabel("Melee")) && !GameRuleXML.IsBlocked(banIndex, equipment.WeaponExplosive, ref List, Translation.GetLabel("Explosive")) && !GameRuleXML.IsBlocked(banIndex, equipment.WeaponSpecial, ref List, Translation.GetLabel("Special")) && !GameRuleXML.IsBlocked(banIndex, equipment.CharaRedId, ref List, Translation.GetLabel("Character")) && !GameRuleXML.IsBlocked(banIndex, equipment.CharaBlueId, ref List, Translation.GetLabel("Character")) && !GameRuleXML.IsBlocked(banIndex, equipment.PartHead, ref List, Translation.GetLabel("PartHead")) && !GameRuleXML.IsBlocked(banIndex, equipment.PartFace, ref List, Translation.GetLabel("PartFace")) && !GameRuleXML.IsBlocked(banIndex, equipment.BeretItem, ref List, Translation.GetLabel("BeretItem")))
				{
					GameRuleXML.IsBlocked(banIndex, equipment.AccessoryId, ref List, Translation.GetLabel("Accessory"));
				}
			}
		}
		if (List.Count > 0)
		{
			Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("ClassicModeWarn", string.Join(", ", List.ToArray()))));
			return true;
		}
		return false;
	}

	public static bool Check4vs4(RoomModel Room, bool IsLeader, ref int PlayerFR, ref int PlayerCT, ref int TotalEnemies)
	{
		if (!IsLeader)
		{
			if (PlayerFR + PlayerCT >= 8)
			{
				return true;
			}
			return false;
		}
		int num = PlayerFR + PlayerCT + 1;
		if (num > 8)
		{
			int num2 = num - 8;
			if (num2 > 0)
			{
				for (int num3 = 15; num3 >= 0; num3--)
				{
					if (num3 != Room.LeaderSlot)
					{
						SlotModel slot = Room.GetSlot(num3);
						if (slot != null && slot.State == SlotState.READY)
						{
							Room.ChangeSlotState(num3, SlotState.NORMAL, SendInfo: false);
							if (num3 % 2 == 0)
							{
								PlayerFR--;
							}
							else
							{
								PlayerCT--;
							}
							if (--num2 == 0)
							{
								break;
							}
						}
					}
				}
				Room.UpdateSlotsInfo();
				if (Room.LeaderSlot % 2 == 0)
				{
					TotalEnemies = PlayerCT;
				}
				else
				{
					TotalEnemies = PlayerFR;
				}
				return true;
			}
		}
		return false;
	}

	public static void UpdateSeasonPass(Account Player)
	{
		if (SeasonChallengeXML.GetActiveSeasonPass() != null && Player.UpdateSeasonpass)
		{
			Player.UpdateSeasonpass = false;
			Player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE());
			Player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(Player));
		}
	}

	public static void CalculateBattlePass(Account Player, SlotModel Slot, BattlePassModel CurrentSC)
	{
		PlayerBattlepass battlepass = Player.Battlepass;
		if (CurrentSC == null || battlepass == null)
		{
			return;
		}
		if (battlepass.Id == CurrentSC.Id)
		{
			if (battlepass.Level >= CurrentSC.Cards.Count)
			{
				Player.UpdateSeasonpass = true;
			}
			else
			{
				Slot.SeasonPoint += ComDiv.Percentage(Slot.Exp, 35);
				int num = Slot.SeasonPoint + ComDiv.Percentage(Slot.SeasonPoint, Slot.BonusBattlePass);
				battlepass.TotalPoints += num;
				battlepass.DailyPoints += num;
				uint num2 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				if (ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, new string[3] { "total_points", "daily_points", "last_record" }, battlepass.TotalPoints, battlepass.DailyPoints, (long)num2))
				{
					battlepass.LastRecord = num2;
				}
				Player.UpdateSeasonpass = true;
			}
		}
		smethod_19(Player, battlepass, CurrentSC);
	}

	private static void smethod_19(Account account_0, PlayerBattlepass playerBattlepass_0, BattlePassModel battlePassModel_0)
	{
		PassBoxModel passBoxModel = battlePassModel_0.Cards[playerBattlepass_0.Level];
		if (battlePassModel_0.SeasonIsEnabled() && passBoxModel != null && playerBattlepass_0.TotalPoints >= passBoxModel.RequiredPoints)
		{
			int num = playerBattlepass_0.Level + 1;
			if (ComDiv.UpdateDB("player_battlepass", "level", num, "owner_id", account_0.PlayerId))
			{
				playerBattlepass_0.Level = num;
			}
			int[] array = new int[3]
			{
				passBoxModel.Normal?.GoodId ?? 0,
				0,
				0
			};
			if (playerBattlepass_0.IsPremium)
			{
				array[1] = passBoxModel.PremiumA?.GoodId ?? 0;
				array[2] = passBoxModel.PremiumB?.GoodId ?? 0;
			}
			smethod_22(account_0, array);
		}
	}

	public static void ProcessBattlepassPremiumBuy(Account Player)
	{
		PlayerBattlepass battlepass = Player.Battlepass;
		if (battlepass == null)
		{
			return;
		}
		BattlePassModel seasonPass = SeasonChallengeXML.GetSeasonPass(battlepass.Id);
		if (seasonPass != null)
		{
			battlepass.IsPremium = true;
			for (int i = 0; i < battlepass.Level; i++)
			{
				PassBoxModel passBoxModel = seasonPass.Cards[i];
				int[] int_ = new int[3]
				{
					0,
					passBoxModel.PremiumA?.GoodId ?? 0,
					passBoxModel.PremiumB?.GoodId ?? 0
				};
				smethod_22(Player, int_);
			}
			ComDiv.UpdateDB("player_battlepass", "premium", battlepass.IsPremium, "owner_id", Player.PlayerId);
		}
	}

	public static void SendCompetitiveInfo(Account Player)
	{
		try
		{
			Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, bool_1: true, Translation.GetLabel("CompetitiveRank", Player.Competitive.Rank().Name, Player.Competitive.Points, Player.Competitive.Rank().Points)));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.ToString(), LoggerType.Error);
		}
	}

	public static void CalculateCompetitive(RoomModel Room, Account Player, SlotModel Slot, bool HaveWin)
	{
		if (Room.Competitive)
		{
			int num = (HaveWin ? 50 : (-30));
			num += 2 * Slot.AllKills;
			num += Slot.AllAssists;
			num -= Slot.AllDeaths;
			Player.Competitive.Points += num;
			if (Player.Competitive.Points < 0)
			{
				Player.Competitive.Points = 0;
			}
			smethod_20(Player.Competitive);
			string label = Translation.GetLabel("CompetitivePointsEarned", num);
			string label2 = Translation.GetLabel("CompetitiveRank", Player.Competitive.Rank().Name, Player.Competitive.Points, Player.Competitive.Rank().Points);
			Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, bool_1: true, label + "\n\r" + label2));
		}
	}

	private static void smethod_20(PlayerCompetitive playerCompetitive_0)
	{
		int num = 0;
		num = CompetitiveXML.Ranks.FirstOrDefault((CompetitiveRank competitiveRank_0) => playerCompetitive_0.Points <= competitiveRank_0.Points)?.Id ?? playerCompetitive_0.Level;
		ComDiv.UpdateDB("player_competitive", "points", playerCompetitive_0.Points, "owner_id", playerCompetitive_0.OwnerId);
		if (num != playerCompetitive_0.Level && ComDiv.UpdateDB("player_competitive", "level", num, "owner_id", playerCompetitive_0.OwnerId))
		{
			playerCompetitive_0.Level = num;
		}
	}

	public static bool CanOpenSlotCompetitive(RoomModel Room, SlotModel Opening)
	{
		return Room.Slots.Where((SlotModel slotModel_1) => slotModel_1.Team == Opening.Team && slotModel_1.State != SlotState.CLOSE).Count() < 5;
	}

	public static bool CanCloseSlotCompetitive(RoomModel Room, SlotModel Closing)
	{
		return Room.Slots.Where((SlotModel slotModel_1) => slotModel_1.Team == Closing.Team && slotModel_1.State != SlotState.CLOSE).Count() > 3;
	}

	private static void smethod_21(Account account_0)
	{
		List<ItemsModel> ıtems = account_0.Inventory.Items;
		lock (ıtems)
		{
			foreach (ItemsModel item in ıtems)
			{
				if (ComDiv.GetIdStatics(item.Id, 1) == 6 && account_0.Character.GetCharacter(item.Id) == null)
				{
					CreateCharacter(account_0, item);
				}
			}
		}
	}

	public static void CreateCharacter(Account Player, ItemsModel Item)
	{
		CharacterModel characterModel = new CharacterModel
		{
			Id = Item.Id,
			Name = Item.Name,
			Slot = Player.Character.GenSlotId(Item.Id),
			CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
			PlayTime = 0u
		};
		Player.Character.AddCharacter(characterModel);
		Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, Item));
		if (DaoManagerSQL.CreatePlayerCharacter(characterModel, Player.PlayerId))
		{
			Player.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0u, 3, characterModel, Player));
		}
		else
		{
			Player.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147483648u, byte.MaxValue, null, null));
		}
	}

	private static void smethod_22(Account account_0, int[] int_0)
	{
		foreach (int num in int_0)
		{
			if (num == 0)
			{
				continue;
			}
			GoodsItem good = ShopManager.GetGood(num);
			if (good != null)
			{
				if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && account_0.Character.GetCharacter(good.Item.Id) == null)
				{
					CreateCharacter(account_0, good.Item);
				}
				else
				{
					account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
				}
			}
		}
		account_0.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK(0u, int_0));
	}

	private static int smethod_23(Account account_0, BattleRewardType battleRewardType_0)
	{
		Random random = new Random();
		BattleRewardModel rewardType = BattleRewardXML.GetRewardType(battleRewardType_0);
		if (rewardType != null && rewardType.Enable && random.Next(100) < rewardType.Percentage)
		{
			GoodsItem good = ShopManager.GetGood(rewardType.Rewards[random.Next(rewardType.Rewards.Length)]);
			if (good == null)
			{
				return 0;
			}
			account_0.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(account_0, good.Item));
			if (ComDiv.GetIdStatics(good.Item.Id, 1) == 29)
			{
				int num = 0;
				switch (good.Item.Id)
				{
				case 2900001:
					num = 1;
					break;
				case 2900002:
					num = 2;
					break;
				case 2900003:
					num = 3;
					break;
				case 2900004:
					num = 4;
					break;
				case 2900005:
					num = 5;
					break;
				}
				account_0.Tags += num;
				ComDiv.UpdateDB("accounts", "tags", account_0.Tags, "player_id", account_0.PlayerId);
				return good.Item.Id;
			}
			if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && account_0.Character.GetCharacter(good.Item.Id) == null)
			{
				CreateCharacter(account_0, good.Item);
			}
			else
			{
				account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
			}
			return good.Item.Id;
		}
		return 0;
	}

	public static (byte[], int[]) GetRewardData(RoomModel Room, List<SlotModel> Slots)
	{
		byte[] byte_ = new byte[5];
		int[] int_ = new int[5];
		for (int i = 0; i < 5; i++)
		{
			byte_[i] = byte.MaxValue;
			int_[i] = 0;
		}
		int int_2 = 0;
		if (!Room.IsBotMode() && Slots.Count > 0)
		{
			SlotModel slotModel = (from slotModel_0 in Slots
				where slotModel_0.Score > 0
				orderby slotModel_0.Score descending
				select slotModel_0).FirstOrDefault();
			if (slotModel != null)
			{
				smethod_24(Room, slotModel, BattleRewardType.MVP, 5, ref int_2, ref byte_, ref int_);
			}
			SlotModel slotModel2 = (from slotModel_0 in Slots
				where slotModel_0.AllAssists > 0
				orderby slotModel_0.AllAssists descending
				select slotModel_0).FirstOrDefault();
			if (slotModel2 != null)
			{
				smethod_24(Room, slotModel2, BattleRewardType.AssistKing, 5, ref int_2, ref byte_, ref int_);
			}
			SlotModel slotModel3 = (from slotModel_0 in Slots
				where slotModel_0.KillsOnLife > 0
				orderby slotModel_0.KillsOnLife descending
				select slotModel_0).FirstOrDefault();
			if (slotModel3 != null)
			{
				smethod_24(Room, slotModel3, BattleRewardType.MultiKill, 5, ref int_2, ref byte_, ref int_);
			}
		}
		return (byte_, int_);
	}

	private static void smethod_24(RoomModel roomModel_0, SlotModel slotModel_0, BattleRewardType battleRewardType_0, int int_0, ref int int_1, ref byte[] byte_0, ref int[] int_2)
	{
		if (int_1 < int_0 && roomModel_0.GetPlayerBySlot(slotModel_0, out var Player))
		{
			byte b = (byte)slotModel_0.Id;
			int num = smethod_23(Player, battleRewardType_0);
			if (b != byte.MaxValue && num != 0)
			{
				byte_0[int_1] = b;
				int_2[int_1] = num;
				int_1++;
			}
		}
	}

	public static int InitBotRank(int BotLevel)
	{
		Random random = new Random();
		return BotLevel switch
		{
			1 => random.Next(0, 4), 
			2 => random.Next(5, 9), 
			3 => random.Next(10, 14), 
			4 => random.Next(15, 19), 
			5 => random.Next(20, 24), 
			6 => random.Next(25, 29), 
			7 => random.Next(30, 34), 
			8 => random.Next(35, 39), 
			9 => random.Next(40, 44), 
			10 => random.Next(45, 49), 
			_ => 52, 
		};
	}
}
