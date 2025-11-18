using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Data.Utils;

public static class AllUtils
{
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
			smethod_2(Player);
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
				smethod_1(Player, pCCafeReward.Id);
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

	public static bool DiscountPlayerItems(Account Player)
	{
		try
		{
			bool flag = false;
			uint num = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
			List<object> list = new List<object>();
			int num2 = ((Player.Bonus != null) ? Player.Bonus.Bonuses : 0);
			int num3 = ((Player.Bonus != null) ? Player.Bonus.FreePass : 0);
			lock (Player.Inventory.Items)
			{
				for (int i = 0; i < Player.Inventory.Items.Count; i++)
				{
					ItemsModel ıtemsModel = Player.Inventory.Items[i];
					if (ıtemsModel.Count <= num && ıtemsModel.Equip == ItemEquipType.Temporary)
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
								}
								else if (ıtemsModel.Id == 1600006)
								{
									ComDiv.UpdateDB("accounts", "nick_color", 0, "player_id", Player.PlayerId);
									Player.NickColor = 0;
								}
								else if (ıtemsModel.Id == 1600009)
								{
									ComDiv.UpdateDB("player_bonus", "fake_rank", 55, "owner_id", Player.PlayerId);
									Player.Bonus.FakeRank = 55;
								}
								else if (ıtemsModel.Id == 1600010)
								{
									if (Player.Bonus.FakeNick.Length > 0)
									{
										ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", Player.PlayerId);
										ComDiv.UpdateDB("accounts", "nickname", Player.Bonus.FakeNick, "player_id", Player.PlayerId);
										Player.Nickname = Player.Bonus.FakeNick;
										Player.Bonus.FakeNick = "";
									}
								}
								else if (ıtemsModel.Id == 1600187)
								{
									ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", Player.PlayerId);
									Player.Bonus.MuzzleColor = 0;
								}
								else if (ıtemsModel.Id == 1600205)
								{
									ComDiv.UpdateDB("player_bonus", "nick_border_color", 0, "owner_id", Player.PlayerId);
									Player.Bonus.NickBorderColor = 0;
								}
							}
							CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtemsModel.Id);
							if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && Player.Effects.HasFlag(couponEffect.EffectFlag))
							{
								Player.Effects -= (long)couponEffect.EffectFlag;
								flag = true;
							}
						}
						list.Add(ıtemsModel.ObjectId);
						Player.Inventory.Items.RemoveAt(i--);
					}
					else if (ıtemsModel.Count == 0)
					{
						list.Add(ıtemsModel.ObjectId);
						Player.Inventory.Items.RemoveAt(i--);
					}
				}
			}
			if (list.Count > 0)
			{
				for (int j = 0; j < list.Count; j++)
				{
					ItemsModel ıtem = Player.Inventory.GetItem((long)list[j]);
					if (ıtem != null && ıtem.Category == ItemCategory.Character && ComDiv.GetIdStatics(ıtem.Id, 1) == 6)
					{
						smethod_1(Player, ıtem.Id);
					}
				}
				ComDiv.DeleteDB("player_items", "object_id", list.ToArray(), "owner_id", Player.PlayerId);
			}
			list.Clear();
			list = null;
			if (Player.Bonus != null && (Player.Bonus.Bonuses != num2 || Player.Bonus.FreePass != num3))
			{
				DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
			}
			if (Player.Effects < (CouponEffects)0L)
			{
				Player.Effects = (CouponEffects)0L;
			}
			if (flag)
			{
				ComDiv.UpdateDB("accounts", "coupon_effect", (long)Player.Effects, "player_id", Player.PlayerId);
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

	private static void smethod_1(Account account_0, int int_0)
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

	public static void CheckGameEvents(Account Player)
	{
		uint[] array = new uint[2]
		{
			uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
			uint.Parse(DateTimeUtil.Now("yyMMdd"))
		};
		PlayerEvent @event = Player.Event;
		if (@event != null)
		{
			EventQuestModel runningEvent = EventQuestXML.GetRunningEvent();
			if (runningEvent != null)
			{
				uint lastQuestDate = @event.LastQuestDate;
				int lastQuestFinish = @event.LastQuestFinish;
				if (@event.LastQuestDate < runningEvent.BeginDate)
				{
					@event.LastQuestDate = 0u;
					@event.LastQuestFinish = 0;
				}
				if (@event.LastQuestFinish == 0)
				{
					Player.Mission.Mission4 = 13;
					if (@event.LastQuestDate == 0)
					{
						@event.LastQuestDate = array[0];
					}
				}
				if (@event.LastQuestDate != lastQuestDate || @event.LastQuestFinish != lastQuestFinish)
				{
					EventQuestXML.ResetPlayerEvent(Player.PlayerId, @event);
				}
			}
			EventLoginModel runningEvent2 = EventLoginXML.GetRunningEvent();
			if (runningEvent2 != null)
			{
				if (@event.LastLoginDate < runningEvent2.BeginDate)
				{
					@event.LastLoginDate = 0u;
					ComDiv.UpdateDB("player_events", "last_login_date", 0, "owner_id", Player.PlayerId);
				}
				if (uint.Parse($"{DateTimeUtil.Convert($"{@event.LastLoginDate}"):yyMMdd}") < array[1])
				{
					foreach (int good2 in runningEvent2.Goods)
					{
						GoodsItem good = ShopManager.GetGood(good2);
						if (good != null)
						{
							ComDiv.TryCreateItem(new ItemsModel(good.Item), Player.Inventory, Player.PlayerId);
						}
					}
					ComDiv.UpdateDB("player_events", "last_login_date", (long)array[0], "owner_id", Player.PlayerId);
					Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("LoginGiftMessage")));
				}
			}
			EventVisitModel runningEvent3 = EventVisitXML.GetRunningEvent();
			if (runningEvent3 != null && @event.LastVisitDate < runningEvent3.BeginDate)
			{
				@event.LastVisitDate = 0u;
				@event.LastVisitCheckDay = 0;
				@event.LastVisitSeqType = 0;
				EventVisitXML.ResetPlayerEvent(Player.PlayerId, @event);
			}
			EventXmasModel runningEvent4 = EventXmasXML.GetRunningEvent();
			if (runningEvent4 != null && @event.LastXmasDate < runningEvent4.BeginDate)
			{
				@event.LastXmasDate = 0u;
				ComDiv.UpdateDB("player_events", "last_xmas_date", 0, "owner_id", Player.PlayerId);
			}
			EventPlaytimeModel runningEvent5 = EventPlaytimeXML.GetRunningEvent();
			if (runningEvent5 != null)
			{
				if (@event.LastPlaytimeDate < runningEvent5.BeginDate)
				{
					@event.LastPlaytimeDate = 0u;
					@event.LastPlaytimeFinish = 0;
					@event.LastPlaytimeValue = 0L;
					EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, @event);
				}
				if (uint.Parse($"{DateTimeUtil.Convert($"{@event.LastPlaytimeDate}"):yyMMdd}") < array[1])
				{
					@event.LastPlaytimeValue = 0L;
					@event.LastPlaytimeFinish = 0;
					EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, @event);
				}
			}
		}
		ComDiv.UpdateDB("accounts", "last_login_date", (long)array[0], "player_id", Player.PlayerId);
	}

	public static void ProcessBattlepass(Account Player)
	{
		BattlePassModel activeSeasonPass = SeasonChallengeXML.GetActiveSeasonPass();
		if (activeSeasonPass == null)
		{
			return;
		}
		PlayerBattlepass battlepass = Player.Battlepass;
		if (battlepass != null)
		{
			if (battlepass.Id != activeSeasonPass.Id)
			{
				battlepass.Id = activeSeasonPass.Id;
				battlepass.IsPremium = false;
				battlepass.Level = 0;
				battlepass.TotalPoints = 0;
				battlepass.DailyPoints = 0;
				battlepass.LastRecord = 0u;
				ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, new string[6] { "id", "level", "premium", "total_points", "daily_points", "last_record" }, battlepass.Id, battlepass.Level, battlepass.IsPremium, battlepass.TotalPoints, battlepass.DailyPoints, (long)battlepass.LastRecord);
			}
			(int, int, int, int) levelProgression = activeSeasonPass.GetLevelProgression(battlepass.TotalPoints);
			if (battlepass.Level != levelProgression.Item1)
			{
				(battlepass.Level, _, _, _) = levelProgression;
				ComDiv.UpdateDB("player_battlepass", "level", battlepass.Level, "owner_id", Player.PlayerId);
			}
			if (uint.Parse($"{DateTimeUtil.Convert($"{battlepass.LastRecord}"):yyMMdd}") < uint.Parse(DateTimeUtil.Now("yyMMdd")))
			{
				battlepass.DailyPoints = 0;
				battlepass.LastRecord = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, new string[2] { "daily_points", "last_record" }, battlepass.DailyPoints, (long)battlepass.LastRecord);
			}
		}
	}

	public static long LoadCouponEffects(Account Player)
	{
		long num = 0L;
		foreach (var item in new List<(CouponEffects, long)>
		{
			(CouponEffects.Ammo40, 1L),
			(CouponEffects.Ammo10, 2L),
			(CouponEffects.GetDroppedWeapon, 4L),
			(CouponEffects.QuickChangeWeapon, 16L),
			(CouponEffects.QuickChangeReload, 128L),
			(CouponEffects.Invincible, 512L),
			(CouponEffects.FullMetalJack, 2048L),
			(CouponEffects.HollowPoint, 8192L),
			(CouponEffects.HollowPointPlus, 32768L),
			(CouponEffects.C4SpeedKit, 65536L),
			(CouponEffects.ExtraGrenade, 131072L),
			(CouponEffects.ExtraThrowGrenade, 262144L),
			(CouponEffects.JackHollowPoint, 524288L),
			(CouponEffects.HP5, 1048576L),
			(CouponEffects.HP10, 2097152L),
			(CouponEffects.Defense5, 4194304L),
			(CouponEffects.Defense10, 8388608L),
			(CouponEffects.Defense20, 16777216L),
			(CouponEffects.Defense90, 33554432L),
			(CouponEffects.Respawn20, 67108864L),
			(CouponEffects.Respawn30, 268435456L),
			(CouponEffects.Respawn50, 1073741824L),
			(CouponEffects.Respawn100, 8589934592L),
			(CouponEffects.Camoflage50, 34359738368L),
			(CouponEffects.Camoflage99, 68719476736L)
		})
		{
			if (Player.Effects.HasFlag(item.Item1))
			{
				num += item.Item2;
			}
		}
		return num;
	}

	public static List<ItemsModel> LimitationIndex(Account Player, List<ItemsModel> Items)
	{
		int num = 600 + Player.InventoryPlus;
		if (Items.Count > num)
		{
			int num2 = num / 3;
			if (Items.Count > num2)
			{
				Items.RemoveRange(num2, Items.Count - num);
			}
		}
		return Items;
	}

	private static void smethod_2(Account account_0)
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
		if (!DaoManagerSQL.CreatePlayerCharacter(characterModel, Player.PlayerId))
		{
			CLogger.Print($"There is an error while cheating a character! (ID: {Item.Id}", LoggerType.Warning);
		}
	}

	public static uint GetFeatures()
	{
		AccountFeatures accountFeatures = AccountFeatures.ALL;
		if (!AuthXender.Client.Config.EnableClan)
		{
			accountFeatures -= 4096;
		}
		if (!AuthXender.Client.Config.EnableTicket)
		{
			accountFeatures -= 16384;
		}
		if (!AuthXender.Client.Config.EnableTags)
		{
			accountFeatures -= 67108864;
		}
		EventPlaytimeModel runningEvent = EventPlaytimeXML.GetRunningEvent();
		if (!AuthXender.Client.Config.EnablePlaytime || runningEvent == null || !runningEvent.EventIsEnabled())
		{
			accountFeatures -= 256;
		}
		return (uint)accountFeatures;
	}

	public static uint ValidateKey(long PlayerId, int SessionId, uint Unknown)
	{
		int num = (int)(Unknown % 999u);
		int num2 = (int)(PlayerId % 999L);
		int num3 = SessionId % 999;
		return uint.Parse($"{num:000}{num2:000}{num3:000}");
	}
}
