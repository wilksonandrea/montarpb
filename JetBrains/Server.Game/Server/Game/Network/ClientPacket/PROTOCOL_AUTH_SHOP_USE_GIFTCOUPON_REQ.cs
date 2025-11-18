// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ : GameClientPacket
{
  private uint uint_0;
  private string string_0;
  private TicketType ticketType_0;

  public PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ()
  {
    ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).uint_0 = 1U;
    ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).random_0 = new Random();
    ((PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ) this).object_0 = new object();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).long_0 = (long) this.ReadUD();
    ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0 = this.ReadB((int) this.ReadC());
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ItemsModel itemsModel = player.Inventory.GetItem(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).long_0);
      if (itemsModel != null && itemsModel.Id > 1600000)
      {
        int itemId = ComDiv.CreateItemId(16 /*0x10*/, 0, ComDiv.GetIdStatics(itemsModel.Id, 3));
        switch (itemId)
        {
          case 1600005:
          case 1610052:
            ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0 = BitConverter.ToUInt32(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0, 0);
            break;
          case 1600010:
          case 1610047:
          case 1610051:
            ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0 = Bitwise.HexArrayToString(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0, ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0.Length);
            break;
          default:
            if (((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0.Length != 0)
            {
              ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0 = (uint) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0[0];
              break;
            }
            break;
        }
        this.method_0(itemId, player);
      }
      else
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  private void method_0([In] int obj0, Account string_0)
  {
    switch (obj0)
    {
      case 1600005:
        ClanModel clan1 = ClanManager.GetClan(string_0.ClanId);
        if (clan1.Id > 0 && clan1.OwnerId == this.Client.PlayerId && ComDiv.UpdateDB("system_clan", "name_color", (object) (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0, "id", (object) clan1.Id))
        {
          clan1.NameColor = (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_REPLACE_MANAGEMENT_ACK(clan1.NameColor));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(string_0));
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600006:
        if (ComDiv.UpdateDB("accounts", "nick_color", (object) (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0, "player_id", (object) string_0.PlayerId))
        {
          string_0.NickColor = (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0.Length, string_0));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(string_0));
          if (string_0.Room == null)
            break;
          using (PROTOCOL_ROOM_GET_NICKNAME_ACK Player = (PROTOCOL_ROOM_GET_NICKNAME_ACK) new PROTOCOL_ROOM_GET_RANK_ACK(string_0.SlotId, string_0.Nickname))
            string_0.Room.SendPacketToPlayers((GameServerPacket) Player);
          string_0.Room.UpdateSlotsInfo();
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600009:
        if ((int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0 < 51 && (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0 >= string_0.Rank - 10 && (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0 <= string_0.Rank + 10)
        {
          if (ComDiv.UpdateDB("player_bonus", "fake_rank", (object) (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0, "owner_id", (object) string_0.PlayerId))
          {
            string_0.Bonus.FakeRank = (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0;
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0.Length, string_0));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(string_0));
            if (string_0.Room == null)
              break;
            using (PROTOCOL_ROOM_GET_RANK_ACK Player = (PROTOCOL_ROOM_GET_RANK_ACK) new PROTOCOL_ROOM_GET_SLOTINFO_ACK(string_0.SlotId, string_0.GetRank()))
              string_0.Room.SendPacketToPlayers((GameServerPacket) Player);
            string_0.Room.UpdateSlotsInfo();
            break;
          }
          ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600010:
        if (!string.IsNullOrEmpty(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0) && ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0.Length >= ConfigLoader.MinNickSize && ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0.Length <= ConfigLoader.MaxNickSize)
        {
          if (ComDiv.UpdateDB("player_bonus", "fake_nick", (object) string_0.Nickname, "owner_id", (object) string_0.PlayerId) && ComDiv.UpdateDB("accounts", "nickname", (object) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0, "player_id", (object) string_0.PlayerId))
          {
            string_0.Bonus.FakeNick = string_0.Nickname;
            string_0.Nickname = ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0;
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0.Length, string_0));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(string_0.Nickname));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(string_0));
            if (string_0.Room == null)
              break;
            using (PROTOCOL_ROOM_GET_NICKNAME_ACK Player = (PROTOCOL_ROOM_GET_NICKNAME_ACK) new PROTOCOL_ROOM_GET_RANK_ACK(string_0.SlotId, string_0.Nickname))
              string_0.Room.SendPacketToPlayers((GameServerPacket) Player);
            string_0.Room.UpdateSlotsInfo();
            break;
          }
          ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600014:
        if (ComDiv.UpdateDB("player_bonus", "crosshair_color", (object) (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0, "owner_id", (object) string_0.PlayerId))
        {
          string_0.Bonus.CrosshairColor = (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0.Length, string_0));
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600047:
        if (!string.IsNullOrEmpty(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0) && ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0.Length >= ConfigLoader.MinNickSize && ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0.Length <= ConfigLoader.MaxNickSize && string_0.Inventory.GetItem(1600010) == null)
        {
          if (!DaoManagerSQL.IsPlayerNameExist(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0))
          {
            if (ComDiv.UpdateDB("accounts", "nickname", (object) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0, "player_id", (object) string_0.PlayerId))
            {
              DaoManagerSQL.CreatePlayerNickHistory(string_0.PlayerId, string_0.Nickname, ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0, "Changed (Coupon)");
              string_0.Nickname = ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(string_0.Nickname));
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(string_0));
              if (string_0.Room != null)
              {
                using (PROTOCOL_ROOM_GET_NICKNAME_ACK Player = (PROTOCOL_ROOM_GET_NICKNAME_ACK) new PROTOCOL_ROOM_GET_RANK_ACK(string_0.SlotId, string_0.Nickname))
                  string_0.Room.SendPacketToPlayers((GameServerPacket) Player);
                string_0.Room.UpdateSlotsInfo();
              }
              if (string_0.ClanId > 0)
              {
                using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK Packet = (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) new PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(string_0))
                  ClanManager.SendPacket((GameServerPacket) Packet, string_0.ClanId, -1L, true, true);
              }
              AllUtils.SyncPlayerToFriends(string_0, true);
              break;
            }
            ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
            break;
          }
          ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483923U;
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600051:
        if (!string.IsNullOrEmpty(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0) && ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0.Length <= 16 /*0x10*/)
        {
          ClanModel clan2 = ClanManager.GetClan(string_0.ClanId);
          if (clan2.Id > 0 && clan2.OwnerId == this.Client.PlayerId)
          {
            if (!CommandManager.IsClanNameExist(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0) && ComDiv.UpdateDB("system_clan", "name", (object) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0, "id", (object) string_0.ClanId))
            {
              clan2.Name = ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0;
              using (PROTOCOL_CS_REPLACE_NAME_RESULT_ACK Packet = (PROTOCOL_CS_REPLACE_NAME_RESULT_ACK) new PROTOCOL_CS_REPLACE_PERSONMAX_ACK(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0))
              {
                ClanManager.SendPacket((GameServerPacket) Packet, string_0.ClanId, -1L, true, true);
                break;
              }
            }
            ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
            break;
          }
          ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600052:
        ClanModel clan3 = ClanManager.GetClan(string_0.ClanId);
        if (clan3.Id > 0 && clan3.OwnerId == this.Client.PlayerId && !CommandManager.IsClanLogoExist(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0) && DaoManagerSQL.UpdateClanLogo(string_0.ClanId, ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0))
        {
          clan3.Logo = ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0;
          using (PROTOCOL_CS_REPLACE_MARK_RESULT_ACK Packet = (PROTOCOL_CS_REPLACE_MARK_RESULT_ACK) new PROTOCOL_CS_REPLACE_NAME_RESULT_ACK(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0))
          {
            ClanManager.SendPacket((GameServerPacket) Packet, string_0.ClanId, -1L, true, true);
            break;
          }
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600085:
        if (string_0.Room != null)
        {
          Account playerBySlot = string_0.Room.GetPlayerBySlot((int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0);
          if (playerBySlot != null)
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_INFO_ENTER_ACK(playerBySlot));
            break;
          }
          ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600183:
        if (!string.IsNullOrWhiteSpace(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0) && ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0.Length <= 60 && !string.IsNullOrWhiteSpace(string_0.Nickname))
        {
          GameXender.Client.SendPacketToAllClients((GameServerPacket) new PROTOCOL_BASE_TICKET_UPDATE_ACK(string_0.Nickname, ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).string_0));
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600187:
        if (ComDiv.UpdateDB("player_bonus", "muzzle_color", (object) (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0, "owner_id", (object) string_0.PlayerId))
        {
          string_0.Bonus.MuzzleColor = (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0.Length, string_0));
          if (string_0.Room == null)
            break;
          using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK Player = (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK) new PROTOCOL_ROOM_GET_NAMECARD_ACK(string_0.SlotId, string_0.Bonus.MuzzleColor))
            string_0.Room.SendPacketToPlayers((GameServerPacket) Player);
          string_0.Room.UpdateSlotsInfo();
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600193:
        ClanModel clan4 = ClanManager.GetClan(string_0.ClanId);
        if (clan4.Id > 0 && clan4.OwnerId == this.Client.PlayerId)
        {
          if (ComDiv.UpdateDB("system_clan", "effects", (object) (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0, "id", (object) string_0.ClanId))
          {
            clan4.Effect = (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0;
            using (PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK Packet = (PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK) new PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK((int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0))
              ClanManager.SendPacket((GameServerPacket) Packet, string_0.ClanId, -1L, true, true);
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(string_0));
            break;
          }
          ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600205:
        if (ComDiv.UpdateDB("player_bonus", "nick_border_color", (object) (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0, "owner_id", (object) string_0.PlayerId))
        {
          string_0.Bonus.NickBorderColor = (int) ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).byte_0.Length, string_0));
          if (string_0.Room == null)
            break;
          using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK Player = (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK) new PROTOCOL_ROOM_GET_NICKNAME_ACK(string_0.SlotId, string_0.Bonus.NickBorderColor))
            string_0.Room.SendPacketToPlayers((GameServerPacket) Player);
          string_0.Room.UpdateSlotsInfo();
          break;
        }
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      default:
        CLogger.Print($"Coupon effect not found! Id: {obj0}", LoggerType.Warning, (Exception) null);
        ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
    }
  }

  public PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ()
  {
  }
}
