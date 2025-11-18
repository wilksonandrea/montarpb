// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ
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

public class PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ : GameClientPacket
{
  private int int_0;

  private void method_0([In] int obj0, [In] uint obj1, Account long_1)
  {
    switch (obj0)
    {
      case 1600005:
        ClanModel clan1 = ClanManager.GetClan(long_1.ClanId);
        if (clan1.Id > 0 && clan1.OwnerId == this.Client.PlayerId && ComDiv.UpdateDB("system_clan", "name_color", (object) (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0, "id", (object) clan1.Id))
        {
          clan1.NameColor = (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_REPLACE_MANAGEMENT_ACK(clan1.NameColor));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(long_1));
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600006:
        if (ComDiv.UpdateDB("accounts", "nick_color", (object) (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0, "player_id", (object) long_1.PlayerId))
        {
          long_1.NickColor = (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0.Length, long_1));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(long_1));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, long_1, new ItemsModel(obj0, "Name Color [Active]", ItemEquipType.Temporary, obj1)));
          if (long_1.Room == null)
            break;
          using (PROTOCOL_ROOM_GET_COLOR_NICK_ACK Player = (PROTOCOL_ROOM_GET_COLOR_NICK_ACK) new PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(long_1.SlotId, long_1.NickColor))
            long_1.Room.SendPacketToPlayers((GameServerPacket) Player);
          long_1.Room.UpdateSlotsInfo();
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600009:
        if ((int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0 < 51 && (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0 >= long_1.Rank - 10 && (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0 <= long_1.Rank + 10)
        {
          if (ComDiv.UpdateDB("player_bonus", "fake_rank", (object) (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0, "owner_id", (object) long_1.PlayerId))
          {
            long_1.Bonus.FakeRank = (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0;
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0.Length, long_1));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(long_1));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, long_1, new ItemsModel(obj0, "Fake Rank [Active]", ItemEquipType.Temporary, obj1)));
            if (long_1.Room == null)
              break;
            using (PROTOCOL_ROOM_GET_RANK_ACK Player = (PROTOCOL_ROOM_GET_RANK_ACK) new PROTOCOL_ROOM_GET_SLOTINFO_ACK(long_1.SlotId, long_1.GetRank()))
              long_1.Room.SendPacketToPlayers((GameServerPacket) Player);
            long_1.Room.UpdateSlotsInfo();
            break;
          }
          ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600010:
        if (!string.IsNullOrEmpty(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0) && ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0.Length >= ConfigLoader.MinNickSize && ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0.Length <= ConfigLoader.MaxNickSize)
        {
          if (ComDiv.UpdateDB("player_bonus", "fake_nick", (object) long_1.Nickname, "owner_id", (object) long_1.PlayerId) && ComDiv.UpdateDB("accounts", "nickname", (object) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0, "player_id", (object) long_1.PlayerId))
          {
            long_1.Bonus.FakeNick = long_1.Nickname;
            long_1.Nickname = ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0;
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0.Length, long_1));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(long_1.Nickname));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(long_1));
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, long_1, new ItemsModel(obj0, "Fake Nick [Active]", ItemEquipType.Temporary, obj1)));
            if (long_1.Room == null)
              break;
            using (PROTOCOL_ROOM_GET_NICKNAME_ACK Player = (PROTOCOL_ROOM_GET_NICKNAME_ACK) new PROTOCOL_ROOM_GET_RANK_ACK(long_1.SlotId, long_1.Nickname))
              long_1.Room.SendPacketToPlayers((GameServerPacket) Player);
            long_1.Room.UpdateSlotsInfo();
            break;
          }
          ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600014:
        if (ComDiv.UpdateDB("player_bonus", "crosshair_color", (object) (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0, "owner_id", (object) long_1.PlayerId))
        {
          long_1.Bonus.CrosshairColor = (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0.Length, long_1));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, long_1, new ItemsModel(obj0, "Crosshair Color [Active]", ItemEquipType.Temporary, obj1)));
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600047:
        if (!string.IsNullOrEmpty(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0) && ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0.Length >= ConfigLoader.MinNickSize && ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0.Length <= ConfigLoader.MaxNickSize && long_1.Inventory.GetItem(1600010) == null)
        {
          if (!DaoManagerSQL.IsPlayerNameExist(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0))
          {
            if (ComDiv.UpdateDB("accounts", "nickname", (object) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0, "player_id", (object) long_1.PlayerId))
            {
              DaoManagerSQL.CreatePlayerNickHistory(long_1.PlayerId, long_1.Nickname, ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0, "Nickname changed (Item)");
              long_1.Nickname = ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(long_1.Nickname));
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(long_1));
              if (long_1.Room != null)
              {
                using (PROTOCOL_ROOM_GET_NICKNAME_ACK Player = (PROTOCOL_ROOM_GET_NICKNAME_ACK) new PROTOCOL_ROOM_GET_RANK_ACK(long_1.SlotId, long_1.Nickname))
                  long_1.Room.SendPacketToPlayers((GameServerPacket) Player);
                long_1.Room.UpdateSlotsInfo();
              }
              if (long_1.ClanId > 0)
              {
                using (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK Packet = (PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK) new PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(long_1))
                  ClanManager.SendPacket((GameServerPacket) Packet, long_1.ClanId, -1L, true, true);
              }
              AllUtils.SyncPlayerToFriends(long_1, true);
              break;
            }
            ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
            break;
          }
          ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483923U;
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600051:
        if (!string.IsNullOrEmpty(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0) && ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0.Length <= 16 /*0x10*/)
        {
          ClanModel clan2 = ClanManager.GetClan(long_1.ClanId);
          if (clan2.Id > 0 && clan2.OwnerId == this.Client.PlayerId)
          {
            if (!CommandManager.IsClanNameExist(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0) && ComDiv.UpdateDB("system_clan", "name", (object) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0, "id", (object) long_1.ClanId))
            {
              clan2.Name = ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0;
              using (PROTOCOL_CS_REPLACE_NAME_RESULT_ACK Packet = (PROTOCOL_CS_REPLACE_NAME_RESULT_ACK) new PROTOCOL_CS_REPLACE_PERSONMAX_ACK(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0))
              {
                ClanManager.SendPacket((GameServerPacket) Packet, long_1.ClanId, -1L, true, true);
                break;
              }
            }
            ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
            break;
          }
          ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600052:
        ClanModel clan3 = ClanManager.GetClan(long_1.ClanId);
        if (clan3.Id > 0 && clan3.OwnerId == this.Client.PlayerId && !CommandManager.IsClanLogoExist(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0) && DaoManagerSQL.UpdateClanLogo(long_1.ClanId, ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0))
        {
          clan3.Logo = ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0;
          using (PROTOCOL_CS_REPLACE_MARK_RESULT_ACK Packet = (PROTOCOL_CS_REPLACE_MARK_RESULT_ACK) new PROTOCOL_CS_REPLACE_NAME_RESULT_ACK(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0))
          {
            ClanManager.SendPacket((GameServerPacket) Packet, long_1.ClanId, -1L, true, true);
            break;
          }
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600085:
        if (long_1.Room != null)
        {
          Account playerBySlot = long_1.Room.GetPlayerBySlot((int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0);
          if (playerBySlot != null)
          {
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_ROOM_INFO_ENTER_ACK(playerBySlot));
            break;
          }
          ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600183:
        if (!string.IsNullOrWhiteSpace(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0) && ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0.Length <= 60 && !string.IsNullOrWhiteSpace(long_1.Nickname))
        {
          GameXender.Client.SendPacketToAllClients((GameServerPacket) new PROTOCOL_BASE_TICKET_UPDATE_ACK(long_1.Nickname, ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0));
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600187:
        if (ComDiv.UpdateDB("player_bonus", "muzzle_color", (object) (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0, "owner_id", (object) long_1.PlayerId))
        {
          long_1.Bonus.MuzzleColor = (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0.Length, long_1));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, long_1, new ItemsModel(obj0, "Muzzle Color [Active]", ItemEquipType.Temporary, obj1)));
          if (long_1.Room == null)
            break;
          using (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK Player = (PROTOCOL_ROOM_GET_COLOR_MUZZLE_FLASH_ACK) new PROTOCOL_ROOM_GET_NAMECARD_ACK(long_1.SlotId, long_1.Bonus.MuzzleColor))
            long_1.Room.SendPacketToPlayers((GameServerPacket) Player);
          long_1.Room.UpdateSlotsInfo();
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600193:
        ClanModel clan4 = ClanManager.GetClan(long_1.ClanId);
        if (clan4.Id > 0 && clan4.OwnerId == this.Client.PlayerId)
        {
          if (ComDiv.UpdateDB("system_clan", "effects", (object) (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0, "id", (object) long_1.ClanId))
          {
            clan4.Effect = (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0;
            using (PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK Packet = (PROTOCOL_CS_REPLACE_MARKEFFECT_RESULT_ACK) new PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK((int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0))
              ClanManager.SendPacket((GameServerPacket) Packet, long_1.ClanId, -1L, true, true);
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(long_1));
            break;
          }
          ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      case 1600205:
        if (ComDiv.UpdateDB("player_bonus", "nick_border_color", (object) (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0, "owner_id", (object) long_1.PlayerId))
        {
          long_1.Bonus.NickBorderColor = (int) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_NEW_REWARD_POPUP_ACK(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0.Length, long_1));
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, long_1, new ItemsModel(obj0, "Nick Border Color [Active]", ItemEquipType.Temporary, obj1)));
          if (long_1.Room == null)
            break;
          using (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK Player = (PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK) new PROTOCOL_ROOM_GET_NICKNAME_ACK(long_1.SlotId, long_1.Bonus.NickBorderColor))
            long_1.Room.SendPacketToPlayers((GameServerPacket) Player);
          long_1.Room.UpdateSlotsInfo();
          break;
        }
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
      default:
        CLogger.Print($"Coupon effect not found! Id: {obj0}", LoggerType.Warning, (Exception) null);
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
        break;
    }
  }

  public PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ()
  {
    ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 1U;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
  }
}
