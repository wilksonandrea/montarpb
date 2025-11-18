namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_INVENTORY_USE_ITEM_REQ : GameClientPacket
    {
        private long long_0;
        private uint uint_0;
        private uint uint_1 = 1;
        private byte[] byte_0;
        private string string_0;

        private void method_0(int int_0, uint uint_2, Account account_0)
        {
            if (int_0 == 0x186a33)
            {
                if (string.IsNullOrEmpty(this.string_0) || (this.string_0.Length > 0x10))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    ClanModel clan = ClanManager.GetClan(account_0.ClanId);
                    if ((clan.Id <= 0) || (clan.OwnerId != base.Client.PlayerId))
                    {
                        this.uint_1 = 0x80000000;
                    }
                    else if (ClanManager.IsClanNameExist(this.string_0) || !ComDiv.UpdateDB("system_clan", "name", this.string_0, "id", account_0.ClanId))
                    {
                        this.uint_1 = 0x80000000;
                    }
                    else
                    {
                        clan.Name = this.string_0;
                        using (PROTOCOL_CS_REPLACE_NAME_RESULT_ACK protocol_cs_replace_name_result_ack = new PROTOCOL_CS_REPLACE_NAME_RESULT_ACK(this.string_0))
                        {
                            ClanManager.SendPacket(protocol_cs_replace_name_result_ack, account_0.ClanId, -1L, true, true);
                        }
                    }
                }
            }
            else if (int_0 == 0x186a34)
            {
                ClanModel clan = ClanManager.GetClan(account_0.ClanId);
                if ((clan.Id <= 0) || ((clan.OwnerId != base.Client.PlayerId) || (ClanManager.IsClanLogoExist(this.uint_0) || !DaoManagerSQL.UpdateClanLogo(account_0.ClanId, this.uint_0))))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    clan.Logo = this.uint_0;
                    using (PROTOCOL_CS_REPLACE_MARK_RESULT_ACK protocol_cs_replace_mark_result_ack = new PROTOCOL_CS_REPLACE_MARK_RESULT_ACK(this.uint_0))
                    {
                        ClanManager.SendPacket(protocol_cs_replace_mark_result_ack, account_0.ClanId, -1L, true, true);
                    }
                }
            }
            else if (int_0 == 0x186a2f)
            {
                if (string.IsNullOrEmpty(this.string_0) || ((this.string_0.Length < ConfigLoader.MinNickSize) || ((this.string_0.Length > ConfigLoader.MaxNickSize) || (account_0.Inventory.GetItem(0x186a0a) != null))))
                {
                    this.uint_1 = 0x80000000;
                }
                else if (DaoManagerSQL.IsPlayerNameExist(this.string_0))
                {
                    this.uint_1 = 0x80000113;
                }
                else if (!ComDiv.UpdateDB("accounts", "nickname", this.string_0, "player_id", account_0.PlayerId))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    DaoManagerSQL.CreatePlayerNickHistory(account_0.PlayerId, account_0.Nickname, this.string_0, "Nickname changed (Item)");
                    account_0.Nickname = this.string_0;
                    base.Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(account_0.Nickname));
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
                    if (account_0.Room != null)
                    {
                        using (PROTOCOL_ROOM_GET_NICKNAME_ACK protocol_room_get_nickname_ack = new PROTOCOL_ROOM_GET_NICKNAME_ACK(account_0.SlotId, account_0.Nickname))
                        {
                            account_0.Room.SendPacketToPlayers(protocol_room_get_nickname_ack);
                        }
                        account_0.Room.UpdateSlotsInfo();
                    }
                    if (account_0.ClanId > 0)
                    {
                        using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK protocol_cs_member_info_change_ack = new PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(account_0))
                        {
                            ClanManager.SendPacket(protocol_cs_member_info_change_ack, account_0.ClanId, -1L, true, true);
                        }
                    }
                    AllUtils.SyncPlayerToFriends(account_0, true);
                }
            }
            else if (int_0 == 0x186a06)
            {
                if (!ComDiv.UpdateDB("accounts", "nick_color", (int) this.uint_0, "player_id", account_0.PlayerId))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    account_0.NickColor = (int) this.uint_0;
                    base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Name Color [Active]", ItemEquipType.Temporary, uint_2)));
                    if (account_0.Room != null)
                    {
                        using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK protocol_room_get_color_nick_ack = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(account_0.SlotId, account_0.NickColor))
                        {
                            account_0.Room.SendPacketToPlayers(protocol_room_get_color_nick_ack);
                        }
                        account_0.Room.UpdateSlotsInfo();
                    }
                }
            }
            else if (int_0 == 0x186abb)
            {
                if (!ComDiv.UpdateDB("player_bonus", "muzzle_color", (int) this.uint_0, "owner_id", account_0.PlayerId))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    account_0.Bonus.MuzzleColor = (int) this.uint_0;
                    base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Muzzle Color [Active]", ItemEquipType.Temporary, uint_2)));
                    if (account_0.Room != null)
                    {
                        using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK protocol_room_get_color_muzzle_flash_ack = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(account_0.SlotId, account_0.Bonus.MuzzleColor))
                        {
                            account_0.Room.SendPacketToPlayers(protocol_room_get_color_muzzle_flash_ack);
                        }
                        account_0.Room.UpdateSlotsInfo();
                    }
                }
            }
            else if (int_0 == 0x186acd)
            {
                if (!ComDiv.UpdateDB("player_bonus", "nick_border_color", (int) this.uint_0, "owner_id", account_0.PlayerId))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    account_0.Bonus.NickBorderColor = (int) this.uint_0;
                    base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Nick Border Color [Active]", ItemEquipType.Temporary, uint_2)));
                    if (account_0.Room != null)
                    {
                        using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK protocol_room_get_nick_outline_color_ack = new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(account_0.SlotId, account_0.Bonus.NickBorderColor))
                        {
                            account_0.Room.SendPacketToPlayers(protocol_room_get_nick_outline_color_ack);
                        }
                        account_0.Room.UpdateSlotsInfo();
                    }
                }
            }
            else if (int_0 == 0x186ac1)
            {
                ClanModel clan = ClanManager.GetClan(account_0.ClanId);
                if ((clan.Id <= 0) || (clan.OwnerId != base.Client.PlayerId))
                {
                    this.uint_1 = 0x80000000;
                }
                else if (!ComDiv.UpdateDB("system_clan", "effects", (int) this.uint_0, "id", account_0.ClanId))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    clan.Effect = (int) this.uint_0;
                    using (PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK protocol_cs_replace_markeffect_result_ack = new PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK((int) this.uint_0))
                    {
                        ClanManager.SendPacket(protocol_cs_replace_markeffect_result_ack, account_0.ClanId, -1L, true, true);
                    }
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
                }
            }
            else if (int_0 == 0x186a09)
            {
                if ((this.uint_0 >= 0x33) || ((this.uint_0 < (account_0.Rank - 10)) || (this.uint_0 > (account_0.Rank + 10))))
                {
                    this.uint_1 = 0x80000000;
                }
                else if (!ComDiv.UpdateDB("player_bonus", "fake_rank", (int) this.uint_0, "owner_id", account_0.PlayerId))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    account_0.Bonus.FakeRank = (int) this.uint_0;
                    base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Fake Rank [Active]", ItemEquipType.Temporary, uint_2)));
                    if (account_0.Room != null)
                    {
                        using (PROTOCOL_ROOM_GET_RANK_ACK protocol_room_get_rank_ack = new PROTOCOL_ROOM_GET_RANK_ACK(account_0.SlotId, account_0.GetRank()))
                        {
                            account_0.Room.SendPacketToPlayers(protocol_room_get_rank_ack);
                        }
                        account_0.Room.UpdateSlotsInfo();
                    }
                }
            }
            else if (int_0 == 0x186a0a)
            {
                if (string.IsNullOrEmpty(this.string_0) || ((this.string_0.Length < ConfigLoader.MinNickSize) || (this.string_0.Length > ConfigLoader.MaxNickSize)))
                {
                    this.uint_1 = 0x80000000;
                }
                else if (!ComDiv.UpdateDB("player_bonus", "fake_nick", account_0.Nickname, "owner_id", account_0.PlayerId) || !ComDiv.UpdateDB("accounts", "nickname", this.string_0, "player_id", account_0.PlayerId))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    account_0.Bonus.FakeNick = account_0.Nickname;
                    account_0.Nickname = this.string_0;
                    base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
                    base.Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(account_0.Nickname));
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Fake Nick [Active]", ItemEquipType.Temporary, uint_2)));
                    if (account_0.Room != null)
                    {
                        using (PROTOCOL_ROOM_GET_NICKNAME_ACK protocol_room_get_nickname_ack2 = new PROTOCOL_ROOM_GET_NICKNAME_ACK(account_0.SlotId, account_0.Nickname))
                        {
                            account_0.Room.SendPacketToPlayers(protocol_room_get_nickname_ack2);
                        }
                        account_0.Room.UpdateSlotsInfo();
                    }
                }
            }
            else if (int_0 == 0x186a0e)
            {
                if (!ComDiv.UpdateDB("player_bonus", "crosshair_color", (int) this.uint_0, "owner_id", account_0.PlayerId))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    account_0.Bonus.CrosshairColor = (int) this.uint_0;
                    base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(this.byte_0.Length, account_0));
                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account_0, new ItemsModel(int_0, "Crosshair Color [Active]", ItemEquipType.Temporary, uint_2)));
                }
            }
            else if (int_0 == 0x186a05)
            {
                ClanModel clan = ClanManager.GetClan(account_0.ClanId);
                if ((clan.Id <= 0) || ((clan.OwnerId != base.Client.PlayerId) || !ComDiv.UpdateDB("system_clan", "name_color", (int) this.uint_0, "id", clan.Id)))
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    clan.NameColor = (int) this.uint_0;
                    base.Client.SendPacket(new PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK(clan.NameColor));
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(account_0));
                }
            }
            else if (int_0 == 0x186ab7)
            {
                if (!string.IsNullOrWhiteSpace(this.string_0) && ((this.string_0.Length <= 60) && !string.IsNullOrWhiteSpace(account_0.Nickname)))
                {
                    GameXender.Client.SendPacketToAllClients(new PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK(account_0.Nickname, this.string_0));
                }
                else
                {
                    this.uint_1 = 0x80000000;
                }
            }
            else if (int_0 == 0x186a55)
            {
                if (account_0.Room == null)
                {
                    this.uint_1 = 0x80000000;
                }
                else
                {
                    Account playerBySlot = account_0.Room.GetPlayerBySlot((int) this.uint_0);
                    if (playerBySlot != null)
                    {
                        base.Client.SendPacket(new PROTOCOL_ROOM_GET_USER_ITEM_ACK(playerBySlot));
                    }
                    else
                    {
                        this.uint_1 = 0x80000000;
                    }
                }
            }
            else
            {
                CLogger.Print($"Coupon effect not found! Id: {int_0}", LoggerType.Warning, null);
                this.uint_1 = 0x80000000;
            }
        }

        public override void Read()
        {
            this.long_0 = base.ReadD();
            this.byte_0 = base.ReadB(base.ReadC());
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ItemsModel model = player.Inventory.GetItem(this.long_0);
                    if ((model == null) || (model.Id <= 0x19f0a0))
                    {
                        this.uint_1 = 0x80000000;
                    }
                    else
                    {
                        int num = ComDiv.CreateItemId(0x10, 0, ComDiv.GetIdStatics(model.Id, 3));
                        uint num2 = Convert.ToUInt32(DateTimeUtil.Now().AddDays((double) ComDiv.GetIdStatics(model.Id, 2)).ToString("yyMMddHHmm"));
                        if ((num == 0x186a2f) || ((num == 0x186a33) || (num == 0x186a0a)))
                        {
                            this.string_0 = Bitwise.HexArrayToString(this.byte_0, this.byte_0.Length);
                        }
                        else if ((num == 0x186a34) || (num == 0x186a05))
                        {
                            this.uint_0 = BitConverter.ToUInt32(this.byte_0, 0);
                        }
                        else if (this.byte_0.Length != 0)
                        {
                            this.uint_0 = this.byte_0[0];
                        }
                        this.method_0(num, num2, player);
                    }
                    base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(this.uint_1, model, player));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_INVENTORY_USE_ITEM_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

