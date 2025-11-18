namespace Server.Game.Data.Utils
{
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
    using Server.Game;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public static class AllUtils
    {
        public static int AddFriend(Account Owner, Account Friend, int state)
        {
            int num;
            if ((Owner == null) || (Friend == null))
            {
                return -1;
            }
            try
            {
                FriendModel friend = Owner.Friend.GetFriend(Friend.PlayerId);
                if (friend != null)
                {
                    if (friend.Removed)
                    {
                        friend.Removed = false;
                        DaoManagerSQL.UpdatePlayerFriendBlock(Owner.PlayerId, friend);
                        SendFriendInfo.Load(Owner, friend, 1);
                    }
                    num = 1;
                }
                else
                {
                    using (NpgsqlConnection connection = ConnectionSQL.GetInstance().Conn())
                    {
                        connection.Open();
                        NpgsqlCommand command1 = connection.CreateCommand();
                        command1.CommandType = CommandType.Text;
                        command1.Parameters.AddWithValue("@friend", Friend.PlayerId);
                        command1.Parameters.AddWithValue("@owner", Owner.PlayerId);
                        command1.Parameters.AddWithValue("@state", state);
                        command1.CommandText = "INSERT INTO player_friends (id, owner_id, state) VALUES (@friend, @owner, @state)";
                        command1.ExecuteNonQuery();
                        command1.Dispose();
                        connection.Dispose();
                        connection.Close();
                    }
                    List<FriendModel> friends = Owner.Friend.Friends;
                    lock (friends)
                    {
                        FriendModel model1 = new FriendModel(Friend.PlayerId, Friend.Rank, Friend.NickColor, Friend.Nickname, Friend.IsOnline, Friend.Status);
                        model1.State = state;
                        model1.Removed = false;
                        FriendModel item = model1;
                        Owner.Friend.Friends.Add(item);
                        SendFriendInfo.Load(Owner, item, 0);
                    }
                    num = 0;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("AllUtils.AddFriend: " + exception.Message, LoggerType.Error, exception);
                num = -1;
            }
            return num;
        }

        public static void BattleEndKills(RoomModel room)
        {
            smethod_5(room, room.IsBotMode());
        }

        public static void BattleEndKills(RoomModel room, bool isBotMode)
        {
            smethod_5(room, isBotMode);
        }

        public static void BattleEndKillsFreeForAll(RoomModel room)
        {
            smethod_6(room);
        }

        public static void BattleEndPlayersCount(RoomModel Room, bool IsBotMode)
        {
            if (!(ReferenceEquals(Room, null) | IsBotMode) && Room.IsPreparing())
            {
                int num = 0;
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                SlotModel[] slots = Room.Slots;
                int index = 0;
                while (true)
                {
                    if (index >= slots.Length)
                    {
                        if ((((num2 == 0) || (num == 0)) && (Room.State == RoomState.BATTLE)) || (((num4 == 0) || (num3 == 0)) && (Room.State <= RoomState.PRE_BATTLE)))
                        {
                            EndBattle(Room, IsBotMode);
                        }
                        break;
                    }
                    SlotModel model = slots[index];
                    if (model.State == SlotState.BATTLE)
                    {
                        if (model.Team == TeamEnum.FR_TEAM)
                        {
                            num2++;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    else if (model.State >= SlotState.LOAD)
                    {
                        if (model.Team == TeamEnum.FR_TEAM)
                        {
                            num4++;
                        }
                        else
                        {
                            num3++;
                        }
                    }
                    index++;
                }
            }
        }

        public static void BattleEndRound(RoomModel Room, TeamEnum Winner, RoundEndType Motive)
        {
            using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_battle_mission_round_end_ack = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(Room, Winner, Motive))
            {
                Room.SendPacketToPlayers(protocol_battle_mission_round_end_ack, SlotState.BATTLE, 0);
            }
            Room.StopBomb();
            int roundsByMask = Room.GetRoundsByMask();
            if ((Room.FRRounds >= roundsByMask) || (Room.CTRounds >= roundsByMask))
            {
                EndBattle(Room, Room.IsBotMode(), Winner);
            }
            else
            {
                Room.ChangeRounds();
                RoundSync.SendUDPRoundSync(Room);
                Room.RoundRestart();
            }
        }

        public static void BattleEndRound(RoomModel Room, TeamEnum Winner, bool ForceRestart, FragInfos Kills, SlotModel Killer)
        {
            Class9 class2 = new Class9 {
                roomModel_0 = Room,
                teamEnum_0 = Winner,
                bool_0 = ForceRestart,
                fragInfos_0 = Kills,
                slotModel_0 = Killer
            };
            class2.roomModel_0.MatchEndTime.StartJob(0x4e2, new TimerCallback(class2.method_0));
        }

        public static void BattleEndRoundPlayersCount(RoomModel Room)
        {
            if (!Room.RoundTime.IsTimer() && ((Room.RoomType == RoomCondition.Bomb) || ((Room.RoomType == RoomCondition.Annihilation) || ((Room.RoomType == RoomCondition.Destroy) || (Room.RoomType == RoomCondition.Ace)))))
            {
                int num;
                int num2;
                int num3;
                int num4;
                Room.GetPlayingPlayers(true, out num, out num2, out num3, out num4);
                smethod_4(Room, ref num, ref num2, ref num3, ref num4);
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
                    BattleEndRound(Room, Room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM, false, null, null);
                }
                else if (num4 == num2)
                {
                    if (Room.SwapRound)
                    {
                        Room.CTRounds++;
                    }
                    else
                    {
                        Room.FRRounds++;
                    }
                    BattleEndRound(Room, Room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, true, null, null);
                }
            }
        }

        public static void CalculateBattlePass(Account Player, SlotModel Slot, BattlePassModel CurrentSC)
        {
            PlayerBattlepass battlepass = Player.Battlepass;
            if ((CurrentSC != null) && (battlepass != null))
            {
                if (battlepass.Id == CurrentSC.Id)
                {
                    if (battlepass.Level >= CurrentSC.Cards.Count)
                    {
                        Player.UpdateSeasonpass = true;
                    }
                    else
                    {
                        Slot.SeasonPoint += ComDiv.Percentage(Slot.Exp, 0x23);
                        int num = Slot.SeasonPoint + ComDiv.Percentage(Slot.SeasonPoint, Slot.BonusBattlePass);
                        battlepass.TotalPoints += num;
                        battlepass.DailyPoints += num;
                        uint num2 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                        string[] cOLUMNS = new string[] { "total_points", "daily_points", "last_record" };
                        object[] vALUES = new object[] { battlepass.TotalPoints, battlepass.DailyPoints, num2 };
                        if (ComDiv.UpdateDB("player_battlepass", "owner_id", Player.PlayerId, cOLUMNS, vALUES))
                        {
                            battlepass.LastRecord = num2;
                        }
                        Player.UpdateSeasonpass = true;
                    }
                }
                smethod_19(Player, battlepass, CurrentSC);
            }
        }

        public static void CalculateCompetitive(RoomModel Room, Account Player, SlotModel Slot, bool HaveWin)
        {
            if (Room.Competitive)
            {
                int num = (((HaveWin ? 50 : -30) + (2 * Slot.AllKills)) + Slot.AllAssists) - Slot.AllDeaths;
                Player.Competitive.Points += num;
                if (Player.Competitive.Points < 0)
                {
                    Player.Competitive.Points = 0;
                }
                smethod_20(Player.Competitive);
                object[] argumens = new object[] { num };
                string label = Translation.GetLabel("CompetitivePointsEarned", argumens);
                object[] objArray2 = new object[] { Player.Competitive.Rank().Name, Player.Competitive.Points, Player.Competitive.Rank().Points };
                string str2 = Translation.GetLabel("CompetitiveRank", objArray2);
                Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, label + "\n\r" + str2));
            }
        }

        public static bool CanCloseSlotCompetitive(RoomModel Room, SlotModel Closing)
        {
            Class8 class2 = new Class8 {
                slotModel_0 = Closing
            };
            return (Room.Slots.Where<SlotModel>(new Func<SlotModel, bool>(class2.method_0)).Count<SlotModel>() > 3);
        }

        public static bool CanOpenSlotCompetitive(RoomModel Room, SlotModel Opening)
        {
            Class7 class2 = new Class7 {
                slotModel_0 = Opening
            };
            return (Room.Slots.Where<SlotModel>(new Func<SlotModel, bool>(class2.method_0)).Count<SlotModel>() < 5);
        }

        public static bool ChangeCostume(SlotModel Slot, TeamEnum CostumeTeam)
        {
            if (Slot.CostumeTeam != CostumeTeam)
            {
                Slot.CostumeTeam = CostumeTeam;
            }
            return (Slot.CostumeTeam == CostumeTeam);
        }

        public static bool ChannelRequirementCheck(Account Player, ChannelModel Channel) => 
            !Player.IsGM() ? (((Channel.Type != ChannelType.Clan) || (Player.ClanId != 0)) ? (((Channel.Type != ChannelType.Novice) || ((Player.Statistic.GetKDRatio() <= 40) || (Player.Statistic.GetSeasonKDRatio() <= 40))) ? (((Channel.Type != ChannelType.Training) || (Player.Rank < 4)) ? (((Channel.Type != ChannelType.Special) || (Player.Rank > 0x19)) ? (Channel.Type == ChannelType.Blocked) : true) : true) : true) : true) : false;

        public static bool Check4vs4(RoomModel Room, bool IsLeader, ref int PlayerFR, ref int PlayerCT, ref int TotalEnemies)
        {
            if (!IsLeader)
            {
                return ((PlayerFR + PlayerCT) >= 8);
            }
            int num = (PlayerFR + PlayerCT) + 1;
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
                            if ((slot != null) && (slot.State == SlotState.READY))
                            {
                                Room.ChangeSlotState(i, SlotState.NORMAL, false);
                                if ((i % 2) == 0)
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
                    TotalEnemies = ((Room.LeaderSlot % 2) != 0) ? ref PlayerFR : ref PlayerCT;
                    return true;
                }
            }
            return false;
        }

        public static bool CheckClanMatchRestrict(RoomModel room)
        {
            if (room.ChannelType == ChannelType.Clan)
            {
                using (IEnumerator<ClanTeam> enumerator = smethod_7(room).Values.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        ClanTeam current = enumerator.Current;
                        if ((current.PlayersFR >= 1) && (current.PlayersCT >= 1))
                        {
                            room.BlockedClan = true;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool CheckDuplicateCouponEffects(Account Player, int CouponId)
        {
            bool flag = false;
            List<(int, CouponEffects, bool)> list1 = new List<(int, CouponEffects, bool)>();
            list1.Add((0x186a41, CouponEffects.Defense10 | CouponEffects.Defense20 | CouponEffects.Defense5, true));
            list1.Add((0x186a4f, CouponEffects.Defense10 | CouponEffects.Defense5 | CouponEffects.Defense90, true));
            list1.Add((0x186a2c, CouponEffects.Defense20 | CouponEffects.Defense5 | CouponEffects.Defense90, true));
            list1.Add((0x186a1e, CouponEffects.Defense10 | CouponEffects.Defense20 | CouponEffects.Defense90, true));
            list1.Add((0x186a4e, CouponEffects.FullMetalJack | CouponEffects.HollowPoint | CouponEffects.JackHollowPoint, true));
            list1.Add((0x186a20, CouponEffects.FullMetalJack | CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint, true));
            list1.Add((0x186a1f, CouponEffects.HollowPoint | CouponEffects.HollowPointPlus | CouponEffects.JackHollowPoint, true));
            list1.Add((0x186a24, CouponEffects.FullMetalJack | CouponEffects.HollowPoint | CouponEffects.HollowPointPlus, true));
            list1.Add((0x186a1c, CouponEffects.HP5, false));
            list1.Add((0x186a28, CouponEffects.HP10, false));
            list1.Add((0x186ad1, CouponEffects.Camoflage50, false));
            list1.Add((0x186ad0, CouponEffects.Camoflage99, false));
            foreach ((int, CouponEffects, bool) tuple in list1)
            {
                if (smethod_17(CouponId, Player.Effects, tuple))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        public static bool ClanMatchCheck(RoomModel Room, ChannelType Type, int TotalEnemys, out uint Error)
        {
            if (ConfigLoader.IsTestMode || (Type != ChannelType.Clan))
            {
                Error = 0;
                return false;
            }
            if (!Have2ClansToClanMatch(Room))
            {
                Error = 0x80001071;
                return true;
            }
            if ((TotalEnemys > 0) && !HavePlayersToClanMatch(Room))
            {
                Error = 0x80001072;
                return true;
            }
            Error = 0;
            return false;
        }

        public static bool ClassicModeCheck(Account Player, RoomModel Room)
        {
            TRuleModel model = GameRuleXML.CheckTRuleByRoomName(Room.Name);
            if (model == null)
            {
                return false;
            }
            PlayerEquipment equipment = Player.Equipment;
            if (equipment == null)
            {
                string[] textArray1 = new string[5];
                textArray1[0] = "Player '";
                textArray1[1] = Player.Nickname;
                textArray1[2] = "' has invalid equipment (Error) on ";
                textArray1[3] = ConfigLoader.TournamentRule ? "Enabled" : "Disabled";
                string[] local1 = textArray1;
                local1[4] = " Tournament Rules!";
                CLogger.Print(string.Concat(local1), LoggerType.Warning, null);
                return false;
            }
            List<string> list = new List<string>();
            if (model.BanIndexes.Count > 0)
            {
                foreach (int num in model.BanIndexes)
                {
                    if (!GameRuleXML.IsBlocked(num, equipment.WeaponPrimary, ref list, Translation.GetLabel("Primary")) && (!GameRuleXML.IsBlocked(num, equipment.WeaponSecondary, ref list, Translation.GetLabel("Secondary")) && (!GameRuleXML.IsBlocked(num, equipment.WeaponMelee, ref list, Translation.GetLabel("Melee")) && (!GameRuleXML.IsBlocked(num, equipment.WeaponExplosive, ref list, Translation.GetLabel("Explosive")) && (!GameRuleXML.IsBlocked(num, equipment.WeaponSpecial, ref list, Translation.GetLabel("Special")) && (!GameRuleXML.IsBlocked(num, equipment.CharaRedId, ref list, Translation.GetLabel("Character")) && (!GameRuleXML.IsBlocked(num, equipment.CharaBlueId, ref list, Translation.GetLabel("Character")) && (!GameRuleXML.IsBlocked(num, equipment.PartHead, ref list, Translation.GetLabel("PartHead")) && (!GameRuleXML.IsBlocked(num, equipment.PartFace, ref list, Translation.GetLabel("PartFace")) && !GameRuleXML.IsBlocked(num, equipment.BeretItem, ref list, Translation.GetLabel("BeretItem")))))))))))
                    {
                        GameRuleXML.IsBlocked(num, equipment.AccessoryId, ref list, Translation.GetLabel("Accessory"));
                    }
                }
            }
            if (list.Count <= 0)
            {
                return false;
            }
            object[] argumens = new object[] { string.Join(", ", list.ToArray()) };
            Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("ClassicModeWarn", argumens)));
            return true;
        }

        public static void ClassicModeCheck(RoomModel Room, PlayerEquipment Equip)
        {
            if (ConfigLoader.TournamentRule)
            {
                TRuleModel model = GameRuleXML.CheckTRuleByRoomName(Room.Name);
                if ((model != null) && (model.BanIndexes.Count > 0))
                {
                    foreach (int num in model.BanIndexes)
                    {
                        if (GameRuleXML.IsBlocked(num, Equip.WeaponPrimary))
                        {
                            Equip.WeaponPrimary = 0x1925c;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.WeaponSecondary))
                        {
                            Equip.WeaponSecondary = 0x31513;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.WeaponMelee))
                        {
                            Equip.WeaponMelee = 0x497c9;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.WeaponExplosive))
                        {
                            Equip.WeaponExplosive = 0x635d9;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.WeaponSpecial))
                        {
                            Equip.WeaponSpecial = 0x7c061;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.PartHead))
                        {
                            Equip.PartHead = 0x3ba57860;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.PartFace))
                        {
                            Equip.PartFace = 0x3ba6ff00;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.PartJacket))
                        {
                            Equip.PartJacket = 0x3ba885a0;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.PartPocket))
                        {
                            Equip.PartPocket = 0x3baa0c40;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.PartGlove))
                        {
                            Equip.PartGlove = 0x3bab92e0;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.PartBelt))
                        {
                            Equip.PartBelt = 0x3bad1980;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.PartHolster))
                        {
                            Equip.PartHolster = 0x3baea020;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.PartSkin))
                        {
                            Equip.PartSkin = 0x3bb026c0;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.BeretItem))
                        {
                            Equip.BeretItem = 0;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.DinoItem))
                        {
                            Equip.DinoItem = 0x16e55f;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.AccessoryId))
                        {
                            Equip.AccessoryId = 0;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.SprayId))
                        {
                            Equip.SprayId = 0;
                            continue;
                        }
                        if (GameRuleXML.IsBlocked(num, Equip.NameCardId))
                        {
                            Equip.NameCardId = 0;
                        }
                    }
                }
            }
        }

        public static bool CompetitiveMatchCheck(Account Player, RoomModel Room, out uint Error)
        {
            if (Room.Competitive)
            {
                foreach (SlotModel model in Room.Slots)
                {
                    if ((model != null) && ((model.State != SlotState.CLOSE) && (model.State < SlotState.READY)))
                    {
                        Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, Translation.GetLabel("CompetitiveFullSlot")));
                        Error = 0x80001072;
                        return true;
                    }
                }
            }
            Error = 0;
            return false;
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
            catch (Exception exception)
            {
                CLogger.Print("[AllUtils.CompleteMission2] " + exception.Message, LoggerType.Error, exception);
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
            catch (Exception exception)
            {
                CLogger.Print("[AllUtils.CompleteMission1] " + exception.Message, LoggerType.Error, exception);
            }
        }

        public static void CompleteMission(RoomModel room, Account player, SlotModel slot, MissionType autoComplete, int moreInfo)
        {
            smethod_9(room, player, slot, autoComplete, moreInfo);
        }

        public static void CompleteMission(RoomModel room, Account player, SlotModel slot, FragInfos kills, MissionType autoComplete, int moreInfo)
        {
            smethod_8(room, player, slot, kills, autoComplete, moreInfo);
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
            Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, Item));
            if (DaoManagerSQL.CreatePlayerCharacter(character, Player.PlayerId))
            {
                Player.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0, 3, character, Player));
            }
            else
            {
                Player.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0x80000000, 0xff, null, null));
            }
        }

        public static bool DiscountPlayerItems(SlotModel Slot, Account Player)
        {
            try
            {
                bool flag3;
                bool flag = false;
                bool flag2 = false;
                uint num = Convert.ToUInt32(DateTimeUtil.Now("yyMMddHHmm"));
                List<ItemsModel> list = new List<ItemsModel>();
                List<object> list2 = new List<object>();
                int num2 = (Player.Bonus != null) ? Player.Bonus.Bonuses : 0;
                int num3 = (Player.Bonus != null) ? Player.Bonus.FreePass : 0;
                List<ItemsModel> list3 = Player.Inventory.Items;
                lock (list3)
                {
                    int num5 = 0;
                    while (true)
                    {
                        while (true)
                        {
                            if (num5 < Player.Inventory.Items.Count)
                            {
                                ItemsModel item = Player.Inventory.Items[num5];
                                if ((item.Equip == ItemEquipType.Durable) && (Slot.ItemUsages.Contains(item.Id) && !Slot.SpecGM))
                                {
                                    if ((item.Count <= num) && ((item.Id == 0xc35d8) || ((item.Id == 0x2932ed) || (item.Id == 0xc35a9))))
                                    {
                                        DaoManagerSQL.DeletePlayerInventoryItem(item.ObjectId, Player.PlayerId);
                                    }
                                    uint num6 = item.Count - 1;
                                    item.Count = num6;
                                    if (num6 < 1)
                                    {
                                        list2.Add(item.ObjectId);
                                        Player.Inventory.Items.RemoveAt(num5--);
                                    }
                                    else
                                    {
                                        list.Add(item);
                                        ComDiv.UpdateDB("player_items", "count", (long) item.Count, "object_id", item.ObjectId, "owner_id", Player.PlayerId);
                                    }
                                }
                                else if ((item.Count > num) || (item.Equip != ItemEquipType.Temporary))
                                {
                                    if (item.Count == 0)
                                    {
                                        list2.Add(item.ObjectId);
                                        Player.Inventory.Items.RemoveAt(num5--);
                                    }
                                }
                                else
                                {
                                    if (item.Category == ItemCategory.Coupon)
                                    {
                                        if (Player.Bonus == null)
                                        {
                                            break;
                                        }
                                        if (!Player.Bonus.RemoveBonuses(item.Id))
                                        {
                                            if (item.Id == 0x186a0e)
                                            {
                                                ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", Player.PlayerId);
                                                Player.Bonus.CrosshairColor = 4;
                                                flag = true;
                                            }
                                            else if (item.Id == 0x186a06)
                                            {
                                                ComDiv.UpdateDB("accounts", "nick_color", 0, "player_id", Player.PlayerId);
                                                Player.NickColor = 0;
                                                if (Player.Room != null)
                                                {
                                                    using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK protocol_room_get_color_nick_ack = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(Player.SlotId, Player.NickColor))
                                                    {
                                                        Player.Room.SendPacketToPlayers(protocol_room_get_color_nick_ack);
                                                    }
                                                    Player.Room.UpdateSlotsInfo();
                                                }
                                                flag = true;
                                            }
                                            else if (item.Id == 0x186a09)
                                            {
                                                ComDiv.UpdateDB("player_bonus", "fake_rank", (int) 0x37, "owner_id", Player.PlayerId);
                                                Player.Bonus.FakeRank = 0x37;
                                                if (Player.Room != null)
                                                {
                                                    using (PROTOCOL_ROOM_GET_RANK_ACK protocol_room_get_rank_ack = new PROTOCOL_ROOM_GET_RANK_ACK(Player.SlotId, Player.Rank))
                                                    {
                                                        Player.Room.SendPacketToPlayers(protocol_room_get_rank_ack);
                                                    }
                                                    Player.Room.UpdateSlotsInfo();
                                                }
                                                flag = true;
                                            }
                                            else if (item.Id == 0x186a0a)
                                            {
                                                ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", Player.PlayerId);
                                                ComDiv.UpdateDB("accounts", "nickname", Player.Bonus.FakeNick, "player_id", Player.PlayerId);
                                                Player.Nickname = Player.Bonus.FakeNick;
                                                Player.Bonus.FakeNick = "";
                                                if (Player.Room != null)
                                                {
                                                    using (PROTOCOL_ROOM_GET_NICKNAME_ACK protocol_room_get_nickname_ack = new PROTOCOL_ROOM_GET_NICKNAME_ACK(Player.SlotId, Player.Nickname))
                                                    {
                                                        Player.Room.SendPacketToPlayers(protocol_room_get_nickname_ack);
                                                    }
                                                    Player.Room.UpdateSlotsInfo();
                                                }
                                                flag = true;
                                            }
                                            else if (item.Id == 0x186abb)
                                            {
                                                ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", Player.PlayerId);
                                                Player.Bonus.MuzzleColor = 0;
                                                if (Player.Room != null)
                                                {
                                                    using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK protocol_room_get_color_muzzle_flash_ack = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(Player.SlotId, Player.Bonus.MuzzleColor))
                                                    {
                                                        Player.Room.SendPacketToPlayers(protocol_room_get_color_muzzle_flash_ack);
                                                    }
                                                }
                                                flag = true;
                                            }
                                            else if (item.Id == 0x186acd)
                                            {
                                                ComDiv.UpdateDB("player_bonus", "nick_border_color", 0, "owner_id", Player.PlayerId);
                                                Player.Bonus.NickBorderColor = 0;
                                                if (Player.Room != null)
                                                {
                                                    using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK protocol_room_get_nick_outline_color_ack = new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(Player.SlotId, Player.Bonus.NickBorderColor))
                                                    {
                                                        Player.Room.SendPacketToPlayers(protocol_room_get_nick_outline_color_ack);
                                                    }
                                                }
                                                flag = true;
                                            }
                                        }
                                        CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(item.Id);
                                        if ((couponEffect != null) && ((couponEffect.EffectFlag > 0L) && Player.Effects.HasFlag(couponEffect.EffectFlag)))
                                        {
                                            Player.Effects -= couponEffect.EffectFlag;
                                            flag2 = true;
                                        }
                                    }
                                    list2.Add(item.ObjectId);
                                    Player.Inventory.Items.RemoveAt(num5--);
                                }
                            }
                            else
                            {
                                goto TR_0024;
                            }
                            break;
                        }
                        num5++;
                    }
                }
                return flag3;
            TR_0024:
                if (list2.Count > 0)
                {
                    int num7 = 0;
                    while (true)
                    {
                        if (num7 >= list2.Count)
                        {
                            ComDiv.DeleteDB("player_items", "object_id", list2.ToArray(), "owner_id", Player.PlayerId);
                            break;
                        }
                        ItemsModel model2 = Player.Inventory.GetItem((long) list2[num7]);
                        if ((model2 != null) && ((model2.Category == ItemCategory.Character) && (ComDiv.GetIdStatics(model2.Id, 1) == 6)))
                        {
                            smethod_3(Player, model2.Id);
                        }
                        Player.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(1, (long) list2[num7]));
                        num7++;
                    }
                }
                list2.Clear();
                list2 = null;
                if ((Player.Bonus != null) && ((Player.Bonus.Bonuses != num2) || (Player.Bonus.FreePass != num3)))
                {
                    DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
                }
                if (Player.Effects < 0L)
                {
                    Player.Effects = 0L;
                }
                if (list.Count > 0)
                {
                    Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, Player, list));
                }
                list.Clear();
                list = null;
                if (flag2)
                {
                    ComDiv.UpdateDB("accounts", "coupon_effect", (long) Player.Effects, "player_id", Player.PlayerId);
                }
                if (flag)
                {
                    Player.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, Player));
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
                    Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK(Player, Slot));
                    Slot.Equipment = Player.Equipment;
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

        public static void EnableQuestMission(Account Player)
        {
            PlayerEvent event2 = Player.Event;
            if ((event2 != null) && ((event2.LastQuestFinish == 0) && (EventQuestXML.GetRunningEvent() != null)))
            {
                Player.Mission.Mission4 = 13;
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

        public static void EndBattle(RoomModel Room, bool IsBotMode, TeamEnum WinnerTeam)
        {
            List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
            if (allPlayers.Count > 0)
            {
                int num;
                int num2;
                byte[] buffer;
                Room.CalculateResult(WinnerTeam, IsBotMode);
                GetBattleResult(Room, out num, out num2, out buffer);
                foreach (Account local1 in allPlayers)
                {
                    List<Account>.Enumerator enumerator;
                    local1.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(enumerator.Current, WinnerTeam, num2, num, IsBotMode, buffer));
                    UpdateSeasonPass(local1);
                }
            }
            ResetBattleInfo(Room);
        }

        public static void EndBattleNoPoints(RoomModel Room)
        {
            List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
            if (allPlayers.Count > 0)
            {
                int num;
                int num2;
                byte[] buffer;
                GetBattleResult(Room, out num, out num2, out buffer);
                bool flag = Room.IsBotMode();
                foreach (Account local1 in allPlayers)
                {
                    List<Account>.Enumerator enumerator;
                    local1.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(enumerator.Current, TeamEnum.TEAM_DRAW, num2, num, flag, buffer));
                    UpdateSeasonPass(local1);
                }
            }
            ResetBattleInfo(Room);
        }

        public static void EndMatchMission(RoomModel Room, Account Player, SlotModel Slot, TeamEnum WinnerTeam)
        {
            if (WinnerTeam != TeamEnum.TEAM_DRAW)
            {
                CompleteMission(Room, Player, Slot, (Slot.Team == WinnerTeam) ? MissionType.WIN : MissionType.DEFEAT, 0);
            }
        }

        public static void FreepassEffect(Account Player, SlotModel Slot, RoomModel Room, bool IsBotMode)
        {
            DBQuery query = new DBQuery();
            if ((Player.Bonus.FreePass == 0) || ((Player.Bonus.FreePass == 1) && (Room.ChannelType == ChannelType.Clan)))
            {
                if (IsBotMode || (Slot.State < SlotState.BATTLE_READY))
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
                    query.AddQuery("gold", Player.Gold);
                }
                StatisticTotal basic = Player.Statistic.Basic;
                int num = basic.EscapesCount + 1;
                basic.EscapesCount = num;
                ComDiv.UpdateDB("player_stat_basics", "owner_id", Player.PlayerId, "escapes_count", num);
                StatisticSeason season = Player.Statistic.Season;
                num = season.EscapesCount + 1;
                season.EscapesCount = num;
                ComDiv.UpdateDB("player_stat_seasons", "owner_id", Player.PlayerId, "escapes_count", num);
            }
            else
            {
                if (Room.State != RoomState.BATTLE)
                {
                    return;
                }
                int num2 = 0;
                int num3 = 0;
                if (IsBotMode)
                {
                    int num4 = Room.IngameAiLevel * (150 + Slot.AllDeaths);
                    num4++;
                    int num5 = Slot.Score / num4;
                    num3 += num5;
                    num2 += num5;
                }
                else
                {
                    int num6 = ((Slot.AllKills != 0) || (Slot.AllDeaths != 0)) ? ((int) Slot.InBattleTime(DateTimeUtil.Now())) : 0;
                    if ((Room.RoomType != RoomCondition.Bomb) && ((Room.RoomType != RoomCondition.FreeForAll) && (Room.RoomType != RoomCondition.Destroy)))
                    {
                        num2 = ((int) ((Slot.Score + (((double) num6) / 2.5)) + (Slot.AllDeaths * 1.8))) + (Slot.Objects * 20);
                        num3 = ((int) ((Slot.Score + (((double) num6) / 3.0)) + (Slot.AllDeaths * 1.8))) + (Slot.Objects * 20);
                    }
                    else
                    {
                        num2 = ((int) ((Slot.Score + (((double) num6) / 2.5)) + (Slot.AllDeaths * 2.2))) + (Slot.Objects * 20);
                        num3 = ((int) ((Slot.Score + (((double) num6) / 3.0)) + (Slot.AllDeaths * 2.2))) + (Slot.Objects * 20);
                    }
                }
                Player.Exp += (ConfigLoader.MaxExpReward < num2) ? ConfigLoader.MaxExpReward : num2;
                Player.Gold += (ConfigLoader.MaxGoldReward < num3) ? ConfigLoader.MaxGoldReward : num3;
                if (num3 > 0)
                {
                    query.AddQuery("gold", Player.Gold);
                }
                if (num2 > 0)
                {
                    query.AddQuery("experience", Player.Exp);
                }
            }
            ComDiv.UpdateDB("accounts", "player_id", Player.PlayerId, query.GetTables(), query.GetValues());
        }

        public static void GenerateMissionAwards(Account Player, DBQuery query)
        {
            try
            {
                PlayerMissions mission = Player.Mission;
                int actualMission = mission.ActualMission;
                int currentMissionId = mission.GetCurrentMissionId();
                int currentCard = mission.GetCurrentCard();
                if ((currentMissionId > 0) && !mission.SelectedCard)
                {
                    int num4 = 0;
                    int num5 = 0;
                    byte[] currentMissionList = mission.GetCurrentMissionList();
                    foreach (MissionCardModel model in MissionCardRAW.GetCards(currentMissionId, -1))
                    {
                        if (currentMissionList[model.ArrayIdx] >= model.MissionLimit)
                        {
                            num5++;
                            if (model.CardBasicId == currentCard)
                            {
                                num4++;
                            }
                        }
                    }
                    if (num5 < 40)
                    {
                        if ((num4 == 4) && !mission.SelectedCard)
                        {
                            MissionCardAwards award = MissionCardRAW.GetAward(currentMissionId, currentCard);
                            if (award != null)
                            {
                                int ribbon = Player.Ribbon;
                                int medal = Player.Medal;
                                int ensign = Player.Ensign;
                                Player.Ribbon += award.Ribbon;
                                Player.Medal += award.Medal;
                                Player.Ensign += award.Ensign;
                                Player.Gold += award.Gold;
                                Player.Exp += award.Exp;
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
                            }
                            mission.SelectedCard = true;
                            Player.SendPacket(new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(1, 1, Player));
                        }
                    }
                    else
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
                        MissionAwards awards2 = MissionAwardXML.GetAward(currentMissionId);
                        if (awards2 != null)
                        {
                            Player.MasterMedal += awards2.MasterMedal;
                            Player.Exp += awards2.Exp;
                            Player.Gold += awards2.Gold;
                        }
                        List<ItemsModel> missionAwards = MissionCardRAW.GetMissionAwards(currentMissionId);
                        if (missionAwards.Count > 0)
                        {
                            Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, missionAwards));
                        }
                        Player.SendPacket(new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_ACK(0x111, 4, Player));
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
                        string[] cOLUMNS = new string[] { $"card{actualMission + 1}", $"mission{actualMission + 1}_raw" };
                        object[] vALUES = new object[] { 0, new byte[0] };
                        ComDiv.UpdateDB("player_missions", "owner_id", Player.PlayerId, cOLUMNS, vALUES);
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
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("AllUtils.GenerateMissionAwards: " + exception.Message, LoggerType.Error, exception);
            }
        }

        public static TeamEnum GetBalanceTeamIdx(RoomModel Room, bool InBattle, TeamEnum PlayerTeamIdx)
        {
            int num = (!InBattle || (PlayerTeamIdx != TeamEnum.FR_TEAM)) ? 0 : 1;
            int num2 = (!InBattle || (PlayerTeamIdx != TeamEnum.CT_TEAM)) ? 0 : 1;
            foreach (SlotModel model in Room.Slots)
            {
                if (((model.State == SlotState.NORMAL) && !InBattle) || ((model.State >= SlotState.LOAD) & InBattle))
                {
                    if (model.Team == TeamEnum.FR_TEAM)
                    {
                        num++;
                    }
                    else
                    {
                        num2++;
                    }
                }
            }
            return (((num + 1) < num2) ? TeamEnum.FR_TEAM : (((num2 + 1) < (num + 1)) ? TeamEnum.CT_TEAM : TeamEnum.ALL_TEAM));
        }

        public static void GetBattleResult(RoomModel Room, out int MissionFlag, out int SlotFlag, out byte[] Data)
        {
            MissionFlag = 0;
            SlotFlag = 0;
            Data = new byte[0x132];
            if (Room != null)
            {
                using (SyncServerPacket packet = new SyncServerPacket())
                {
                    SlotModel[] slots = Room.Slots;
                    int index = 0;
                    while (true)
                    {
                        if (index >= slots.Length)
                        {
                            slots = Room.Slots;
                            index = 0;
                            while (true)
                            {
                                if (index >= slots.Length)
                                {
                                    slots = Room.Slots;
                                    index = 0;
                                    while (true)
                                    {
                                        if (index >= slots.Length)
                                        {
                                            slots = Room.Slots;
                                            index = 0;
                                            while (true)
                                            {
                                                if (index >= slots.Length)
                                                {
                                                    slots = Room.Slots;
                                                    index = 0;
                                                    while (true)
                                                    {
                                                        if (index >= slots.Length)
                                                        {
                                                            slots = Room.Slots;
                                                            index = 0;
                                                            while (true)
                                                            {
                                                                if (index >= slots.Length)
                                                                {
                                                                    Data = packet.ToArray();
                                                                    break;
                                                                }
                                                                SlotModel model6 = slots[index];
                                                                packet.WriteH((ushort) model6.BonusCafePoint);
                                                                packet.WriteH((ushort) model6.BonusItemPoint);
                                                                packet.WriteH((ushort) model6.BonusEventPoint);
                                                                index++;
                                                            }
                                                            break;
                                                        }
                                                        SlotModel model5 = slots[index];
                                                        packet.WriteH((ushort) model5.BonusCafeExp);
                                                        packet.WriteH((ushort) model5.BonusItemExp);
                                                        packet.WriteH((ushort) model5.BonusEventExp);
                                                        index++;
                                                    }
                                                    break;
                                                }
                                                SlotModel model4 = slots[index];
                                                packet.WriteC((byte) model4.BonusFlags);
                                                index++;
                                            }
                                            break;
                                        }
                                        SlotModel model3 = slots[index];
                                        packet.WriteH((ushort) model3.Gold);
                                        index++;
                                    }
                                    break;
                                }
                                SlotModel model2 = slots[index];
                                packet.WriteH((ushort) model2.Exp);
                                index++;
                            }
                            break;
                        }
                        SlotModel model = slots[index];
                        if (model.State >= SlotState.LOAD)
                        {
                            int flag = model.Flag;
                            if (model.MissionsCompleted)
                            {
                                MissionFlag += flag;
                            }
                            SlotFlag += flag;
                        }
                        index++;
                    }
                }
            }
        }

        public static List<int> GetDinossaurs(RoomModel Room, bool ForceNewTRex, int ForceRexIdx)
        {
            List<int> list = new List<int>();
            if (Room.IsDinoMode(""))
            {
                TeamEnum team = (Room.Rounds == 1) ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM;
                int[] teamArray = Room.GetTeamArray(team);
                int index = 0;
                while (true)
                {
                    if (index >= teamArray.Length)
                    {
                        if ((((Room.TRex == -1) || (Room.Slots[Room.TRex].State <= SlotState.BATTLE_LOAD)) | ForceNewTRex) && ((list.Count > 1) && Room.IsDinoMode("DE")))
                        {
                            if ((ForceRexIdx >= 0) && list.Contains(ForceRexIdx))
                            {
                                Room.TRex = ForceRexIdx;
                            }
                            else if (ForceRexIdx == -2)
                            {
                                Room.TRex = list[new Random().Next(0, list.Count)];
                            }
                        }
                        break;
                    }
                    int num2 = teamArray[index];
                    SlotModel model = Room.Slots[num2];
                    if ((model.State == SlotState.BATTLE) && !model.SpecGM)
                    {
                        list.Add(num2);
                    }
                    index++;
                }
            }
            return list;
        }

        public static int GetKillScore(KillingMessage msg)
        {
            int num = 0;
            if ((msg == KillingMessage.MassKill) || (msg == KillingMessage.PiercingShot))
            {
                num += 6;
            }
            else if (msg == KillingMessage.ChainStopper)
            {
                num += 8;
            }
            else if (msg == KillingMessage.Headshot)
            {
                num += 10;
            }
            else if (msg == KillingMessage.ChainHeadshot)
            {
                num += 14;
            }
            else if (msg == KillingMessage.ChainSlugger)
            {
                num += 6;
            }
            else if (msg == KillingMessage.ObjectDefense)
            {
                num += 7;
            }
            else if (msg != KillingMessage.Suicide)
            {
                num += 5;
            }
            return num;
        }

        public static int GetNewSlotId(int SlotIdx) => 
            ((SlotIdx % 2) == 0) ? (SlotIdx + 1) : (SlotIdx - 1);

        public static void GetReadyPlayers(RoomModel Room, ref int FRPlayers, ref int CTPlayers, ref int TotalEnemys)
        {
            int num = 0;
            for (int i = 0; i < Room.Slots.Length; i++)
            {
                SlotModel model = Room.Slots[i];
                if (model.State == SlotState.READY)
                {
                    if ((Room.RoomType == RoomCondition.FreeForAll) && (i > 0))
                    {
                        num++;
                    }
                    else if (model.Team == TeamEnum.FR_TEAM)
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
            else if ((Room.LeaderSlot % 2) == 0)
            {
                TotalEnemys = CTPlayers;
            }
            else
            {
                TotalEnemys = FRPlayers;
            }
        }

        public static (byte[], int[]) GetRewardData(RoomModel Room, List<SlotModel> Slots)
        {
            byte[] buffer = new byte[5];
            int[] numArray = new int[5];
            for (int i = 0; i < 5; i++)
            {
                buffer[i] = 0xff;
                numArray[i] = 0;
            }
            int num = 0;
            if (!Room.IsBotMode() && (Slots.Count > 0))
            {
                Func<SlotModel, bool> predicate = Class5.<>9__125_0;
                if (Class5.<>9__125_0 == null)
                {
                    Func<SlotModel, bool> local1 = Class5.<>9__125_0;
                    predicate = Class5.<>9__125_0 = new Func<SlotModel, bool>(Class5.<>9.method_0);
                }
                Func<SlotModel, int> keySelector = Class5.<>9__125_1;
                if (Class5.<>9__125_1 == null)
                {
                    Func<SlotModel, int> local2 = Class5.<>9__125_1;
                    keySelector = Class5.<>9__125_1 = new Func<SlotModel, int>(Class5.<>9.method_1);
                }
                SlotModel model = Slots.Where<SlotModel>(predicate).OrderByDescending<SlotModel, int>(keySelector).FirstOrDefault<SlotModel>();
                if (model != null)
                {
                    smethod_24(Room, model, BattleRewardType.MVP, 5, ref num, ref buffer, ref numArray);
                }
                Func<SlotModel, bool> func3 = Class5.<>9__125_2;
                if (Class5.<>9__125_2 == null)
                {
                    Func<SlotModel, bool> local3 = Class5.<>9__125_2;
                    func3 = Class5.<>9__125_2 = new Func<SlotModel, bool>(Class5.<>9.method_2);
                }
                Func<SlotModel, int> func4 = Class5.<>9__125_3;
                if (Class5.<>9__125_3 == null)
                {
                    Func<SlotModel, int> local4 = Class5.<>9__125_3;
                    func4 = Class5.<>9__125_3 = new Func<SlotModel, int>(Class5.<>9.method_3);
                }
                SlotModel model2 = Slots.Where<SlotModel>(func3).OrderByDescending<SlotModel, int>(func4).FirstOrDefault<SlotModel>();
                if (model2 != null)
                {
                    smethod_24(Room, model2, BattleRewardType.AssistKing, 5, ref num, ref buffer, ref numArray);
                }
                Func<SlotModel, bool> func5 = Class5.<>9__125_4;
                if (Class5.<>9__125_4 == null)
                {
                    Func<SlotModel, bool> local5 = Class5.<>9__125_4;
                    func5 = Class5.<>9__125_4 = new Func<SlotModel, bool>(Class5.<>9.method_4);
                }
                Func<SlotModel, int> func6 = Class5.<>9__125_5;
                if (Class5.<>9__125_5 == null)
                {
                    Func<SlotModel, int> local6 = Class5.<>9__125_5;
                    func6 = Class5.<>9__125_5 = new Func<SlotModel, int>(Class5.<>9.method_5);
                }
                SlotModel model3 = Slots.Where<SlotModel>(func5).OrderByDescending<SlotModel, int>(func6).FirstOrDefault<SlotModel>();
                if (model3 != null)
                {
                    smethod_24(Room, model3, BattleRewardType.MultiKill, 5, ref num, ref buffer, ref numArray);
                }
            }
            return (buffer, numArray);
        }

        public static int GetSlotsFlag(RoomModel Room, bool OnlyNoSpectators, bool MissionSuccess)
        {
            if (Room == null)
            {
                return 0;
            }
            int num = 0;
            foreach (SlotModel model in Room.Slots)
            {
                if ((model.State >= SlotState.LOAD) && ((MissionSuccess && model.MissionsCompleted) || (!MissionSuccess && (!OnlyNoSpectators || !model.Spectator))))
                {
                    num += model.Flag;
                }
            }
            return num;
        }

        public static TeamEnum GetWinnerTeam(RoomModel room)
        {
            if (room == null)
            {
                return TeamEnum.TEAM_DRAW;
            }
            TeamEnum enum2 = TeamEnum.TEAM_DRAW;
            if ((room.RoomType == RoomCondition.Bomb) || ((room.RoomType == RoomCondition.Destroy) || ((room.RoomType == RoomCondition.Annihilation) || ((room.RoomType == RoomCondition.Defense) || (room.RoomType == RoomCondition.Destroy)))))
            {
                if (room.CTRounds == room.FRRounds)
                {
                    enum2 = TeamEnum.TEAM_DRAW;
                }
                else if (room.CTRounds > room.FRRounds)
                {
                    enum2 = TeamEnum.CT_TEAM;
                }
                else if (room.CTRounds < room.FRRounds)
                {
                    enum2 = TeamEnum.FR_TEAM;
                }
            }
            else if (room.IsDinoMode("DE"))
            {
                if (room.CTDino == room.FRDino)
                {
                    enum2 = TeamEnum.TEAM_DRAW;
                }
                else if (room.CTDino > room.FRDino)
                {
                    enum2 = TeamEnum.CT_TEAM;
                }
                else if (room.CTDino < room.FRDino)
                {
                    enum2 = TeamEnum.FR_TEAM;
                }
            }
            else if (room.CTKills == room.FRKills)
            {
                enum2 = TeamEnum.TEAM_DRAW;
            }
            else if (room.CTKills > room.FRKills)
            {
                enum2 = TeamEnum.CT_TEAM;
            }
            else if (room.CTKills < room.FRKills)
            {
                enum2 = TeamEnum.FR_TEAM;
            }
            return enum2;
        }

        public static TeamEnum GetWinnerTeam(RoomModel room, int RedPlayers, int BluePlayers)
        {
            if (room == null)
            {
                return TeamEnum.TEAM_DRAW;
            }
            TeamEnum enum2 = TeamEnum.TEAM_DRAW;
            if (RedPlayers == 0)
            {
                enum2 = TeamEnum.CT_TEAM;
            }
            else if (BluePlayers == 0)
            {
                enum2 = TeamEnum.FR_TEAM;
            }
            return enum2;
        }

        public static void GetXmasReward(Account Player)
        {
            EventXmasModel runningEvent = EventXmasXML.GetRunningEvent();
            if (runningEvent != null)
            {
                PlayerEvent event2 = Player.Event;
                uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                if (((event2 != null) && ((event2.LastXmasDate <= runningEvent.BeginDate) || (event2.LastXmasDate > runningEvent.EndedDate))) && ComDiv.UpdateDB("player_events", "last_xmas_date", (long) num, "owner_id", Player.PlayerId))
                {
                    event2.LastXmasDate = num;
                    GoodsItem good = ShopManager.GetGood(runningEvent.GoodId);
                    if (good != null)
                    {
                        if ((ComDiv.GetIdStatics(good.Item.Id, 1) == 6) && (Player.Character.GetCharacter(good.Item.Id) == null))
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
            }
        }

        public static bool Have2ClansToClanMatch(RoomModel room) => 
            smethod_7(room).Count == 2;

        public static bool HavePlayersToClanMatch(RoomModel room)
        {
            bool flag = false;
            bool flag2 = false;
            foreach (ClanTeam team in smethod_7(room).Values)
            {
                if (team.PlayersFR >= 4)
                {
                    flag = true;
                    continue;
                }
                if (team.PlayersCT >= 4)
                {
                    flag2 = true;
                }
            }
            return (flag & flag2);
        }

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
                    return random.Next(15, 0x13);

                case 5:
                    return random.Next(20, 0x18);

                case 6:
                    return random.Next(0x19, 0x1d);

                case 7:
                    return random.Next(30, 0x22);

                case 8:
                    return random.Next(0x23, 0x27);

                case 9:
                    return random.Next(40, 0x2c);

                case 10:
                    return random.Next(0x2d, 0x31);
            }
            return 0x34;
        }

        public static void InsertItem(int ItemId, SlotModel Slot)
        {
            List<int> list = Slot.ItemUsages;
            lock (list)
            {
                if (!Slot.ItemUsages.Contains(ItemId))
                {
                    Slot.ItemUsages.Add(ItemId);
                }
            }
        }

        public static void LeaveHostEndBattlePVE(RoomModel Room, Account Player)
        {
            List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
            if (allPlayers.Count > 0)
            {
                using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_battle_giveupbattle_ack = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
                {
                    int num;
                    int num2;
                    byte[] buffer2;
                    byte[] completeBytes = protocol_battle_giveupbattle_ack.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-3");
                    TeamEnum winnerTeam = GetWinnerTeam(Room);
                    GetBattleResult(Room, out num, out num2, out buffer2);
                    foreach (Account local1 in allPlayers)
                    {
                        local1.SendCompletePacket(completeBytes, protocol_battle_giveupbattle_ack.GetType().Name);
                        local1.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(local1, winnerTeam, num2, num, true, buffer2));
                        UpdateSeasonPass(local1);
                    }
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
                TeamEnum resultType = GetWinnerTeam(Room, TeamFR, TeamCT);
                if (Room.State == RoomState.BATTLE)
                {
                    Room.CalculateResult(resultType, false);
                }
                using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_battle_giveupbattle_ack = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
                {
                    int num;
                    int num2;
                    byte[] buffer2;
                    byte[] completeBytes = protocol_battle_giveupbattle_ack.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-4");
                    GetBattleResult(Room, out num, out num2, out buffer2);
                    foreach (Account local1 in allPlayers)
                    {
                        local1.SendCompletePacket(completeBytes, protocol_battle_giveupbattle_ack.GetType().Name);
                        local1.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(local1, resultType, num2, num, false, buffer2));
                        UpdateSeasonPass(local1);
                    }
                }
            }
            ResetBattleInfo(Room);
        }

        public static void LeaveHostGiveBattlePVE(RoomModel Room, Account Player)
        {
            List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
            if (allPlayers.Count != 0)
            {
                int leaderSlot = Room.LeaderSlot;
                Room.SetNewLeader(-1, SlotState.BATTLE_READY, leaderSlot, true);
                using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_battle_giveupbattle_ack = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
                {
                    using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK protocol_battle_leaveppserver_ack = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(Room))
                    {
                        byte[] completeBytes = protocol_battle_giveupbattle_ack.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-1");
                        byte[] data = protocol_battle_leaveppserver_ack.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-2");
                        foreach (Account account in allPlayers)
                        {
                            SlotModel slot = Room.GetSlot(account.SlotId);
                            if (slot != null)
                            {
                                if (slot.State >= SlotState.PRESTART)
                                {
                                    account.SendCompletePacket(data, protocol_battle_leaveppserver_ack.GetType().Name);
                                }
                                account.SendCompletePacket(completeBytes, protocol_battle_giveupbattle_ack.GetType().Name);
                            }
                        }
                    }
                }
            }
        }

        public static void LeaveHostGiveBattlePVP(RoomModel Room, Account Player)
        {
            List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
            if (allPlayers.Count != 0)
            {
                int leaderSlot = Room.LeaderSlot;
                SlotState state = (Room.State == RoomState.BATTLE) ? SlotState.BATTLE_READY : SlotState.READY;
                Room.SetNewLeader(-1, state, leaderSlot, true);
                using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK protocol_battle_leaveppserver_ack = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(Room))
                {
                    using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_battle_giveupbattle_ack = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
                    {
                        byte[] completeBytes = protocol_battle_leaveppserver_ack.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-6");
                        byte[] data = protocol_battle_giveupbattle_ack.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-7");
                        foreach (Account account in allPlayers)
                        {
                            if (Room.Slots[account.SlotId].State >= SlotState.PRESTART)
                            {
                                account.SendCompletePacket(completeBytes, protocol_battle_leaveppserver_ack.GetType().Name);
                            }
                            account.SendCompletePacket(data, protocol_battle_giveupbattle_ack.GetType().Name);
                        }
                    }
                }
            }
        }

        public static void LeavePlayerEndBattlePVP(RoomModel Room, Account Player, int TeamFR, int TeamCT, out bool IsFinished)
        {
            IsFinished = true;
            TeamEnum resultType = GetWinnerTeam(Room, TeamFR, TeamCT);
            List<Account> allPlayers = Room.GetAllPlayers(SlotState.READY, 1);
            if (allPlayers.Count > 0)
            {
                if (Room.State == RoomState.BATTLE)
                {
                    Room.CalculateResult(resultType, false);
                }
                using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_battle_giveupbattle_ack = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
                {
                    int num;
                    int num2;
                    byte[] buffer2;
                    byte[] completeBytes = protocol_battle_giveupbattle_ack.GetCompleteBytes("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ-8");
                    GetBattleResult(Room, out num, out num2, out buffer2);
                    foreach (Account local1 in allPlayers)
                    {
                        local1.SendCompletePacket(completeBytes, protocol_battle_giveupbattle_ack.GetType().Name);
                        local1.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(local1, resultType, num2, num, false, buffer2));
                        UpdateSeasonPass(local1);
                    }
                }
            }
            ResetBattleInfo(Room);
        }

        public static void LeavePlayerQuitBattle(RoomModel Room, Account Player)
        {
            using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_battle_giveupbattle_ack = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(Player, 0))
            {
                Room.SendPacketToPlayers(protocol_battle_giveupbattle_ack, SlotState.READY, 1);
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

        public static bool PlayerIsBattle(Account Player)
        {
            SlotModel model2;
            RoomModel room = Player.Room;
            return ((room != null) && (room.GetSlot(Player.SlotId, out model2) && (model2.State >= SlotState.READY)));
        }

        public static void PlayTimeEvent(Account Player, EventPlaytimeModel EvPlaytime, bool IsBotMode, SlotModel Slot, long PlayedTime)
        {
            try
            {
                PlayerEvent event2 = Player.Event;
                if ((Player.Room != null) && (event2 != null))
                {
                    int num = EvPlaytime.Minutes1;
                    int num2 = EvPlaytime.Minutes2;
                    int num3 = EvPlaytime.Minutes3;
                    if ((num != 0) || ((num2 != 0) || (num3 != 0)))
                    {
                        long lastPlaytimeValue = event2.LastPlaytimeValue;
                        long lastPlaytimeFinish = event2.LastPlaytimeFinish;
                        long lastPlaytimeDate = event2.LastPlaytimeDate;
                        if ((event2.LastPlaytimeFinish >= 0) && (event2.LastPlaytimeFinish <= 2))
                        {
                            event2.LastPlaytimeValue += PlayedTime;
                            int num7 = (event2.LastPlaytimeFinish == 0) ? EvPlaytime.Minutes1 : ((event2.LastPlaytimeFinish == 1) ? EvPlaytime.Minutes2 : ((event2.LastPlaytimeFinish == 2) ? EvPlaytime.Minutes3 : 0));
                            if (num7 != 0)
                            {
                                int num8 = num7 * 60;
                                if (event2.LastPlaytimeValue >= num8)
                                {
                                    Random random = new Random();
                                    List<int> list = (event2.LastPlaytimeFinish == 0) ? EvPlaytime.Goods1 : ((event2.LastPlaytimeFinish == 1) ? EvPlaytime.Goods2 : ((event2.LastPlaytimeFinish == 2) ? EvPlaytime.Goods3 : new List<int>()));
                                    if (list.Count > 0)
                                    {
                                        GoodsItem good = ShopManager.GetGood(list[random.Next(0, list.Count)]);
                                        if (good != null)
                                        {
                                            if ((ComDiv.GetIdStatics(good.Item.Id, 1) == 6) && (Player.Character.GetCharacter(good.Item.Id) == null))
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
                                    event2.LastPlaytimeFinish++;
                                    event2.LastPlaytimeValue = 0L;
                                }
                                event2.LastPlaytimeDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                            }
                            else
                            {
                                return;
                            }
                        }
                        if ((event2.LastPlaytimeValue != lastPlaytimeValue) || ((event2.LastPlaytimeFinish != lastPlaytimeFinish) || (event2.LastPlaytimeDate != lastPlaytimeDate)))
                        {
                            EventPlaytimeXML.ResetPlayerEvent(Player.PlayerId, event2);
                        }
                    }
                    else
                    {
                        CLogger.Print($"Event Playtime Disabled Due To: 0 Value! (Minutes1: {num}; Minutes2: {num2}; Minutes3: {num3}", LoggerType.Warning, null);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("[AllUtils.PlayTimeEvent] " + exception.Message, LoggerType.Error, exception);
            }
        }

        public static void ProcessBattlepassPremiumBuy(Account Player)
        {
            PlayerBattlepass battlepass = Player.Battlepass;
            if (battlepass != null)
            {
                BattlePassModel seasonPass = SeasonChallengeXML.GetSeasonPass(battlepass.Id);
                if (seasonPass != null)
                {
                    battlepass.IsPremium = true;
                    int num = 0;
                    while (true)
                    {
                        int goodId;
                        int goodId;
                        if (num >= battlepass.Level)
                        {
                            ComDiv.UpdateDB("player_battlepass", "premium", battlepass.IsPremium, "owner_id", Player.PlayerId);
                            break;
                        }
                        PassBoxModel model2 = seasonPass.Cards[num];
                        PassItemModel premiumA = model2.PremiumA;
                        int[] numArray1 = new int[3];
                        if (premiumA != null)
                        {
                            goodId = premiumA.GoodId;
                        }
                        else
                        {
                            PassItemModel local1 = premiumA;
                            goodId = 0;
                        }
                        new int[3][1] = goodId;
                        PassItemModel premiumB = model2.PremiumB;
                        int[] numArray2 = new int[3];
                        int[] numArray3 = new int[3];
                        if (premiumB != null)
                        {
                            goodId = premiumB.GoodId;
                        }
                        else
                        {
                            PassItemModel local2 = premiumB;
                            goodId = 0;
                        }
                        new int[3][2] = goodId;
                        int[] numArray = new int[3];
                        smethod_22(Player, numArray);
                        num++;
                    }
                }
            }
        }

        public static List<ItemsModel> RepairableItems(Account Player, List<long> ObjectIds, out int Gold, out int Cash, out uint Error)
        {
            Gold = 0;
            Cash = 0;
            Error = 0;
            List<ItemsModel> list = new List<ItemsModel>();
            if (ObjectIds.Count > 0)
            {
                foreach (long num in ObjectIds)
                {
                    ItemsModel model = Player.Inventory.GetItem(num);
                    if (model == null)
                    {
                        Error = 0x80000110;
                        continue;
                    }
                    uint[] numArray = smethod_18(Player, model);
                    Gold = (int) (Gold + numArray[0]);
                    Cash = (int) (Cash + numArray[1]);
                    Error = numArray[2];
                    list.Add(model);
                }
            }
            return list;
        }

        public static void ResetBattleInfo(RoomModel Room)
        {
            foreach (SlotModel model in Room.Slots)
            {
                if ((model.PlayerId > 0L) && (model.State >= SlotState.LOAD))
                {
                    model.State = SlotState.NORMAL;
                    model.ResetSlot();
                }
                Room.CheckGhostSlot(model);
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
            Room.BattleStart = new DateTime();
            Room.TimeRoom = 0;
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

        public static void ResetSlotInfo(RoomModel Room, SlotModel Slot, bool UpdateInfo)
        {
            if (Slot.State >= SlotState.LOAD)
            {
                Room.ChangeSlotState(Slot, SlotState.NORMAL, UpdateInfo);
                Slot.ResetSlot();
            }
        }

        public static void RoomPingSync(RoomModel Room)
        {
            if (ComDiv.GetDuration(Room.LastPingSync) >= ConfigLoader.PingUpdateTimeSeconds)
            {
                byte[] buffer = new byte[0x12];
                for (int i = 0; i < 0x12; i++)
                {
                    buffer[i] = (byte) Room.Slots[i].Ping;
                }
                using (PROTOCOL_BATTLE_SENDPING_ACK protocol_battle_sendping_ack = new PROTOCOL_BATTLE_SENDPING_ACK(buffer))
                {
                    Room.SendPacketToPlayers(protocol_battle_sendping_ack, SlotState.BATTLE, 0);
                }
                Room.LastPingSync = DateTimeUtil.Now();
            }
        }

        public static void SendCompetitiveInfo(Account Player)
        {
            try
            {
                object[] argumens = new object[] { Player.Competitive.Rank().Name, Player.Competitive.Points, Player.Competitive.Rank().Points };
                Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), Player.Session.SessionId, Player.NickColor, true, Translation.GetLabel("CompetitiveRank", argumens)));
            }
            catch (Exception exception1)
            {
                CLogger.Print(exception1.ToString(), LoggerType.Error, null);
            }
        }

        public static bool ServerCommands(Account Player, string Text)
        {
            try
            {
                bool flag1 = CommandManager.TryParse(Text, Player);
                if (flag1)
                {
                    CLogger.Print($"Player '{Player.Nickname}' (UID: {Player.PlayerId}) Running Command '{Text}'", LoggerType.Command, null);
                }
                return flag1;
            }
            catch
            {
                Player.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, Translation.GetLabel("CommandsExceptionError")));
                return true;
            }
        }

        public static bool SlotValidMessage(SlotModel Sender, SlotModel Receiver)
        {
            if (((Sender.State != SlotState.NORMAL) && (Sender.State != SlotState.READY)) || ((Receiver.State != SlotState.NORMAL) && (Receiver.State != SlotState.READY)))
            {
                return ((Sender.State >= SlotState.LOAD) && ((Receiver.State >= SlotState.LOAD) && (Receiver.SpecGM || (Sender.SpecGM || (Sender.DeathState.HasFlag(DeadEnum.UseChat) || ((Sender.DeathState.HasFlag(DeadEnum.Dead) && Receiver.DeathState.HasFlag(DeadEnum.Dead)) || ((Sender.Spectator && Receiver.Spectator) || (Sender.DeathState.HasFlag(DeadEnum.Alive) && (Receiver.DeathState.HasFlag(DeadEnum.Alive) && ((!Sender.Spectator || !Receiver.Spectator) ? (!Sender.Spectator && !Receiver.Spectator) : true))))))))));
            }
            return true;
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

        private static ClassType smethod_1(ClassType classType_0) => 
            (classType_0 != ClassType.DualSMG) ? ((classType_0 != ClassType.DualHandGun) ? (((classType_0 == ClassType.DualKnife) || (classType_0 == ClassType.Knuckle)) ? ClassType.Knife : ((classType_0 != ClassType.DualShotgun) ? classType_0 : ClassType.Shotgun)) : ClassType.HandGun) : ClassType.SMG;

        private static int smethod_10(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0, FragInfos fragInfos_0)
        {
            int num = 0;
            if (((missionCardModel_0.WeaponReqId == 0) || (missionCardModel_0.WeaponReqId == int_0)) && ((missionCardModel_0.WeaponReq == ClassType.Unknown) || ((missionCardModel_0.WeaponReq == classType_0) || (missionCardModel_0.WeaponReq == classType_1))))
            {
                using (List<FragModel>.Enumerator enumerator = fragInfos_0.Frags.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if ((enumerator.Current.VictimSlot % 2) == (fragInfos_0.KillerSlot % 2))
                        {
                            continue;
                        }
                        num++;
                    }
                }
            }
            return num;
        }

        private static int smethod_11(MissionCardModel missionCardModel_0, FragInfos fragInfos_0)
        {
            int num = 0;
            foreach (FragModel model in fragInfos_0.Frags)
            {
                if (((model.VictimSlot % 2) != (fragInfos_0.KillerSlot % 2)) && ((missionCardModel_0.WeaponReq == ClassType.Unknown) || ((missionCardModel_0.WeaponReq == ((ClassType) model.WeaponClass)) || (missionCardModel_0.WeaponReq == smethod_1((ClassType) model.WeaponClass)))))
                {
                    num++;
                }
            }
            return num;
        }

        private static int smethod_12(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0, int int_1, FragModel fragModel_0)
        {
            if ((((missionCardModel_0.WeaponReqId != 0) && (missionCardModel_0.WeaponReqId != int_0)) || ((missionCardModel_0.WeaponReq != ClassType.Unknown) && ((missionCardModel_0.WeaponReq != classType_0) && (missionCardModel_0.WeaponReq != classType_1)))) || ((fragModel_0.VictimSlot % 2) == (int_1 % 2)))
            {
                return 0;
            }
            return 1;
        }

        private static int smethod_13(MissionCardModel missionCardModel_0, ClassType classType_0, ClassType classType_1, int int_0)
        {
            if (((missionCardModel_0.WeaponReqId != 0) && (missionCardModel_0.WeaponReqId != int_0)) || ((missionCardModel_0.WeaponReq != ClassType.Unknown) && ((missionCardModel_0.WeaponReq != classType_0) && (missionCardModel_0.WeaponReq != classType_1))))
            {
                return 0;
            }
            return 1;
        }

        private static int smethod_14(int int_0, SortedList<int, int> sortedList_0)
        {
            int num;
            return (!sortedList_0.TryGetValue(int_0, out num) ? 0 : num);
        }

        private static int smethod_15(int int_0, SortedList<int, int> sortedList_0)
        {
            int num;
            return (!sortedList_0.TryGetValue(int_0, out num) ? 0 : num);
        }

        private static int smethod_16(Account account_0, int int_0)
        {
            ItemsModel model = account_0.Inventory.GetItem(int_0);
            return ((model == null) ? 0 : model.Id);
        }

        private static bool smethod_17(int int_0, CouponEffects couponEffects_0, (int, CouponEffects, bool) valueTuple_0) => 
            (int_0 == valueTuple_0.Item1) ? (!valueTuple_0.Item3 ? couponEffects_0.HasFlag(valueTuple_0.Item2) : ((couponEffects_0 & ((CouponEffects) valueTuple_0.Item2)) > 0L)) : false;

        private static uint[] smethod_18(Account account_0, ItemsModel itemsModel_0)
        {
            uint[] numArray = new uint[3];
            ItemsRepair repairItem = ShopManager.GetRepairItem(itemsModel_0.Id);
            if (repairItem == null)
            {
                numArray[2] = 0x80000110;
                return numArray;
            }
            uint num = repairItem.Quantity - itemsModel_0.Count;
            if (repairItem.Point > repairItem.Cash)
            {
                uint num2 = (uint) ComDiv.Percentage(repairItem.Point, (int) num);
                if (account_0.Gold < num2)
                {
                    numArray[2] = 0x80000110;
                    return numArray;
                }
                numArray[0] = num2;
            }
            else
            {
                if (repairItem.Cash <= repairItem.Point)
                {
                    numArray[2] = 0x80000110;
                    return numArray;
                }
                uint num3 = (uint) ComDiv.Percentage(repairItem.Cash, (int) num);
                if (account_0.Cash < num3)
                {
                    numArray[2] = 0x80000110;
                    return numArray;
                }
                numArray[1] = num3;
            }
            itemsModel_0.Count = repairItem.Quantity;
            ComDiv.UpdateDB("player_items", "count", (long) itemsModel_0.Count, "owner_id", account_0.PlayerId, "id", itemsModel_0.Id);
            numArray[2] = 1;
            return numArray;
        }

        private static void smethod_19(Account account_0, PlayerBattlepass playerBattlepass_0, BattlePassModel battlePassModel_0)
        {
            PassBoxModel model = battlePassModel_0.Cards[playerBattlepass_0.Level];
            if (battlePassModel_0.SeasonIsEnabled() && ((model != null) && (playerBattlepass_0.TotalPoints >= model.RequiredPoints)))
            {
                int goodId;
                int vALUE = playerBattlepass_0.Level + 1;
                if (ComDiv.UpdateDB("player_battlepass", "level", vALUE, "owner_id", account_0.PlayerId))
                {
                    playerBattlepass_0.Level = vALUE;
                }
                PassItemModel normal = model.Normal;
                int[] numArray1 = new int[3];
                if (normal != null)
                {
                    goodId = normal.GoodId;
                }
                else
                {
                    PassItemModel local1 = normal;
                    goodId = 0;
                }
                new int[3][0] = goodId;
                int[] numArray = new int[3];
                if (playerBattlepass_0.IsPremium)
                {
                    int goodId;
                    int goodId;
                    PassItemModel premiumA = model.PremiumA;
                    if (premiumA != null)
                    {
                        goodId = premiumA.GoodId;
                    }
                    else
                    {
                        PassItemModel local2 = premiumA;
                        goodId = 0;
                    }
                    numArray[1] = goodId;
                    PassItemModel premiumB = model.PremiumB;
                    if (premiumB != null)
                    {
                        goodId = premiumB.GoodId;
                    }
                    else
                    {
                        PassItemModel local3 = premiumB;
                        goodId = 0;
                    }
                    numArray[2] = goodId;
                }
                smethod_22(account_0, numArray);
            }
        }

        private static void smethod_2(RoomModel roomModel_0, TeamEnum teamEnum_0, bool bool_0, FragInfos fragInfos_0, SlotModel slotModel_0)
        {
            int roundsByMask = roomModel_0.GetRoundsByMask();
            if ((roomModel_0.FRRounds >= roundsByMask) || (roomModel_0.CTRounds >= roundsByMask))
            {
                roomModel_0.StopBomb();
                using (PROTOCOL_BATTLE_WINNING_CAM_ACK protocol_battle_winning_cam_ack = new PROTOCOL_BATTLE_WINNING_CAM_ACK(fragInfos_0))
                {
                    using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_battle_mission_round_end_ack = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, teamEnum_0, RoundEndType.AllDeath))
                    {
                        roomModel_0.SendPacketToPlayers(protocol_battle_winning_cam_ack, protocol_battle_mission_round_end_ack, SlotState.BATTLE, 0);
                    }
                }
                EndBattle(roomModel_0, roomModel_0.IsBotMode(), teamEnum_0);
            }
            else if (!roomModel_0.ActiveC4 | bool_0)
            {
                roomModel_0.StopBomb();
                roomModel_0.ChangeRounds();
                RoundSync.SendUDPRoundSync(roomModel_0);
                using (PROTOCOL_BATTLE_WINNING_CAM_ACK protocol_battle_winning_cam_ack2 = new PROTOCOL_BATTLE_WINNING_CAM_ACK(fragInfos_0))
                {
                    using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_battle_mission_round_end_ack2 = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, teamEnum_0, RoundEndType.AllDeath))
                    {
                        roomModel_0.SendPacketToPlayers(protocol_battle_winning_cam_ack2, protocol_battle_mission_round_end_ack2, SlotState.BATTLE, 0);
                    }
                }
                roomModel_0.RoundRestart();
            }
        }

        private static void smethod_20(PlayerCompetitive playerCompetitive_0)
        {
            Class6 class2 = new Class6 {
                playerCompetitive_0 = playerCompetitive_0
            };
            int vALUE = 0;
            CompetitiveRank rank = CompetitiveXML.Ranks.FirstOrDefault<CompetitiveRank>(new Func<CompetitiveRank, bool>(class2.method_0));
            vALUE = (rank == null) ? class2.playerCompetitive_0.Level : rank.Id;
            ComDiv.UpdateDB("player_competitive", "points", class2.playerCompetitive_0.Points, "owner_id", class2.playerCompetitive_0.OwnerId);
            if ((vALUE != class2.playerCompetitive_0.Level) && ComDiv.UpdateDB("player_competitive", "level", vALUE, "owner_id", class2.playerCompetitive_0.OwnerId))
            {
                class2.playerCompetitive_0.Level = vALUE;
            }
        }

        private static void smethod_21(Account account_0)
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

        private static void smethod_22(Account account_0, int[] int_0)
        {
            foreach (int num2 in int_0)
            {
                if (num2 != 0)
                {
                    GoodsItem good = ShopManager.GetGood(num2);
                    if (good != null)
                    {
                        if ((ComDiv.GetIdStatics(good.Item.Id, 1) == 6) && (account_0.Character.GetCharacter(good.Item.Id) == null))
                        {
                            CreateCharacter(account_0, good.Item);
                        }
                        else
                        {
                            account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
                        }
                    }
                }
            }
            account_0.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK(0, int_0));
        }

        private static int smethod_23(Account account_0, BattleRewardType battleRewardType_0)
        {
            Random random = new Random();
            BattleRewardModel rewardType = BattleRewardXML.GetRewardType(battleRewardType_0);
            if ((rewardType == null) || (!rewardType.Enable || (random.Next(100) >= rewardType.Percentage)))
            {
                return 0;
            }
            GoodsItem good = ShopManager.GetGood(rewardType.Rewards[random.Next(rewardType.Rewards.Length)]);
            if (good == null)
            {
                return 0;
            }
            account_0.SendPacket(new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(account_0, good.Item));
            if (ComDiv.GetIdStatics(good.Item.Id, 1) != 0x1d)
            {
                if ((ComDiv.GetIdStatics(good.Item.Id, 1) == 6) && (account_0.Character.GetCharacter(good.Item.Id) == null))
                {
                    CreateCharacter(account_0, good.Item);
                }
                else
                {
                    account_0.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, good.Item));
                }
                return good.Item.Id;
            }
            int num = 0;
            switch (good.Item.Id)
            {
                case 0x2c4021:
                    num = 1;
                    break;

                case 0x2c4022:
                    num = 2;
                    break;

                case 0x2c4023:
                    num = 3;
                    break;

                case 0x2c4024:
                    num = 4;
                    break;

                case 0x2c4025:
                    num = 5;
                    break;

                default:
                    break;
            }
            account_0.Tags += num;
            ComDiv.UpdateDB("accounts", "tags", account_0.Tags, "player_id", account_0.PlayerId);
            return good.Item.Id;
        }

        private static void smethod_24(RoomModel roomModel_0, SlotModel slotModel_0, BattleRewardType battleRewardType_0, int int_0, ref int int_1, ref byte[] byte_0, ref int[] int_2)
        {
            Account account;
            if ((int_1 < int_0) && roomModel_0.GetPlayerBySlot(slotModel_0, out account))
            {
                byte num = (byte) slotModel_0.Id;
                int num2 = smethod_23(account, battleRewardType_0);
                if ((num != 0xff) && (num2 != 0))
                {
                    byte_0[int_1] = num;
                    int_2[int_1] = num2;
                    int_1++;
                }
            }
        }

        private static void smethod_3(Account account_0, int int_0)
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

        private static void smethod_5(RoomModel roomModel_0, bool bool_0)
        {
            int killsByMask = roomModel_0.GetKillsByMask();
            if ((roomModel_0.FRKills >= killsByMask) || (roomModel_0.CTKills >= killsByMask))
            {
                List<Account> allPlayers = roomModel_0.GetAllPlayers(SlotState.READY, 1);
                if (allPlayers.Count > 0)
                {
                    int num2;
                    int num3;
                    byte[] buffer;
                    TeamEnum winnerTeam = GetWinnerTeam(roomModel_0);
                    roomModel_0.CalculateResult(winnerTeam, bool_0);
                    GetBattleResult(roomModel_0, out num2, out num3, out buffer);
                    using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_battle_mission_round_end_ack = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, winnerTeam, RoundEndType.TimeOut))
                    {
                        byte[] completeBytes = protocol_battle_mission_round_end_ack.GetCompleteBytes("AllUtils.BaseEndByKills");
                        foreach (Account account in allPlayers)
                        {
                            SlotModel slot = roomModel_0.GetSlot(account.SlotId);
                            if (slot != null)
                            {
                                if (slot.State == SlotState.BATTLE)
                                {
                                    account.SendCompletePacket(completeBytes, protocol_battle_mission_round_end_ack.GetType().Name);
                                }
                                account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, winnerTeam, num3, num2, bool_0, buffer));
                                UpdateSeasonPass(account);
                            }
                        }
                    }
                }
                ResetBattleInfo(roomModel_0);
            }
        }

        private static void smethod_6(RoomModel roomModel_0)
        {
            int killsByMask = roomModel_0.GetKillsByMask();
            int[] numArray = new int[0x12];
            for (int i = 0; i < numArray.Length; i++)
            {
                SlotModel model = roomModel_0.Slots[i];
                numArray[i] = (model.PlayerId == 0) ? 0 : model.AllKills;
            }
            int index = 0;
            for (int j = 0; j < numArray.Length; j++)
            {
                if (numArray[j] > numArray[index])
                {
                    index = j;
                }
            }
            if (numArray[index] >= killsByMask)
            {
                List<Account> allPlayers = roomModel_0.GetAllPlayers(SlotState.READY, 1);
                if (allPlayers.Count > 0)
                {
                    int num5;
                    int num6;
                    byte[] buffer;
                    roomModel_0.CalculateResultFreeForAll(index);
                    GetBattleResult(roomModel_0, out num5, out num6, out buffer);
                    using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_battle_mission_round_end_ack = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, index, RoundEndType.FreeForAll))
                    {
                        byte[] completeBytes = protocol_battle_mission_round_end_ack.GetCompleteBytes("AllUtils.BaseEndByKills");
                        foreach (Account account in allPlayers)
                        {
                            SlotModel slot = roomModel_0.GetSlot(account.SlotId);
                            if (slot != null)
                            {
                                if (slot.State == SlotState.BATTLE)
                                {
                                    account.SendCompletePacket(completeBytes, protocol_battle_mission_round_end_ack.GetType().Name);
                                }
                                account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, index, num6, num5, false, buffer));
                                UpdateSeasonPass(account);
                            }
                        }
                    }
                }
                ResetBattleInfo(roomModel_0);
            }
        }

        private static SortedList<int, ClanTeam> smethod_7(RoomModel roomModel_0)
        {
            SortedList<int, ClanTeam> list = new SortedList<int, ClanTeam>();
            for (int i = 0; i < roomModel_0.GetAllPlayers().Count; i++)
            {
                Account account = roomModel_0.GetAllPlayers()[i];
                if (account.ClanId != 0)
                {
                    ClanTeam team;
                    if (list.TryGetValue(account.ClanId, out team) && (team != null))
                    {
                        if ((account.SlotId % 2) == 0)
                        {
                            team.PlayersFR++;
                        }
                        else
                        {
                            team.PlayersCT++;
                        }
                    }
                    else
                    {
                        ClanTeam team1 = new ClanTeam();
                        team1.ClanId = account.ClanId;
                        team = team1;
                        if ((account.SlotId % 2) == 0)
                        {
                            team.PlayersFR++;
                        }
                        else
                        {
                            team.PlayersCT++;
                        }
                        list.Add(account.ClanId, team);
                    }
                }
            }
            return list;
        }

        private static unsafe void smethod_8(RoomModel roomModel_0, Account account_0, SlotModel slotModel_0, FragInfos fragInfos_0, MissionType missionType_0, int int_0)
        {
            try
            {
                PlayerMissions missions = slotModel_0.Missions;
                if (missions != null)
                {
                    int currentMissionId = missions.GetCurrentMissionId();
                    int currentCard = missions.GetCurrentCard();
                    if ((currentMissionId > 0) && !missions.SelectedCard)
                    {
                        List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
                        if (cards.Count != 0)
                        {
                            KillingMessage allKillFlags = fragInfos_0.GetAllKillFlags();
                            byte[] currentMissionList = missions.GetCurrentMissionList();
                            ClassType type = (ClassType) ComDiv.GetIdStatics(fragInfos_0.WeaponId, 2);
                            ClassType type2 = smethod_1(type);
                            int num3 = ComDiv.GetIdStatics(fragInfos_0.WeaponId, 3);
                            ClassType type3 = (int_0 > 0) ? ((ClassType) ComDiv.GetIdStatics(fragInfos_0.WeaponId, 2)) : ClassType.Unknown;
                            ClassType type4 = (int_0 > 0) ? smethod_1(type3) : ClassType.Unknown;
                            int num4 = (int_0 > 0) ? ComDiv.GetIdStatics(int_0, 3) : 0;
                            foreach (MissionCardModel model in cards)
                            {
                                int num5 = 0;
                                if ((model.MapId == 0) || (model.MapId == roomModel_0.MapId))
                                {
                                    if (fragInfos_0.Frags.Count > 0)
                                    {
                                        if ((((model.MissionType == MissionType.KILL) || ((model.MissionType == MissionType.CHAINSTOPPER) && allKillFlags.HasFlag(KillingMessage.ChainStopper))) || (((model.MissionType == MissionType.CHAINSLUGGER) && allKillFlags.HasFlag(KillingMessage.ChainSlugger)) || (((model.MissionType == MissionType.CHAINKILLER) && (slotModel_0.KillsOnLife >= 4)) || (((model.MissionType == MissionType.TRIPLE_KILL) && (slotModel_0.KillsOnLife == 3)) || (((model.MissionType == MissionType.DOUBLE_KILL) && (slotModel_0.KillsOnLife == 2)) || (((model.MissionType == MissionType.HEADSHOT) && (allKillFlags.HasFlag(KillingMessage.Headshot) || allKillFlags.HasFlag(KillingMessage.ChainHeadshot))) || (((model.MissionType == MissionType.CHAINHEADSHOT) && allKillFlags.HasFlag(KillingMessage.ChainHeadshot)) || (((model.MissionType == MissionType.PIERCING) && allKillFlags.HasFlag(KillingMessage.PiercingShot)) || ((model.MissionType == MissionType.MASS_KILL) && allKillFlags.HasFlag(KillingMessage.MassKill)))))))))) || (((model.MissionType == MissionType.KILL_MAN) && roomModel_0.IsDinoMode("")) && (((slotModel_0.Team == TeamEnum.CT_TEAM) && (roomModel_0.Rounds == 2)) || ((slotModel_0.Team == TeamEnum.FR_TEAM) && (roomModel_0.Rounds == 1)))))
                                        {
                                            num5 = smethod_10(model, type, type2, num3, fragInfos_0);
                                        }
                                        else if (((model.MissionType == MissionType.KILL_WEAPONCLASS) || ((model.MissionType == MissionType.DOUBLE_KILL_WEAPONCLASS) && (slotModel_0.KillsOnLife == 2))) || ((model.MissionType == MissionType.TRIPLE_KILL_WEAPONCLASS) && (slotModel_0.KillsOnLife == 3)))
                                        {
                                            num5 = smethod_11(model, fragInfos_0);
                                        }
                                    }
                                    else if ((model.MissionType == MissionType.DEATHBLOW) && (missionType_0 == MissionType.DEATHBLOW))
                                    {
                                        num5 = smethod_13(model, type3, type4, num4);
                                    }
                                    else if (model.MissionType == missionType_0)
                                    {
                                        num5 = 1;
                                    }
                                }
                                if (num5 != 0)
                                {
                                    int arrayIdx = model.ArrayIdx;
                                    if ((currentMissionList[arrayIdx] + 1) <= model.MissionLimit)
                                    {
                                        slotModel_0.MissionsCompleted = true;
                                        byte* numPtr1 = &(currentMissionList[arrayIdx]);
                                        numPtr1[0] = (byte) (numPtr1[0] + ((byte) num5));
                                        if (currentMissionList[arrayIdx] > model.MissionLimit)
                                        {
                                            currentMissionList[arrayIdx] = (byte) model.MissionLimit;
                                        }
                                        int num7 = currentMissionList[arrayIdx];
                                        account_0.SendPacket(new PROTOCOL_BASE_QUEST_CHANGE_ACK(num7, model));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private static unsafe void smethod_9(RoomModel roomModel_0, Account account_0, SlotModel slotModel_0, MissionType missionType_0, int int_0)
        {
            try
            {
                PlayerMissions missions = slotModel_0.Missions;
                if (missions != null)
                {
                    int currentMissionId = missions.GetCurrentMissionId();
                    int currentCard = missions.GetCurrentCard();
                    if ((currentMissionId > 0) && !missions.SelectedCard)
                    {
                        List<MissionCardModel> cards = MissionCardRAW.GetCards(currentMissionId, currentCard);
                        if (cards.Count != 0)
                        {
                            byte[] currentMissionList = missions.GetCurrentMissionList();
                            ClassType type = (int_0 > 0) ? ((ClassType) ComDiv.GetIdStatics(int_0, 2)) : ClassType.Unknown;
                            ClassType type2 = (int_0 > 0) ? smethod_1(type) : ClassType.Unknown;
                            int num3 = (int_0 > 0) ? ComDiv.GetIdStatics(int_0, 3) : 0;
                            foreach (MissionCardModel model in cards)
                            {
                                int num4 = 0;
                                if ((model.MapId == 0) || (model.MapId == roomModel_0.MapId))
                                {
                                    if ((model.MissionType == MissionType.DEATHBLOW) && (missionType_0 == MissionType.DEATHBLOW))
                                    {
                                        num4 = smethod_13(model, type, type2, num3);
                                    }
                                    else if (model.MissionType == missionType_0)
                                    {
                                        num4 = 1;
                                    }
                                }
                                if (num4 != 0)
                                {
                                    int arrayIdx = model.ArrayIdx;
                                    if ((currentMissionList[arrayIdx] + 1) <= model.MissionLimit)
                                    {
                                        slotModel_0.MissionsCompleted = true;
                                        byte* numPtr1 = &(currentMissionList[arrayIdx]);
                                        numPtr1[0] = (byte) (numPtr1[0] + ((byte) num4));
                                        if (currentMissionList[arrayIdx] > model.MissionLimit)
                                        {
                                            currentMissionList[arrayIdx] = (byte) model.MissionLimit;
                                        }
                                        int num6 = currentMissionList[arrayIdx];
                                        account_0.SendPacket(new PROTOCOL_BASE_QUEST_CHANGE_ACK(num6, model));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static void SyncPlayerToClanMembers(Account player)
        {
            if ((player != null) && (player.ClanId > 0))
            {
                using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK protocol_cs_member_info_change_ack = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(player))
                {
                    ClanManager.SendPacket(protocol_cs_member_info_change_ack, player.ClanId, player.PlayerId, true, true);
                }
            }
        }

        public static void SyncPlayerToFriends(Account p, bool all)
        {
            if ((p != null) && (p.Friend.Friends.Count != 0))
            {
                PlayerInfo info = new PlayerInfo(p.PlayerId, p.Rank, p.NickColor, p.Nickname, p.IsOnline, p.Status);
                for (int i = 0; i < p.Friend.Friends.Count; i++)
                {
                    FriendModel model = p.Friend.Friends[i];
                    if (all || ((model.State == 0) && !model.Removed))
                    {
                        Account account = AccountManager.GetAccount(model.PlayerId, 0x11f);
                        if (account != null)
                        {
                            int index = -1;
                            FriendModel friend = account.Friend.GetFriend(p.PlayerId, out index);
                            if (friend != null)
                            {
                                friend.Info = info;
                                account.SendPacket(new PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState.Update, friend, index), false);
                            }
                        }
                    }
                }
            }
        }

        public static void TryBalancePlayer(RoomModel Room, Account Player, bool InBattle, ref SlotModel MySlot)
        {
            SlotModel slot = Room.GetSlot(Player.SlotId);
            if (slot != null)
            {
                TeamEnum team = slot.Team;
                TeamEnum enum3 = GetBalanceTeamIdx(Room, InBattle, team);
                if ((team != enum3) && (enum3 != TeamEnum.ALL_TEAM))
                {
                    SlotModel model2 = null;
                    int[] numArray = (team == TeamEnum.CT_TEAM) ? Room.FR_TEAM : Room.CT_TEAM;
                    int index = 0;
                    while (true)
                    {
                        if (index < numArray.Length)
                        {
                            SlotModel model3 = Room.Slots[numArray[index]];
                            if ((model3.State == SlotState.CLOSE) || (model3.PlayerId != 0))
                            {
                                index++;
                                continue;
                            }
                            model2 = model3;
                        }
                        if (model2 != null)
                        {
                            List<SlotChange> slotChanges = new List<SlotChange>();
                            SlotModel[] slots = Room.Slots;
                            lock (slots)
                            {
                                Room.SwitchSlots(slotChanges, model2.Id, slot.Id, false);
                            }
                            if (slotChanges.Count > 0)
                            {
                                Player.SlotId = slot.Id;
                                MySlot = slot;
                                using (PROTOCOL_ROOM_TEAM_BALANCE_ACK protocol_room_team_balance_ack = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(slotChanges, Room.LeaderSlot, 1))
                                {
                                    Room.SendPacketToPlayers(protocol_room_team_balance_ack);
                                }
                                Room.UpdateSlotsInfo();
                            }
                        }
                        return;
                    }
                }
            }
        }

        public static void TryBalanceTeams(RoomModel Room)
        {
            if ((Room.BalanceType == TeamBalance.Count) && !Room.IsBotMode())
            {
                TeamEnum enum2 = GetBalanceTeamIdx(Room, false, TeamEnum.ALL_TEAM);
                if (enum2 != TeamEnum.ALL_TEAM)
                {
                    int[] numArray = (enum2 == TeamEnum.CT_TEAM) ? Room.FR_TEAM : Room.CT_TEAM;
                    SlotModel slot = null;
                    int index = numArray.Length - 1;
                    while (true)
                    {
                        Account account;
                        if (index >= 0)
                        {
                            SlotModel model2 = Room.Slots[numArray[index]];
                            if ((model2.State != SlotState.READY) || (Room.LeaderSlot == model2.Id))
                            {
                                index--;
                                continue;
                            }
                            slot = model2;
                        }
                        if ((slot != null) && Room.GetPlayerBySlot(slot, out account))
                        {
                            TryBalancePlayer(Room, account, false, ref slot);
                        }
                        return;
                    }
                }
            }
        }

        public static void UpdateDailyRecord(bool WonTheMatch, Account Player, int winnerTeam, DBQuery query)
        {
            int num;
            if (winnerTeam == 2)
            {
                StatisticDaily daily1 = Player.Statistic.Daily;
                num = daily1.MatchDraws + 1;
                daily1.MatchDraws = num;
                query.AddQuery("match_draws", num);
            }
            else if (WonTheMatch)
            {
                StatisticDaily daily2 = Player.Statistic.Daily;
                num = daily2.MatchWins + 1;
                daily2.MatchWins = num;
                query.AddQuery("match_wins", num);
            }
            else
            {
                StatisticDaily daily3 = Player.Statistic.Daily;
                num = daily3.MatchLoses + 1;
                daily3.MatchLoses = num;
                query.AddQuery("match_loses", num);
            }
            StatisticDaily daily = Player.Statistic.Daily;
            num = daily.Matches + 1;
            daily.Matches = num;
            query.AddQuery("matches", num);
        }

        public static void UpdateMatchCount(bool WonTheMatch, Account Player, int WinnerTeam, DBQuery TotalQuery, DBQuery SeasonQuery)
        {
            int num;
            if (WinnerTeam == 2)
            {
                StatisticTotal total1 = Player.Statistic.Basic;
                num = total1.MatchDraws + 1;
                total1.MatchDraws = num;
                TotalQuery.AddQuery("match_draws", num);
                StatisticSeason season1 = Player.Statistic.Season;
                num = season1.MatchDraws + 1;
                season1.MatchDraws = num;
                SeasonQuery.AddQuery("match_draws", num);
            }
            else if (WonTheMatch)
            {
                StatisticTotal total2 = Player.Statistic.Basic;
                num = total2.MatchWins + 1;
                total2.MatchWins = num;
                TotalQuery.AddQuery("match_wins", num);
                StatisticSeason season2 = Player.Statistic.Season;
                num = season2.MatchWins + 1;
                season2.MatchWins = num;
                SeasonQuery.AddQuery("match_wins", num);
            }
            else
            {
                StatisticTotal total3 = Player.Statistic.Basic;
                num = total3.MatchLoses + 1;
                total3.MatchLoses = num;
                TotalQuery.AddQuery("match_loses", num);
                StatisticSeason season3 = Player.Statistic.Season;
                num = season3.MatchLoses + 1;
                season3.MatchLoses = num;
                SeasonQuery.AddQuery("match_loses", num);
            }
            StatisticTotal basic = Player.Statistic.Basic;
            num = basic.Matches + 1;
            basic.Matches = num;
            TotalQuery.AddQuery("matches", num);
            StatisticTotal total5 = Player.Statistic.Basic;
            num = total5.TotalMatchesCount + 1;
            total5.TotalMatchesCount = num;
            TotalQuery.AddQuery("total_matches", num);
            StatisticSeason season = Player.Statistic.Season;
            num = season.Matches + 1;
            season.Matches = num;
            SeasonQuery.AddQuery("matches", num);
            StatisticSeason season5 = Player.Statistic.Season;
            num = season5.TotalMatchesCount + 1;
            season5.TotalMatchesCount = num;
            SeasonQuery.AddQuery("total_matches", num);
        }

        public static void UpdateMatchCountFFA(RoomModel Room, Account Player, int SlotWin, DBQuery TotalQuery, DBQuery SeasonQuery)
        {
            int num4;
            int[] numArray = new int[0x12];
            for (int i = 0; i < numArray.Length; i++)
            {
                SlotModel model = Room.Slots[i];
                numArray[i] = (model.PlayerId == 0) ? 0 : model.AllKills;
            }
            int index = 0;
            for (int j = 0; j < numArray.Length; j++)
            {
                if (numArray[j] > numArray[index])
                {
                    index = j;
                }
            }
            if (numArray[index] == SlotWin)
            {
                StatisticTotal total1 = Player.Statistic.Basic;
                num4 = total1.MatchWins + 1;
                total1.MatchWins = num4;
                TotalQuery.AddQuery("match_wins", num4);
                StatisticSeason season1 = Player.Statistic.Season;
                num4 = season1.MatchWins + 1;
                season1.MatchWins = num4;
                SeasonQuery.AddQuery("match_wins", num4);
            }
            else
            {
                StatisticTotal total2 = Player.Statistic.Basic;
                num4 = total2.MatchLoses + 1;
                total2.MatchLoses = num4;
                TotalQuery.AddQuery("match_loses", num4);
                StatisticSeason season2 = Player.Statistic.Season;
                num4 = season2.MatchLoses + 1;
                season2.MatchLoses = num4;
                SeasonQuery.AddQuery("match_loses", num4);
            }
            StatisticTotal basic = Player.Statistic.Basic;
            num4 = basic.Matches + 1;
            basic.Matches = num4;
            TotalQuery.AddQuery("matches", num4);
            StatisticTotal total4 = Player.Statistic.Basic;
            num4 = total4.TotalMatchesCount + 1;
            total4.TotalMatchesCount = num4;
            TotalQuery.AddQuery("total_matches", num4);
            StatisticSeason season = Player.Statistic.Season;
            num4 = season.Matches + 1;
            season.Matches = num4;
            SeasonQuery.AddQuery("matches", num4);
            StatisticSeason season4 = Player.Statistic.Season;
            num4 = season4.TotalMatchesCount + 1;
            season4.TotalMatchesCount = num4;
            SeasonQuery.AddQuery("total_matches", num4);
        }

        public static void UpdateMatchDailyRecordFFA(RoomModel Room, Account Player, int SlotWin, DBQuery Query)
        {
            int num4;
            int[] numArray = new int[0x12];
            for (int i = 0; i < numArray.Length; i++)
            {
                SlotModel model = Room.Slots[i];
                numArray[i] = (model.PlayerId == 0) ? 0 : model.AllKills;
            }
            int index = 0;
            for (int j = 0; j < numArray.Length; j++)
            {
                if (numArray[j] > numArray[index])
                {
                    index = j;
                }
            }
            if (numArray[index] == SlotWin)
            {
                StatisticDaily daily1 = Player.Statistic.Daily;
                num4 = daily1.MatchWins + 1;
                daily1.MatchWins = num4;
                Query.AddQuery("match_wins", num4);
            }
            else
            {
                StatisticDaily daily2 = Player.Statistic.Daily;
                num4 = daily2.MatchLoses + 1;
                daily2.MatchLoses = num4;
                Query.AddQuery("match_loses", num4);
            }
            StatisticDaily daily = Player.Statistic.Daily;
            num4 = daily.Matches + 1;
            daily.Matches = num4;
            Query.AddQuery("matches", num4);
        }

        public static void UpdateSeasonPass(Account Player)
        {
            if ((SeasonChallengeXML.GetActiveSeasonPass() != null) && Player.UpdateSeasonpass)
            {
                Player.UpdateSeasonpass = false;
                Player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_SEASON_CHANGE());
                Player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_INFO_ACK(Player));
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
            SlotModel model;
            if (Room.GetSlot(Player.SlotId, out model))
            {
                model.Equipment = Player.Equipment;
            }
            Room.UpdateSlotsInfo();
        }

        public static void UpdateWeaponRecord(Account Player, SlotModel Slot, DBQuery Query)
        {
            int num;
            StatisticWeapon weapon = Player.Statistic.Weapon;
            if (Slot.AR[0] > 0)
            {
                num = weapon.AssaultKills + 1;
                weapon.AssaultKills = num;
                Query.AddQuery("assault_rifle_kills", num);
            }
            if (Slot.AR[1] > 0)
            {
                num = weapon.AssaultDeaths + 1;
                weapon.AssaultDeaths = num;
                Query.AddQuery("assault_rifle_deaths", num);
            }
            if (Slot.SMG[0] > 0)
            {
                num = weapon.SmgKills + 1;
                weapon.SmgKills = num;
                Query.AddQuery("sub_machine_gun_kills", num);
            }
            if (Slot.SMG[1] > 0)
            {
                num = weapon.SmgDeaths + 1;
                weapon.SmgDeaths = num;
                Query.AddQuery("sub_machine_gun_deaths", num);
            }
            if (Slot.SR[0] > 0)
            {
                num = weapon.SniperKills + 1;
                weapon.SniperKills = num;
                Query.AddQuery("sniper_rifle_kills", num);
            }
            if (Slot.SR[1] > 0)
            {
                num = weapon.SniperDeaths + 1;
                weapon.SniperDeaths = num;
                Query.AddQuery("sniper_rifle_deaths", num);
            }
            if (Slot.SG[0] > 0)
            {
                num = weapon.ShotgunKills + 1;
                weapon.ShotgunKills = num;
                Query.AddQuery("shot_gun_kills", num);
            }
            if (Slot.SG[1] > 0)
            {
                num = weapon.ShotgunDeaths + 1;
                weapon.ShotgunDeaths = num;
                Query.AddQuery("shot_gun_deaths", num);
            }
            if (Slot.MG[0] > 0)
            {
                num = weapon.MachinegunKills + 1;
                weapon.MachinegunKills = num;
                Query.AddQuery("machine_gun_kills", num);
            }
            if (Slot.MG[1] > 0)
            {
                num = weapon.MachinegunDeaths + 1;
                weapon.MachinegunDeaths = num;
                Query.AddQuery("machine_gun_deaths", num);
            }
            if (Slot.SHD[0] > 0)
            {
                num = weapon.ShieldKills + 1;
                weapon.ShieldKills = num;
                Query.AddQuery("shield_kills", num);
            }
            if (Slot.SHD[1] > 0)
            {
                num = weapon.ShieldDeaths + 1;
                weapon.ShieldDeaths = num;
                Query.AddQuery("shield_deaths", num);
            }
        }

        public static void ValidateAccesoryEquipment(Account Player, int AccessoryId)
        {
            if (Player.Equipment.AccessoryId != AccessoryId)
            {
                Player.Equipment.AccessoryId = smethod_16(Player, AccessoryId);
                ComDiv.UpdateDB("player_equipments", "accesory_id", Player.Equipment.AccessoryId, "owner_id", Player.PlayerId);
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

        public static void ValidateBanPlayer(Account Player, string Message)
        {
            if (ConfigLoader.AutoBan && DaoManagerSQL.SaveAutoBan(Player.PlayerId, Player.Username, Player.Nickname, "Cheat " + Message + ")", DateTimeUtil.Now("dd -MM-yyyy HH:mm:ss"), Player.PublicIP.ToString(), "Illegal Program"))
            {
                using (PROTOCOL_LOBBY_CHATTING_ACK protocol_lobby_chatting_ack = new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 1, false, "Permanently ban player [" + Player.Nickname + "], " + Message))
                {
                    GameXender.Client.SendPacketToAllClients(protocol_lobby_chatting_ack);
                }
                Player.SendPacket(new PROTOCOL_AUTH_ACCOUNT_KICK_ACK(2), false);
                Player.Close(0x3e8, true);
            }
            CLogger.Print($"Player: {Player.Nickname}; Id: {Player.PlayerId}; User: {Player.Username}; Reason: {Message}", LoggerType.Hack, null);
        }

        public static void ValidateCharacterEquipment(Account Player, PlayerEquipment Equip, int[] EquipmentList, int CharacterId, int[] CharaSlots)
        {
            DBQuery query = new DBQuery();
            CharacterModel character = Player.Character.GetCharacter(CharacterId);
            if (character != null)
            {
                int num = ComDiv.GetIdStatics(character.Id, 1);
                int num2 = ComDiv.GetIdStatics(character.Id, 2);
                int num3 = ComDiv.GetIdStatics(character.Id, 5);
                if (((num != 6) || ((num2 != 1) && (num3 != 0x278))) || (CharaSlots[0] != character.Slot))
                {
                    if ((((num == 6) && ((num2 == 2) || (num3 == 0x298))) && (CharaSlots[1] == character.Slot)) && (Equip.CharaBlueId != character.Id))
                    {
                        Equip.CharaBlueId = character.Id;
                        query.AddQuery("chara_blue_side", Equip.CharaBlueId);
                    }
                }
                else if (Equip.CharaRedId != character.Id)
                {
                    Equip.CharaRedId = character.Id;
                    query.AddQuery("chara_red_side", Equip.CharaRedId);
                }
            }
            for (int i = 0; i < EquipmentList.Length; i++)
            {
                int num5 = smethod_16(Player, EquipmentList[i]);
                switch (i)
                {
                    case 0:
                        if ((num5 != 0) && (Equip.WeaponPrimary != num5))
                        {
                            Equip.WeaponPrimary = num5;
                            query.AddQuery("weapon_primary", Equip.WeaponPrimary);
                        }
                        break;

                    case 1:
                        if ((num5 != 0) && (Equip.WeaponSecondary != num5))
                        {
                            Equip.WeaponSecondary = num5;
                            query.AddQuery("weapon_secondary", Equip.WeaponSecondary);
                        }
                        break;

                    case 2:
                        if ((num5 != 0) && (Equip.WeaponMelee != num5))
                        {
                            Equip.WeaponMelee = num5;
                            query.AddQuery("weapon_melee", Equip.WeaponMelee);
                        }
                        break;

                    case 3:
                        if ((num5 != 0) && (Equip.WeaponExplosive != num5))
                        {
                            Equip.WeaponExplosive = num5;
                            query.AddQuery("weapon_explosive", Equip.WeaponExplosive);
                        }
                        break;

                    case 4:
                        if ((num5 != 0) && (Equip.WeaponSpecial != num5))
                        {
                            Equip.WeaponSpecial = num5;
                            query.AddQuery("weapon_special", Equip.WeaponSpecial);
                        }
                        break;

                    case 5:
                        if (Equip.PartHead != num5)
                        {
                            Equip.PartHead = num5;
                            query.AddQuery("part_head", Equip.PartHead);
                        }
                        break;

                    case 6:
                        if ((num5 != 0) && (Equip.PartFace != num5))
                        {
                            Equip.PartFace = num5;
                            query.AddQuery("part_face", Equip.PartFace);
                        }
                        break;

                    case 7:
                        if ((num5 != 0) && (Equip.PartJacket != num5))
                        {
                            Equip.PartJacket = num5;
                            query.AddQuery("part_jacket", Equip.PartJacket);
                        }
                        break;

                    case 8:
                        if ((num5 != 0) && (Equip.PartPocket != num5))
                        {
                            Equip.PartPocket = num5;
                            query.AddQuery("part_pocket", Equip.PartPocket);
                        }
                        break;

                    case 9:
                        if ((num5 != 0) && (Equip.PartGlove != num5))
                        {
                            Equip.PartGlove = num5;
                            query.AddQuery("part_glove", Equip.PartGlove);
                        }
                        break;

                    case 10:
                        if ((num5 != 0) && (Equip.PartBelt != num5))
                        {
                            Equip.PartBelt = num5;
                            query.AddQuery("part_belt", Equip.PartBelt);
                        }
                        break;

                    case 11:
                        if ((num5 != 0) && (Equip.PartHolster != num5))
                        {
                            Equip.PartHolster = num5;
                            query.AddQuery("part_holster", Equip.PartHolster);
                        }
                        break;

                    case 12:
                        if ((num5 != 0) && (Equip.PartSkin != num5))
                        {
                            Equip.PartSkin = num5;
                            query.AddQuery("part_skin", Equip.PartSkin);
                        }
                        break;

                    case 13:
                        if (Equip.BeretItem != num5)
                        {
                            Equip.BeretItem = num5;
                            query.AddQuery("beret_item_part", Equip.BeretItem);
                        }
                        break;

                    default:
                        break;
                }
            }
            ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, query.GetTables(), query.GetValues());
        }

        public static void ValidateCharacterSlot(Account Player, PlayerEquipment Equip, int[] Slots)
        {
            DBQuery query = new DBQuery();
            CharacterModel characterSlot = Player.Character.GetCharacterSlot(Slots[0]);
            if ((characterSlot != null) && (Equip.CharaRedId != characterSlot.Id))
            {
                Equip.CharaRedId = smethod_16(Player, characterSlot.Id);
                query.AddQuery("chara_red_side", Equip.CharaRedId);
            }
            CharacterModel model2 = Player.Character.GetCharacterSlot(Slots[1]);
            if ((model2 != null) && (Equip.CharaBlueId != model2.Id))
            {
                Equip.CharaBlueId = smethod_16(Player, model2.Id);
                query.AddQuery("chara_blue_side", Equip.CharaBlueId);
            }
            ComDiv.UpdateDB("player_equipments", "owner_id", Player.PlayerId, query.GetTables(), query.GetValues());
        }

        public static void ValidateDisabledCoupon(Account Player, SortedList<int, int> Coupons)
        {
            for (int i = 0; i < Coupons.Keys.Count; i++)
            {
                ItemsModel model = Player.Inventory.GetItem(smethod_14(i, Coupons));
                if (model != null)
                {
                    CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(model.Id);
                    if ((couponEffect != null) && ((couponEffect.EffectFlag > 0L) && Player.Effects.HasFlag(couponEffect.EffectFlag)))
                    {
                        Player.Effects -= couponEffect.EffectFlag;
                        DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
                    }
                }
            }
        }

        public static void ValidateEnabledCoupon(Account Player, SortedList<int, int> Coupons)
        {
            for (int i = 0; i < Coupons.Keys.Count; i++)
            {
                ItemsModel model = Player.Inventory.GetItem(smethod_14(i, Coupons));
                if (model != null)
                {
                    CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(model.Id);
                    if ((couponEffect != null) && ((couponEffect.EffectFlag > 0L) && !Player.Effects.HasFlag(couponEffect.EffectFlag)))
                    {
                        Player.Effects |= couponEffect.EffectFlag;
                        DaoManagerSQL.UpdateCouponEffect(Player.PlayerId, Player.Effects);
                    }
                    if (Player.Bonus.AddBonuses(model.Id))
                    {
                        DaoManagerSQL.UpdatePlayerBonus(Player.PlayerId, Player.Bonus.Bonuses, Player.Bonus.FreePass);
                    }
                }
            }
        }

        public static void ValidateItemEquipment(Account Player, SortedList<int, int> Items)
        {
            for (int i = 0; i < Items.Keys.Count; i++)
            {
                int num2 = smethod_15(i, Items);
                switch (i)
                {
                    case 0:
                        if ((num2 != 0) && (Player.Equipment.DinoItem != num2))
                        {
                            Player.Equipment.DinoItem = smethod_16(Player, num2);
                            ComDiv.UpdateDB("player_equipments", "dino_item_chara", Player.Equipment.DinoItem, "owner_id", Player.PlayerId);
                        }
                        break;

                    case 1:
                        if (Player.Equipment.SprayId != num2)
                        {
                            Player.Equipment.SprayId = smethod_16(Player, num2);
                            ComDiv.UpdateDB("player_equipments", "spray_id", Player.Equipment.SprayId, "owner_id", Player.PlayerId);
                        }
                        break;

                    case 2:
                        if (Player.Equipment.NameCardId != num2)
                        {
                            Player.Equipment.NameCardId = smethod_16(Player, num2);
                            ComDiv.UpdateDB("player_equipments", "namecard_id", Player.Equipment.NameCardId, "owner_id", Player.PlayerId);
                        }
                        break;

                    default:
                        break;
                }
            }
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
                smethod_21(Player);
            }
            if (!smethod_0(Player, out str))
            {
                foreach (ItemsModel model2 in TemplatePackXML.GetPCCafeRewards(Player.CafePC))
                {
                    if ((ComDiv.GetIdStatics(model2.Id, 1) == 6) && (Player.Character.GetCharacter(model2.Id) != null))
                    {
                        smethod_3(Player, model2.Id);
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

        public static PlayerEquipment ValidateRespawnEQ(SlotModel Slot, int[] ItemIds)
        {
            PlayerEquipment equipment1 = new PlayerEquipment();
            equipment1.WeaponPrimary = ItemIds[0];
            equipment1.WeaponSecondary = ItemIds[1];
            equipment1.WeaponMelee = ItemIds[2];
            equipment1.WeaponExplosive = ItemIds[3];
            equipment1.WeaponSpecial = ItemIds[4];
            equipment1.PartHead = ItemIds[6];
            equipment1.PartFace = ItemIds[7];
            equipment1.PartJacket = ItemIds[8];
            equipment1.PartPocket = ItemIds[9];
            equipment1.PartGlove = ItemIds[10];
            equipment1.PartBelt = ItemIds[11];
            equipment1.PartHolster = ItemIds[12];
            equipment1.PartSkin = ItemIds[13];
            equipment1.BeretItem = ItemIds[14];
            equipment1.AccessoryId = ItemIds[15];
            equipment1.CharaRedId = Slot.Equipment.CharaRedId;
            equipment1.CharaBlueId = Slot.Equipment.CharaBlueId;
            equipment1.DinoItem = Slot.Equipment.DinoItem;
            PlayerEquipment equipment = equipment1;
            int num = ComDiv.GetIdStatics(ItemIds[5], 1);
            int num2 = ComDiv.GetIdStatics(ItemIds[5], 2);
            int num3 = ComDiv.GetIdStatics(ItemIds[5], 5);
            if (num != 6)
            {
                if (num == 15)
                {
                    equipment.DinoItem = ItemIds[5];
                }
            }
            else if ((num2 == 1) || (num3 == 0x278))
            {
                equipment.CharaRedId = ItemIds[5];
            }
            else if ((num2 == 2) || (num3 == 0x298))
            {
                equipment.CharaBlueId = ItemIds[5];
            }
            return equipment;
        }

        public static void VotekickResult(RoomModel Room)
        {
            VoteKickModel voteKick = Room.VoteKick;
            if (voteKick != null)
            {
                int num = voteKick.GetInGamePlayers();
                if ((voteKick.Accept > voteKick.Denie) && ((voteKick.Enemies > 0) && ((voteKick.Allies > 0) && (voteKick.Votes.Count >= (num / 2)))))
                {
                    Account playerBySlot = Room.GetPlayerBySlot(voteKick.VictimIdx);
                    if (playerBySlot != null)
                    {
                        playerBySlot.SendPacket(new PROTOCOL_BATTLE_NOTIFY_BE_KICKED_BY_KICKVOTE_ACK());
                        Room.KickedPlayersVote.Add(playerBySlot.PlayerId);
                        Room.RemovePlayer(playerBySlot, true, 2);
                    }
                }
                uint num2 = 0;
                if (voteKick.Allies == 0)
                {
                    num2 = 0x80001101;
                }
                else if (voteKick.Enemies == 0)
                {
                    num2 = 0x80001102;
                }
                else if ((voteKick.Denie < voteKick.Accept) || (voteKick.Votes.Count < (num / 2)))
                {
                    num2 = 0x80001100;
                }
                using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK protocol_battle_notify_kickvote_result_ack = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK(num2, voteKick))
                {
                    Room.SendPacketToPlayers(protocol_battle_notify_kickvote_result_ack, SlotState.BATTLE, 0);
                }
            }
            Room.VoteKick = null;
        }

        [Serializable, CompilerGenerated]
        private sealed class Class5
        {
            public static readonly AllUtils.Class5 <>9 = new AllUtils.Class5();
            public static Func<SlotModel, bool> <>9__125_0;
            public static Func<SlotModel, int> <>9__125_1;
            public static Func<SlotModel, bool> <>9__125_2;
            public static Func<SlotModel, int> <>9__125_3;
            public static Func<SlotModel, bool> <>9__125_4;
            public static Func<SlotModel, int> <>9__125_5;

            internal bool method_0(SlotModel slotModel_0) => 
                slotModel_0.Score > 0;

            internal int method_1(SlotModel slotModel_0) => 
                slotModel_0.Score;

            internal bool method_2(SlotModel slotModel_0) => 
                slotModel_0.AllAssists > 0;

            internal int method_3(SlotModel slotModel_0) => 
                slotModel_0.AllAssists;

            internal bool method_4(SlotModel slotModel_0) => 
                slotModel_0.KillsOnLife > 0;

            internal int method_5(SlotModel slotModel_0) => 
                slotModel_0.KillsOnLife;
        }

        [CompilerGenerated]
        private sealed class Class6
        {
            public PlayerCompetitive playerCompetitive_0;

            internal bool method_0(CompetitiveRank competitiveRank_0) => 
                this.playerCompetitive_0.Points <= competitiveRank_0.Points;
        }

        [CompilerGenerated]
        private sealed class Class7
        {
            public SlotModel slotModel_0;

            internal bool method_0(SlotModel slotModel_1) => 
                (slotModel_1.Team == this.slotModel_0.Team) && (slotModel_1.State != SlotState.CLOSE);
        }

        [CompilerGenerated]
        private sealed class Class8
        {
            public SlotModel slotModel_0;

            internal bool method_0(SlotModel slotModel_1) => 
                (slotModel_1.Team == this.slotModel_0.Team) && (slotModel_1.State != SlotState.CLOSE);
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
                AllUtils.smethod_2(this.roomModel_0, this.teamEnum_0, this.bool_0, this.fragInfos_0, this.slotModel_0);
                object obj2 = object_0;
                lock (obj2)
                {
                    this.roomModel_0.MatchEndTime.StopJob();
                }
            }
        }
    }
}

