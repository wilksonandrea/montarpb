namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ : GameClientPacket
    {
        private long long_0;
        private int int_0;
        private long long_1;
        private uint uint_0 = 1;
        private readonly Random random_0 = new Random();
        private readonly object object_0 = new object();

        private int method_0(int int_1, int int_2)
        {
            object obj2 = this.object_0;
            lock (obj2)
            {
                return this.random_0.Next(int_1, int_2);
            }
        }

        private void method_1(Account account_0, string string_0)
        {
            int couponId = ComDiv.CreateItemId(0x10, 0, ComDiv.GetIdStatics(this.int_0, 3));
            int num2 = ComDiv.GetIdStatics(this.int_0, 2);
            if (AllUtils.CheckDuplicateCouponEffects(account_0, couponId))
            {
                this.uint_0 = 0x80000000;
            }
            else
            {
                ItemsModel model = account_0.Inventory.GetItem(couponId);
                if (model != null)
                {
                    DateTime time2 = DateTime.ParseExact(model.Count.ToString(), "yyMMddHHmm", CultureInfo.InvariantCulture);
                    model.Count = Convert.ToUInt32(time2.AddDays((double) num2).ToString("yyMMddHHmm"));
                    ComDiv.UpdateDB("player_items", "count", (long) model.Count, "object_id", model.ObjectId, "owner_id", account_0.PlayerId);
                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, account_0, model));
                }
                else
                {
                    CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(couponId);
                    if ((couponEffect != null) && ((couponEffect.EffectFlag > 0L) && !account_0.Effects.HasFlag(couponEffect.EffectFlag)))
                    {
                        account_0.Effects |= couponEffect.EffectFlag;
                        DaoManagerSQL.UpdateCouponEffect(account_0.PlayerId, account_0.Effects);
                    }
                    if (account_0.Bonus.AddBonuses(couponId))
                    {
                        DaoManagerSQL.UpdatePlayerBonus(account_0.PlayerId, account_0.Bonus.Bonuses, account_0.Bonus.FreePass);
                    }
                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(couponId, string_0 + " [Active]", ItemEquipType.Temporary, Convert.ToUInt32(DateTimeUtil.Now().AddDays((double) num2).ToString("yyMMddHHmm")))));
                }
            }
        }

        private void method_2(Account account_0, int int_1)
        {
            int num = (ComDiv.GetIdStatics(int_1, 3) * 100) + (ComDiv.GetIdStatics(int_1, 2) * 0x186a0);
            if (!DaoManagerSQL.UpdateAccountGold(account_0.PlayerId, account_0.Gold + num))
            {
                this.uint_0 = 0x80000000;
            }
            else
            {
                account_0.Gold += num;
                base.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(num, account_0.Gold, 0));
            }
        }

        private void method_3(Account account_0, int int_1)
        {
            PlayerBattlepass battlepass = account_0.Battlepass;
            if (battlepass == null)
            {
                this.uint_0 = 0x80000000;
            }
            else
            {
                int num = (ComDiv.GetIdStatics(int_1, 3) * 10) + (ComDiv.GetIdStatics(int_1, 2) * 0x186a0);
                battlepass.TotalPoints += num;
                if (!ComDiv.UpdateDB("player_battlepass", "total_points", battlepass.TotalPoints, "owner_id", account_0.PlayerId))
                {
                    this.uint_0 = 0x80000000;
                }
                else
                {
                    account_0.UpdateSeasonpass = true;
                    AllUtils.UpdateSeasonPass(account_0);
                }
            }
        }

        public override void Read()
        {
            this.long_0 = base.ReadUD();
            base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ItemsModel model = player.Inventory.GetItem(this.long_0);
                    if (model == null)
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else
                    {
                        this.int_0 = model.Id;
                        this.long_1 = model.Count;
                        if ((model.Category != ItemCategory.Coupon) || (player.Inventory.Items.Count < 500))
                        {
                            if (this.int_0 == 0x1b7771)
                            {
                                if (!DaoManagerSQL.UpdatePlayerKD(player.PlayerId, 0, 0, player.Statistic.Season.HeadshotsCount, player.Statistic.Season.TotalKillsCount))
                                {
                                    this.uint_0 = 0x80000000;
                                }
                                else
                                {
                                    player.Statistic.Season.KillsCount = 0;
                                    player.Statistic.Season.DeathsCount = 0;
                                    base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
                                }
                            }
                            else if (this.int_0 == 0x1b7770)
                            {
                                if (!DaoManagerSQL.UpdatePlayerMatches(0, 0, 0, 0, player.Statistic.Season.TotalMatchesCount, player.PlayerId))
                                {
                                    this.uint_0 = 0x80000000;
                                }
                                else
                                {
                                    player.Statistic.Season.Matches = 0;
                                    player.Statistic.Season.MatchWins = 0;
                                    player.Statistic.Season.MatchLoses = 0;
                                    player.Statistic.Season.MatchDraws = 0;
                                    base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
                                }
                            }
                            else if (this.int_0 == 0x1b7772)
                            {
                                if (!ComDiv.UpdateDB("player_stat_seasons", "escapes_count", 0, "owner_id", player.PlayerId))
                                {
                                    this.uint_0 = 0x80000000;
                                }
                                else
                                {
                                    player.Statistic.Season.EscapesCount = 0;
                                    base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(player.Statistic));
                                }
                            }
                            else if (this.int_0 == 0x1b7775)
                            {
                                if (!DaoManagerSQL.UpdateClanBattles(player.ClanId, 0, 0, 0))
                                {
                                    this.uint_0 = 0x80000000;
                                }
                                else
                                {
                                    ClanModel clan = ClanManager.GetClan(player.ClanId);
                                    if ((clan.Id <= 0) || (clan.OwnerId != base.Client.PlayerId))
                                    {
                                        this.uint_0 = 0x80000000;
                                    }
                                    else
                                    {
                                        clan.Matches = 0;
                                        clan.MatchWins = 0;
                                        clan.MatchLoses = 0;
                                        base.Client.SendPacket(new PROTOCOL_CS_RECORD_RESET_RESULT_ACK());
                                    }
                                }
                            }
                            else if (this.int_0 == 0x1b7777)
                            {
                                ClanModel clan = ClanManager.GetClan(player.ClanId);
                                if ((clan.Id <= 0) || (clan.OwnerId != base.Client.PlayerId))
                                {
                                    this.uint_0 = 0x80001056;
                                }
                                else if (((clan.MaxPlayers + 50) > 250) || !ComDiv.UpdateDB("system_clan", "max_players", clan.MaxPlayers + 50, "id", player.ClanId))
                                {
                                    this.uint_0 = 0x80001056;
                                }
                                else
                                {
                                    clan.MaxPlayers += 50;
                                    base.Client.SendPacket(new PROTOCOL_CS_REPLACE_PERSONMAX_ACK(clan.MaxPlayers));
                                }
                            }
                            else if (this.int_0 == 0x1b7778)
                            {
                                ClanModel clan = ClanManager.GetClan(player.ClanId);
                                if ((clan.Id <= 0) || (clan.Points == 1000f))
                                {
                                    this.uint_0 = 0x80001056;
                                }
                                else if (!ComDiv.UpdateDB("system_clan", "points", 1000f, "id", player.ClanId))
                                {
                                    this.uint_0 = 0x80001056;
                                }
                                else
                                {
                                    clan.Points = 1000f;
                                    base.Client.SendPacket(new PROTOCOL_CS_POINT_RESET_RESULT_ACK());
                                }
                            }
                            else if ((this.int_0 > 0x1b77b1) && (this.int_0 < 0x1b77b7))
                            {
                                int num = (this.int_0 == 0x1b77b2) ? 500 : ((this.int_0 == 0x1b77b3) ? 0x3e8 : ((this.int_0 == 0x1b77b4) ? 0x1388 : ((this.int_0 == 0x1b77b5) ? 0x2710 : 0x7530)));
                                if (!ComDiv.UpdateDB("accounts", "gold", player.Gold + num, "player_id", player.PlayerId))
                                {
                                    this.uint_0 = 0x80000000;
                                }
                                else
                                {
                                    player.Gold += num;
                                    base.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(num, player.Gold, 0));
                                }
                            }
                            else if (this.int_0 == 0x1b7bb9)
                            {
                                int num2 = 0;
                                int num3 = new Random().Next(0, 9);
                                switch (num3)
                                {
                                    case 0:
                                        num2 = 1;
                                        break;

                                    case 1:
                                        num2 = 2;
                                        break;

                                    case 2:
                                        num2 = 3;
                                        break;

                                    case 3:
                                        num2 = 4;
                                        break;

                                    case 4:
                                        num2 = 5;
                                        break;

                                    case 5:
                                        num2 = 10;
                                        break;

                                    case 6:
                                        num2 = 15;
                                        break;

                                    case 7:
                                        num2 = 0x19;
                                        break;

                                    case 8:
                                        num2 = 30;
                                        break;

                                    case 9:
                                        num2 = 50;
                                        break;

                                    default:
                                        break;
                                }
                                if (num2 <= 0)
                                {
                                    this.uint_0 = 0x80000000;
                                }
                                else if (!DaoManagerSQL.UpdateAccountTags(player.PlayerId, player.Tags + num2))
                                {
                                    this.uint_0 = 0x80000000;
                                }
                                else
                                {
                                    player.Tags += num2;
                                    base.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, player));
                                    base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(new ItemsModel(), this.int_0, num3));
                                }
                            }
                            else if ((model.Category != ItemCategory.Coupon) || !RandomBoxXML.ContainsBox(this.int_0))
                            {
                                int num5 = ComDiv.GetIdStatics(model.Id, 1);
                                if ((((num5 < 1) || (num5 > 8)) && (num5 != 15)) && ((num5 != 0x1b) && ((num5 < 30) || (num5 > 0x23))))
                                {
                                    if (num5 == 0x11)
                                    {
                                        this.method_1(player, model.Name);
                                    }
                                    else if (num5 == 20)
                                    {
                                        this.method_2(player, model.Id);
                                    }
                                    else if (num5 == 0x25)
                                    {
                                        this.method_3(player, model.Id);
                                    }
                                    else
                                    {
                                        this.uint_0 = 0x80000000;
                                    }
                                }
                                else if (model.Equip != ItemEquipType.Durable)
                                {
                                    this.uint_0 = 0x80000000;
                                }
                                else
                                {
                                    model.Equip = ItemEquipType.Temporary;
                                    model.Count = Convert.ToUInt32(DateTimeUtil.Now().AddSeconds((double) model.Count).ToString("yyMMddHHmm"));
                                    string[] cOLUMNS = new string[] { "count", "equip" };
                                    object[] vALUES = new object[] { model.Count, (int) model.Equip };
                                    ComDiv.UpdateDB("player_items", "object_id", this.long_0, "owner_id", player.PlayerId, cOLUMNS, vALUES);
                                    if (num5 == 6)
                                    {
                                        CharacterModel character = player.Character.GetCharacter(model.Id);
                                        if (character != null)
                                        {
                                            base.Client.SendPacket(new PROTOCOL_CHAR_CHANGE_STATE_ACK(character));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                RandomBoxModel box = RandomBoxXML.GetBox(this.int_0);
                                if (box == null)
                                {
                                    this.uint_0 = 0x80000000;
                                }
                                else
                                {
                                    List<RandomBoxItem> sortedList = box.GetSortedList(this.method_0(1, 100));
                                    List<RandomBoxItem> rewardList = box.GetRewardList(sortedList, this.method_0(0, sortedList.Count));
                                    if (rewardList.Count <= 0)
                                    {
                                        this.uint_0 = 0x80000000;
                                    }
                                    else
                                    {
                                        int num4 = rewardList[0].Index;
                                        List<ItemsModel> list3 = new List<ItemsModel>();
                                        foreach (RandomBoxItem item in rewardList)
                                        {
                                            GoodsItem good = ShopManager.GetGood(item.GoodsId);
                                            if (good != null)
                                            {
                                                list3.Add(good.Item);
                                            }
                                            else
                                            {
                                                if (!DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold + num4))
                                                {
                                                    this.uint_0 = 0x80000000;
                                                    break;
                                                }
                                                player.Gold += num4;
                                                base.Client.SendPacket(new PROTOCOL_SHOP_PLUS_POINT_ACK(num4, player.Gold, 0));
                                            }
                                            if (item.Special)
                                            {
                                                using (PROTOCOL_AUTH_SHOP_JACKPOT_ACK protocol_auth_shop_jackpot_ack = new PROTOCOL_AUTH_SHOP_JACKPOT_ACK(player.Nickname, this.int_0, num4))
                                                {
                                                    GameXender.Client.SendPacketToAllClients(protocol_auth_shop_jackpot_ack);
                                                }
                                            }
                                        }
                                        base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(list3, this.int_0, num4));
                                        if (list3.Count > 0)
                                        {
                                            base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, list3));
                                        }
                                    }
                                    sortedList = null;
                                    rewardList = null;
                                }
                            }
                        }
                        else
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(0x80001029, null, null));
                            return;
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(this.uint_0, model, player));
                }
            }
            catch (OverflowException exception)
            {
                CLogger.Print($"Obj: {this.long_0} ItemId: {this.int_0} Count: {this.long_1} PlayerId: {base.Client.Player} Name: '{base.Client.Player.Nickname}' {exception.Message}", LoggerType.Error, exception);
            }
            catch (Exception exception2)
            {
                CLogger.Print("PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ: " + exception2.Message, LoggerType.Error, exception2);
            }
        }
    }
}

