// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ
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
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ : GameClientPacket
{
  private string string_0;

  public virtual void Read()
  {
    ((PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ) this).string_0 = this.ReadS((int) this.ReadC());
    ((PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ) this).ticketType_0 = (TicketType) this.ReadC();
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      TicketModel ticket = RedeemCodeXML.GetTicket(((PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ) this).string_0, ((PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ) this).ticketType_0);
      if (ticket != null)
      {
        if ((long) ComDiv.CountDB($"SELECT COUNT(used_count) FROM base_redeem_history WHERE used_token = '{ticket.Token}'") >= (long) ticket.TicketCount)
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_ATTENDANCE_ACK(2147483648U /*0x80000000*/));
          return;
        }
        int usedTicket = DaoManagerSQL.GetUsedTicket(player.PlayerId, ticket.Token);
        if ((long) usedTicket < (long) ticket.PlayerRation)
        {
          int num = usedTicket + 1;
          if (ticket.Type == TicketType.COUPON)
          {
            List<GoodsItem> goods = this.GetGoods(ticket);
            if (goods.Count > 0 && ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).method_0(player, ticket, num))
            {
              foreach (GoodsItem goodsItem in goods)
              {
                if (ComDiv.GetIdStatics(goodsItem.Item.Id, 1) == 6 && player.Character.GetCharacter(goodsItem.Item.Id) == null)
                  AllUtils.CreateCharacter(player, goodsItem.Item);
                else
                  player.SendPacket((GameServerPacket) new PROTOCOL_LOBBY_CHATTING_ACK(0, player, goodsItem.Item));
              }
            }
          }
          else if (ticket.Type == TicketType.VOUCHER && (ticket.GoldReward != 0 || ticket.CashReward != 0 || ticket.TagsReward != 0) && ((PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ) this).method_0(player, ticket, num))
          {
            if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold + ticket.GoldReward, player.Cash + ticket.CashReward, player.Tags + ticket.TagsReward))
            {
              player.Gold += ticket.GoldReward;
              player.Cash += ticket.CashReward;
              player.Tags += ticket.TagsReward;
            }
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_RECV_WHISPER_ACK(0U, player));
          }
        }
        else
          ((PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      }
      else
        ((PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ) this).uint_0 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_ATTENDANCE_ACK(((PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ) this).uint_0));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public List<GoodsItem> GetGoods([In] TicketModel obj0)
  {
    List<GoodsItem> goods = new List<GoodsItem>();
    if (obj0.Rewards.Count == 0)
      return goods;
    foreach (int reward in obj0.Rewards)
    {
      GoodsItem good = ShopManager.GetGood(reward);
      if (good != null)
        goods.Add(good);
    }
    return goods;
  }
}
