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

namespace Server.Auth.Data.Utils
{
	// Token: 0x0200004A RID: 74
	public static class AllUtils
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000090C0 File Offset: 0x000072C0
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

		// Token: 0x06000101 RID: 257 RVA: 0x00009120 File Offset: 0x00007320
		public static void LoadPlayerInventory(Account Player)
		{
			List<ItemsModel> items = Player.Inventory.Items;
			lock (items)
			{
				Player.Inventory.Items.AddRange(DaoManagerSQL.GetPlayerInventoryItems(Player.PlayerId));
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000917C File Offset: 0x0000737C
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

		// Token: 0x06000103 RID: 259 RVA: 0x000091E8 File Offset: 0x000073E8
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
				AllUtils.smethod_2(Player);
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
					AllUtils.smethod_1(Player, itemsModel2.Id);
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

		// Token: 0x06000104 RID: 260 RVA: 0x00009520 File Offset: 0x00007720
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

		// Token: 0x06000105 RID: 261 RVA: 0x000095F8 File Offset: 0x000077F8
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

		// Token: 0x06000106 RID: 262 RVA: 0x00009638 File Offset: 0x00007838
		public static void LoadPlayerCharacters(Account Player)
		{
			List<CharacterModel> playerCharactersDB = DaoManagerSQL.GetPlayerCharactersDB(Player.PlayerId);
			if (playerCharactersDB.Count > 0)
			{
				Player.Character.Characters = playerCharactersDB;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009668 File Offset: 0x00007868
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

		// Token: 0x06000108 RID: 264 RVA: 0x000097F8 File Offset: 0x000079F8
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

		// Token: 0x06000109 RID: 265 RVA: 0x00009838 File Offset: 0x00007A38
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

		// Token: 0x0600010A RID: 266 RVA: 0x00009878 File Offset: 0x00007A78
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

		// Token: 0x0600010B RID: 267 RVA: 0x000098B4 File Offset: 0x00007AB4
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

		// Token: 0x0600010C RID: 268 RVA: 0x000098F4 File Offset: 0x00007AF4
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

		// Token: 0x0600010D RID: 269 RVA: 0x00009934 File Offset: 0x00007B34
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

		// Token: 0x0600010E RID: 270 RVA: 0x0000997C File Offset: 0x00007B7C
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

		// Token: 0x0600010F RID: 271 RVA: 0x000099BC File Offset: 0x00007BBC
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

		// Token: 0x06000110 RID: 272 RVA: 0x000099FC File Offset: 0x00007BFC
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

		// Token: 0x06000111 RID: 273 RVA: 0x00009A3C File Offset: 0x00007C3C
		public static bool DiscountPlayerItems(Account Player)
		{
			bool flag2;
			try
			{
				bool flag = false;
				uint num = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
				List<object> list = new List<object>();
				int num2 = ((Player.Bonus != null) ? Player.Bonus.Bonuses : 0);
				int num3 = ((Player.Bonus != null) ? Player.Bonus.FreePass : 0);
				List<ItemsModel> items = Player.Inventory.Items;
				lock (items)
				{
					for (int i = 0; i < Player.Inventory.Items.Count; i++)
					{
						ItemsModel itemsModel = Player.Inventory.Items[i];
						if (itemsModel.Count <= num && itemsModel.Equip == ItemEquipType.Temporary)
						{
							if (itemsModel.Category == ItemCategory.Coupon)
							{
								if (Player.Bonus == null)
								{
									goto IL_365;
								}
								if (!Player.Bonus.RemoveBonuses(itemsModel.Id))
								{
									if (itemsModel.Id == 1600014)
									{
										ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", Player.PlayerId);
										Player.Bonus.CrosshairColor = 4;
									}
									else if (itemsModel.Id == 1600006)
									{
										ComDiv.UpdateDB("accounts", "nick_color", 0, "player_id", Player.PlayerId);
										Player.NickColor = 0;
									}
									else if (itemsModel.Id == 1600009)
									{
										ComDiv.UpdateDB("player_bonus", "fake_rank", 55, "owner_id", Player.PlayerId);
										Player.Bonus.FakeRank = 55;
									}
									else if (itemsModel.Id == 1600010)
									{
										if (Player.Bonus.FakeNick.Length > 0)
										{
											ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", Player.PlayerId);
											ComDiv.UpdateDB("accounts", "nickname", Player.Bonus.FakeNick, "player_id", Player.PlayerId);
											Player.Nickname = Player.Bonus.FakeNick;
											Player.Bonus.FakeNick = "";
										}
									}
									else if (itemsModel.Id == 1600187)
									{
										ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", Player.PlayerId);
										Player.Bonus.MuzzleColor = 0;
									}
									else if (itemsModel.Id == 1600205)
									{
										ComDiv.UpdateDB("player_bonus", "nick_border_color", 0, "owner_id", Player.PlayerId);
										Player.Bonus.NickBorderColor = 0;
									}
								}
								CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemsModel.Id);
								if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects)0L && Player.Effects.HasFlag(couponEffect.EffectFlag))
								{
									Player.Effects -= (long)couponEffect.EffectFlag;
									flag = true;
								}
							}
							list.Add(itemsModel.ObjectId);
							Player.Inventory.Items.RemoveAt(i--);
						}
						else if (itemsModel.Count == 0U)
						{
							list.Add(itemsModel.ObjectId);
							Player.Inventory.Items.RemoveAt(i--);
						}
						IL_365:;
					}
				}
				if (list.Count > 0)
				{
					for (int j = 0; j < list.Count; j++)
					{
						ItemsModel item = Player.Inventory.GetItem((long)list[j]);
						if (item != null && item.Category == ItemCategory.Character && ComDiv.GetIdStatics(item.Id, 1) == 6)
						{
							AllUtils.smethod_1(Player, item.Id);
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
				int num4 = ComDiv.CheckEquipedItems(Player.Equipment, Player.Inventory.Items, false);
				if (num4 > 0)
				{
					DBQuery dbquery = new DBQuery();
					if ((num4 & 2) == 2)
					{
						ComDiv.UpdateWeapons(Player.Equipment, dbquery);
					}
					if ((num4 & 1) == 1)
					{
						ComDiv.UpdateChars(Player.Equipment, dbquery);
					}
					if ((num4 & 3) == 3)
					{
						ComDiv.UpdateItems(Player.Equipment, dbquery);
					}
					ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, dbquery.GetTables(), dbquery.GetValues());
				}
				flag2 = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009FE0 File Offset: 0x000081E0
		private static void smethod_1(Account account_0, int int_0)
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

		// Token: 0x06000113 RID: 275 RVA: 0x0000A094 File Offset: 0x00008294
		public static void CheckGameEvents(Account Player)
		{
			uint[] array = new uint[]
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
						@event.LastQuestDate = 0U;
						@event.LastQuestFinish = 0;
					}
					if (@event.LastQuestFinish == 0)
					{
						Player.Mission.Mission4 = 13;
						if (@event.LastQuestDate == 0U)
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
						@event.LastLoginDate = 0U;
						ComDiv.UpdateDB("player_events", "last_login_date", 0, "owner_id", Player.PlayerId);
					}
					if (uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", @event.LastLoginDate)))) < array[1])
					{
						foreach (int num in runningEvent2.Goods)
						{
							GoodsItem good = ShopManager.GetGood(num);
							if (good != null)
							{
								ComDiv.TryCreateItem(new ItemsModel(good.Item), Player.Inventory, Player.PlayerId);
							}
						}
						ComDiv.UpdateDB("player_events", "last_login_date", (long)((ulong)array[0]), "owner_id", Player.PlayerId);
						Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("LoginGiftMessage")));
					}
				}
				EventVisitModel runningEvent3 = EventVisitXML.GetRunningEvent();
				if (runningEvent3 != null && @event.LastVisitDate < runningEvent3.BeginDate)
				{
					@event.LastVisitDate = 0U;
					@event.LastVisitCheckDay = 0;
					@event.LastVisitSeqType = 0;
					EventVisitXML.ResetPlayerEvent(Player.PlayerId, @event);
				}
				EventXmasModel runningEvent4 = EventXmasXML.GetRunningEvent();
				if (runningEvent4 != null && @event.LastXmasDate < runningEvent4.BeginDate)
				{
					@event.LastXmasDate = 0U;
					ComDiv.UpdateDB("player_events", "last_xmas_date", 0, "owner_id", Player.PlayerId);
				}
				EventPlaytimeModel runningEvent5 = EventPlaytimeXML.GetRunningEvent();
				if (runningEvent5 != null)
				{
					if (@event.LastPlaytimeDate < runningEvent5.BeginDate)
					{
						@event.LastPlaytimeDate = 0U;
						@event.LastPlaytimeFinish = 0;
						@event.LastPlaytimeValue = 0L;
						EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, @event);
					}
					if (uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", @event.LastPlaytimeDate)))) < array[1])
					{
						@event.LastPlaytimeValue = 0L;
						@event.LastPlaytimeFinish = 0;
						EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, @event);
					}
				}
			}
			ComDiv.UpdateDB("accounts", "last_login_date", (long)((ulong)array[0]), "player_id", Player.PlayerId);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000A3C0 File Offset: 0x000085C0
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
						battlepass.LastRecord = 0U;
						ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, new string[] { "id", "level", "premium", "total_points", "daily_points", "last_record" }, new object[]
						{
							battlepass.Id,
							battlepass.Level,
							battlepass.IsPremium,
							battlepass.TotalPoints,
							battlepass.DailyPoints,
							(long)((ulong)battlepass.LastRecord)
						});
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
						ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, new string[] { "daily_points", "last_record" }, new object[]
						{
							battlepass.DailyPoints,
							(long)((ulong)battlepass.LastRecord)
						});
					}
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000A5D4 File Offset: 0x000087D4
		public static long LoadCouponEffects(Account Player)
		{
			long num = 0L;
			foreach (ValueTuple<CouponEffects, long> valueTuple in new List<ValueTuple<CouponEffects, long>>
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
				if (Player.Effects.HasFlag(valueTuple.Item1))
				{
					num += valueTuple.Item2;
				}
			}
			return num;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000A928 File Offset: 0x00008B28
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

		// Token: 0x06000117 RID: 279 RVA: 0x0000A968 File Offset: 0x00008B68
		private static void smethod_2(Account account_0)
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

		// Token: 0x06000118 RID: 280 RVA: 0x0000AA0C File Offset: 0x00008C0C
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
			if (!DaoManagerSQL.CreatePlayerCharacter(characterModel, Player.PlayerId))
			{
				CLogger.Print(string.Format("There is an error while cheating a character! (ID: {0}", Item.Id), LoggerType.Warning, null);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		public static uint GetFeatures()
		{
			AccountFeatures accountFeatures = (AccountFeatures)2389079934U;
			if (!AuthXender.Client.Config.EnableClan)
			{
				accountFeatures -= 4096U;
			}
			if (!AuthXender.Client.Config.EnableTicket)
			{
				accountFeatures -= 16384U;
			}
			if (!AuthXender.Client.Config.EnableTags)
			{
				accountFeatures -= 67108864U;
			}
			EventPlaytimeModel runningEvent = EventPlaytimeXML.GetRunningEvent();
			if (!AuthXender.Client.Config.EnablePlaytime || runningEvent == null || !runningEvent.EventIsEnabled())
			{
				accountFeatures -= 256U;
			}
			return (uint)accountFeatures;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public static uint ValidateKey(long PlayerId, int SessionId, uint Unknown)
		{
			int num = (int)(Unknown % 999U);
			int num2 = (int)(PlayerId % 999L);
			int num3 = SessionId % 999;
			return uint.Parse(string.Format("{0:000}{1:000}{2:000}", num, num2, num3));
		}
	}
}
