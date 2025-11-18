using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Auth.Data.Utils
{
	public static class AllUtils
	{
		public static void CheckGameEvents(Account Player)
		{
			uint[] uInt32Array = new uint[] { uint.Parse(DateTimeUtil.Now("yyMMddHHmm")), uint.Parse(DateTimeUtil.Now("yyMMdd")) };
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
						@event.LastQuestDate = 0;
						@event.LastQuestFinish = 0;
					}
					if (@event.LastQuestFinish == 0)
					{
						Player.Mission.Mission4 = 13;
						if (@event.LastQuestDate == 0)
						{
							@event.LastQuestDate = uInt32Array[0];
						}
					}
					if (@event.LastQuestDate != lastQuestDate || @event.LastQuestFinish != lastQuestFinish)
					{
						EventQuestXML.ResetPlayerEvent(Player.PlayerId, @event);
					}
				}
				EventLoginModel eventLoginModel = EventLoginXML.GetRunningEvent();
				if (eventLoginModel != null)
				{
					if (@event.LastLoginDate < eventLoginModel.BeginDate)
					{
						@event.LastLoginDate = 0;
						ComDiv.UpdateDB("player_events", "last_login_date", 0, "owner_id", Player.PlayerId);
					}
					if (uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", @event.LastLoginDate)))) < uInt32Array[1])
					{
						foreach (int good in eventLoginModel.Goods)
						{
							GoodsItem goodsItem = ShopManager.GetGood(good);
							if (goodsItem == null)
							{
								continue;
							}
							ComDiv.TryCreateItem(new ItemsModel(goodsItem.Item), Player.Inventory, Player.PlayerId);
						}
						ComDiv.UpdateDB("player_events", "last_login_date", (long)((ulong)uInt32Array[0]), "owner_id", Player.PlayerId);
						Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("LoginGiftMessage")));
					}
				}
				EventVisitModel eventVisitModel = EventVisitXML.GetRunningEvent();
				if (eventVisitModel != null && @event.LastVisitDate < eventVisitModel.BeginDate)
				{
					@event.LastVisitDate = 0;
					@event.LastVisitCheckDay = 0;
					@event.LastVisitSeqType = 0;
					EventVisitXML.ResetPlayerEvent(Player.PlayerId, @event);
				}
				EventXmasModel eventXmasModel = EventXmasXML.GetRunningEvent();
				if (eventXmasModel != null && @event.LastXmasDate < eventXmasModel.BeginDate)
				{
					@event.LastXmasDate = 0;
					ComDiv.UpdateDB("player_events", "last_xmas_date", 0, "owner_id", Player.PlayerId);
				}
				EventPlaytimeModel eventPlaytimeModel = EventPlaytimeXML.GetRunningEvent();
				if (eventPlaytimeModel != null)
				{
					if (@event.LastPlaytimeDate < eventPlaytimeModel.BeginDate)
					{
						@event.LastPlaytimeDate = 0;
						@event.LastPlaytimeFinish = 0;
						@event.LastPlaytimeValue = 0L;
						EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, @event);
					}
					if (uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", @event.LastPlaytimeDate)))) < uInt32Array[1])
					{
						@event.LastPlaytimeValue = 0L;
						@event.LastPlaytimeFinish = 0;
						EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, @event);
					}
				}
			}
			ComDiv.UpdateDB("accounts", "last_login_date", (long)((ulong)uInt32Array[0]), "player_id", Player.PlayerId);
		}

		public static void CreateCharacter(Account Player, ItemsModel Item)
		{
			CharacterModel characterModel = new CharacterModel()
			{
				Id = Item.Id,
				Name = Item.Name,
				Slot = Player.Character.GenSlotId(Item.Id),
				CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
				PlayTime = 0
			};
			Player.Character.AddCharacter(characterModel);
			if (!DaoManagerSQL.CreatePlayerCharacter(characterModel, Player.PlayerId))
			{
				CLogger.Print(string.Format("There is an error while cheating a character! (ID: {0}", Item.Id), LoggerType.Warning, null);
			}
		}

		public static bool DiscountPlayerItems(Account Player)
		{
			bool flag;
			try
			{
				bool flag1 = false;
				uint uInt32 = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
				List<object> objs = new List<object>();
				int ınt32 = (Player.Bonus != null ? Player.Bonus.Bonuses : 0);
				int ınt321 = (Player.Bonus != null ? Player.Bonus.FreePass : 0);
				lock (Player.Inventory.Items)
				{
					for (int i = 0; i < Player.Inventory.Items.Count; i++)
					{
						ItemsModel ıtem = Player.Inventory.Items[i];
						if (ıtem.Count <= uInt32 && ıtem.Equip == ItemEquipType.Temporary)
						{
							if (ıtem.Category == ItemCategory.Coupon)
							{
								if (Player.Bonus == null)
								{
									goto Label0;
								}
								if (!Player.Bonus.RemoveBonuses(ıtem.Id))
								{
									if (ıtem.Id == 1600014)
									{
										ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", Player.PlayerId);
										Player.Bonus.CrosshairColor = 4;
									}
									else if (ıtem.Id == 1600006)
									{
										ComDiv.UpdateDB("accounts", "nick_color", 0, "player_id", Player.PlayerId);
										Player.NickColor = 0;
									}
									else if (ıtem.Id == 1600009)
									{
										ComDiv.UpdateDB("player_bonus", "fake_rank", 55, "owner_id", Player.PlayerId);
										Player.Bonus.FakeRank = 55;
									}
									else if (ıtem.Id == 1600010)
									{
										if (Player.Bonus.FakeNick.Length > 0)
										{
											ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", Player.PlayerId);
											ComDiv.UpdateDB("accounts", "nickname", Player.Bonus.FakeNick, "player_id", Player.PlayerId);
											Player.Nickname = Player.Bonus.FakeNick;
											Player.Bonus.FakeNick = "";
										}
									}
									else if (ıtem.Id == 1600187)
									{
										ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", Player.PlayerId);
										Player.Bonus.MuzzleColor = 0;
									}
									else if (ıtem.Id == 1600205)
									{
										ComDiv.UpdateDB("player_bonus", "nick_border_color", 0, "owner_id", Player.PlayerId);
										Player.Bonus.NickBorderColor = 0;
									}
								}
								CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(ıtem.Id);
								if (couponEffect != null && (long)couponEffect.EffectFlag > 0L && Player.Effects.HasFlag(couponEffect.EffectFlag))
								{
									Player.Effects -= couponEffect.EffectFlag;
									flag1 = true;
								}
							}
							objs.Add(ıtem.ObjectId);
							int ınt322 = i;
							i = ınt322 - 1;
							Player.Inventory.Items.RemoveAt(ınt322);
						}
						else if (ıtem.Count == 0)
						{
							objs.Add(ıtem.ObjectId);
							int ınt323 = i;
							i = ınt323 - 1;
							Player.Inventory.Items.RemoveAt(ınt323);
						}
					Label0:
					}
				}
				if (objs.Count > 0)
				{
					for (int j = 0; j < objs.Count; j++)
					{
						ItemsModel ıtemsModel = Player.Inventory.GetItem((long)objs[j]);
						if (ıtemsModel != null && ıtemsModel.Category == ItemCategory.Character && ComDiv.GetIdStatics(ıtemsModel.Id, 1) == 6)
						{
							AllUtils.smethod_1(Player, ıtemsModel.Id);
						}
					}
					ComDiv.DeleteDB("player_items", "object_id", objs.ToArray(), "owner_id", Player.PlayerId);
				}
				objs.Clear();
				objs = null;
				if (Player.Bonus != null && (Player.Bonus.Bonuses != ınt32 || Player.Bonus.FreePass != ınt321))
				{
					DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
				}
				if ((long)Player.Effects < 0L)
				{
					Player.Effects = (CouponEffects)0L;
				}
				if (flag1)
				{
					ComDiv.UpdateDB("accounts", "coupon_effect", (long)Player.Effects, "player_id", Player.PlayerId);
				}
				int ınt324 = ComDiv.CheckEquipedItems(Player.Equipment, Player.Inventory.Items, false);
				if (ınt324 > 0)
				{
					DBQuery dBQuery = new DBQuery();
					if ((ınt324 & 2) == 2)
					{
						ComDiv.UpdateWeapons(Player.Equipment, dBQuery);
					}
					if ((ınt324 & 1) == 1)
					{
						ComDiv.UpdateChars(Player.Equipment, dBQuery);
					}
					if ((ınt324 & 3) == 3)
					{
						ComDiv.UpdateItems(Player.Equipment, dBQuery);
					}
					ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
					dBQuery = null;
				}
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static uint GetFeatures()
		{
			AccountFeatures accountFeature = AccountFeatures.ALL;
			if (!AuthXender.Client.Config.EnableClan)
			{
				accountFeature -= AccountFeatures.CLAN_ONLY;
			}
			if (!AuthXender.Client.Config.EnableTicket)
			{
				accountFeature -= AccountFeatures.TICKET_ONLY;
			}
			if (!AuthXender.Client.Config.EnableTags)
			{
				accountFeature -= AccountFeatures.TAGS_ONLY;
			}
			EventPlaytimeModel runningEvent = EventPlaytimeXML.GetRunningEvent();
			if (!AuthXender.Client.Config.EnablePlaytime || runningEvent == null || !runningEvent.EventIsEnabled())
			{
				accountFeature -= AccountFeatures.PLAYTIME_ONLY;
			}
			return (uint)accountFeature;
		}

		public static List<ItemsModel> LimitationIndex(Account Player, List<ItemsModel> Items)
		{
			int ınventoryPlus = 600 + Player.InventoryPlus;
			if (Items.Count > ınventoryPlus)
			{
				int ınt32 = ınventoryPlus / 3;
				if (Items.Count > ınt32)
				{
					Items.RemoveRange(ınt32, Items.Count - ınventoryPlus);
				}
			}
			return Items;
		}

		public static long LoadCouponEffects(Account Player)
		{
			long ıtem2 = 0L;
			foreach (ValueTuple<CouponEffects, long> valueTuple in new List<ValueTuple<CouponEffects, long>>()
			{
				new ValueTuple<CouponEffects, long>(CouponEffects.Ammo40, 1L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Ammo10, 2L),
				new ValueTuple<CouponEffects, long>(CouponEffects.GetDroppedWeapon, 4L),
				new ValueTuple<CouponEffects, long>(CouponEffects.QuickChangeWeapon, 16L),
				new ValueTuple<CouponEffects, long>(CouponEffects.QuickChangeReload, 128L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Invincible, 512L),
				new ValueTuple<CouponEffects, long>(CouponEffects.FullMetalJack, 2048L),
				new ValueTuple<CouponEffects, long>(CouponEffects.HollowPoint, 8192L),
				new ValueTuple<CouponEffects, long>(CouponEffects.HollowPointPlus, 32768L),
				new ValueTuple<CouponEffects, long>(CouponEffects.C4SpeedKit, 65536L),
				new ValueTuple<CouponEffects, long>(CouponEffects.ExtraGrenade, 131072L),
				new ValueTuple<CouponEffects, long>(CouponEffects.ExtraThrowGrenade, 262144L),
				new ValueTuple<CouponEffects, long>(CouponEffects.JackHollowPoint, 524288L),
				new ValueTuple<CouponEffects, long>(CouponEffects.HP5, 1048576L),
				new ValueTuple<CouponEffects, long>(CouponEffects.HP10, 2097152L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Defense5, 4194304L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Defense10, 8388608L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Defense20, 16777216L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Defense90, 33554432L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Respawn20, 67108864L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Respawn30, 268435456L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Respawn50, 1073741824L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Respawn100, 8589934592L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Camoflage50, 34359738368L),
				new ValueTuple<CouponEffects, long>(CouponEffects.Camoflage99, 68719476736L)
			})
			{
				if (!Player.Effects.HasFlag((CouponEffects)valueTuple.Item1))
				{
					continue;
				}
				ıtem2 += valueTuple.Item2;
			}
			return ıtem2;
		}

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

		public static void LoadPlayerCharacters(Account Player)
		{
			List<CharacterModel> playerCharactersDB = DaoManagerSQL.GetPlayerCharactersDB(Player.PlayerId);
			if (playerCharactersDB.Count > 0)
			{
				Player.Character.Characters = playerCharactersDB;
			}
		}

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
				return;
			}
			if (!DaoManagerSQL.CreatePlayerMissionsDB(Player.PlayerId))
			{
				CLogger.Print("There was an error when creating Player Missions!", LoggerType.Warning, null);
			}
		}

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

		public static void ProcessBattlepass(Account Player)
		{
			BattlePassModel activeSeasonPass = SeasonChallengeXML.GetActiveSeasonPass();
			if (activeSeasonPass != null)
			{
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
						battlepass.LastRecord = 0;
						ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, new string[] { "id", "level", "premium", "total_points", "daily_points", "last_record" }, new object[] { battlepass.Id, battlepass.Level, battlepass.IsPremium, battlepass.TotalPoints, battlepass.DailyPoints, (long)((ulong)battlepass.LastRecord) });
					}
					ValueTuple<int, int, int, int> levelProgression = activeSeasonPass.GetLevelProgression(battlepass.TotalPoints);
					if (battlepass.Level != levelProgression.Item1)
					{
						battlepass.Level = levelProgression.Item1;
						ComDiv.UpdateDB("player_battlepass", "level", battlepass.Level, "owner_id", Player.PlayerId);
					}
					if (uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", battlepass.LastRecord)))) < uint.Parse(DateTimeUtil.Now("yyMMdd")))
					{
						battlepass.DailyPoints = 0;
						battlepass.LastRecord = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
						ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, new string[] { "daily_points", "last_record" }, new object[] { battlepass.DailyPoints, (long)((ulong)battlepass.LastRecord) });
					}
				}
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
			string str = string.Format("{0}", account_0.CafePC);
			if (!playerVIP.Benefit.Equals(str) && ComDiv.UpdateDB("player_vip", "last_benefit", str, "owner_id", account_0.PlayerId))
			{
				playerVIP.Benefit = str;
			}
			string_0 = "Valid Access";
			return true;
		}

		private static void smethod_1(Account account_0, int int_0)
		{
			CharacterModel character = account_0.Character.GetCharacter(int_0);
			if (character != null)
			{
				int ınt32 = 0;
				foreach (CharacterModel characterModel in account_0.Character.Characters)
				{
					if (characterModel.Slot == character.Slot)
					{
						continue;
					}
					characterModel.Slot = ınt32;
					DaoManagerSQL.UpdatePlayerCharacter(ınt32, characterModel.ObjectId, account_0.PlayerId);
					ınt32++;
				}
				if (DaoManagerSQL.DeletePlayerCharacter(character.ObjectId, account_0.PlayerId))
				{
					account_0.Character.RemoveCharacter(character);
				}
			}
		}

		private static void smethod_2(Account account_0)
		{
			List<ItemsModel> ıtems = account_0.Inventory.Items;
			lock (ıtems)
			{
				foreach (ItemsModel ıtem in ıtems)
				{
					if (ComDiv.GetIdStatics(ıtem.Id, 1) != 6 || account_0.Character.GetCharacter(ıtem.Id) != null)
					{
						continue;
					}
					AllUtils.CreateCharacter(account_0, ıtem);
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

		public static uint ValidateKey(long PlayerId, int SessionId, uint Unknown)
		{
			int unknown = (int)(Unknown % 999);
			int playerId = (int)(PlayerId % 999L);
			int sessionId = SessionId % 999;
			return uint.Parse(string.Format("{0:000}{1:000}{2:000}", unknown, playerId, sessionId));
		}

		public static void ValidatePlayerInventoryStatus(Account Player)
		{
			string str;
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
				AllUtils.smethod_2(Player);
			}
			if (!AllUtils.smethod_0(Player, out str))
			{
				foreach (ItemsModel pCCafeReward in TemplatePackXML.GetPCCafeRewards(Player.CafePC))
				{
					if (ComDiv.GetIdStatics(pCCafeReward.Id, 1) == 6 && Player.Character.GetCharacter(pCCafeReward.Id) != null)
					{
						AllUtils.smethod_1(Player, pCCafeReward.Id);
					}
					if (ComDiv.GetIdStatics(pCCafeReward.Id, 1) != 16)
					{
						continue;
					}
					CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(pCCafeReward.Id);
					if (couponEffect == null || (long)couponEffect.EffectFlag <= 0L || !Player.Effects.HasFlag(couponEffect.EffectFlag))
					{
						continue;
					}
					Player.Effects -= couponEffect.EffectFlag;
					DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
				}
				if (Player.CafePC > CafeEnum.None && ComDiv.UpdateDB("accounts", "pc_cafe", 0, "player_id", Player.PlayerId))
				{
					Player.CafePC = CafeEnum.None;
					if (!string.IsNullOrEmpty(str) && ComDiv.DeleteDB("player_vip", "owner_id", Player.PlayerId))
					{
						CLogger.Print(string.Format("VIP for UID: {0} Nick: {1} Deleted Due To {2}", Player.PlayerId, Player.Nickname, str), LoggerType.Info, null);
					}
					CLogger.Print(string.Format("Player PC Cafe was resetted by default into '{0}'; (UID: {1} Nick: {2})", Player.CafePC, Player.PlayerId, Player.Nickname), LoggerType.Info, null);
				}
			}
			else
			{
				List<ItemsModel> pCCafeRewards = TemplatePackXML.GetPCCafeRewards(Player.CafePC);
				lock (Player.Inventory.Items)
				{
					Player.Inventory.Items.AddRange(pCCafeRewards);
				}
				foreach (ItemsModel ıtemsModel in pCCafeRewards)
				{
					if (ComDiv.GetIdStatics(ıtemsModel.Id, 1) == 6 && Player.Character.GetCharacter(ıtemsModel.Id) == null)
					{
						AllUtils.CreateCharacter(Player, ıtemsModel);
					}
					if (ComDiv.GetIdStatics(ıtemsModel.Id, 1) != 16)
					{
						continue;
					}
					CouponFlag couponFlag = CouponEffectXML.GetCouponEffect(ıtemsModel.Id);
					if (couponFlag == null || (long)couponFlag.EffectFlag <= 0L || Player.Effects.HasFlag(couponFlag.EffectFlag))
					{
						continue;
					}
					Player.Effects |= couponFlag.EffectFlag;
					DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
				}
			}
		}
	}
}