// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ : GameClientPacket
{
  private long long_0;
  private int int_0;
  private long long_1;
  private uint uint_0;
  private readonly Random random_0;
  private readonly object object_0;

  public virtual void Run()
  {
    try
    {
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2148110592U));
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ()
  {
  }

  public virtual void Read()
  {
    byte num1 = this.ReadC();
    for (byte index = 0; (int) index < (int) num1; ++index)
    {
      int num2 = (int) this.ReadC();
      ((PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ) this).list_0.Add(new CartGoods()
      {
        GoodId = this.ReadD(),
        BuyType = (int) this.ReadC()
      });
      int num3 = (int) this.ReadC();
      this.ReadQ();
    }
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.Inventory.Items.Count >= 500)
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487929U));
      }
      else
      {
        int num1;
        int num2;
        int num3;
        List<GoodsItem> goods = ShopManager.GetGoods(((PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ) this).list_0, ref num1, ref num2, ref num3);
        if (goods.Count == 0)
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487767U));
        else if (0 <= player.Gold - num1 && 0 <= player.Cash - num2 && 0 <= player.Tags - num3)
        {
          if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - num1, player.Cash - num2, player.Tags - num3))
          {
            player.Gold -= num1;
            player.Cash -= num2;
            player.Tags -= num3;
            foreach (GoodsItem goodsItem in goods)
            {
              if (ComDiv.GetIdStatics(goodsItem.Item.Id, 1) == 36)
              {
                AllUtils.ProcessBattlepassPremiumBuy(player);
                player.UpdateSeasonpass = false;
                player.SendPacket((GameServerPacket) new PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK(0U, player));
              }
              else
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, player, goodsItem.Item));
            }
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(1U, goods, player));
          }
          else
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487769U));
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487768U));
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ()
  {
    ((PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ) this).list_0 = new List<CartGoods>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    this.long_0 = (long) this.ReadUD();
    int num = (int) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ItemsModel list_1 = player.Inventory.GetItem(this.long_0);
      if (list_1 != null)
      {
        this.int_0 = list_1.Id;
        this.long_1 = (long) list_1.Count;
        if (list_1.Category == ItemCategory.Coupon && player.Inventory.Items.Count >= 500)
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_JACKPOT_ACK(2147487785U, (ItemsModel) null, (Account) null));
          return;
        }
        if (this.int_0 == 1800049)
        {
          if (DaoManagerSQL.UpdatePlayerKD(player.PlayerId, 0, 0, player.Statistic.Season.HeadshotsCount, player.Statistic.Season.TotalKillsCount))
          {
            player.Statistic.Season.KillsCount = 0;
            player.Statistic.Season.DeathsCount = 0;
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(player.Statistic));
          }
          else
            this.uint_0 = 2147483648U /*0x80000000*/;
        }
        else if (this.int_0 == 1800048)
        {
          if (DaoManagerSQL.UpdatePlayerMatches(0, 0, 0, 0, player.Statistic.Season.TotalMatchesCount, player.PlayerId))
          {
            player.Statistic.Season.Matches = 0;
            player.Statistic.Season.MatchWins = 0;
            player.Statistic.Season.MatchLoses = 0;
            player.Statistic.Season.MatchDraws = 0;
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(player.Statistic));
          }
          else
            this.uint_0 = 2147483648U /*0x80000000*/;
        }
        else if (this.int_0 == 1800050)
        {
          if (ComDiv.UpdateDB("player_stat_seasons", "escapes_count", (object) 0, "owner_id", (object) player.PlayerId))
          {
            player.Statistic.Season.EscapesCount = 0;
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_USER_BASIC_INFO_ACK(player.Statistic));
          }
          else
            this.uint_0 = 2147483648U /*0x80000000*/;
        }
        else if (this.int_0 == 1800053)
        {
          if (DaoManagerSQL.UpdateClanBattles(player.ClanId, 0, 0, 0))
          {
            ClanModel clan = ClanManager.GetClan(player.ClanId);
            if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId)
            {
              clan.Matches = 0;
              clan.MatchWins = 0;
              clan.MatchLoses = 0;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_REPLACE_INTRO_ACK());
            }
            else
              this.uint_0 = 2147483648U /*0x80000000*/;
          }
          else
            this.uint_0 = 2147483648U /*0x80000000*/;
        }
        else if (this.int_0 == 1800055)
        {
          ClanModel clan = ClanManager.GetClan(player.ClanId);
          if (clan.Id > 0 && clan.OwnerId == this.Client.PlayerId)
          {
            if (clan.MaxPlayers + 50 <= 250 && ComDiv.UpdateDB("system_clan", "max_players", (object) (clan.MaxPlayers + 50), "id", (object) player.ClanId))
            {
              clan.MaxPlayers += 50;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_REQUEST_INFO_ACK(clan.MaxPlayers));
            }
            else
              this.uint_0 = 2147487830U;
          }
          else
            this.uint_0 = 2147487830U;
        }
        else if (this.int_0 == 1800056)
        {
          ClanModel clan = ClanManager.GetClan(player.ClanId);
          if (clan.Id > 0 && (double) clan.Points != 1000.0)
          {
            if (ComDiv.UpdateDB("system_clan", "points", (object) 1000f, "id", (object) player.ClanId))
            {
              clan.Points = 1000f;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_CS_REPLACE_COLOR_NAME_RESULT_ACK());
            }
            else
              this.uint_0 = 2147487830U;
          }
          else
            this.uint_0 = 2147487830U;
        }
        else if (this.int_0 > 1800113 && this.int_0 < 1800119)
        {
          int account_1 = this.int_0 == 1800114 ? 500 : (this.int_0 == 1800115 ? 1000 : (this.int_0 == 1800116 ? 5000 : (this.int_0 == 1800117 ? 10000 : 30000)));
          if (ComDiv.UpdateDB("accounts", "gold", (object) (player.Gold + account_1), "player_id", (object) player.PlayerId))
          {
            player.Gold += account_1;
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK(account_1, player.Gold, 0));
          }
          else
            this.uint_0 = 2147483648U /*0x80000000*/;
        }
        else if (this.int_0 == 1801145)
        {
          int num = 0;
          int uint_1 = new Random().Next(0, 9);
          switch (uint_1)
          {
            case 0:
              num = 1;
              break;
            case 1:
              num = 2;
              break;
            case 2:
              num = 3;
              break;
            case 3:
              num = 4;
              break;
            case 4:
              num = 5;
              break;
            case 5:
              num = 10;
              break;
            case 6:
              num = 15;
              break;
            case 7:
              num = 25;
              break;
            case 8:
              num = 30;
              break;
            case 9:
              num = 50;
              break;
          }
          if (num > 0)
          {
            if (DaoManagerSQL.UpdateAccountTags(player.PlayerId, player.Tags + num))
            {
              player.Tags += num;
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_RECV_WHISPER_ACK(0U, player));
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK(new ItemsModel(), this.int_0, uint_1));
            }
            else
              this.uint_0 = 2147483648U /*0x80000000*/;
          }
          else
            this.uint_0 = 2147483648U /*0x80000000*/;
        }
        else if (list_1.Category == ItemCategory.Coupon && RandomBoxXML.ContainsBox(this.int_0))
        {
          RandomBoxModel box = RandomBoxXML.GetBox(this.int_0);
          if (box != null)
          {
            List<RandomBoxItem> sortedList = box.GetSortedList(((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).method_0(1, 100));
            List<RandomBoxItem> rewardList = box.GetRewardList(sortedList, ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).method_0(0, sortedList.Count));
            if (rewardList.Count > 0)
            {
              int index = rewardList[0].Index;
              List<ItemsModel> itemsModelList = new List<ItemsModel>();
              foreach (RandomBoxItem randomBoxItem in rewardList)
              {
                GoodsItem good = ShopManager.GetGood(randomBoxItem.GoodsId);
                if (good != null)
                  itemsModelList.Add(good.Item);
                else if (DaoManagerSQL.UpdateAccountGold(player.PlayerId, player.Gold + index))
                {
                  player.Gold += index;
                  this.Client.SendPacket((GameServerPacket) new PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK(index, player.Gold, 0));
                }
                else
                {
                  this.uint_0 = 2147483648U /*0x80000000*/;
                  break;
                }
                if (randomBoxItem.Special)
                {
                  using (PROTOCOL_AUTH_SHOP_JACKPOT_ACK iasyncResult_0 = (PROTOCOL_AUTH_SHOP_JACKPOT_ACK) new PROTOCOL_AUTH_SHOP_RECV_GIFT_ACK(player.Nickname, this.int_0, index))
                    GameXender.Client.SendPacketToAllClients((GameServerPacket) iasyncResult_0);
                }
              }
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_GET_GIFTLIST_ACK(itemsModelList, this.int_0, index));
              if (itemsModelList.Count > 0)
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, player, itemsModelList));
            }
            else
              this.uint_0 = 2147483648U /*0x80000000*/;
          }
          else
            this.uint_0 = 2147483648U /*0x80000000*/;
        }
        else
        {
          int idStatics = ComDiv.GetIdStatics(list_1.Id, 1);
          if ((idStatics < 1 || idStatics > 8) && idStatics != 15 && idStatics != 27 && (idStatics < 30 || idStatics > 35))
          {
            switch (idStatics)
            {
              case 17:
                ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).method_1(player, list_1.Name);
                break;
              case 20:
                ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).method_2(player, list_1.Id);
                break;
              case 37:
                ((PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ) this).method_3(player, list_1.Id);
                break;
              default:
                this.uint_0 = 2147483648U /*0x80000000*/;
                break;
            }
          }
          else if (list_1.Equip == ItemEquipType.Durable)
          {
            list_1.Equip = ItemEquipType.Temporary;
            ItemsModel itemsModel = list_1;
            DateTime dateTime = DateTimeUtil.Now();
            dateTime = dateTime.AddSeconds((double) list_1.Count);
            int uint32 = (int) Convert.ToUInt32(dateTime.ToString("yyMMddHHmm"));
            itemsModel.Count = (uint) uint32;
            ComDiv.UpdateDB("player_items", "object_id", (object) this.long_0, "owner_id", (object) player.PlayerId, new string[2]
            {
              "count",
              "equip"
            }, new object[2]
            {
              (object) (long) list_1.Count,
              (object) (int) list_1.Equip
            });
            if (idStatics == 6)
            {
              CharacterModel character = player.Character.GetCharacter(list_1.Id);
              if (character != null)
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK(character));
            }
          }
          else
            this.uint_0 = 2147483648U /*0x80000000*/;
        }
      }
      else
        this.uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_JACKPOT_ACK(this.uint_0, list_1, player));
    }
    catch (OverflowException ex)
    {
      CLogger.Print($"Obj: {this.long_0} ItemId: {this.int_0} Count: {this.long_1} PlayerId: {this.Client.Player} Name: '{this.Client.Player.Nickname}' {ex.Message}", LoggerType.Error, (Exception) ex);
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
