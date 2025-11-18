namespace Server.Auth.Data.Utils
{
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
    using System.Runtime.InteropServices;

    public static class AllUtils
    {
        public static void CheckGameEvents(Account Player)
        {
            uint[] numArray = new uint[] { uint.Parse(DateTimeUtil.Now("yyMMddHHmm")), uint.Parse(DateTimeUtil.Now("yyMMdd")) };
            PlayerEvent pE = Player.Event;
            if (pE != null)
            {
                EventQuestModel runningEvent = EventQuestXML.GetRunningEvent();
                if (runningEvent != null)
                {
                    uint lastQuestDate = pE.LastQuestDate;
                    int lastQuestFinish = pE.LastQuestFinish;
                    if (pE.LastQuestDate < runningEvent.BeginDate)
                    {
                        pE.LastQuestDate = 0;
                        pE.LastQuestFinish = 0;
                    }
                    if (pE.LastQuestFinish == 0)
                    {
                        Player.Mission.Mission4 = 13;
                        pE.LastQuestDate ??= numArray[0];
                    }
                    if ((pE.LastQuestDate != lastQuestDate) || (pE.LastQuestFinish != lastQuestFinish))
                    {
                        EventQuestXML.ResetPlayerEvent(Player.PlayerId, pE);
                    }
                }
                EventLoginModel model2 = EventLoginXML.GetRunningEvent();
                if (model2 != null)
                {
                    if (pE.LastLoginDate < model2.BeginDate)
                    {
                        pE.LastLoginDate = 0;
                        ComDiv.UpdateDB("player_events", "last_login_date", 0, "owner_id", Player.PlayerId);
                    }
                    if (uint.Parse($"{DateTimeUtil.Convert($"{pE.LastLoginDate}"):yyMMdd}") < numArray[1])
                    {
                        using (List<int>.Enumerator enumerator = model2.Goods.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                GoodsItem good = ShopManager.GetGood(enumerator.Current);
                                if (good != null)
                                {
                                    ComDiv.TryCreateItem(new ItemsModel(good.Item), Player.Inventory, Player.PlayerId);
                                }
                            }
                        }
                        ComDiv.UpdateDB("player_events", "last_login_date", (long) numArray[0], "owner_id", Player.PlayerId);
                        Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("LoginGiftMessage")));
                    }
                }
                EventVisitModel model3 = EventVisitXML.GetRunningEvent();
                if ((model3 != null) && (pE.LastVisitDate < model3.BeginDate))
                {
                    pE.LastVisitDate = 0;
                    pE.LastVisitCheckDay = 0;
                    pE.LastVisitSeqType = 0;
                    EventVisitXML.ResetPlayerEvent(Player.PlayerId, pE);
                }
                EventXmasModel model4 = EventXmasXML.GetRunningEvent();
                if ((model4 != null) && (pE.LastXmasDate < model4.BeginDate))
                {
                    pE.LastXmasDate = 0;
                    ComDiv.UpdateDB("player_events", "last_xmas_date", 0, "owner_id", Player.PlayerId);
                }
                EventPlaytimeModel model5 = EventPlaytimeXML.GetRunningEvent();
                if (model5 != null)
                {
                    if (pE.LastPlaytimeDate < model5.BeginDate)
                    {
                        pE.LastPlaytimeDate = 0;
                        pE.LastPlaytimeFinish = 0;
                        pE.LastPlaytimeValue = 0L;
                        EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, pE);
                    }
                    if (uint.Parse($"{DateTimeUtil.Convert($"{pE.LastPlaytimeDate}"):yyMMdd}") < numArray[1])
                    {
                        pE.LastPlaytimeValue = 0L;
                        pE.LastPlaytimeFinish = 0;
                        EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, pE);
                    }
                }
            }
            ComDiv.UpdateDB("accounts", "last_login_date", (long) numArray[0], "player_id", Player.PlayerId);
        }

        public static void CreateCharacter(Account Player, ItemsModel Item)
        {
            CharacterModel model1 = new CharacterModel();
            model1.Id = Item.Id;
            model1.Name = Item.Name;
            model1.Slot = Player.Character.GenSlotId(Item.Id);
            model1.CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
            model1.PlayTime = 0;
            CharacterModel character = model1;
            Player.Character.AddCharacter(character);
            if (!DaoManagerSQL.CreatePlayerCharacter(character, Player.PlayerId))
            {
                CLogger.Print($"There is an error while cheating a character! (ID: {Item.Id}", LoggerType.Warning, null);
            }
        }

        public static bool DiscountPlayerItems(Account Player)
        {
            try
            {
                bool flag2;
                bool flag = false;
                uint num = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
                List<object> list = new List<object>();
                int num2 = (Player.Bonus != null) ? Player.Bonus.Bonuses : 0;
                int num3 = (Player.Bonus != null) ? Player.Bonus.FreePass : 0;
                List<ItemsModel> list2 = Player.Inventory.Items;
                lock (list2)
                {
                    int num5 = 0;
                    while (true)
                    {
                        while (true)
                        {
                            if (num5 < Player.Inventory.Items.Count)
                            {
                                ItemsModel model = Player.Inventory.Items[num5];
                                if ((model.Count > num) || (model.Equip != ItemEquipType.Temporary))
                                {
                                    if (model.Count == 0)
                                    {
                                        list.Add(model.ObjectId);
                                        Player.Inventory.Items.RemoveAt(num5--);
                                    }
                                }
                                else
                                {
                                    if (model.Category == ItemCategory.Coupon)
                                    {
                                        if (Player.Bonus == null)
                                        {
                                            break;
                                        }
                                        if (!Player.Bonus.RemoveBonuses(model.Id))
                                        {
                                            if (model.Id == 0x186a0e)
                                            {
                                                ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", Player.PlayerId);
                                                Player.Bonus.CrosshairColor = 4;
                                            }
                                            else if (model.Id == 0x186a06)
                                            {
                                                ComDiv.UpdateDB("accounts", "nick_color", 0, "player_id", Player.PlayerId);
                                                Player.NickColor = 0;
                                            }
                                            else if (model.Id == 0x186a09)
                                            {
                                                ComDiv.UpdateDB("player_bonus", "fake_rank", (int) 0x37, "owner_id", Player.PlayerId);
                                                Player.Bonus.FakeRank = 0x37;
                                            }
                                            else if (model.Id == 0x186a0a)
                                            {
                                                if (Player.Bonus.FakeNick.Length > 0)
                                                {
                                                    ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", Player.PlayerId);
                                                    ComDiv.UpdateDB("accounts", "nickname", Player.Bonus.FakeNick, "player_id", Player.PlayerId);
                                                    Player.Nickname = Player.Bonus.FakeNick;
                                                    Player.Bonus.FakeNick = "";
                                                }
                                            }
                                            else if (model.Id == 0x186abb)
                                            {
                                                ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", Player.PlayerId);
                                                Player.Bonus.MuzzleColor = 0;
                                            }
                                            else if (model.Id == 0x186acd)
                                            {
                                                ComDiv.UpdateDB("player_bonus", "nick_border_color", 0, "owner_id", Player.PlayerId);
                                                Player.Bonus.NickBorderColor = 0;
                                            }
                                        }
                                        CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(model.Id);
                                        if ((couponEffect != null) && ((couponEffect.EffectFlag > 0L) && Player.Effects.HasFlag(couponEffect.EffectFlag)))
                                        {
                                            Player.Effects -= couponEffect.EffectFlag;
                                            flag = true;
                                        }
                                    }
                                    list.Add(model.ObjectId);
                                    Player.Inventory.Items.RemoveAt(num5--);
                                }
                            }
                            else
                            {
                                goto TR_001F;
                            }
                            break;
                        }
                        num5++;
                    }
                }
                return flag2;
            TR_001F:
                if (list.Count > 0)
                {
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= list.Count)
                        {
                            ComDiv.DeleteDB("player_items", "object_id", list.ToArray(), "owner_id", Player.PlayerId);
                            break;
                        }
                        ItemsModel model2 = Player.Inventory.GetItem((long) list[num6]);
                        if ((model2 != null) && ((model2.Category == ItemCategory.Character) && (ComDiv.GetIdStatics(model2.Id, 1) == 6)))
                        {
                            smethod_1(Player, model2.Id);
                        }
                        num6++;
                    }
                }
                list.Clear();
                list = null;
                if ((Player.Bonus != null) && ((Player.Bonus.Bonuses != num2) || (Player.Bonus.FreePass != num3)))
                {
                    DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
                }
                if (Player.Effects < 0L)
                {
                    Player.Effects = 0L;
                }
                if (flag)
                {
                    ComDiv.UpdateDB("accounts", "coupon_effect", (long) Player.Effects, "player_id", Player.PlayerId);
                }
                int num4 = ComDiv.CheckEquipedItems(Player.Equipment, Player.Inventory.Items, false);
                if (num4 > 0)
                {
                    DBQuery query = new DBQuery();
                    if ((num4 & 2) == 2)
                    {
                        ComDiv.UpdateWeapons(Player.Equipment, query);
                    }
                    if ((num4 & 1) == 1)
                    {
                        ComDiv.UpdateChars(Player.Equipment, query);
                    }
                    if ((num4 & 3) == 3)
                    {
                        ComDiv.UpdateItems(Player.Equipment, query);
                    }
                    ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, query.GetTables(), query.GetValues());
                    query = null;
                }
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static uint GetFeatures()
        {
            AccountFeatures features = (AccountFeatures) (-1905887362);
            if (!AuthXender.Client.Config.EnableClan)
            {
                features -= AccountFeatures.CLAN_ONLY;
            }
            if (!AuthXender.Client.Config.EnableTicket)
            {
                features -= AccountFeatures.TICKET_ONLY;
            }
            if (!AuthXender.Client.Config.EnableTags)
            {
                features -= AccountFeatures.TAGS_ONLY;
            }
            EventPlaytimeModel runningEvent = EventPlaytimeXML.GetRunningEvent();
            if (!AuthXender.Client.Config.EnablePlaytime || ((runningEvent == null) || !runningEvent.EventIsEnabled()))
            {
                features -= AccountFeatures.PLAYTIME_ONLY;
            }
            return (uint) features;
        }

        public static List<ItemsModel> LimitationIndex(Account Player, List<ItemsModel> Items)
        {
            int num = 600 + Player.InventoryPlus;
            if (Items.Count > num)
            {
                int index = num / 3;
                if (Items.Count > index)
                {
                    Items.RemoveRange(index, Items.Count - num);
                }
            }
            return Items;
        }

        public static long LoadCouponEffects(Account Player)
        {
            long num = 0L;
            List<(CouponEffects, long)> list1 = new List<(CouponEffects, long)>();
            list1.Add((CouponEffects.Ammo40, 1L));
            list1.Add((CouponEffects.Ammo10, 2L));
            list1.Add((CouponEffects.GetDroppedWeapon, 4L));
            list1.Add((CouponEffects.QuickChangeWeapon, 0x10L));
            list1.Add((CouponEffects.QuickChangeReload, 0x80L));
            list1.Add((CouponEffects.Invincible, 0x200L));
            list1.Add((CouponEffects.FullMetalJack, 0x800L));
            list1.Add((CouponEffects.HollowPoint, 0x2000L));
            list1.Add((CouponEffects.HollowPointPlus, 0x8000L));
            list1.Add((CouponEffects.C4SpeedKit, 0x10000L));
            list1.Add((CouponEffects.ExtraGrenade, 0x20000L));
            list1.Add((CouponEffects.ExtraThrowGrenade, 0x40000L));
            list1.Add((CouponEffects.JackHollowPoint, 0x80000L));
            list1.Add((CouponEffects.HP5, 0x100000L));
            list1.Add((CouponEffects.HP10, 0x200000L));
            list1.Add((CouponEffects.Defense5, 0x400000L));
            list1.Add((CouponEffects.Defense10, 0x800000L));
            list1.Add((CouponEffects.Defense20, 0x1000000L));
            list1.Add((CouponEffects.Defense90, 0x2000000L));
            list1.Add((CouponEffects.Respawn20, 0x4000000L));
            list1.Add((CouponEffects.Respawn30, 0x10000000L));
            list1.Add((CouponEffects.Respawn50, 0x40000000L));
            list1.Add((CouponEffects.Respawn100, 0x200000000L));
            list1.Add((CouponEffects.Camoflage50, 0x800000000L));
            list1.Add((CouponEffects.Camoflage99, 0x1000000000L));
            foreach ((CouponEffects, long) tuple in list1)
            {
                if (Player.Effects.HasFlag(tuple.Item1))
                {
                    num += tuple.Item2;
                }
            }
            return num;
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
                CLogger.Print("There was an error when creating Player Battlepass!", LoggerType.Warning, null);
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
            }
            else if (!DaoManagerSQL.CreatePlayerCompetitiveDB(Player.PlayerId))
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
            }
            else if (!DaoManagerSQL.CreatePlayerConfigDB(Player.PlayerId))
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
            }
            else if (!DaoManagerSQL.CreatePlayerEquipmentsDB(Player.PlayerId))
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
            }
            else if (!DaoManagerSQL.CreatePlayerEventDB(Player.PlayerId))
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
            List<ItemsModel> list = Player.Inventory.Items;
            lock (list)
            {
                Player.Inventory.Items.AddRange(DaoManagerSQL.GetPlayerInventoryItems(Player.PlayerId));
            }
        }

        public static void LoadPlayerMissions(Account Player)
        {
            PlayerMissions missions = DaoManagerSQL.GetPlayerMissionsDB(Player.PlayerId, Player.Mission.Mission1, Player.Mission.Mission2, Player.Mission.Mission3, Player.Mission.Mission4);
            if (missions != null)
            {
                Player.Mission = missions;
            }
            else if (!DaoManagerSQL.CreatePlayerMissionsDB(Player.PlayerId))
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
            }
            else if (!DaoManagerSQL.CreatePlayerQuickstartsDB(Player.PlayerId))
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
            }
            else if (!DaoManagerSQL.CreatePlayerReportDB(Player.PlayerId))
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
            }
            else if (!DaoManagerSQL.CreatePlayerStatBattlecupsDB(Player.PlayerId))
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
            }
            else if (!DaoManagerSQL.CreatePlayerTitlesDB(Player.PlayerId))
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
                        string[] cOLUMNS = new string[] { "id", "level", "premium", "total_points", "daily_points", "last_record" };
                        object[] vALUES = new object[] { battlepass.Id, battlepass.Level, battlepass.IsPremium, battlepass.TotalPoints, battlepass.DailyPoints, battlepass.LastRecord };
                        ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, cOLUMNS, vALUES);
                    }
                    (int, int, int, int) levelProgression = activeSeasonPass.GetLevelProgression(battlepass.TotalPoints);
                    if (battlepass.Level != levelProgression.Item1)
                    {
                        battlepass.Level = levelProgression.Item1;
                        ComDiv.UpdateDB("player_battlepass", "level", battlepass.Level, "owner_id", Player.PlayerId);
                    }
                    if (uint.Parse($"{DateTimeUtil.Convert($"{battlepass.LastRecord}"):yyMMdd}") < uint.Parse(DateTimeUtil.Now("yyMMdd")))
                    {
                        battlepass.DailyPoints = 0;
                        battlepass.LastRecord = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                        string[] cOLUMNS = new string[] { "daily_points", "last_record" };
                        object[] vALUES = new object[] { battlepass.DailyPoints, battlepass.LastRecord };
                        ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, cOLUMNS, vALUES);
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
            string str = $"{account_0.CafePC}";
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
                int slot = 0;
                foreach (CharacterModel model2 in account_0.Character.Characters)
                {
                    if (model2.Slot != character.Slot)
                    {
                        model2.Slot = slot;
                        DaoManagerSQL.UpdatePlayerCharacter(slot, model2.ObjectId, account_0.PlayerId);
                        slot++;
                    }
                }
                if (DaoManagerSQL.DeletePlayerCharacter(character.ObjectId, account_0.PlayerId))
                {
                    account_0.Character.RemoveCharacter(character);
                }
            }
        }

        private static void smethod_2(Account account_0)
        {
            List<ItemsModel> list = account_0.Inventory.Items;
            List<ItemsModel> list2 = list;
            lock (list2)
            {
                foreach (ItemsModel model in list)
                {
                    if ((ComDiv.GetIdStatics(model.Id, 1) == 6) && (account_0.Character.GetCharacter(model.Id) == null))
                    {
                        CreateCharacter(account_0, model);
                    }
                }
            }
        }

        public static void ValidateAuthLevel(Account Player)
        {
            if (!Enum.IsDefined(typeof(AccessLevel), Player.Access))
            {
                AccessLevel level = Player.AuthLevel();
                if (ComDiv.UpdateDB("accounts", "access_level", (int) level, "player_id", Player.PlayerId))
                {
                    Player.Access = level;
                }
            }
        }

        public static uint ValidateKey(long PlayerId, int SessionId, uint Unknown)
        {
            int num = (int) (Unknown % 0x3e7);
            int num2 = (int) (PlayerId % 0x3e7L);
            int num3 = SessionId % 0x3e7;
            return uint.Parse($"{num:000}{num2:000}{num3:000}");
        }

        public static void ValidatePlayerInventoryStatus(Account Player)
        {
            string str;
            Player.Inventory.LoadBasicItems();
            if (Player.Rank >= 0x2e)
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
            if (!smethod_0(Player, out str))
            {
                foreach (ItemsModel model2 in TemplatePackXML.GetPCCafeRewards(Player.CafePC))
                {
                    if ((ComDiv.GetIdStatics(model2.Id, 1) == 6) && (Player.Character.GetCharacter(model2.Id) != null))
                    {
                        smethod_1(Player, model2.Id);
                    }
                    if (ComDiv.GetIdStatics(model2.Id, 1) == 0x10)
                    {
                        CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(model2.Id);
                        if ((couponEffect != null) && ((couponEffect.EffectFlag > 0L) && Player.Effects.HasFlag(couponEffect.EffectFlag)))
                        {
                            Player.Effects -= couponEffect.EffectFlag;
                            DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
                        }
                    }
                }
                if ((Player.CafePC > CafeEnum.None) && ComDiv.UpdateDB("accounts", "pc_cafe", 0, "player_id", Player.PlayerId))
                {
                    Player.CafePC = CafeEnum.None;
                    if (!string.IsNullOrEmpty(str) && ComDiv.DeleteDB("player_vip", "owner_id", Player.PlayerId))
                    {
                        CLogger.Print($"VIP for UID: {Player.PlayerId} Nick: {Player.Nickname} Deleted Due To {str}", LoggerType.Info, null);
                    }
                    CLogger.Print($"Player PC Cafe was resetted by default into '{Player.CafePC}'; (UID: {Player.PlayerId} Nick: {Player.Nickname})", LoggerType.Info, null);
                }
            }
            else
            {
                List<ItemsModel> pCCafeRewards = TemplatePackXML.GetPCCafeRewards(Player.CafePC);
                List<ItemsModel> list2 = Player.Inventory.Items;
                lock (list2)
                {
                    Player.Inventory.Items.AddRange(pCCafeRewards);
                }
                foreach (ItemsModel model in pCCafeRewards)
                {
                    if ((ComDiv.GetIdStatics(model.Id, 1) == 6) && (Player.Character.GetCharacter(model.Id) == null))
                    {
                        CreateCharacter(Player, model);
                    }
                    if (ComDiv.GetIdStatics(model.Id, 1) == 0x10)
                    {
                        CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(model.Id);
                        if ((couponEffect != null) && ((couponEffect.EffectFlag > 0L) && !Player.Effects.HasFlag(couponEffect.EffectFlag)))
                        {
                            Player.Effects |= couponEffect.EffectFlag;
                            DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
                        }
                    }
                }
            }
        }
    }
}

