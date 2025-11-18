using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
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

namespace Server.Game.Data.Utils
{
	// Token: 0x020001DD RID: 477
	public static class AllUtils
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x00027594 File Offset: 0x00025794
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

		// Token: 0x0600051A RID: 1306 RVA: 0x000275F4 File Offset: 0x000257F4
		public static void LoadPlayerInventory(Account Player)
		{
			List<ItemsModel> items = Player.Inventory.Items;
			lock (items)
			{
				Player.Inventory.Items.AddRange(DaoManagerSQL.GetPlayerInventoryItems(Player.PlayerId));
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00027650 File Offset: 0x00025850
		public static void LoadPlayerMissions(Account Player)
		{
			PlayerMissions playerMissionsDB = DaoManagerSQL.GetPlayerMissionsDB(Player.PlayerId, Player.Mission.Mission1, Player.Mission.Mission2, Player.Mission.Mission3, Player.Mission.Mission4);
			if (playerMissionsDB != null)
			{
				Player.Mission = playerMissionsDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerMissionsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Missions!", LoggerType.Warning, null);
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000276BC File Offset: 0x000258BC
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
				AllUtils.smethod_21(Player);
			}
			string text;
			if (AllUtils.smethod_0(Player, out text))
			{
				List<ItemsModel> pccafeRewards = TemplatePackXML.GetPCCafeRewards(Player.CafePC);
				List<ItemsModel> items = Player.Inventory.Items;
				lock (items)
				{
					Player.Inventory.Items.AddRange(pccafeRewards);
				}
				using (List<ItemsModel>.Enumerator enumerator = pccafeRewards.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ItemsModel itemsModel = enumerator.Current;
						if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 6 && Player.Character.GetCharacter(itemsModel.Id) == null)
						{
							AllUtils.CreateCharacter(Player, itemsModel);
						}
						if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 16)
						{
							CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemsModel.Id);
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
			foreach (ItemsModel itemsModel2 in TemplatePackXML.GetPCCafeRewards(Player.CafePC))
			{
				if (ComDiv.GetIdStatics(itemsModel2.Id, 1) == 6 && Player.Character.GetCharacter(itemsModel2.Id) != null)
				{
					AllUtils.smethod_3(Player, itemsModel2.Id);
				}
				if (ComDiv.GetIdStatics(itemsModel2.Id, 1) == 16)
				{
					CouponFlag couponEffect2 = CouponEffectXML.GetCouponEffect(itemsModel2.Id);
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
				if (!string.IsNullOrEmpty(text) && ComDiv.DeleteDB("player_vip", "owner_id", Player.PlayerId))
				{
					CLogger.Print(string.Format("VIP for UID: {0} Nick: {1} Deleted Due To {2}", Player.PlayerId, Player.Nickname, text), LoggerType.Info, null);
				}
				CLogger.Print(string.Format("Player PC Cafe was resetted by default into '{0}'; (UID: {1} Nick: {2})", Player.CafePC, Player.PlayerId, Player.Nickname), LoggerType.Info, null);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000279F4 File Offset: 0x00025BF4
		private static bool smethod_0(Account account_0, out string string_0)
		{
			if (account_0.IsGM())
			{
				string_0 = "GM Special Access";
				return true;
			}
			PlayerVip playerVIP = DaoManagerSQL.GetPlayerVIP(account_0.PlayerId);
			if (playerVIP == null)
			{
				string_0 = "Database Not Found!";
				return false;
			}
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
			string text = string.Format("{0}", account_0.CafePC);
			if (!playerVIP.Benefit.Equals(text) && ComDiv.UpdateDB("player_vip", "last_benefit", text, "owner_id", account_0.PlayerId))
			{
				playerVIP.Benefit = text;
			}
			string_0 = "Valid Access";
			return true;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00027ACC File Offset: 0x00025CCC
		public static void LoadPlayerEquipments(Account Player)
		{
			PlayerEquipment playerEquipmentsDB = DaoManagerSQL.GetPlayerEquipmentsDB(Player.PlayerId);
			if (playerEquipmentsDB != null)
			{
				Player.Equipment = playerEquipmentsDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerEquipmentsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Equipment!", LoggerType.Warning, null);
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00027B0C File Offset: 0x00025D0C
		public static void LoadPlayerCharacters(Account Player)
		{
			List<CharacterModel> playerCharactersDB = DaoManagerSQL.GetPlayerCharactersDB(Player.PlayerId);
			if (playerCharactersDB.Count > 0)
			{
				Player.Character.Characters = playerCharactersDB;
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00027B3C File Offset: 0x00025D3C
		public static void LoadPlayerStatistic(Account Player)
		{
			StatisticTotal playerStatBasicDB = DaoManagerSQL.GetPlayerStatBasicDB(Player.PlayerId);
			if (playerStatBasicDB != null)
			{
				Player.Statistic.Basic = playerStatBasicDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatBasicDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Basic Statistic!", LoggerType.Warning, null);
			}
			StatisticSeason playerStatSeasonDB = DaoManagerSQL.GetPlayerStatSeasonDB(Player.PlayerId);
			if (playerStatSeasonDB != null)
			{
				Player.Statistic.Season = playerStatSeasonDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatSeasonDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Season Statistic!", LoggerType.Warning, null);
			}
			StatisticClan playerStatClanDB = DaoManagerSQL.GetPlayerStatClanDB(Player.PlayerId);
			if (playerStatClanDB != null)
			{
				Player.Statistic.Clan = playerStatClanDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatClanDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Clan Statistic!", LoggerType.Warning, null);
			}
			StatisticDaily playerStatDailiesDB = DaoManagerSQL.GetPlayerStatDailiesDB(Player.PlayerId);
			if (playerStatDailiesDB != null)
			{
				Player.Statistic.Daily = playerStatDailiesDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatDailiesDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Daily Statistic!", LoggerType.Warning, null);
			}
			StatisticWeapon playerStatWeaponsDB = DaoManagerSQL.GetPlayerStatWeaponsDB(Player.PlayerId);
			if (playerStatWeaponsDB != null)
			{
				Player.Statistic.Weapon = playerStatWeaponsDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatWeaponsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Weapon Statistic!", LoggerType.Warning, null);
			}
			StatisticAcemode playerStatAcemodesDB = DaoManagerSQL.GetPlayerStatAcemodesDB(Player.PlayerId);
			if (playerStatAcemodesDB != null)
			{
				Player.Statistic.Acemode = playerStatAcemodesDB;
			}
			else if (!DaoManagerSQL.CreatePlayerStatAcemodesDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Acemode Statistic!", LoggerType.Warning, null);
			}
			StatisticBattlecup playerStatBattlecupDB = DaoManagerSQL.GetPlayerStatBattlecupDB(Player.PlayerId);
			if (playerStatBattlecupDB != null)
			{
				Player.Statistic.Battlecup = playerStatBattlecupDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerStatBattlecupsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Battlecup Statistic!", LoggerType.Warning, null);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00027CCC File Offset: 0x00025ECC
		public static void LoadPlayerTitles(Account Player)
		{
			PlayerTitles playerTitlesDB = DaoManagerSQL.GetPlayerTitlesDB(Player.PlayerId);
			if (playerTitlesDB != null)
			{
				Player.Title = playerTitlesDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Title!", LoggerType.Warning, null);
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00027D0C File Offset: 0x00025F0C
		public static void LoadPlayerBattlepass(Account Player)
		{
			PlayerBattlepass playerBattlepassDB = DaoManagerSQL.GetPlayerBattlepassDB(Player.PlayerId);
			if (playerBattlepassDB != null)
			{
				Player.Battlepass = playerBattlepassDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerBattlepassDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Battlepass!", LoggerType.Warning, null);
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00027D4C File Offset: 0x00025F4C
		public static void LoadPlayerCompetitive(Account Player)
		{
			PlayerCompetitive playerCompetitiveDB = DaoManagerSQL.GetPlayerCompetitiveDB(Player.PlayerId);
			if (playerCompetitiveDB != null)
			{
				Player.Competitive = playerCompetitiveDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerCompetitiveDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Competitive!", LoggerType.Warning, null);
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00027D8C File Offset: 0x00025F8C
		public static void LoadPlayerBonus(Account Player)
		{
			PlayerBonus playerBonusDB = DaoManagerSQL.GetPlayerBonusDB(Player.PlayerId);
			if (playerBonusDB != null)
			{
				Player.Bonus = playerBonusDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerBonusDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Bonus!", LoggerType.Warning, null);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00027DCC File Offset: 0x00025FCC
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

		// Token: 0x06000526 RID: 1318 RVA: 0x00027E08 File Offset: 0x00026008
		public static void LoadPlayerEvent(Account Player)
		{
			PlayerEvent playerEventDB = DaoManagerSQL.GetPlayerEventDB(Player.PlayerId);
			if (playerEventDB != null)
			{
				Player.Event = playerEventDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerEventDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Event!", LoggerType.Warning, null);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00027E48 File Offset: 0x00026048
		public static void LoadPlayerConfig(Account Player)
		{
			PlayerConfig playerConfigDB = DaoManagerSQL.GetPlayerConfigDB(Player.PlayerId);
			if (playerConfigDB != null)
			{
				Player.Config = playerConfigDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerConfigDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Config!", LoggerType.Warning, null);
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00027E88 File Offset: 0x00026088
		public static void LoadPlayerQuickstarts(Account Player)
		{
			List<QuickstartModel> playerQuickstartsDB = DaoManagerSQL.GetPlayerQuickstartsDB(Player.PlayerId);
			if (playerQuickstartsDB.Count > 0)
			{
				Player.Quickstart.Quickjoins = playerQuickstartsDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerQuickstartsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Quickstarts!", LoggerType.Warning, null);
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00027ED0 File Offset: 0x000260D0
		public static void LoadPlayerReport(Account Player)
		{
			PlayerReport playerReportDB = DaoManagerSQL.GetPlayerReportDB(Player.PlayerId);
			if (playerReportDB != null)
			{
				Player.Report = playerReportDB;
				return;
			}
			if (!DaoManagerSQL.CreatePlayerReportDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Report!", LoggerType.Warning, null);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00027F10 File Offset: 0x00026110
		public static int GetKillScore(KillingMessage msg)
		{
			int num = 0;
			if (msg != KillingMessage.MassKill)
			{
				if (msg != KillingMessage.PiercingShot)
				{
					if (msg == KillingMessage.ChainStopper)
					{
						return num + 8;
					}
					if (msg == KillingMessage.Headshot)
					{
						return num + 10;
					}
					if (msg == KillingMessage.ChainHeadshot)
					{
						return num + 14;
					}
					if (msg == KillingMessage.ChainSlugger)
					{
						return num + 6;
					}
					if (msg == KillingMessage.ObjectDefense)
					{
						return num + 7;
					}
					if (msg != KillingMessage.Suicide)
					{
						return num + 5;
					}
					return num;
				}
			}
			num += 6;
			return num;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000058A3 File Offset: 0x00003AA3
		private static ClassType smethod_1(ClassType classType_0)
		{
			if (classType_0 == ClassType.DualSMG)
			{
				return ClassType.SMG;
			}
			if (classType_0 == ClassType.DualHandGun)
			{
				return ClassType.HandGun;
			}
			if (classType_0 != ClassType.DualKnife)
			{
				if (classType_0 != ClassType.Knuckle)
				{
					if (classType_0 == ClassType.DualShotgun)
					{
						return ClassType.Shotgun;
					}
					return classType_0;
				}
			}
			return ClassType.Knife;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00027F74 File Offset: 0x00026174
		public static TeamEnum GetWinnerTeam(RoomModel room)
		{
			if (room == null)
			{
				return TeamEnum.TEAM_DRAW;
			}
			TeamEnum teamEnum = TeamEnum.TEAM_DRAW;
			if (room.RoomType != RoomCondition.Bomb && room.RoomType != RoomCondition.Destroy && room.RoomType != RoomCondition.Annihilation && room.RoomType != RoomCondition.Defense)
			{
				if (room.RoomType != RoomCondition.Destroy)
				{
					if (room.IsDinoMode("DE"))
					{
						if (room.CTDino == room.FRDino)
						{
							return TeamEnum.TEAM_DRAW;
						}
						if (room.CTDino > room.FRDino)
						{
							return TeamEnum.CT_TEAM;
						}
						if (room.CTDino < room.FRDino)
						{
							return TeamEnum.FR_TEAM;
						}
						return teamEnum;
					}
					else
					{
						if (room.CTKills == room.FRKills)
						{
							return TeamEnum.TEAM_DRAW;
						}
						if (room.CTKills > room.FRKills)
						{
							return TeamEnum.CT_TEAM;
						}
						if (room.CTKills < room.FRKills)
						{
							return TeamEnum.FR_TEAM;
						}
						return teamEnum;
					}
				}
			}
			if (room.CTRounds == room.FRRounds)
			{
				teamEnum = TeamEnum.TEAM_DRAW;
			}
			else if (room.CTRounds > room.FRRounds)
			{
				teamEnum = TeamEnum.CT_TEAM;
			}
			else if (room.CTRounds < room.FRRounds)
			{
				teamEnum = TeamEnum.FR_TEAM;
			}
			return teamEnum;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00028078 File Offset: 0x00026278
		public static TeamEnum GetWinnerTeam(RoomModel room, int RedPlayers, int BluePlayers)
		{
			if (room == null)
			{
				return TeamEnum.TEAM_DRAW;
			}
			TeamEnum teamEnum = TeamEnum.TEAM_DRAW;
			if (RedPlayers == 0)
			{
				teamEnum = TeamEnum.CT_TEAM;
			}
			else if (BluePlayers == 0)
			{
				teamEnum = TeamEnum.FR_TEAM;
			}
			return teamEnum;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002809C File Offset: 0x0002629C
		public static void UpdateMatchCount(bool WonTheMatch, Account Player, int WinnerTeam, DBQuery TotalQuery, DBQuery SeasonQuery)
		{
			int num;
			if (WinnerTeam == 2)
			{
				string text = "match_draws";
				StatisticTotal basic = Player.Statistic.Basic;
				num = basic.MatchDraws + 1;
				basic.MatchDraws = num;
				TotalQuery.AddQuery(text, num);
				string text2 = "match_draws";
				StatisticSeason season = Player.Statistic.Season;
				num = season.MatchDraws + 1;
				season.MatchDraws = num;
				SeasonQuery.AddQuery(text2, num);
			}
			else if (WonTheMatch)
			{
				string text3 = "match_wins";
				StatisticTotal basic2 = Player.Statistic.Basic;
				num = basic2.MatchWins + 1;
				basic2.MatchWins = num;
				TotalQuery.AddQuery(text3, num);
				string text4 = "match_wins";
				StatisticSeason season2 = Player.Statistic.Season;
				num = season2.MatchWins + 1;
				season2.MatchWins = num;
				SeasonQuery.AddQuery(text4, num);
			}
			else
			{
				string text5 = "match_loses";
				StatisticTotal basic3 = Player.Statistic.Basic;
				num = basic3.MatchLoses + 1;
				basic3.MatchLoses = num;
				TotalQuery.AddQuery(text5, num);
				string text6 = "match_loses";
				StatisticSeason season3 = Player.Statistic.Season;
				num = season3.MatchLoses + 1;
				season3.MatchLoses = num;
				SeasonQuery.AddQuery(text6, num);
			}
			string text7 = "matches";
			StatisticTotal basic4 = Player.Statistic.Basic;
			num = basic4.Matches + 1;
			basic4.Matches = num;
			TotalQuery.AddQuery(text7, num);
			string text8 = "total_matches";
			StatisticTotal basic5 = Player.Statistic.Basic;
			num = basic5.TotalMatchesCount + 1;
			basic5.TotalMatchesCount = num;
			TotalQuery.AddQuery(text8, num);
			string text9 = "matches";
			StatisticSeason season4 = Player.Statistic.Season;
			num = season4.Matches + 1;
			season4.Matches = num;
			SeasonQuery.AddQuery(text9, num);
			string text10 = "total_matches";
			StatisticSeason season5 = Player.Statistic.Season;
			num = season5.TotalMatchesCount + 1;
			season5.TotalMatchesCount = num;
			SeasonQuery.AddQuery(text10, num);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0002826C File Offset: 0x0002646C
		public static void UpdateDailyRecord(bool WonTheMatch, Account Player, int winnerTeam, DBQuery query)
		{
			int num;
			if (winnerTeam == 2)
			{
				string text = "match_draws";
				StatisticDaily daily = Player.Statistic.Daily;
				num = daily.MatchDraws + 1;
				daily.MatchDraws = num;
				query.AddQuery(text, num);
			}
			else if (WonTheMatch)
			{
				string text2 = "match_wins";
				StatisticDaily daily2 = Player.Statistic.Daily;
				num = daily2.MatchWins + 1;
				daily2.MatchWins = num;
				query.AddQuery(text2, num);
			}
			else
			{
				string text3 = "match_loses";
				StatisticDaily daily3 = Player.Statistic.Daily;
				num = daily3.MatchLoses + 1;
				daily3.MatchLoses = num;
				query.AddQuery(text3, num);
			}
			string text4 = "matches";
			StatisticDaily daily4 = Player.Statistic.Daily;
			num = daily4.Matches + 1;
			daily4.Matches = num;
			query.AddQuery(text4, num);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00028330 File Offset: 0x00026530
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
			int num2;
			if (array[num] == SlotWin)
			{
				string text = "match_wins";
				StatisticTotal basic = Player.Statistic.Basic;
				num2 = basic.MatchWins + 1;
				basic.MatchWins = num2;
				TotalQuery.AddQuery(text, num2);
				string text2 = "match_wins";
				StatisticSeason season = Player.Statistic.Season;
				num2 = season.MatchWins + 1;
				season.MatchWins = num2;
				SeasonQuery.AddQuery(text2, num2);
			}
			else
			{
				string text3 = "match_loses";
				StatisticTotal basic2 = Player.Statistic.Basic;
				num2 = basic2.MatchLoses + 1;
				basic2.MatchLoses = num2;
				TotalQuery.AddQuery(text3, num2);
				string text4 = "match_loses";
				StatisticSeason season2 = Player.Statistic.Season;
				num2 = season2.MatchLoses + 1;
				season2.MatchLoses = num2;
				SeasonQuery.AddQuery(text4, num2);
			}
			string text5 = "matches";
			StatisticTotal basic3 = Player.Statistic.Basic;
			num2 = basic3.Matches + 1;
			basic3.Matches = num2;
			TotalQuery.AddQuery(text5, num2);
			string text6 = "total_matches";
			StatisticTotal basic4 = Player.Statistic.Basic;
			num2 = basic4.TotalMatchesCount + 1;
			basic4.TotalMatchesCount = num2;
			TotalQuery.AddQuery(text6, num2);
			string text7 = "matches";
			StatisticSeason season3 = Player.Statistic.Season;
			num2 = season3.Matches + 1;
			season3.Matches = num2;
			SeasonQuery.AddQuery(text7, num2);
			string text8 = "total_matches";
			StatisticSeason season4 = Player.Statistic.Season;
			num2 = season4.TotalMatchesCount + 1;
			season4.TotalMatchesCount = num2;
			SeasonQuery.AddQuery(text8, num2);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00028510 File Offset: 0x00026710
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
			int num2;
			if (array[num] == SlotWin)
			{
				string text = "match_wins";
				StatisticDaily daily = Player.Statistic.Daily;
				num2 = daily.MatchWins + 1;
				daily.MatchWins = num2;
				Query.AddQuery(text, num2);
			}
			else
			{
				string text2 = "match_loses";
				StatisticDaily daily2 = Player.Statistic.Daily;
				num2 = daily2.MatchLoses + 1;
				daily2.MatchLoses = num2;
				Query.AddQuery(text2, num2);
			}
			string text3 = "matches";
			StatisticDaily daily3 = Player.Statistic.Daily;
			num2 = daily3.Matches + 1;
			daily3.Matches = num2;
			Query.AddQuery(text3, num2);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00028608 File Offset: 0x00026808
		public static void UpdateWeaponRecord(Account Player, SlotModel Slot, DBQuery Query)
		{
			StatisticWeapon weapon = Player.Statistic.Weapon;
			if (Slot.AR[0] > 0)
			{
				string text = "assault_rifle_kills";
				StatisticWeapon statisticWeapon = weapon;
				int num = statisticWeapon.AssaultKills + 1;
				statisticWeapon.AssaultKills = num;
				Query.AddQuery(text, num);
			}
			if (Slot.AR[1] > 0)
			{
				string text2 = "assault_rifle_deaths";
				StatisticWeapon statisticWeapon2 = weapon;
				int num = statisticWeapon2.AssaultDeaths + 1;
				statisticWeapon2.AssaultDeaths = num;
				Query.AddQuery(text2, num);
			}
			if (Slot.SMG[0] > 0)
			{
				string text3 = "sub_machine_gun_kills";
				StatisticWeapon statisticWeapon3 = weapon;
				int num = statisticWeapon3.SmgKills + 1;
				statisticWeapon3.SmgKills = num;
				Query.AddQuery(text3, num);
			}
			if (Slot.SMG[1] > 0)
			{
				string text4 = "sub_machine_gun_deaths";
				StatisticWeapon statisticWeapon4 = weapon;
				int num = statisticWeapon4.SmgDeaths + 1;
				statisticWeapon4.SmgDeaths = num;
				Query.AddQuery(text4, num);
			}
			if (Slot.SR[0] > 0)
			{
				string text5 = "sniper_rifle_kills";
				StatisticWeapon statisticWeapon5 = weapon;
				int num = statisticWeapon5.SniperKills + 1;
				statisticWeapon5.SniperKills = num;
				Query.AddQuery(text5, num);
			}
			if (Slot.SR[1] > 0)
			{
				string text6 = "sniper_rifle_deaths";
				StatisticWeapon statisticWeapon6 = weapon;
				int num = statisticWeapon6.SniperDeaths + 1;
				statisticWeapon6.SniperDeaths = num;
				Query.AddQuery(text6, num);
			}
			if (Slot.SG[0] > 0)
			{
				string text7 = "shot_gun_kills";
				StatisticWeapon statisticWeapon7 = weapon;
				int num = statisticWeapon7.ShotgunKills + 1;
				statisticWeapon7.ShotgunKills = num;
				Query.AddQuery(text7, num);
			}
			if (Slot.SG[1] > 0)
			{
				string text8 = "shot_gun_deaths";
				StatisticWeapon statisticWeapon8 = weapon;
				int num = statisticWeapon8.ShotgunDeaths + 1;
				statisticWeapon8.ShotgunDeaths = num;
				Query.AddQuery(text8, num);
			}
			if (Slot.MG[0] > 0)
			{
				string text9 = "machine_gun_kills";
				StatisticWeapon statisticWeapon9 = weapon;
				int num = statisticWeapon9.MachinegunKills + 1;
				statisticWeapon9.MachinegunKills = num;
				Query.AddQuery(text9, num);
			}
			if (Slot.MG[1] > 0)
			{
				string text10 = "machine_gun_deaths";
				StatisticWeapon statisticWeapon10 = weapon;
				int num = statisticWeapon10.MachinegunDeaths + 1;
				statisticWeapon10.MachinegunDeaths = num;
				Query.AddQuery(text10, num);
			}
			if (Slot.SHD[0] > 0)
			{
				string text11 = "shield_kills";
				StatisticWeapon statisticWeapon11 = weapon;
				int num = statisticWeapon11.ShieldKills + 1;
				statisticWeapon11.ShieldKills = num;
				Query.AddQuery(text11, num);
			}
			if (Slot.SHD[1] > 0)
			{
				string text12 = "shield_deaths";
				StatisticWeapon statisticWeapon12 = weapon;
				int num = statisticWeapon12.ShieldDeaths + 1;
				statisticWeapon12.ShieldDeaths = num;
				Query.AddQuery(text12, num);
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00028834 File Offset: 0x00026A34
		public static void GenerateMissionAwards(Account Player, DBQuery query)
		{
			try
			{
				PlayerMissions mission = Player.Mission;
				int actualMission = mission.ActualMission;
				int currentMissionId = mission.GetCurrentMissionId();
				int currentCard = mission.GetCurrentCard();
				if (currentMissionId > 0 && !mission.SelectedCard)
				{
					int num = 0;
					int num2 = 0;
					byte[] currentMissionList = mission.GetCurrentMissionList();
					foreach (MissionCardModel missionCardModel in MissionCardRAW.GetCards(currentMissionId, -1))
					{
						if ((int)currentMissionList[missionCardModel.ArrayIdx] >= missionCardModel.MissionLimit)
						{
							num2++;
							if (missionCardModel.CardBasicId == currentCard)
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
						Player.SendPacket(new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(273U, 4, Player));
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
						query.AddQuery(string.Format("mission_id{0}", actualMission + 1), 0);
						ComDiv.UpdateDB("player_missions", "owner_id", Player.PlayerId, new string[]
						{
							string.Format("card{0}", actualMission + 1),
							string.Format("mission{0}_raw", actualMission + 1)
						}, new object[]
						{
							0,
							new byte[0]
						});
						if (actualMission == 0)
						{
							mission.Mission1 = 0;
							mission.Card1 = 0;
							mission.List1 = new byte[40];
						}
						else if (actualMission == 1)
						{
							mission.Mission2 = 0;
							mission.Card2 = 0;
							mission.List2 = new byte[40];
						}
						else if (actualMission == 2)
						{
							mission.Mission3 = 0;
							mission.Card3 = 0;
							mission.List3 = new byte[40];
						}
						else if (actualMission == 3)
						{
							mission.Mission4 = 0;
							mission.Card3 = 0;
							mission.List4 = new byte[40];
							if (Player.Event != null)
							{
								Player.Event.LastQuestFinish = 1;
								ComDiv.UpdateDB("player_events", "last_quest_finish", 1, "owner_id", Player.PlayerId);
							}
						}
					}
					else if (num == 4 && !mission.SelectedCard)
					{
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
						Player.SendPacket(new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(1U, 1, Player));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("AllUtils.GenerateMissionAwards: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000058C9 File Offset: 0x00003AC9
		public static void ResetSlotInfo(RoomModel Room, SlotModel Slot, bool UpdateInfo)
		{
			if (Slot.State >= SlotState.LOAD)
			{
				Room.ChangeSlotState(Slot, SlotState.NORMAL, UpdateInfo);
				Slot.ResetSlot();
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000058E5 File Offset: 0x00003AE5
		public static void EndMatchMission(RoomModel Room, Account Player, SlotModel Slot, TeamEnum WinnerTeam)
		{
			if (WinnerTeam != TeamEnum.TEAM_DRAW)
			{
				AllUtils.CompleteMission(Room, Player, Slot, (Slot.Team == WinnerTeam) ? MissionType.WIN : MissionType.DEFEAT, 0);
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00028D28 File Offset: 0x00026F28
		public static void VotekickResult(RoomModel Room)
		{
			VoteKickModel voteKick = Room.VoteKick;
			if (voteKick != null)
			{
				int inGamePlayers = voteKick.GetInGamePlayers();
				if (voteKick.Accept > voteKick.Denie && voteKick.Enemies > 0 && voteKick.Allies > 0 && voteKick.Votes.Count >= inGamePlayers / 2)
				{
					Account playerBySlot = Room.GetPlayerBySlot(voteKick.VictimIdx);
					if (playerBySlot != null)
					{
						playerBySlot.SendPacket(new PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK());
						Room.KickedPlayersVote.Add(playerBySlot.PlayerId);
						Room.RemovePlayer(playerBySlot, true, 2);
					}
				}
				uint num = 0U;
				if (voteKick.Allies == 0)
				{
					num = 2147488001U;
				}
				else if (voteKick.Enemies == 0)
				{
					num = 2147488002U;
				}
				else if (voteKick.Denie < voteKick.Accept || voteKick.Votes.Count < inGamePlayers / 2)
				{
					num = 2147488000U;
				}
				using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK protocol_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK(num, voteKick))
				{
					Room.SendPacketToPlayers(protocol_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK, SlotState.BATTLE, 0);
				}
			}
			Room.VoteKick = null;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00028E30 File Offset: 0x00027030
		public static void ResetBattleInfo(RoomModel Room)
		{
			foreach (SlotModel slotModel in Room.Slots)
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
			Room.TimeRoom = 0U;
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

		// Token: 0x06000538 RID: 1336 RVA: 0x00028F80 File Offset: 0x00027180
		public static List<int> GetDinossaurs(RoomModel Room, bool ForceNewTRex, int ForceRexIdx)
		{
			List<int> list = new List<int>();
			if (Room.IsDinoMode(""))
			{
				TeamEnum teamEnum = ((Room.Rounds == 1) ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
				foreach (int num in Room.GetTeamArray(teamEnum))
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

		// Token: 0x06000539 RID: 1337 RVA: 0x0002906C File Offset: 0x0002726C
		public static void BattleEndPlayersCount(RoomModel Room, bool IsBotMode)
		{
			if (Room != null && !IsBotMode && Room.IsPreparing())
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				foreach (SlotModel slotModel in Room.Slots)
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
					AllUtils.EndBattle(Room, IsBotMode);
				}
				return;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00005903 File Offset: 0x00003B03
		public static void EndBattle(RoomModel Room)
		{
			AllUtils.EndBattle(Room, Room.IsBotMode());
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00005911 File Offset: 0x00003B11
		public static void EndBattle(RoomModel Room, bool isBotMode)
		{
			AllUtils.EndBattle(Room, isBotMode, AllUtils.GetWinnerTeam(Room));
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00029120 File Offset: 0x00027320
		public static void EndBattleNoPoints(RoomModel Room)
		{
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				int num;
				int num2;
				byte[] array;
				AllUtils.GetBattleResult(Room, out num, out num2, out array);
				bool flag = Room.IsBotMode();
				foreach (Account account in allPlayers)
				{
					account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, TeamEnum.TEAM_DRAW, num2, num, flag, array));
					AllUtils.UpdateSeasonPass(account);
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000291AC File Offset: 0x000273AC
		public static void EndBattle(RoomModel Room, bool IsBotMode, TeamEnum WinnerTeam)
		{
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				Room.CalculateResult(WinnerTeam, IsBotMode);
				int num;
				int num2;
				byte[] array;
				AllUtils.GetBattleResult(Room, out num, out num2, out array);
				foreach (Account account in allPlayers)
				{
					account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, WinnerTeam, num2, num, IsBotMode, array));
					AllUtils.UpdateSeasonPass(account);
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00029238 File Offset: 0x00027438
		public static void BattleEndRound(RoomModel Room, TeamEnum Winner, bool ForceRestart, FragInfos Kills, SlotModel Killer)
		{
			AllUtils.Class9 @class = new AllUtils.Class9();
			@class.roomModel_0 = Room;
			@class.teamEnum_0 = Winner;
			@class.bool_0 = ForceRestart;
			@class.fragInfos_0 = Kills;
			@class.slotModel_0 = Killer;
			@class.roomModel_0.MatchEndTime.StartJob(1250, new TimerCallback(@class.method_0));
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00029290 File Offset: 0x00027490
		private static void smethod_2(RoomModel roomModel_0, TeamEnum teamEnum_0, bool bool_0, FragInfos fragInfos_0, SlotModel slotModel_0)
		{
			int roundsByMask = roomModel_0.GetRoundsByMask();
			if (roomModel_0.FRRounds < roundsByMask && roomModel_0.CTRounds < roundsByMask)
			{
				if (!roomModel_0.ActiveC4 || bool_0)
				{
					roomModel_0.StopBomb();
					roomModel_0.ChangeRounds();
					RoundSync.SendUDPRoundSync(roomModel_0);
					using (PROTOCOL_BATTLE_WINNING_CAM_ACK protocol_BATTLE_WINNING_CAM_ACK = new PROTOCOL_BATTLE_WINNING_CAM_ACK(fragInfos_0))
					{
						using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_BATTLE_MISSION_ROUND_END_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, teamEnum_0, RoundEndType.AllDeath))
						{
							roomModel_0.SendPacketToPlayers(protocol_BATTLE_WINNING_CAM_ACK, protocol_BATTLE_MISSION_ROUND_END_ACK, SlotState.BATTLE, 0);
						}
					}
					roomModel_0.RoundRestart();
				}
				return;
			}
			roomModel_0.StopBomb();
			using (PROTOCOL_BATTLE_WINNING_CAM_ACK protocol_BATTLE_WINNING_CAM_ACK2 = new PROTOCOL_BATTLE_WINNING_CAM_ACK(fragInfos_0))
			{
				using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_BATTLE_MISSION_ROUND_END_ACK2 = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, teamEnum_0, RoundEndType.AllDeath))
				{
					roomModel_0.SendPacketToPlayers(protocol_BATTLE_WINNING_CAM_ACK2, protocol_BATTLE_MISSION_ROUND_END_ACK2, SlotState.BATTLE, 0);
				}
			}
			AllUtils.EndBattle(roomModel_0, roomModel_0.IsBotMode(), teamEnum_0);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002938C File Offset: 0x0002758C
		public static void BattleEndRound(RoomModel Room, TeamEnum Winner, RoundEndType Motive)
		{
			using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_BATTLE_MISSION_ROUND_END_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(Room, Winner, Motive))
			{
				Room.SendPacketToPlayers(protocol_BATTLE_MISSION_ROUND_END_ACK, SlotState.BATTLE, 0);
			}
			Room.StopBomb();
			int roundsByMask = Room.GetRoundsByMask();
			if (Room.FRRounds < roundsByMask && Room.CTRounds < roundsByMask)
			{
				Room.ChangeRounds();
				RoundSync.SendUDPRoundSync(Room);
				Room.RoundRestart();
				return;
			}
			AllUtils.EndBattle(Room, Room.IsBotMode(), Winner);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00029408 File Offset: 0x00027608
		public static int AddFriend(Account Owner, Account Friend, int state)
		{
			if (Owner != null && Friend != null)
			{
				int num;
				try
				{
					FriendModel friend = Owner.Friend.GetFriend(Friend.PlayerId);
					if (friend == null)
					{
						using (NpgsqlConnection npgsqlConnection = ConnectionSQL.GetInstance().Conn())
						{
							NpgsqlCommand npgsqlCommand = npgsqlConnection.CreateCommand();
							npgsqlConnection.Open();
							npgsqlCommand.CommandType = CommandType.Text;
							npgsqlCommand.Parameters.AddWithValue("@friend", Friend.PlayerId);
							npgsqlCommand.Parameters.AddWithValue("@owner", Owner.PlayerId);
							npgsqlCommand.Parameters.AddWithValue("@state", state);
							npgsqlCommand.CommandText = "INSERT INTO player_friends (id, owner_id, state) VALUES (@friend, @owner, @state)";
							npgsqlCommand.ExecuteNonQuery();
							npgsqlCommand.Dispose();
							npgsqlConnection.Dispose();
							npgsqlConnection.Close();
						}
						List<FriendModel> friends = Owner.Friend.Friends;
						lock (friends)
						{
							FriendModel friendModel = new FriendModel(Friend.PlayerId, Friend.Rank, Friend.NickColor, Friend.Nickname, Friend.IsOnline, Friend.Status)
							{
								State = state,
								Removed = false
							};
							Owner.Friend.Friends.Add(friendModel);
							SendFriendInfo.Load(Owner, friendModel, 0);
						}
						num = 0;
					}
					else
					{
						if (friend.Removed)
						{
							friend.Removed = false;
							DaoManagerSQL.UpdatePlayerFriendBlock(Owner.PlayerId, friend);
							SendFriendInfo.Load(Owner, friend, 1);
						}
						num = 1;
					}
				}
				catch (Exception ex)
				{
					CLogger.Print("AllUtils.AddFriend: " + ex.Message, LoggerType.Error, ex);
					num = -1;
				}
				return num;
			}
			return -1;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000295EC File Offset: 0x000277EC
		public static void SyncPlayerToFriends(Account p, bool all)
		{
			if (p != null && p.Friend.Friends.Count != 0)
			{
				PlayerInfo playerInfo = new PlayerInfo(p.PlayerId, p.Rank, p.NickColor, p.Nickname, p.IsOnline, p.Status);
				for (int i = 0; i < p.Friend.Friends.Count; i++)
				{
					FriendModel friendModel = p.Friend.Friends[i];
					if (all || (friendModel.State == 0 && !friendModel.Removed))
					{
						Account account = AccountManager.GetAccount(friendModel.PlayerId, 287);
						if (account != null)
						{
							int num = -1;
							FriendModel friend = account.Friend.GetFriend(p.PlayerId, out num);
							if (friend != null)
							{
								friend.Info = playerInfo;
								account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, num), false);
							}
						}
					}
				}
				return;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000296CC File Offset: 0x000278CC
		public static void SyncPlayerToClanMembers(Account player)
		{
			if (player != null && player.ClanId > 0)
			{
				using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK protocol_CS_MEMBER_INFO_CHANGE_ACK = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(player))
				{
					ClanManager.SendPacket(protocol_CS_MEMBER_INFO_CHANGE_ACK, player.ClanId, player.PlayerId, true, true);
				}
				return;
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00029720 File Offset: 0x00027920
		public static void UpdateSlotEquips(Account Player)
		{
			RoomModel room = Player.Room;
			if (room != null)
			{
				AllUtils.UpdateSlotEquips(Player, room);
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00029740 File Offset: 0x00027940
		public static void UpdateSlotEquips(Account Player, RoomModel Room)
		{
			SlotModel slotModel;
			if (Room.GetSlot(Player.SlotId, out slotModel))
			{
				slotModel.Equipment = Player.Equipment;
			}
			Room.UpdateSlotsInfo();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00029770 File Offset: 0x00027970
		public static int GetSlotsFlag(RoomModel Room, bool OnlyNoSpectators, bool MissionSuccess)
		{
			if (Room == null)
			{
				return 0;
			}
			int num = 0;
			foreach (SlotModel slotModel in Room.Slots)
			{
				if (slotModel.State >= SlotState.LOAD && ((MissionSuccess && slotModel.MissionsCompleted) || (!MissionSuccess && (!OnlyNoSpectators || !slotModel.Spectator))))
				{
					num += slotModel.Flag;
				}
			}
			return num;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000297CC File Offset: 0x000279CC
		public static void GetBattleResult(RoomModel Room, out int MissionFlag, out int SlotFlag, out byte[] Data)
		{
			MissionFlag = 0;
			SlotFlag = 0;
			Data = new byte[306];
			if (Room == null)
			{
				return;
			}
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				foreach (SlotModel slotModel in Room.Slots)
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
				foreach (SlotModel slotModel2 in Room.Slots)
				{
					syncServerPacket.WriteH((ushort)slotModel2.Exp);
				}
				foreach (SlotModel slotModel3 in Room.Slots)
				{
					syncServerPacket.WriteH((ushort)slotModel3.Gold);
				}
				foreach (SlotModel slotModel4 in Room.Slots)
				{
					syncServerPacket.WriteC((byte)slotModel4.BonusFlags);
				}
				foreach (SlotModel slotModel5 in Room.Slots)
				{
					syncServerPacket.WriteH((ushort)slotModel5.BonusCafeExp);
					syncServerPacket.WriteH((ushort)slotModel5.BonusItemExp);
					syncServerPacket.WriteH((ushort)slotModel5.BonusEventExp);
				}
				foreach (SlotModel slotModel6 in Room.Slots)
				{
					syncServerPacket.WriteH((ushort)slotModel6.BonusCafePoint);
					syncServerPacket.WriteH((ushort)slotModel6.BonusItemPoint);
					syncServerPacket.WriteH((ushort)slotModel6.BonusEventPoint);
				}
				Data = syncServerPacket.ToArray();
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00029968 File Offset: 0x00027B68
		public static bool DiscountPlayerItems(SlotModel Slot, Account Player)
		{
			bool flag3;
			try
			{
				bool flag = false;
				bool flag2 = false;
				uint num = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
				List<ItemsModel> list = new List<ItemsModel>();
				List<object> list2 = new List<object>();
				int num2 = ((Player.Bonus != null) ? Player.Bonus.Bonuses : 0);
				int num3 = ((Player.Bonus != null) ? Player.Bonus.FreePass : 0);
				List<ItemsModel> items = Player.Inventory.Items;
				lock (items)
				{
					for (int i = 0; i < Player.Inventory.Items.Count; i++)
					{
						ItemsModel itemsModel = Player.Inventory.Items[i];
						if (itemsModel.Equip == ItemEquipType.Durable && Slot.ItemUsages.Contains(itemsModel.Id) && !Slot.SpecGM)
						{
							if (itemsModel.Count <= num && (itemsModel.Id == 800216 || itemsModel.Id == 2700013 || itemsModel.Id == 800169))
							{
								DaoManagerSQL.DeletePlayerInventoryItem(itemsModel.ObjectId, Player.PlayerId);
							}
							ItemsModel itemsModel2 = itemsModel;
							uint num4 = itemsModel2.Count - 1U;
							itemsModel2.Count = num4;
							if (num4 < 1U)
							{
								list2.Add(itemsModel.ObjectId);
								Player.Inventory.Items.RemoveAt(i--);
							}
							else
							{
								list.Add(itemsModel);
								ComDiv.UpdateDB("player_items", "count", (long)((ulong)itemsModel.Count), "object_id", itemsModel.ObjectId, "owner_id", Player.PlayerId);
							}
						}
						else if (itemsModel.Count <= num && itemsModel.Equip == ItemEquipType.Temporary)
						{
							if (itemsModel.Category == ItemCategory.Coupon)
							{
								if (Player.Bonus == null)
								{
									goto IL_5A8;
								}
								if (!Player.Bonus.RemoveBonuses(itemsModel.Id))
								{
									if (itemsModel.Id == 1600014)
									{
										ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", Player.PlayerId);
										Player.Bonus.CrosshairColor = 4;
										flag = true;
									}
									else if (itemsModel.Id == 1600006)
									{
										ComDiv.UpdateDB("accounts", "nick_color", 0, "player_id", Player.PlayerId);
										Player.NickColor = 0;
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK protocol_ROOM_GET_COLOR_NICK_ACK = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(Player.SlotId, Player.NickColor))
											{
												Player.Room.SendPacketToPlayers(protocol_ROOM_GET_COLOR_NICK_ACK);
											}
											Player.Room.UpdateSlotsInfo();
										}
										flag = true;
									}
									else if (itemsModel.Id == 1600009)
									{
										ComDiv.UpdateDB("player_bonus", "fake_rank", 55, "owner_id", Player.PlayerId);
										Player.Bonus.FakeRank = 55;
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_RANK_ACK protocol_ROOM_GET_RANK_ACK = new PROTOCOL_ROOM_GET_RANK_ACK(Player.SlotId, Player.Rank))
											{
												Player.Room.SendPacketToPlayers(protocol_ROOM_GET_RANK_ACK);
											}
											Player.Room.UpdateSlotsInfo();
										}
										flag = true;
									}
									else if (itemsModel.Id == 1600010)
									{
										ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", Player.PlayerId);
										ComDiv.UpdateDB("accounts", "nickname", Player.Bonus.FakeNick, "player_id", Player.PlayerId);
										Player.Nickname = Player.Bonus.FakeNick;
										Player.Bonus.FakeNick = "";
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_NICKNAME_ACK protocol_ROOM_GET_NICKNAME_ACK = new PROTOCOL_ROOM_GET_NICKNAME_ACK(Player.SlotId, Player.Nickname))
											{
												Player.Room.SendPacketToPlayers(protocol_ROOM_GET_NICKNAME_ACK);
											}
											Player.Room.UpdateSlotsInfo();
										}
										flag = true;
									}
									else if (itemsModel.Id == 1600187)
									{
										ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", Player.PlayerId);
										Player.Bonus.MuzzleColor = 0;
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK protocol_ROOM_GET_COLOR_MUZZLE_FLASH_ACK = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(Player.SlotId, Player.Bonus.MuzzleColor))
											{
												Player.Room.SendPacketToPlayers(protocol_ROOM_GET_COLOR_MUZZLE_FLASH_ACK);
											}
										}
										flag = true;
									}
									else if (itemsModel.Id == 1600205)
									{
										ComDiv.UpdateDB("player_bonus", "nick_border_color", 0, "owner_id", Player.PlayerId);
										Player.Bonus.NickBorderColor = 0;
										if (Player.Room != null)
										{
											using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK protocol_ROOM_GET_NICK_OUTLINE_COLOR_ACK = new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(Player.SlotId, Player.Bonus.NickBorderColor))
											{
												Player.Room.SendPacketToPlayers(protocol_ROOM_GET_NICK_OUTLINE_COLOR_ACK);
											}
										}
										flag = true;
									}
								}
								CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemsModel.Id);
								if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && Player.Effects.HasFlag(couponEffect.EffectFlag))
								{
									Player.Effects -= (long)couponEffect.EffectFlag;
									flag2 = true;
								}
							}
							list2.Add(itemsModel.ObjectId);
							Player.Inventory.Items.RemoveAt(i--);
						}
						else if (itemsModel.Count == 0U)
						{
							list2.Add(itemsModel.ObjectId);
							Player.Inventory.Items.RemoveAt(i--);
						}
						IL_5A8:;
					}
				}
				if (list2.Count > 0)
				{
					for (int j = 0; j < list2.Count; j++)
					{
						ItemsModel item = Player.Inventory.GetItem((long)list2[j]);
						if (item != null && item.Category == ItemCategory.Character && ComDiv.GetIdStatics(item.Id, 1) == 6)
						{
							AllUtils.smethod_3(Player, item.Id);
						}
						Player.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(1U, (long)list2[j]));
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
				int num5 = ComDiv.CheckEquipedItems(Player.Equipment, Player.Inventory.Items, false);
				if (num5 > 0)
				{
					DBQuery dbquery = new DBQuery();
					if ((num5 & 2) == 2)
					{
						ComDiv.UpdateWeapons(Player.Equipment, dbquery);
					}
					if ((num5 & 1) == 1)
					{
						ComDiv.UpdateChars(Player.Equipment, dbquery);
					}
					if ((num5 & 3) == 3)
					{
						ComDiv.UpdateItems(Player.Equipment, dbquery);
					}
					ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dbquery.GetTables(), dbquery.GetValues());
					Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK(Player, Slot));
					Slot.Equipment = Player.Equipment;
				}
				flag3 = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0002A234 File Offset: 0x00028434
		private static void smethod_3(Account account_0, int int_0)
		{
			CharacterModel character = account_0.Character.GetCharacter(int_0);
			if (character != null)
			{
				int num = 0;
				foreach (CharacterModel characterModel in account_0.Character.Characters)
				{
					if (characterModel.Slot != character.Slot)
					{
						characterModel.Slot = num;
						DaoManagerSQL.UpdatePlayerCharacter(num, characterModel.ObjectId, account_0.PlayerId);
						num++;
					}
				}
				if (DaoManagerSQL.DeletePlayerCharacter(character.ObjectId, account_0.PlayerId))
				{
					account_0.Character.RemoveCharacter(character);
				}
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0002A2E8 File Offset: 0x000284E8
		public static void TryBalancePlayer(RoomModel Room, Account Player, bool InBattle, ref SlotModel MySlot)
		{
			SlotModel slot = Room.GetSlot(Player.SlotId);
			if (slot == null)
			{
				return;
			}
			TeamEnum team = slot.Team;
			TeamEnum balanceTeamIdx = AllUtils.GetBalanceTeamIdx(Room, InBattle, team);
			if (team != balanceTeamIdx)
			{
				if (balanceTeamIdx != TeamEnum.ALL_TEAM)
				{
					SlotModel slotModel = null;
					int[] array = ((team == TeamEnum.CT_TEAM) ? Room.FR_TEAM : Room.CT_TEAM);
					for (int i = 0; i < array.Length; i++)
					{
						SlotModel slotModel2 = Room.Slots[array[i]];
						if (slotModel2.State != SlotState.CLOSE && slotModel2.PlayerId == 0L)
						{
							slotModel = slotModel2;
							IL_80:
							if (slotModel != null)
							{
								List<SlotChange> list = new List<SlotChange>();
								SlotModel[] slots = Room.Slots;
								lock (slots)
								{
									Room.SwitchSlots(list, slotModel.Id, slot.Id, false);
								}
								if (list.Count > 0)
								{
									Player.SlotId = slot.Id;
									MySlot = slot;
									using (PROTOCOL_ROOM_TEAM_BALANCE_ACK protocol_ROOM_TEAM_BALANCE_ACK = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(list, Room.LeaderSlot, 1))
									{
										Room.SendPacketToPlayers(protocol_ROOM_TEAM_BALANCE_ACK);
									}
									Room.UpdateSlotsInfo();
								}
							}
							return;
						}
					}
					goto IL_80;
				}
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0002A41C File Offset: 0x0002861C
		public static TeamEnum GetBalanceTeamIdx(RoomModel Room, bool InBattle, TeamEnum PlayerTeamIdx)
		{
			int num = ((!InBattle || PlayerTeamIdx != TeamEnum.FR_TEAM) ? 0 : 1);
			int num2;
			if (InBattle)
			{
				if (PlayerTeamIdx == TeamEnum.CT_TEAM)
				{
					num2 = 1;
					goto IL_18;
				}
			}
			num2 = 0;
			IL_18:
			int num3 = num2;
			foreach (SlotModel slotModel in Room.Slots)
			{
				if ((slotModel.State == SlotState.NORMAL && !InBattle) || (slotModel.State >= SlotState.LOAD && InBattle))
				{
					if (slotModel.Team == TeamEnum.FR_TEAM)
					{
						num++;
					}
					else
					{
						num3++;
					}
				}
			}
			if (num + 1 < num3)
			{
				return TeamEnum.FR_TEAM;
			}
			if (num3 + 1 >= num + 1)
			{
				return TeamEnum.ALL_TEAM;
			}
			return TeamEnum.CT_TEAM;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00005920 File Offset: 0x00003B20
		public static int GetNewSlotId(int SlotIdx)
		{
			if (SlotIdx % 2 != 0)
			{
				return SlotIdx - 1;
			}
			return SlotIdx + 1;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0002A4A4 File Offset: 0x000286A4
		public static void GetXmasReward(Account Player)
		{
			EventXmasModel runningEvent = EventXmasXML.GetRunningEvent();
			if (runningEvent == null)
			{
				return;
			}
			PlayerEvent @event = Player.Event;
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			if (@event != null && (@event.LastXmasDate <= runningEvent.BeginDate || @event.LastXmasDate > runningEvent.EndedDate) && ComDiv.UpdateDB("player_events", "last_xmas_date", (long)((ulong)num), "owner_id", Player.PlayerId))
			{
				@event.LastXmasDate = num;
				GoodsItem good = ShopManager.GetGood(runningEvent.GoodId);
				if (good != null)
				{
					if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && Player.Character.GetCharacter(good.Item.Id) == null)
					{
						AllUtils.CreateCharacter(Player, good.Item);
					}
					else
					{
						Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, good.Item));
					}
					Player.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(Player, good.Item));
				}
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002A594 File Offset: 0x00028794
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

		// Token: 0x0600054F RID: 1359 RVA: 0x0002A5C4 File Offset: 0x000287C4
		public static void BattleEndRoundPlayersCount(RoomModel Room)
		{
			if (!Room.RoundTime.IsTimer() && (Room.RoomType == RoomCondition.Bomb || Room.RoomType == RoomCondition.Annihilation || Room.RoomType == RoomCondition.Destroy || Room.RoomType == RoomCondition.Ace))
			{
				int num;
				int num2;
				int num3;
				int num4;
				Room.GetPlayingPlayers(true, out num, out num2, out num3, out num4);
				AllUtils.smethod_4(Room, ref num, ref num2, ref num3, ref num4);
				if (num3 == num)
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
					AllUtils.BattleEndRound(Room, Room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM, false, null, null);
					return;
				}
				if (num4 == num2)
				{
					if (Room.SwapRound)
					{
						Room.CTRounds++;
					}
					else
					{
						Room.FRRounds++;
					}
					AllUtils.BattleEndRound(Room, Room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, true, null, null);
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000592E File Offset: 0x00003B2E
		public static void BattleEndKills(RoomModel room)
		{
			AllUtils.smethod_5(room, room.IsBotMode());
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000593C File Offset: 0x00003B3C
		public static void BattleEndKills(RoomModel room, bool isBotMode)
		{
			AllUtils.smethod_5(room, isBotMode);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0002A6B0 File Offset: 0x000288B0
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
				TeamEnum winnerTeam = AllUtils.GetWinnerTeam(roomModel_0);
				roomModel_0.CalculateResult(winnerTeam, bool_0);
				int num;
				int num2;
				byte[] array;
				AllUtils.GetBattleResult(roomModel_0, out num, out num2, out array);
				using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_BATTLE_MISSION_ROUND_END_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, winnerTeam, RoundEndType.TimeOut))
				{
					byte[] completeBytes = protocol_BATTLE_MISSION_ROUND_END_ACK.GetCompleteBytes("AllUtils.BaseEndByKills");
					foreach (Account account in allPlayers)
					{
						SlotModel slot = roomModel_0.GetSlot(account.SlotId);
						if (slot != null)
						{
							if (slot.State == SlotState.BATTLE)
							{
								account.SendCompletePacket(completeBytes, protocol_BATTLE_MISSION_ROUND_END_ACK.GetType().Name);
							}
							account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, winnerTeam, num2, num, bool_0, array));
							AllUtils.UpdateSeasonPass(account);
						}
					}
				}
			}
			AllUtils.ResetBattleInfo(roomModel_0);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00005945 File Offset: 0x00003B45
		public static void BattleEndKillsFreeForAll(RoomModel room)
		{
			AllUtils.smethod_6(room);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002A7CC File Offset: 0x000289CC
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
				int num2;
				int num3;
				byte[] array2;
				AllUtils.GetBattleResult(roomModel_0, out num2, out num3, out array2);
				using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_BATTLE_MISSION_ROUND_END_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, num, RoundEndType.FreeForAll))
				{
					byte[] completeBytes = protocol_BATTLE_MISSION_ROUND_END_ACK.GetCompleteBytes("AllUtils.BaseEndByKills");
					foreach (Account account in allPlayers)
					{
						SlotModel slot = roomModel_0.GetSlot(account.SlotId);
						if (slot != null)
						{
							if (slot.State == SlotState.BATTLE)
							{
								account.SendCompletePacket(completeBytes, protocol_BATTLE_MISSION_ROUND_END_ACK.GetType().Name);
							}
							account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, num, num3, num2, false, array2));
							AllUtils.UpdateSeasonPass(account);
						}
					}
				}
			}
			AllUtils.ResetBattleInfo(roomModel_0);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0002A934 File Offset: 0x00028B34
		public static bool CheckClanMatchRestrict(RoomModel room)
		{
			if (room.ChannelType == ChannelType.Clan)
			{
				using (IEnumerator<ClanTeam> enumerator = AllUtils.smethod_7(room).Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ClanTeam clanTeam = enumerator.Current;
						if (clanTeam.PlayersFR >= 1 && clanTeam.PlayersCT >= 1)
						{
							room.BlockedClan = true;
							return true;
						}
					}
					return false;
				}
				bool flag;
				return flag;
			}
			return false;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000594D File Offset: 0x00003B4D
		public static bool Have2ClansToClanMatch(RoomModel room)
		{
			return AllUtils.smethod_7(room).Count == 2;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002A9AC File Offset: 0x00028BAC
		public static bool HavePlayersToClanMatch(RoomModel room)
		{
			SortedList<int, ClanTeam> sortedList = AllUtils.smethod_7(room);
			bool flag = false;
			bool flag2 = false;
			foreach (ClanTeam clanTeam in sortedList.Values)
			{
				if (clanTeam.PlayersFR >= 4)
				{
					flag = true;
				}
				else if (clanTeam.PlayersCT >= 4)
				{
					flag2 = true;
				}
			}
			return flag && flag2;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0002AA18 File Offset: 0x00028C18
		private static SortedList<int, ClanTeam> smethod_7(RoomModel roomModel_0)
		{
			SortedList<int, ClanTeam> sortedList = new SortedList<int, ClanTeam>();
			for (int i = 0; i < roomModel_0.GetAllPlayers().Count; i++)
			{
				Account account = roomModel_0.GetAllPlayers()[i];
				if (account.ClanId != 0)
				{
					ClanTeam clanTeam;
					if (sortedList.TryGetValue(account.ClanId, out clanTeam) && clanTeam != null)
					{
						if (account.SlotId % 2 == 0)
						{
							ClanTeam clanTeam2 = clanTeam;
							int num = clanTeam2.PlayersFR;
							clanTeam2.PlayersFR = num + 1;
						}
						else
						{
							ClanTeam clanTeam3 = clanTeam;
							int num = clanTeam3.PlayersCT;
							clanTeam3.PlayersCT = num + 1;
						}
					}
					else
					{
						clanTeam = new ClanTeam
						{
							ClanId = account.ClanId
						};
						if (account.SlotId % 2 == 0)
						{
							ClanTeam clanTeam4 = clanTeam;
							int num = clanTeam4.PlayersFR;
							clanTeam4.PlayersFR = num + 1;
						}
						else
						{
							ClanTeam clanTeam5 = clanTeam;
							int num = clanTeam5.PlayersCT;
							clanTeam5.PlayersCT = num + 1;
						}
						sortedList.Add(account.ClanId, clanTeam);
					}
				}
			}
			return sortedList;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0002AAF4 File Offset: 0x00028CF4
		public static void PlayTimeEvent(Account Player, EventPlaytimeModel EvPlaytime, bool IsBotMode, SlotModel Slot, long PlayedTime)
		{
			try
			{
				bool room = Player.Room != null;
				PlayerEvent @event = Player.Event;
				if (room && @event != null)
				{
					int minutes = EvPlaytime.Minutes1;
					int minutes2 = EvPlaytime.Minutes2;
					int minutes3 = EvPlaytime.Minutes3;
					if (minutes == 0 && minutes2 == 0 && minutes3 == 0)
					{
						CLogger.Print(string.Format("Event Playtime Disabled Due To: 0 Value! (Minutes1: {0}; Minutes2: {1}; Minutes3: {2}", minutes, minutes2, minutes3), LoggerType.Warning, null);
					}
					else
					{
						long lastPlaytimeValue = @event.LastPlaytimeValue;
						long num = (long)@event.LastPlaytimeFinish;
						long num2 = (long)((ulong)@event.LastPlaytimeDate);
						if (@event.LastPlaytimeFinish >= 0 && @event.LastPlaytimeFinish <= 2)
						{
							@event.LastPlaytimeValue += PlayedTime;
							int num3 = ((@event.LastPlaytimeFinish == 0) ? EvPlaytime.Minutes1 : ((@event.LastPlaytimeFinish == 1) ? EvPlaytime.Minutes2 : ((@event.LastPlaytimeFinish == 2) ? EvPlaytime.Minutes3 : 0)));
							if (num3 == 0)
							{
								return;
							}
							int num4 = num3 * 60;
							if (@event.LastPlaytimeValue >= (long)num4)
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
											AllUtils.CreateCharacter(Player, good.Item);
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
						if (@event.LastPlaytimeValue != lastPlaytimeValue || (long)@event.LastPlaytimeFinish != num || (ulong)@event.LastPlaytimeDate != (ulong)num2)
						{
							EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, @event);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("[AllUtils.PlayTimeEvent] " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002AD60 File Offset: 0x00028F60
		public static void CompleteMission(RoomModel Room, SlotModel Slot, FragInfos Kills, MissionType AutoComplete, int MoreInfo)
		{
			try
			{
				Account playerBySlot = Room.GetPlayerBySlot(Slot);
				if (playerBySlot != null)
				{
					AllUtils.smethod_8(Room, playerBySlot, Slot, Kills, AutoComplete, MoreInfo);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("[AllUtils.CompleteMission1] " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		public static void CompleteMission(RoomModel room, SlotModel slot, MissionType autoComplete, int moreInfo)
		{
			try
			{
				Account playerBySlot = room.GetPlayerBySlot(slot);
				if (playerBySlot != null)
				{
					AllUtils.smethod_9(room, playerBySlot, slot, autoComplete, moreInfo);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("[AllUtils.CompleteMission2] " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000595D File Offset: 0x00003B5D
		public static void CompleteMission(RoomModel room, Account player, SlotModel slot, FragInfos kills, MissionType autoComplete, int moreInfo)
		{
			AllUtils.smethod_8(room, player, slot, kills, autoComplete, moreInfo);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000596C File Offset: 0x00003B6C
		public static void CompleteMission(RoomModel room, Account player, SlotModel slot, MissionType autoComplete, int moreInfo)
		{
			AllUtils.smethod_9(room, player, slot, autoComplete, moreInfo);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002AE04 File Offset: 0x00029004
		private static void smethod_8(RoomModel roomModel_0, Account account_0, SlotModel slotModel_0, FragInfos fragInfos_0, MissionType missionType_0, int int_0)
		{
			try
			{
				PlayerMissions missions = slotModel_0.Missions;
				if (missions != null)
				{
					int currentMissionId = missions.GetCurrentMissionId();
					int currentCard = missions.GetCurrentCard();
					if (currentMissionId > 0 && !missions.SelectedCard)
					{
						List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
						if (cards.Count != 0)
						{
							KillingMessage allKillFlags = fragInfos_0.GetAllKillFlags();
							byte[] currentMissionList = missions.GetCurrentMissionList();
							ClassType idStatics = (ClassType)ComDiv.GetIdStatics(fragInfos_0.WeaponId, 2);
							ClassType classType = AllUtils.smethod_1(idStatics);
							int idStatics2 = ComDiv.GetIdStatics(fragInfos_0.WeaponId, 3);
							ClassType classType2 = (ClassType)((int_0 > 0) ? ComDiv.GetIdStatics(fragInfos_0.WeaponId, 2) : 0);
							ClassType classType3 = ((int_0 > 0) ? AllUtils.smethod_1(classType2) : ClassType.Unknown);
							int num = ((int_0 > 0) ? ComDiv.GetIdStatics(int_0, 3) : 0);
							foreach (MissionCardModel missionCardModel in cards)
							{
								int num2 = 0;
								if (missionCardModel.MapId == 0 || missionCardModel.MapId == (int)roomModel_0.MapId)
								{
									if (fragInfos_0.Frags.Count > 0)
									{
										if (missionCardModel.MissionType != MissionType.KILL && (missionCardModel.MissionType != MissionType.CHAINSTOPPER || !allKillFlags.HasFlag(KillingMessage.ChainStopper)) && (missionCardModel.MissionType != MissionType.CHAINSLUGGER || !allKillFlags.HasFlag(KillingMessage.ChainSlugger)) && (missionCardModel.MissionType != MissionType.CHAINKILLER || slotModel_0.KillsOnLife < 4) && (missionCardModel.MissionType != MissionType.TRIPLE_KILL || slotModel_0.KillsOnLife != 3) && (missionCardModel.MissionType != MissionType.DOUBLE_KILL || slotModel_0.KillsOnLife != 2) && (missionCardModel.MissionType != MissionType.HEADSHOT || (!allKillFlags.HasFlag(KillingMessage.Headshot) && !allKillFlags.HasFlag(KillingMessage.ChainHeadshot))) && (missionCardModel.MissionType != MissionType.CHAINHEADSHOT || !allKillFlags.HasFlag(KillingMessage.ChainHeadshot)) && (missionCardModel.MissionType != MissionType.PIERCING || !allKillFlags.HasFlag(KillingMessage.PiercingShot)) && (missionCardModel.MissionType != MissionType.MASS_KILL || !allKillFlags.HasFlag(KillingMessage.MassKill)))
										{
											if (missionCardModel.MissionType == MissionType.KILL_MAN && roomModel_0.IsDinoMode(""))
											{
												if (slotModel_0.Team == TeamEnum.CT_TEAM && roomModel_0.Rounds == 2)
												{
													goto IL_2AA;
												}
												if (slotModel_0.Team == TeamEnum.FR_TEAM && roomModel_0.Rounds == 1)
												{
													goto IL_2AA;
												}
											}
											if (missionCardModel.MissionType == MissionType.KILL_WEAPONCLASS || (missionCardModel.MissionType == MissionType.DOUBLE_KILL_WEAPONCLASS && slotModel_0.KillsOnLife == 2) || (missionCardModel.MissionType == MissionType.TRIPLE_KILL_WEAPONCLASS && slotModel_0.KillsOnLife == 3))
											{
												num2 = AllUtils.smethod_11(missionCardModel, fragInfos_0);
												goto IL_2EC;
											}
											goto IL_2EC;
										}
										IL_2AA:
										num2 = AllUtils.smethod_10(missionCardModel, idStatics, classType, idStatics2, fragInfos_0);
									}
									else if (missionCardModel.MissionType == MissionType.DEATHBLOW && missionType_0 == MissionType.DEATHBLOW)
									{
										num2 = AllUtils.smethod_13(missionCardModel, classType2, classType3, num);
									}
									else if (missionCardModel.MissionType == missionType_0)
									{
										num2 = 1;
									}
								}
								IL_2EC:
								if (num2 != 0)
								{
									int arrayIdx = missionCardModel.ArrayIdx;
									if ((int)(currentMissionList[arrayIdx] + 1) <= missionCardModel.MissionLimit)
									{
										slotModel_0.MissionsCompleted = true;
										byte[] array = currentMissionList;
										int num3 = arrayIdx;
										array[num3] += (byte)num2;
										if ((int)currentMissionList[arrayIdx] > missionCardModel.MissionLimit)
										{
											currentMissionList[arrayIdx] = (byte)missionCardModel.MissionLimit;
										}
										int num4 = (int)currentMissionList[arrayIdx];
										account_0.SendPacket(new PROTOCOL_BASE_QUEST_CHANGE_ACK(num4, missionCardModel));
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002B1CC File Offset: 0x000293CC
		private static void smethod_9(RoomModel roomModel_0, Account account_0, SlotModel slotModel_0, MissionType missionType_0, int int_0)
		{
			try
			{
				PlayerMissions missions = slotModel_0.Missions;
				if (missions != null)
				{
					int currentMissionId = missions.GetCurrentMissionId();
					int currentCard = missions.GetCurrentCard();
					if (currentMissionId > 0 && !missions.SelectedCard)
					{
						List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
						if (cards.Count != 0)
						{
							byte[] currentMissionList = missions.GetCurrentMissionList();
							ClassType classType = (ClassType)((int_0 > 0) ? ComDiv.GetIdStatics(int_0, 2) : 0);
							ClassType classType2 = ((int_0 > 0) ? AllUtils.smethod_1(classType) : ClassType.Unknown);
							int num = ((int_0 > 0) ? ComDiv.GetIdStatics(int_0, 3) : 0);
							foreach (MissionCardModel missionCardModel in cards)
							{
								int num2 = 0;
								if (missionCardModel.MapId == 0 || missionCardModel.MapId == (int)roomModel_0.MapId)
								{
									if (missionCardModel.MissionType == MissionType.DEATHBLOW && missionType_0 == MissionType.DEATHBLOW)
									{
										num2 = AllUtils.smethod_13(missionCardModel, classType, classType2, num);
									}
									else if (missionCardModel.MissionType == missionType_0)
									{
										num2 = 1;
									}
								}
								if (num2 != 0)
								{
									int arrayIdx = missionCardModel.ArrayIdx;
									if ((int)(currentMissionList[arrayIdx] + 1) <= missionCardModel.MissionLimit)
									{
										slotModel_0.MissionsCompleted = true;
										byte[] array = currentMissionList;
										int num3 = arrayIdx;
										array[num3] += (byte)num2;
										if ((int)currentMissionList[arrayIdx] > missionCardModel.MissionLimit)
										{
											currentMissionList[arrayIdx] = (byte)missionCardModel.MissionLimit;
										}
										int num4 = (int)currentMissionList[arrayIdx];
										account_0.SendPacket(new PROTOCOL_BASE_QUEST_CHANGE_ACK(num4, missionCardModel));
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002B388 File Offset: 0x00029588
		private static int smethod_10(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0, FragInfos fragInfos_0)
		{
			int num = 0;
			if ((missionCardModel_0.WeaponReqId == 0 || missionCardModel_0.WeaponReqId == int_0) && (missionCardModel_0.WeaponReq == ClassType.Unknown || missionCardModel_0.WeaponReq == classType_0 || missionCardModel_0.WeaponReq == classType_1))
			{
				using (List<FragModel>.Enumerator enumerator = fragInfos_0.Frags.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.VictimSlot % 2 != fragInfos_0.KillerSlot % 2)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002B41C File Offset: 0x0002961C
		private static int smethod_11(MissionCardModel missionCardModel_0, FragInfos fragInfos_0)
		{
			int num = 0;
			foreach (FragModel fragModel in fragInfos_0.Frags)
			{
				if (fragModel.VictimSlot % 2 != fragInfos_0.KillerSlot % 2 && (missionCardModel_0.WeaponReq == ClassType.Unknown || missionCardModel_0.WeaponReq == (ClassType)fragModel.WeaponClass || missionCardModel_0.WeaponReq == AllUtils.smethod_1((ClassType)fragModel.WeaponClass)))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00005979 File Offset: 0x00003B79
		private static int smethod_12(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0, int int_1, FragModel fragModel_0)
		{
			if ((missionCardModel_0.WeaponReqId == 0 || missionCardModel_0.WeaponReqId == int_0) && (missionCardModel_0.WeaponReq == ClassType.Unknown || missionCardModel_0.WeaponReq == classType_0 || missionCardModel_0.WeaponReq == classType_1) && (int)(fragModel_0.VictimSlot % 2) != int_1 % 2)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000059B8 File Offset: 0x00003BB8
		private static int smethod_13(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0)
		{
			if (missionCardModel_0.WeaponReqId == 0 || missionCardModel_0.WeaponReqId == int_0)
			{
				if (missionCardModel_0.WeaponReq != ClassType.Unknown && missionCardModel_0.WeaponReq != classType_0)
				{
					if (missionCardModel_0.WeaponReq != classType_1)
					{
						return 0;
					}
				}
				return 1;
			}
			return 0;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0002B4AC File Offset: 0x000296AC
		public static void EnableQuestMission(Account Player)
		{
			PlayerEvent @event = Player.Event;
			if (@event == null)
			{
				return;
			}
			if (@event.LastQuestFinish == 0 && EventQuestXML.GetRunningEvent() != null)
			{
				Player.Mission.Mission4 = 13;
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0002B4E0 File Offset: 0x000296E0
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
				return;
			}
			if (Room.LeaderSlot % 2 == 0)
			{
				TotalEnemys = CTPlayers;
				return;
			}
			TotalEnemys = FRPlayers;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0002B560 File Offset: 0x00029760
		public static bool CompetitiveMatchCheck(Account Player, RoomModel Room, out uint Error)
		{
			if (Room.Competitive)
			{
				foreach (SlotModel slotModel in Room.Slots)
				{
					if (slotModel != null && slotModel.State != SlotState.CLOSE && slotModel.State < SlotState.READY)
					{
						Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, Translation.GetLabel("CompetitiveFullSlot")));
						Error = 2147487858U;
						return true;
					}
				}
			}
			Error = 0U;
			return false;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000059EA File Offset: 0x00003BEA
		public static bool ClanMatchCheck(RoomModel Room, ChannelType Type, int TotalEnemys, out uint Error)
		{
			if (!ConfigLoader.IsTestMode)
			{
				if (Type == ChannelType.Clan)
				{
					if (!AllUtils.Have2ClansToClanMatch(Room))
					{
						Error = 2147487857U;
						return true;
					}
					if (TotalEnemys > 0 && !AllUtils.HavePlayersToClanMatch(Room))
					{
						Error = 2147487858U;
						return true;
					}
					Error = 0U;
					return false;
				}
			}
			Error = 0U;
			return false;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0002B5E4 File Offset: 0x000297E4
		public static void TryBalanceTeams(RoomModel Room)
		{
			if (Room.BalanceType != TeamBalance.Count || Room.IsBotMode())
			{
				return;
			}
			TeamEnum balanceTeamIdx = AllUtils.GetBalanceTeamIdx(Room, false, TeamEnum.ALL_TEAM);
			if (balanceTeamIdx == TeamEnum.ALL_TEAM)
			{
				return;
			}
			int[] array = ((balanceTeamIdx == TeamEnum.CT_TEAM) ? Room.FR_TEAM : Room.CT_TEAM);
			SlotModel slotModel = null;
			for (int i = array.Length - 1; i >= 0; i--)
			{
				SlotModel slotModel2 = Room.Slots[array[i]];
				if (slotModel2.State == SlotState.READY && Room.LeaderSlot != slotModel2.Id)
				{
					slotModel = slotModel2;
					IL_79:
					Account account;
					if (slotModel != null && Room.GetPlayerBySlot(slotModel, out account))
					{
						AllUtils.TryBalancePlayer(Room, account, false, ref slotModel);
					}
					return;
				}
			}
			goto IL_79;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002B684 File Offset: 0x00029884
		public static void FreepassEffect(Account Player, SlotModel Slot, RoomModel Room, bool IsBotMode)
		{
			DBQuery dbquery = new DBQuery();
			if (Player.Bonus.FreePass != 0)
			{
				if (Player.Bonus.FreePass != 1 || Room.ChannelType != ChannelType.Clan)
				{
					if (Room.State != RoomState.BATTLE)
					{
						return;
					}
					int num = 0;
					int num2 = 0;
					if (IsBotMode)
					{
						int num3 = (int)Room.IngameAiLevel * (150 + Slot.AllDeaths);
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
						if (Room.RoomType != RoomCondition.Bomb && Room.RoomType != RoomCondition.FreeForAll)
						{
							if (Room.RoomType != RoomCondition.Destroy)
							{
								num = (int)((double)Slot.Score + (double)num5 / 2.5 + (double)Slot.AllDeaths * 1.8 + (double)(Slot.Objects * 20));
								num2 = (int)((double)Slot.Score + (double)num5 / 3.0 + (double)Slot.AllDeaths * 1.8 + (double)(Slot.Objects * 20));
								goto IL_191;
							}
						}
						num = (int)((double)Slot.Score + (double)num5 / 2.5 + (double)Slot.AllDeaths * 2.2 + (double)(Slot.Objects * 20));
						num2 = (int)((double)Slot.Score + (double)num5 / 3.0 + (double)Slot.AllDeaths * 2.2 + (double)(Slot.Objects * 20));
					}
					IL_191:
					Player.Exp += ((ConfigLoader.MaxExpReward < num) ? ConfigLoader.MaxExpReward : num);
					Player.Gold += ((ConfigLoader.MaxGoldReward < num2) ? ConfigLoader.MaxGoldReward : num2);
					if (num2 > 0)
					{
						dbquery.AddQuery("gold", Player.Gold);
					}
					if (num > 0)
					{
						dbquery.AddQuery("experience", Player.Exp);
						goto IL_2DB;
					}
					goto IL_2DB;
				}
			}
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
				dbquery.AddQuery("gold", Player.Gold);
			}
			string text = "player_stat_basics";
			string text2 = "owner_id";
			object obj = Player.PlayerId;
			string text3 = "escapes_count";
			StatisticTotal basic = Player.Statistic.Basic;
			int num6 = basic.EscapesCount + 1;
			basic.EscapesCount = num6;
			ComDiv.UpdateDB(text, text2, obj, text3, num6);
			string text4 = "player_stat_seasons";
			string text5 = "owner_id";
			object obj2 = Player.PlayerId;
			string text6 = "escapes_count";
			StatisticSeason season = Player.Statistic.Season;
			num6 = season.EscapesCount + 1;
			season.EscapesCount = num6;
			ComDiv.UpdateDB(text4, text5, obj2, text6, num6);
			IL_2DB:
			ComDiv.UpdateDB("accounts", "player_id", Player.PlayerId, dbquery.GetTables(), dbquery.GetValues());
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002B994 File Offset: 0x00029B94
		public static void LeaveHostGiveBattlePVE(RoomModel Room, Account Player)
		{
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count == 0)
			{
				return;
			}
			int leaderSlot = Room.LeaderSlot;
			Room.SetNewLeader(-1, SlotState.BATTLE_READY, leaderSlot, true);
			using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
			{
				using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK protocol_BATTLE_LEAVEP2PSERVER_ACK = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(Room))
				{
					byte[] completeBytes = protocol_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-1");
					byte[] completeBytes2 = protocol_BATTLE_LEAVEP2PSERVER_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-2");
					foreach (Account account in allPlayers)
					{
						SlotModel slot = Room.GetSlot(account.SlotId);
						if (slot != null)
						{
							if (slot.State >= SlotState.PRESTART)
							{
								account.SendCompletePacket(completeBytes2, protocol_BATTLE_LEAVEP2PSERVER_ACK.GetType().Name);
							}
							account.SendCompletePacket(completeBytes, protocol_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
						}
					}
				}
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002BAA0 File Offset: 0x00029CA0
		public static void LeaveHostEndBattlePVE(RoomModel Room, Account Player)
		{
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
				{
					byte[] completeBytes = protocol_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-3");
					TeamEnum winnerTeam = AllUtils.GetWinnerTeam(Room);
					int num;
					int num2;
					byte[] array;
					AllUtils.GetBattleResult(Room, out num, out num2, out array);
					foreach (Account account in allPlayers)
					{
						account.SendCompletePacket(completeBytes, protocol_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
						account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, winnerTeam, num2, num, true, array));
						AllUtils.UpdateSeasonPass(account);
					}
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0002BB6C File Offset: 0x00029D6C
		public static void LeaveHostEndBattlePVP(RoomModel Room, Account Player, int TeamFR, int TeamCT, out bool IsFinished)
		{
			IsFinished = true;
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				TeamEnum winnerTeam = AllUtils.GetWinnerTeam(Room, TeamFR, TeamCT);
				if (Room.State == RoomState.BATTLE)
				{
					Room.CalculateResult(winnerTeam, false);
				}
				using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
				{
					byte[] completeBytes = protocol_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-4");
					int num;
					int num2;
					byte[] array;
					AllUtils.GetBattleResult(Room, out num, out num2, out array);
					foreach (Account account in allPlayers)
					{
						account.SendCompletePacket(completeBytes, protocol_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
						account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, winnerTeam, num2, num, false, array));
						AllUtils.UpdateSeasonPass(account);
					}
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0002BC50 File Offset: 0x00029E50
		public static void LeaveHostGiveBattlePVP(RoomModel Room, Account Player)
		{
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count == 0)
			{
				return;
			}
			int leaderSlot = Room.LeaderSlot;
			SlotState slotState = ((Room.State == RoomState.BATTLE) ? SlotState.BATTLE_READY : SlotState.READY);
			Room.SetNewLeader(-1, slotState, leaderSlot, true);
			using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK protocol_BATTLE_LEAVEP2PSERVER_ACK = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(Room))
			{
				using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
				{
					byte[] completeBytes = protocol_BATTLE_LEAVEP2PSERVER_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-6");
					byte[] completeBytes2 = protocol_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-7");
					foreach (Account account in allPlayers)
					{
						if (Room.Slots[account.SlotId].State >= SlotState.PRESTART)
						{
							account.SendCompletePacket(completeBytes, protocol_BATTLE_LEAVEP2PSERVER_ACK.GetType().Name);
						}
						account.SendCompletePacket(completeBytes2, protocol_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
					}
				}
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0002BD68 File Offset: 0x00029F68
		public static void LeavePlayerEndBattlePVP(RoomModel Room, Account Player, int TeamFR, int TeamCT, out bool IsFinished)
		{
			IsFinished = true;
			TeamEnum winnerTeam = AllUtils.GetWinnerTeam(Room, TeamFR, TeamCT);
			List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				if (Room.State == RoomState.BATTLE)
				{
					Room.CalculateResult(winnerTeam, false);
				}
				using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
				{
					byte[] completeBytes = protocol_BATTLE_GIVEUPBATTLE_ACK.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-8");
					int num;
					int num2;
					byte[] array;
					AllUtils.GetBattleResult(Room, out num, out num2, out array);
					foreach (Account account in allPlayers)
					{
						account.SendCompletePacket(completeBytes, protocol_BATTLE_GIVEUPBATTLE_ACK.GetType().Name);
						account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, winnerTeam, num2, num, false, array));
						AllUtils.UpdateSeasonPass(account);
					}
				}
			}
			AllUtils.ResetBattleInfo(Room);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0002BE4C File Offset: 0x0002A04C
		public static void LeavePlayerQuitBattle(RoomModel Room, Account Player)
		{
			using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
			{
				Room.SendPacketToPlayers(protocol_BATTLE_GIVEUPBATTLE_ACK, SlotState.READY, 1);
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002BE88 File Offset: 0x0002A088
		private static int smethod_14(int int_0, SortedList<int, int> sortedList_0)
		{
			int num;
			if (sortedList_0.TryGetValue(int_0, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002BE88 File Offset: 0x0002A088
		private static int smethod_15(int int_0, SortedList<int, int> sortedList_0)
		{
			int num;
			if (sortedList_0.TryGetValue(int_0, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0002BEA4 File Offset: 0x0002A0A4
		private static int smethod_16(Account account_0, int int_0)
		{
			ItemsModel item = account_0.Inventory.GetItem(int_0);
			if (item != null)
			{
				return item.Id;
			}
			return 0;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0002BECC File Offset: 0x0002A0CC
		public static void ValidateAccesoryEquipment(Account Player, int AccessoryId)
		{
			if (Player.Equipment.AccessoryId != AccessoryId)
			{
				Player.Equipment.AccessoryId = AllUtils.smethod_16(Player, AccessoryId);
				ComDiv.UpdateDB("player_equipments", "accesory_id", Player.Equipment.AccessoryId, "owner_id", Player.PlayerId);
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0002BF2C File Offset: 0x0002A12C
		public static void ValidateDisabledCoupon(Account Player, SortedList<int, int> Coupons)
		{
			for (int i = 0; i < Coupons.Keys.Count; i++)
			{
				ItemsModel item = Player.Inventory.GetItem(AllUtils.smethod_14(i, Coupons));
				if (item != null)
				{
					CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(item.Id);
					if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && Player.Effects.HasFlag(couponEffect.EffectFlag))
					{
						Player.Effects -= (long)couponEffect.EffectFlag;
						DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
					}
				}
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		public static void ValidateEnabledCoupon(Account Player, SortedList<int, int> Coupons)
		{
			for (int i = 0; i < Coupons.Keys.Count; i++)
			{
				ItemsModel item = Player.Inventory.GetItem(AllUtils.smethod_14(i, Coupons));
				if (item != null)
				{
					bool flag = Player.Bonus.AddBonuses(item.Id);
					CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(item.Id);
					if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && !Player.Effects.HasFlag(couponEffect.EffectFlag))
					{
						Player.Effects |= couponEffect.EffectFlag;
						DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
					}
					if (flag)
					{
						DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
					}
				}
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00005A28 File Offset: 0x00003C28
		private static bool smethod_17(int int_0, CouponEffects couponEffects_0, ValueTuple<int, CouponEffects, bool> valueTuple_0)
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

		// Token: 0x06000577 RID: 1399 RVA: 0x0002C0A4 File Offset: 0x0002A2A4
		public static bool CheckDuplicateCouponEffects(Account Player, int CouponId)
		{
			bool flag = false;
			foreach (ValueTuple<int, CouponEffects, bool> valueTuple in new List<ValueTuple<int, CouponEffects, bool>>
			{
				new ValueTuple<int, CouponEffects, bool>(1600065, CouponEffects.Defense20 | CouponEffects.Defense10 | CouponEffects.Defense5, true),
				new ValueTuple<int, CouponEffects, bool>(1600079, CouponEffects.Defense90 | CouponEffects.Defense10 | CouponEffects.Defense5, true),
				new ValueTuple<int, CouponEffects, bool>(1600044, CouponEffects.Defense90 | CouponEffects.Defense20 | CouponEffects.Defense5, true),
				new ValueTuple<int, CouponEffects, bool>(1600030, CouponEffects.Defense90 | CouponEffects.Defense20 | CouponEffects.Defense10, true),
				new ValueTuple<int, CouponEffects, bool>(1600078, CouponEffects.JackHollowPoint | CouponEffects.HollowPoint | CouponEffects.FullMetalJack, true),
				new ValueTuple<int, CouponEffects, bool>(1600032, CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint | CouponEffects.FullMetalJack, true),
				new ValueTuple<int, CouponEffects, bool>(1600031, CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint | CouponEffects.HollowPoint, true),
				new ValueTuple<int, CouponEffects, bool>(1600036, CouponEffects.HollowPointPlus | CouponEffects.HollowPoint | CouponEffects.FullMetalJack, true),
				new ValueTuple<int, CouponEffects, bool>(1600028, CouponEffects.HP5, false),
				new ValueTuple<int, CouponEffects, bool>(1600040, CouponEffects.HP10, false),
				new ValueTuple<int, CouponEffects, bool>(1600209, CouponEffects.Camoflage50, false),
				new ValueTuple<int, CouponEffects, bool>(1600208, CouponEffects.Camoflage99, false)
			})
			{
				if (AllUtils.smethod_17(CouponId, Player.Effects, valueTuple))
				{
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002C240 File Offset: 0x0002A440
		public static void ValidateCharacterEquipment(Account Player, PlayerEquipment Equip, int[] EquipmentList, int CharacterId, int[] CharaSlots)
		{
			DBQuery dbquery = new DBQuery();
			CharacterModel character = Player.Character.GetCharacter(CharacterId);
			if (character != null)
			{
				int idStatics = ComDiv.GetIdStatics(character.Id, 1);
				int idStatics2 = ComDiv.GetIdStatics(character.Id, 2);
				int idStatics3 = ComDiv.GetIdStatics(character.Id, 5);
				if (idStatics == 6 && (idStatics2 == 1 || idStatics3 == 632) && CharaSlots[0] == character.Slot)
				{
					if (Equip.CharaRedId != character.Id)
					{
						Equip.CharaRedId = character.Id;
						dbquery.AddQuery("chara_red_side", Equip.CharaRedId);
					}
				}
				else if (idStatics == 6 && (idStatics2 == 2 || idStatics3 == 664) && CharaSlots[1] == character.Slot && Equip.CharaBlueId != character.Id)
				{
					Equip.CharaBlueId = character.Id;
					dbquery.AddQuery("chara_blue_side", Equip.CharaBlueId);
				}
			}
			for (int i = 0; i < EquipmentList.Length; i++)
			{
				int num = AllUtils.smethod_16(Player, EquipmentList[i]);
				switch (i)
				{
				case 0:
					if (num != 0 && Equip.WeaponPrimary != num)
					{
						Equip.WeaponPrimary = num;
						dbquery.AddQuery("weapon_primary", Equip.WeaponPrimary);
					}
					break;
				case 1:
					if (num != 0 && Equip.WeaponSecondary != num)
					{
						Equip.WeaponSecondary = num;
						dbquery.AddQuery("weapon_secondary", Equip.WeaponSecondary);
					}
					break;
				case 2:
					if (num != 0 && Equip.WeaponMelee != num)
					{
						Equip.WeaponMelee = num;
						dbquery.AddQuery("weapon_melee", Equip.WeaponMelee);
					}
					break;
				case 3:
					if (num != 0 && Equip.WeaponExplosive != num)
					{
						Equip.WeaponExplosive = num;
						dbquery.AddQuery("weapon_explosive", Equip.WeaponExplosive);
					}
					break;
				case 4:
					if (num != 0 && Equip.WeaponSpecial != num)
					{
						Equip.WeaponSpecial = num;
						dbquery.AddQuery("weapon_special", Equip.WeaponSpecial);
					}
					break;
				case 5:
					if (Equip.PartHead != num)
					{
						Equip.PartHead = num;
						dbquery.AddQuery("part_head", Equip.PartHead);
					}
					break;
				case 6:
					if (num != 0 && Equip.PartFace != num)
					{
						Equip.PartFace = num;
						dbquery.AddQuery("part_face", Equip.PartFace);
					}
					break;
				case 7:
					if (num != 0 && Equip.PartJacket != num)
					{
						Equip.PartJacket = num;
						dbquery.AddQuery("part_jacket", Equip.PartJacket);
					}
					break;
				case 8:
					if (num != 0 && Equip.PartPocket != num)
					{
						Equip.PartPocket = num;
						dbquery.AddQuery("part_pocket", Equip.PartPocket);
					}
					break;
				case 9:
					if (num != 0 && Equip.PartGlove != num)
					{
						Equip.PartGlove = num;
						dbquery.AddQuery("part_glove", Equip.PartGlove);
					}
					break;
				case 10:
					if (num != 0 && Equip.PartBelt != num)
					{
						Equip.PartBelt = num;
						dbquery.AddQuery("part_belt", Equip.PartBelt);
					}
					break;
				case 11:
					if (num != 0 && Equip.PartHolster != num)
					{
						Equip.PartHolster = num;
						dbquery.AddQuery("part_holster", Equip.PartHolster);
					}
					break;
				case 12:
					if (num != 0 && Equip.PartSkin != num)
					{
						Equip.PartSkin = num;
						dbquery.AddQuery("part_skin", Equip.PartSkin);
					}
					break;
				case 13:
					if (Equip.BeretItem != num)
					{
						Equip.BeretItem = num;
						dbquery.AddQuery("beret_item_part", Equip.BeretItem);
					}
					break;
				}
			}
			ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dbquery.GetTables(), dbquery.GetValues());
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002C698 File Offset: 0x0002A898
		public static void ValidateItemEquipment(Account Player, SortedList<int, int> Items)
		{
			for (int i = 0; i < Items.Keys.Count; i++)
			{
				int num = AllUtils.smethod_15(i, Items);
				switch (i)
				{
				case 0:
					if (num != 0 && Player.Equipment.DinoItem != num)
					{
						Player.Equipment.DinoItem = AllUtils.smethod_16(Player, num);
						ComDiv.UpdateDB("player_equipments", "dino_item_chara", Player.Equipment.DinoItem, "owner_id", Player.PlayerId);
					}
					break;
				case 1:
					if (Player.Equipment.SprayId != num)
					{
						Player.Equipment.SprayId = AllUtils.smethod_16(Player, num);
						ComDiv.UpdateDB("player_equipments", "spray_id", Player.Equipment.SprayId, "owner_id", Player.PlayerId);
					}
					break;
				case 2:
					if (Player.Equipment.NameCardId != num)
					{
						Player.Equipment.NameCardId = AllUtils.smethod_16(Player, num);
						ComDiv.UpdateDB("player_equipments", "namecard_id", Player.Equipment.NameCardId, "owner_id", Player.PlayerId);
					}
					break;
				}
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002C7E4 File Offset: 0x0002A9E4
		public static void ValidateCharacterSlot(Account Player, PlayerEquipment Equip, int[] Slots)
		{
			DBQuery dbquery = new DBQuery();
			CharacterModel characterSlot = Player.Character.GetCharacterSlot(Slots[0]);
			if (characterSlot != null && Equip.CharaRedId != characterSlot.Id)
			{
				Equip.CharaRedId = AllUtils.smethod_16(Player, characterSlot.Id);
				dbquery.AddQuery("chara_red_side", Equip.CharaRedId);
			}
			CharacterModel characterSlot2 = Player.Character.GetCharacterSlot(Slots[1]);
			if (characterSlot2 != null && Equip.CharaBlueId != characterSlot2.Id)
			{
				Equip.CharaBlueId = AllUtils.smethod_16(Player, characterSlot2.Id);
				dbquery.AddQuery("chara_blue_side", Equip.CharaBlueId);
			}
			ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dbquery.GetTables(), dbquery.GetValues());
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002C8B0 File Offset: 0x0002AAB0
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
			int idStatics = ComDiv.GetIdStatics(ItemIds[5], 1);
			int idStatics2 = ComDiv.GetIdStatics(ItemIds[5], 2);
			int idStatics3 = ComDiv.GetIdStatics(ItemIds[5], 5);
			if (idStatics == 6)
			{
				if (idStatics2 != 1)
				{
					if (idStatics3 != 632)
					{
						if (idStatics2 == 2 || idStatics3 == 664)
						{
							playerEquipment.CharaBlueId = ItemIds[5];
							return playerEquipment;
						}
						return playerEquipment;
					}
				}
				playerEquipment.CharaRedId = ItemIds[5];
			}
			else if (idStatics == 15)
			{
				playerEquipment.DinoItem = ItemIds[5];
			}
			return playerEquipment;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002C9E8 File Offset: 0x0002ABE8
		public static void InsertItem(int ItemId, SlotModel Slot)
		{
			List<int> itemUsages = Slot.ItemUsages;
			lock (itemUsages)
			{
				if (!Slot.ItemUsages.Contains(ItemId))
				{
					Slot.ItemUsages.Add(ItemId);
				}
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002CA3C File Offset: 0x0002AC3C
		public static void ValidateBanPlayer(Account Player, string Message)
		{
			if (ConfigLoader.AutoBan && DaoManagerSQL.SaveAutoBan(Player.PlayerId, Player.Username, Player.Nickname, "Cheat " + Message + ")", DateTimeUtil.Now("dd -MM-yyyy HH:mm:ss"), Player.PublicIP.ToString(), "Illegal Program"))
			{
				using (PROTOCOL_LOBBY_CHATTING_ACK protocol_LOBBY_CHATTING_ACK = new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 1, false, "Permanently ban player [" + Player.Nickname + "], " + Message))
				{
					GameXender.Client.SendPacketToAllClients(protocol_LOBBY_CHATTING_ACK);
				}
				Player.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
				Player.Close(1000, true);
			}
			CLogger.Print(string.Format("Player: {0}; Id: {1}; User: {2}; Reason: {3}", new object[] { Player.Nickname, Player.PlayerId, Player.Username, Message }), LoggerType.Hack, null);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002CB38 File Offset: 0x0002AD38
		public static bool ServerCommands(Account Player, string Text)
		{
			bool flag2;
			try
			{
				bool flag = CommandManager.TryParse(Text, Player);
				if (flag)
				{
					CLogger.Print(string.Format("Player '{0}' (UID: {1}) Running Command '{2}'", Player.Nickname, Player.PlayerId, Text), LoggerType.Command, null);
				}
				flag2 = flag;
			}
			catch
			{
				Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, Translation.GetLabel("CommandsExceptionError")));
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0002CBA8 File Offset: 0x0002ADA8
		public static bool SlotValidMessage(SlotModel Sender, SlotModel Receiver)
		{
			return ((Sender.State == SlotState.NORMAL || Sender.State == SlotState.READY) && (Receiver.State == SlotState.NORMAL || Receiver.State == SlotState.READY)) || (Sender.State >= SlotState.LOAD && Receiver.State >= SlotState.LOAD && (Receiver.SpecGM || Sender.SpecGM || Sender.DeathState.HasFlag(DeadEnum.UseChat) || (Sender.DeathState.HasFlag(DeadEnum.Dead) && Receiver.DeathState.HasFlag(DeadEnum.Dead)) || (Sender.Spectator && Receiver.Spectator) || (Sender.DeathState.HasFlag(DeadEnum.Alive) && Receiver.DeathState.HasFlag(DeadEnum.Alive) && ((Sender.Spectator && Receiver.Spectator) || (!Sender.Spectator && !Receiver.Spectator)))));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0002CCCC File Offset: 0x0002AECC
		public static bool PlayerIsBattle(Account Player)
		{
			RoomModel room = Player.Room;
			SlotModel slotModel;
			return room != null && room.GetSlot(Player.SlotId, out slotModel) && slotModel.State >= SlotState.READY;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002CD04 File Offset: 0x0002AF04
		public static void RoomPingSync(RoomModel Room)
		{
			if (ComDiv.GetDuration(Room.LastPingSync) < (double)ConfigLoader.PingUpdateTimeSeconds)
			{
				return;
			}
			byte[] array = new byte[18];
			for (int i = 0; i < 18; i++)
			{
				array[i] = (byte)Room.Slots[i].Ping;
			}
			using (PROTOCOL_BATTLE_SENDPING_ACK protocol_BATTLE_SENDPING_ACK = new PROTOCOL_BATTLE_SENDPING_ACK(array))
			{
				Room.SendPacketToPlayers(protocol_BATTLE_SENDPING_ACK, SlotState.BATTLE, 0);
			}
			Room.LastPingSync = DateTimeUtil.Now();
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002CD84 File Offset: 0x0002AF84
		public static List<ItemsModel> RepairableItems(Account Player, List<long> ObjectIds, out int Gold, out int Cash, out uint Error)
		{
			Gold = 0;
			Cash = 0;
			Error = 0U;
			List<ItemsModel> list = new List<ItemsModel>();
			if (ObjectIds.Count > 0)
			{
				foreach (long num in ObjectIds)
				{
					ItemsModel item = Player.Inventory.GetItem(num);
					if (item != null)
					{
						uint[] array = AllUtils.smethod_18(Player, item);
						Gold += (int)array[0];
						Cash += (int)array[1];
						Error = array[2];
						list.Add(item);
					}
					else
					{
						Error = 2147483920U;
					}
				}
			}
			return list;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002CE2C File Offset: 0x0002B02C
		private static uint[] smethod_18(Account account_0, ItemsModel itemsModel_0)
		{
			uint[] array = new uint[3];
			ItemsRepair repairItem = ShopManager.GetRepairItem(itemsModel_0.Id);
			if (repairItem != null)
			{
				uint num = repairItem.Quantity - itemsModel_0.Count;
				if (repairItem.Point > repairItem.Cash)
				{
					uint num2 = (uint)ComDiv.Percentage(repairItem.Point, (int)num);
					if ((long)account_0.Gold < (long)((ulong)num2))
					{
						array[2] = 2147483920U;
						return array;
					}
					array[0] = num2;
				}
				else
				{
					if (repairItem.Cash <= repairItem.Point)
					{
						array[2] = 2147483920U;
						return array;
					}
					uint num3 = (uint)ComDiv.Percentage(repairItem.Cash, (int)num);
					if ((long)account_0.Cash < (long)((ulong)num3))
					{
						array[2] = 2147483920U;
						return array;
					}
					array[1] = num3;
				}
				itemsModel_0.Count = repairItem.Quantity;
				ComDiv.UpdateDB("player_items", "count", (long)((ulong)itemsModel_0.Count), "owner_id", account_0.PlayerId, "id", itemsModel_0.Id);
				array[2] = 1U;
				return array;
			}
			array[2] = 2147483920U;
			return array;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002CF30 File Offset: 0x0002B130
		public static bool ChannelRequirementCheck(Account Player, ChannelModel Channel)
		{
			return !Player.IsGM() && ((Channel.Type == ChannelType.Clan && Player.ClanId == 0) || (Channel.Type == ChannelType.Novice && Player.Statistic.GetKDRatio() > 40 && Player.Statistic.GetSeasonKDRatio() > 40) || (Channel.Type == ChannelType.Training && Player.Rank >= 4) || (Channel.Type == ChannelType.Special && Player.Rank <= 25) || Channel.Type == ChannelType.Blocked);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00005A67 File Offset: 0x00003C67
		public static bool ChangeCostume(SlotModel Slot, TeamEnum CostumeTeam)
		{
			if (Slot.CostumeTeam != CostumeTeam)
			{
				Slot.CostumeTeam = CostumeTeam;
			}
			return Slot.CostumeTeam == CostumeTeam;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0002CFB8 File Offset: 0x0002B1B8
		public static void ClassicModeCheck(RoomModel Room, PlayerEquipment Equip)
		{
			if (!ConfigLoader.TournamentRule)
			{
				return;
			}
			TRuleModel truleModel = GameRuleXML.CheckTRuleByRoomName(Room.Name);
			if (truleModel == null)
			{
				return;
			}
			if (truleModel.BanIndexes.Count > 0)
			{
				foreach (int num in truleModel.BanIndexes)
				{
					if (GameRuleXML.IsBlocked(num, Equip.WeaponPrimary))
					{
						Equip.WeaponPrimary = 103004;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.WeaponSecondary))
					{
						Equip.WeaponSecondary = 202003;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.WeaponMelee))
					{
						Equip.WeaponMelee = 301001;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.WeaponExplosive))
					{
						Equip.WeaponExplosive = 407001;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.WeaponSpecial))
					{
						Equip.WeaponSpecial = 508001;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.PartHead))
					{
						Equip.PartHead = 1000700000;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.PartFace))
					{
						Equip.PartFace = 1000800000;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.PartJacket))
					{
						Equip.PartJacket = 1000900000;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.PartPocket))
					{
						Equip.PartPocket = 1001000000;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.PartGlove))
					{
						Equip.PartGlove = 1001100000;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.PartBelt))
					{
						Equip.PartBelt = 1001200000;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.PartHolster))
					{
						Equip.PartHolster = 1001300000;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.PartSkin))
					{
						Equip.PartSkin = 1001400000;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.BeretItem))
					{
						Equip.BeretItem = 0;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.DinoItem))
					{
						Equip.DinoItem = 1500511;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.AccessoryId))
					{
						Equip.AccessoryId = 0;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.SprayId))
					{
						Equip.SprayId = 0;
					}
					else if (GameRuleXML.IsBlocked(num, Equip.NameCardId))
					{
						Equip.NameCardId = 0;
					}
				}
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0002D238 File Offset: 0x0002B438
		public static bool ClassicModeCheck(Account Player, RoomModel Room)
		{
			TRuleModel truleModel = GameRuleXML.CheckTRuleByRoomName(Room.Name);
			if (truleModel == null)
			{
				return false;
			}
			PlayerEquipment equipment = Player.Equipment;
			if (equipment == null)
			{
				CLogger.Print(string.Concat(new string[]
				{
					"Player '",
					Player.Nickname,
					"' has invalid equipment (Error) on ",
					ConfigLoader.TournamentRule ? "Enabled" : "Disabled",
					" Tournament Rules!"
				}), LoggerType.Warning, null);
				return false;
			}
			List<string> list = new List<string>();
			if (truleModel.BanIndexes.Count > 0)
			{
				foreach (int num in truleModel.BanIndexes)
				{
					if (!GameRuleXML.IsBlocked(num, equipment.WeaponPrimary, ref list, Translation.GetLabel("Primary")) && !GameRuleXML.IsBlocked(num, equipment.WeaponSecondary, ref list, Translation.GetLabel("Secondary")) && !GameRuleXML.IsBlocked(num, equipment.WeaponMelee, ref list, Translation.GetLabel("Melee")) && !GameRuleXML.IsBlocked(num, equipment.WeaponExplosive, ref list, Translation.GetLabel("Explosive")) && !GameRuleXML.IsBlocked(num, equipment.WeaponSpecial, ref list, Translation.GetLabel("Special")) && !GameRuleXML.IsBlocked(num, equipment.CharaRedId, ref list, Translation.GetLabel("Character")) && !GameRuleXML.IsBlocked(num, equipment.CharaBlueId, ref list, Translation.GetLabel("Character")) && !GameRuleXML.IsBlocked(num, equipment.PartHead, ref list, Translation.GetLabel("PartHead")) && !GameRuleXML.IsBlocked(num, equipment.PartFace, ref list, Translation.GetLabel("PartFace")) && !GameRuleXML.IsBlocked(num, equipment.BeretItem, ref list, Translation.GetLabel("BeretItem")))
					{
						GameRuleXML.IsBlocked(num, equipment.AccessoryId, ref list, Translation.GetLabel("Accessory"));
					}
				}
			}
			if (list.Count > 0)
			{
				Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("ClassicModeWarn", new object[] { string.Join(", ", list.ToArray()) })));
				return true;
			}
			return false;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0002D48C File Offset: 0x0002B68C
		public static bool Check4vs4(RoomModel Room, bool IsLeader, ref int PlayerFR, ref int PlayerCT, ref int TotalEnemies)
		{
			if (!IsLeader)
			{
				return PlayerFR + PlayerCT >= 8;
			}
			int num = PlayerFR + PlayerCT + 1;
			if (num > 8)
			{
				int num2 = num - 8;
				if (num2 > 0)
				{
					for (int i = 15; i >= 0; i--)
					{
						if (i != Room.LeaderSlot)
						{
							SlotModel slot = Room.GetSlot(i);
							if (slot != null && slot.State == SlotState.READY)
							{
								Room.ChangeSlotState(i, SlotState.NORMAL, false);
								if (i % 2 == 0)
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

		// Token: 0x06000589 RID: 1417 RVA: 0x00005A82 File Offset: 0x00003C82
		public static void UpdateSeasonPass(Account Player)
		{
			if (SeasonChallengeXML.GetActiveSeasonPass() == null)
			{
				return;
			}
			if (Player.UpdateSeasonpass)
			{
				Player.UpdateSeasonpass = false;
				Player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE());
				Player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(Player));
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0002D52C File Offset: 0x0002B72C
		public static void CalculateBattlePass(Account Player, SlotModel Slot, BattlePassModel CurrentSC)
		{
			PlayerBattlepass battlepass = Player.Battlepass;
			if (CurrentSC != null && battlepass != null)
			{
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
						if (ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, new string[] { "total_points", "daily_points", "last_record" }, new object[]
						{
							battlepass.TotalPoints,
							battlepass.DailyPoints,
							(long)((ulong)num2)
						}))
						{
							battlepass.LastRecord = num2;
						}
						Player.UpdateSeasonpass = true;
					}
				}
				AllUtils.smethod_19(Player, battlepass, CurrentSC);
				return;
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0002D658 File Offset: 0x0002B858
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
				int[] array = new int[3];
				int num2 = 0;
				PassItemModel normal = passBoxModel.Normal;
				array[num2] = ((normal != null) ? normal.GoodId : 0);
				int[] array2 = array;
				if (playerBattlepass_0.IsPremium)
				{
					int[] array3 = array2;
					int num3 = 1;
					PassItemModel premiumA = passBoxModel.PremiumA;
					array3[num3] = ((premiumA != null) ? premiumA.GoodId : 0);
					int[] array4 = array2;
					int num4 = 2;
					PassItemModel premiumB = passBoxModel.PremiumB;
					array4[num4] = ((premiumB != null) ? premiumB.GoodId : 0);
				}
				AllUtils.smethod_22(account_0, array2);
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002D728 File Offset: 0x0002B928
		public static void ProcessBattlepassPremiumBuy(Account Player)
		{
			PlayerBattlepass battlepass = Player.Battlepass;
			if (battlepass != null)
			{
				BattlePassModel seasonPass = SeasonChallengeXML.GetSeasonPass(battlepass.Id);
				if (seasonPass == null)
				{
					return;
				}
				battlepass.IsPremium = true;
				for (int i = 0; i < battlepass.Level; i++)
				{
					PassBoxModel passBoxModel = seasonPass.Cards[i];
					int[] array = new int[3];
					int num = 1;
					PassItemModel premiumA = passBoxModel.PremiumA;
					array[num] = ((premiumA != null) ? premiumA.GoodId : 0);
					int num2 = 2;
					PassItemModel premiumB = passBoxModel.PremiumB;
					array[num2] = ((premiumB != null) ? premiumB.GoodId : 0);
					int[] array2 = array;
					AllUtils.smethod_22(Player, array2);
				}
				ComDiv.UpdateDB("player_battlepass", "premium", battlepass.IsPremium, "owner_id", Player.PlayerId);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002D7DC File Offset: 0x0002B9DC
		public static void SendCompetitiveInfo(Account Player)
		{
			try
			{
				Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, Translation.GetLabel("CompetitiveRank", new object[]
				{
					Player.Competitive.Rank().Name,
					Player.Competitive.Points,
					Player.Competitive.Rank().Points
				})));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.ToString(), LoggerType.Error, null);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0002D880 File Offset: 0x0002BA80
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
				AllUtils.smethod_20(Player.Competitive);
				string label = Translation.GetLabel("CompetitivePointsEarned", new object[] { num });
				string label2 = Translation.GetLabel("CompetitiveRank", new object[]
				{
					Player.Competitive.Rank().Name,
					Player.Competitive.Points,
					Player.Competitive.Rank().Points
				});
				Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, label + "\n\r" + label2));
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0002D994 File Offset: 0x0002BB94
		private static void smethod_20(PlayerCompetitive playerCompetitive_0)
		{
			AllUtils.Class6 @class = new AllUtils.Class6();
			@class.playerCompetitive_0 = playerCompetitive_0;
			CompetitiveRank competitiveRank = CompetitiveXML.Ranks.FirstOrDefault(new Func<CompetitiveRank, bool>(@class.method_0));
			int num;
			if (competitiveRank != null)
			{
				num = competitiveRank.Id;
			}
			else
			{
				num = @class.playerCompetitive_0.Level;
			}
			ComDiv.UpdateDB("player_competitive", "points", @class.playerCompetitive_0.Points, "owner_id", @class.playerCompetitive_0.OwnerId);
			if (num != @class.playerCompetitive_0.Level && ComDiv.UpdateDB("player_competitive", "level", num, "owner_id", @class.playerCompetitive_0.OwnerId))
			{
				@class.playerCompetitive_0.Level = num;
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0002DA5C File Offset: 0x0002BC5C
		public static bool CanOpenSlotCompetitive(RoomModel Room, SlotModel Opening)
		{
			AllUtils.Class7 @class = new AllUtils.Class7();
			@class.slotModel_0 = Opening;
			return Room.Slots.Where(new Func<SlotModel, bool>(@class.method_0)).Count<SlotModel>() < 5;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0002DA98 File Offset: 0x0002BC98
		public static bool CanCloseSlotCompetitive(RoomModel Room, SlotModel Closing)
		{
			AllUtils.Class8 @class = new AllUtils.Class8();
			@class.slotModel_0 = Closing;
			return Room.Slots.Where(new Func<SlotModel, bool>(@class.method_0)).Count<SlotModel>() > 3;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0002DAD4 File Offset: 0x0002BCD4
		private static void smethod_21(Account account_0)
		{
			List<ItemsModel> items = account_0.Inventory.Items;
			List<ItemsModel> list = items;
			lock (list)
			{
				foreach (ItemsModel itemsModel in items)
				{
					if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 6 && account_0.Character.GetCharacter(itemsModel.Id) == null)
					{
						AllUtils.CreateCharacter(account_0, itemsModel);
					}
				}
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0002DB78 File Offset: 0x0002BD78
		public static void CreateCharacter(Account Player, ItemsModel Item)
		{
			CharacterModel characterModel = new CharacterModel
			{
				Id = Item.Id,
				Name = Item.Name,
				Slot = Player.Character.GenSlotId(Item.Id),
				CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
				PlayTime = 0U
			};
			Player.Character.AddCharacter(characterModel);
			Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, Item));
			if (DaoManagerSQL.CreatePlayerCharacter(characterModel, Player.PlayerId))
			{
				Player.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0U, 3, characterModel, Player));
				return;
			}
			Player.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147483648U, byte.MaxValue, null, null));
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0002DC28 File Offset: 0x0002BE28
		private static void smethod_22(Account account_0, int[] int_0)
		{
			foreach (int num in int_0)
			{
				if (num != 0)
				{
					GoodsItem good = ShopManager.GetGood(num);
					if (good != null)
					{
						if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && account_0.Character.GetCharacter(good.Item.Id) == null)
						{
							AllUtils.CreateCharacter(account_0, good.Item);
						}
						else
						{
							account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
						}
					}
				}
			}
			account_0.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK(0U, int_0));
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002DCB0 File Offset: 0x0002BEB0
		private static int smethod_23(Account account_0, BattleRewardType battleRewardType_0)
		{
			Random random = new Random();
			BattleRewardModel rewardType = BattleRewardXML.GetRewardType(battleRewardType_0);
			if (rewardType == null || !rewardType.Enable || random.Next(100) >= rewardType.Percentage)
			{
				return 0;
			}
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
				AllUtils.CreateCharacter(account_0, good.Item);
			}
			else
			{
				account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
			}
			return good.Item.Id;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002DE1C File Offset: 0x0002C01C
		public static ValueTuple<byte[], int[]> GetRewardData(RoomModel Room, List<SlotModel> Slots)
		{
			byte[] array = new byte[5];
			int[] array2 = new int[5];
			for (int i = 0; i < 5; i++)
			{
				array[i] = byte.MaxValue;
				array2[i] = 0;
			}
			int num = 0;
			if (!Room.IsBotMode() && Slots.Count > 0)
			{
				SlotModel slotModel = Slots.Where(new Func<SlotModel, bool>(AllUtils.Class5.<>9.method_0)).OrderByDescending(new Func<SlotModel, int>(AllUtils.Class5.<>9.method_1)).FirstOrDefault<SlotModel>();
				if (slotModel != null)
				{
					AllUtils.smethod_24(Room, slotModel, BattleRewardType.MVP, 5, ref num, ref array, ref array2);
				}
				SlotModel slotModel2 = Slots.Where(new Func<SlotModel, bool>(AllUtils.Class5.<>9.method_2)).OrderByDescending(new Func<SlotModel, int>(AllUtils.Class5.<>9.method_3)).FirstOrDefault<SlotModel>();
				if (slotModel2 != null)
				{
					AllUtils.smethod_24(Room, slotModel2, BattleRewardType.AssistKing, 5, ref num, ref array, ref array2);
				}
				SlotModel slotModel3 = Slots.Where(new Func<SlotModel, bool>(AllUtils.Class5.<>9.method_4)).OrderByDescending(new Func<SlotModel, int>(AllUtils.Class5.<>9.method_5)).FirstOrDefault<SlotModel>();
				if (slotModel3 != null)
				{
					AllUtils.smethod_24(Room, slotModel3, BattleRewardType.MultiKill, 5, ref num, ref array, ref array2);
				}
			}
			return new ValueTuple<byte[], int[]>(array, array2);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002DF9C File Offset: 0x0002C19C
		private static void smethod_24(RoomModel roomModel_0, SlotModel slotModel_0, BattleRewardType battleRewardType_0, int int_0, ref int int_1, ref byte[] byte_0, ref int[] int_2)
		{
			Account account;
			if (int_1 < int_0 && roomModel_0.GetPlayerBySlot(slotModel_0, out account))
			{
				byte b = (byte)slotModel_0.Id;
				int num = AllUtils.smethod_23(account, battleRewardType_0);
				if (b == 255 || num == 0)
				{
					return;
				}
				byte_0[int_1] = b;
				int_2[int_1] = num;
				int_1++;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
		public static int InitBotRank(int BotLevel)
		{
			Random random = new Random();
			switch (BotLevel)
			{
			case 1:
				return random.Next(0, 4);
			case 2:
				return random.Next(5, 9);
			case 3:
				return random.Next(10, 14);
			case 4:
				return random.Next(15, 19);
			case 5:
				return random.Next(20, 24);
			case 6:
				return random.Next(25, 29);
			case 7:
				return random.Next(30, 34);
			case 8:
				return random.Next(35, 39);
			case 9:
				return random.Next(40, 44);
			case 10:
				return random.Next(45, 49);
			default:
				return 52;
			}
		}

		// Token: 0x020001DE RID: 478
		[CompilerGenerated]
		[Serializable]
		private sealed class Class5
		{
			// Token: 0x06000599 RID: 1433 RVA: 0x00005AB2 File Offset: 0x00003CB2
			// Note: this type is marked as 'beforefieldinit'.
			static Class5()
			{
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x000025DF File Offset: 0x000007DF
			public Class5()
			{
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x00005ABE File Offset: 0x00003CBE
			internal bool method_0(SlotModel slotModel_0)
			{
				return slotModel_0.Score > 0;
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x00005AC9 File Offset: 0x00003CC9
			internal int method_1(SlotModel slotModel_0)
			{
				return slotModel_0.Score;
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x00005AD1 File Offset: 0x00003CD1
			internal bool method_2(SlotModel slotModel_0)
			{
				return slotModel_0.AllAssists > 0;
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x00005ADC File Offset: 0x00003CDC
			internal int method_3(SlotModel slotModel_0)
			{
				return slotModel_0.AllAssists;
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x00005AE4 File Offset: 0x00003CE4
			internal bool method_4(SlotModel slotModel_0)
			{
				return slotModel_0.KillsOnLife > 0;
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00005AEF File Offset: 0x00003CEF
			internal int method_5(SlotModel slotModel_0)
			{
				return slotModel_0.KillsOnLife;
			}

			// Token: 0x04000388 RID: 904
			public static readonly AllUtils.Class5 <>9 = new AllUtils.Class5();

			// Token: 0x04000389 RID: 905
			public static Func<SlotModel, bool> <>9__125_0;

			// Token: 0x0400038A RID: 906
			public static Func<SlotModel, int> <>9__125_1;

			// Token: 0x0400038B RID: 907
			public static Func<SlotModel, bool> <>9__125_2;

			// Token: 0x0400038C RID: 908
			public static Func<SlotModel, int> <>9__125_3;

			// Token: 0x0400038D RID: 909
			public static Func<SlotModel, bool> <>9__125_4;

			// Token: 0x0400038E RID: 910
			public static Func<SlotModel, int> <>9__125_5;
		}

		// Token: 0x020001DF RID: 479
		[CompilerGenerated]
		private sealed class Class6
		{
			// Token: 0x060005A1 RID: 1441 RVA: 0x000025DF File Offset: 0x000007DF
			public Class6()
			{
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x00005AF7 File Offset: 0x00003CF7
			internal bool method_0(CompetitiveRank competitiveRank_0)
			{
				return this.playerCompetitive_0.Points <= competitiveRank_0.Points;
			}

			// Token: 0x0400038F RID: 911
			public PlayerCompetitive playerCompetitive_0;
		}

		// Token: 0x020001E0 RID: 480
		[CompilerGenerated]
		private sealed class Class7
		{
			// Token: 0x060005A3 RID: 1443 RVA: 0x000025DF File Offset: 0x000007DF
			public Class7()
			{
			}

			// Token: 0x060005A4 RID: 1444 RVA: 0x00005B0F File Offset: 0x00003D0F
			internal bool method_0(SlotModel slotModel_1)
			{
				return slotModel_1.Team == this.slotModel_0.Team && slotModel_1.State != SlotState.CLOSE;
			}

			// Token: 0x04000390 RID: 912
			public SlotModel slotModel_0;
		}

		// Token: 0x020001E1 RID: 481
		[CompilerGenerated]
		private sealed class Class8
		{
			// Token: 0x060005A5 RID: 1445 RVA: 0x000025DF File Offset: 0x000007DF
			public Class8()
			{
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x00005B32 File Offset: 0x00003D32
			internal bool method_0(SlotModel slotModel_1)
			{
				return slotModel_1.Team == this.slotModel_0.Team && slotModel_1.State != SlotState.CLOSE;
			}

			// Token: 0x04000391 RID: 913
			public SlotModel slotModel_0;
		}

		// Token: 0x020001E2 RID: 482
		[CompilerGenerated]
		private sealed class Class9
		{
			// Token: 0x060005A7 RID: 1447 RVA: 0x000025DF File Offset: 0x000007DF
			public Class9()
			{
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x0002E0A0 File Offset: 0x0002C2A0
			internal void method_0(object object_0)
			{
				AllUtils.smethod_2(this.roomModel_0, this.teamEnum_0, this.bool_0, this.fragInfos_0, this.slotModel_0);
				lock (object_0)
				{
					this.roomModel_0.MatchEndTime.StopJob();
				}
			}

			// Token: 0x04000392 RID: 914
			public RoomModel roomModel_0;

			// Token: 0x04000393 RID: 915
			public TeamEnum teamEnum_0;

			// Token: 0x04000394 RID: 916
			public bool bool_0;

			// Token: 0x04000395 RID: 917
			public FragInfos fragInfos_0;

			// Token: 0x04000396 RID: 918
			public SlotModel slotModel_0;
		}
	}
}
