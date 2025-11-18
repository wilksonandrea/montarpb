namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ : GameClientPacket
    {
        private long long_0;
        private uint uint_0 = 1;

        public override void Read()
        {
            this.long_0 = base.ReadUD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ItemsModel model = player.Inventory.GetItem(this.long_0);
                    PlayerBonus bonus = player.Bonus;
                    if (model != null)
                    {
                        if (ComDiv.GetIdStatics(model.Id, 1) == 0x10)
                        {
                            if (bonus != null)
                            {
                                if (bonus.RemoveBonuses(model.Id))
                                {
                                    DaoManagerSQL.UpdatePlayerBonus(player.PlayerId, bonus.Bonuses, bonus.FreePass);
                                }
                                else if (model.Id == 0x186a0e)
                                {
                                    if (!ComDiv.UpdateDB("player_bonus", "crosshair_color", 4, "owner_id", player.PlayerId))
                                    {
                                        this.uint_0 = 0x80000000;
                                    }
                                    else
                                    {
                                        bonus.CrosshairColor = 4;
                                        base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
                                        base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
                                    }
                                }
                                else if (model.Id == 0x186a0a)
                                {
                                    if (bonus.FakeNick.Length == 0)
                                    {
                                        this.uint_0 = 0x80000000;
                                    }
                                    else if (!ComDiv.UpdateDB("accounts", "nickname", bonus.FakeNick, "player_id", player.PlayerId) || !ComDiv.UpdateDB("player_bonus", "fake_nick", "", "owner_id", player.PlayerId))
                                    {
                                        this.uint_0 = 0x80000000;
                                    }
                                    else
                                    {
                                        player.Nickname = bonus.FakeNick;
                                        bonus.FakeNick = "";
                                        base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
                                        base.Client.SendPacket(new PROTOCOL_AUTH_CHANGE_NICKNAME_ACK(player.Nickname));
                                        base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
                                        RoomModel room = player.Room;
                                        if (room != null)
                                        {
                                            using (PROTOCOL_ROOM_GET_NICKNAME_ACK protocol_room_get_nickname_ack = new PROTOCOL_ROOM_GET_NICKNAME_ACK(player.SlotId, player.Nickname))
                                            {
                                                room.SendPacketToPlayers(protocol_room_get_nickname_ack);
                                            }
                                            room.UpdateSlotsInfo();
                                        }
                                    }
                                }
                                else if (model.Id == 0x186a09)
                                {
                                    if (!ComDiv.UpdateDB("player_bonus", "fake_rank", (int) 0x37, "owner_id", player.PlayerId))
                                    {
                                        this.uint_0 = 0x80000000;
                                    }
                                    else
                                    {
                                        bonus.FakeRank = 0x37;
                                        base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
                                        base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
                                        RoomModel room = player.Room;
                                        if (room != null)
                                        {
                                            using (PROTOCOL_ROOM_GET_RANK_ACK protocol_room_get_rank_ack = new PROTOCOL_ROOM_GET_RANK_ACK(player.SlotId, bonus.MuzzleColor))
                                            {
                                                room.SendPacketToPlayers(protocol_room_get_rank_ack);
                                            }
                                            room.UpdateSlotsInfo();
                                        }
                                    }
                                }
                                else if (model.Id == 0x186abb)
                                {
                                    if (!ComDiv.UpdateDB("player_bonus", "muzzle_color", 0, "owner_id", player.PlayerId))
                                    {
                                        this.uint_0 = 0x80000000;
                                    }
                                    else
                                    {
                                        bonus.MuzzleColor = 0;
                                        base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
                                        RoomModel room = player.Room;
                                        if (room != null)
                                        {
                                            using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK protocol_room_get_color_muzzle_flash_ack = new PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK(player.SlotId, bonus.MuzzleColor))
                                            {
                                                room.SendPacketToPlayers(protocol_room_get_color_muzzle_flash_ack);
                                            }
                                            room.UpdateSlotsInfo();
                                        }
                                    }
                                }
                                else if (model.Id == 0x186a06)
                                {
                                    if (!ComDiv.UpdateDB("accounts", "nick_color", 0, "owner_id", player.PlayerId))
                                    {
                                        this.uint_0 = 0x80000000;
                                    }
                                    else
                                    {
                                        player.NickColor = 0;
                                        base.Client.SendPacket(new PROTOCOL_BASE_INV_ITEM_DATA_ACK(0, player));
                                        base.Client.SendPacket(new PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(player));
                                        RoomModel room = player.Room;
                                        if (room != null)
                                        {
                                            using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK protocol_room_get_color_nick_ack = new PROTOCOL_ROOM_GET_COLOR_NICK_ACK(player.SlotId, player.NickColor))
                                            {
                                                room.SendPacketToPlayers(protocol_room_get_color_nick_ack);
                                            }
                                            room.UpdateSlotsInfo();
                                        }
                                    }
                                }
                                CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(model.Id);
                                if ((couponEffect != null) && ((couponEffect.EffectFlag > 0L) && player.Effects.HasFlag(couponEffect.EffectFlag)))
                                {
                                    player.Effects -= couponEffect.EffectFlag;
                                    DaoManagerSQL.UpdateCouponEffect(player.PlayerId, player.Effects);
                                }
                            }
                            else
                            {
                                base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(0x80000000, 0L));
                                return;
                            }
                        }
                    }
                    else
                    {
                        this.uint_0 = 0x80000000;
                    }
                    if ((this.uint_0 == 1) && (model != null))
                    {
                        if (DaoManagerSQL.DeletePlayerInventoryItem(model.ObjectId, player.PlayerId))
                        {
                            player.Inventory.RemoveItem(model);
                        }
                        else
                        {
                            this.uint_0 = 0x80000000;
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(this.uint_0, this.long_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

