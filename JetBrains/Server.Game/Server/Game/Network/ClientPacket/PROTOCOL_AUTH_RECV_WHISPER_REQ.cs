// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_RECV_WHISPER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_RECV_WHISPER_REQ : GameClientPacket
{
  private string string_0;
  private string string_1;

  public PROTOCOL_AUTH_RECV_WHISPER_REQ()
  {
    ((PROTOCOL_AUTH_SHOP_EXTEND_REQ) this).list_0 = new List<CartGoods>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public virtual void Read()
  {
    byte num1 = this.ReadC();
    for (byte index = 0; (int) index < (int) num1; ++index)
    {
      int num2 = (int) this.ReadC();
      ((PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ) this).list_0.Add(new CartGoods()
      {
        GoodId = this.ReadD(),
        BuyType = (int) this.ReadC()
      });
      int num3 = (int) this.ReadC();
      this.ReadQ();
    }
    ((PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ) this).string_0 = this.ReadU((int) this.ReadC() * 2);
    ((PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ) this).string_1 = this.ReadU((int) this.ReadC() * 2);
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ) this).string_1, 1, 0);
      if (account != null && account.IsOnline && player.Nickname != ((PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ) this).string_1)
      {
        if (account.Inventory.Items.Count >= 500)
        {
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487929U));
        }
        else
        {
          int num1;
          int num2;
          int num3;
          List<GoodsItem> goods = ShopManager.GetGoods(((PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ) this).list_0, ref num1, ref num2, ref num3);
          if (goods.Count == 0)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487767U));
          else if (0 <= player.Gold - num1 && 0 <= player.Cash - num2 && 0 <= player.Tags - num3)
          {
            if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - num1, player.Cash - num2, player.Tags - num3))
            {
              player.Gold -= num1;
              player.Cash -= num2;
              player.Tags -= num3;
              if (DaoManagerSQL.GetMessagesCount(account.PlayerId) >= 100)
              {
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487929U));
              }
              else
              {
                MessageModel bool_1 = ((PROTOCOL_AUTH_SEND_WHISPER_REQ) this).method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
                if (bool_1 != null)
                  account.SendPacket((GameServerPacket) new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(bool_1), false);
                account.SendPacket((GameServerPacket) new PROTOCOL_INVENTORY_LEAVE_ACK(0, account, goods), false);
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SEND_WHISPER_ACK(1U, goods, account));
                this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_RECV_WHISPER_ACK(0U, player));
              }
            }
            else
              this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487769U));
          }
          else
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487768U));
        }
      }
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487769U));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
