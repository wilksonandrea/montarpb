// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ : GameClientPacket
{
  private List<CartGoods> list_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ItemsModel itemsModel = player.Inventory.GetItem(((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).long_0);
      PlayerBonus bonus = player.Bonus;
      if (itemsModel == null)
        ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      else if (ComDiv.GetIdStatics(itemsModel.Id, 1) == 16 /*0x10*/)
      {
        if (bonus == null)
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(2147483648U /*0x80000000*/, 0L));
          return;
        }
        if (!bonus.RemoveBonuses(itemsModel.Id))
        {
          if (itemsModel.Id == 1600014)
          {
            if (ComDiv.UpdateDB("player_bonus", "crosshair_color", (object) 4, "owner_id", (object) player.PlayerId))
            {
              bonus.CrosshairColor = 4;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(0, player));
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(player));
            }
            else
              ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
          }
          else if (itemsModel.Id == 1600010)
          {
            if (bonus.FakeNick.Length == 0)
              ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
            else if (ComDiv.UpdateDB("accounts", "nickname", (object) bonus.FakeNick, "player_id", (object) player.PlayerId) && ComDiv.UpdateDB("player_bonus", "fake_nick", (object) "", "owner_id", (object) player.PlayerId))
            {
              player.Nickname = bonus.FakeNick;
              bonus.FakeNick = "";
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(0, player));
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(player.Nickname));
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(player));
              RoomModel room = player.Room;
              if (room != null)
              {
                using (PROTOCOL_ROOM_GET_NICKNAME_ACK Player = (PROTOCOL_ROOM_GET_NICKNAME_ACK) new PROTOCOL_ROOM_GET_RANK_ACK(player.SlotId, player.Nickname))
                  room.SendPacketToPlayers((GameServerPacket) Player);
                room.UpdateSlotsInfo();
              }
            }
            else
              ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
          }
          else if (itemsModel.Id == 1600009)
          {
            if (ComDiv.UpdateDB("player_bonus", "fake_rank", (object) 55, "owner_id", (object) player.PlayerId))
            {
              bonus.FakeRank = 55;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(0, player));
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(player));
              RoomModel room = player.Room;
              if (room != null)
              {
                using (PROTOCOL_ROOM_GET_RANK_ACK Player = (PROTOCOL_ROOM_GET_RANK_ACK) new PROTOCOL_ROOM_GET_SLOTINFO_ACK(player.SlotId, bonus.MuzzleColor))
                  room.SendPacketToPlayers((GameServerPacket) Player);
                room.UpdateSlotsInfo();
              }
            }
            else
              ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
          }
          else if (itemsModel.Id == 1600187)
          {
            if (ComDiv.UpdateDB("player_bonus", "muzzle_color", (object) 0, "owner_id", (object) player.PlayerId))
            {
              bonus.MuzzleColor = 0;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(0, player));
              RoomModel room = player.Room;
              if (room != null)
              {
                using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK Player = (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK) new PROTOCOL_ROOM_GET_NAMECARD_ACK(player.SlotId, bonus.MuzzleColor))
                  room.SendPacketToPlayers((GameServerPacket) Player);
                room.UpdateSlotsInfo();
              }
            }
            else
              ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
          }
          else if (itemsModel.Id == 1600006)
          {
            if (ComDiv.UpdateDB("accounts", "nick_color", (object) 0, "owner_id", (object) player.PlayerId))
            {
              player.NickColor = 0;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(0, player));
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(player));
              RoomModel room = player.Room;
              if (room != null)
              {
                using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK Player = (PROTOCOL_ROOM_GET_COLOR_NICK_ACK) new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(player.SlotId, player.NickColor))
                  room.SendPacketToPlayers((GameServerPacket) Player);
                room.UpdateSlotsInfo();
              }
            }
            else
              ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
          }
        }
        else
          DaoManagerSQL.UpdatePlayerBonus(player.PlayerId, bonus.Bonuses, bonus.FreePass);
        CouponFlag couponEffect = CouponEffectXML.GetCouponEffect(itemsModel.Id);
        if (couponEffect != null && couponEffect.EffectFlag > (CouponEffects) 0 && player.Effects.HasFlag((Enum) couponEffect.EffectFlag))
        {
          player.Effects -= couponEffect.EffectFlag;
          DaoManagerSQL.UpdateCouponEffect(player.PlayerId, player.Effects);
        }
      }
      if (((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 == 1U && itemsModel != null)
      {
        if (DaoManagerSQL.DeletePlayerInventoryItem(itemsModel.ObjectId, player.PlayerId))
          player.Inventory.RemoveItem(itemsModel);
        else
          ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      }
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0, ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).long_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ()
  {
    ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).uint_0 = 1U;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
  }
}
