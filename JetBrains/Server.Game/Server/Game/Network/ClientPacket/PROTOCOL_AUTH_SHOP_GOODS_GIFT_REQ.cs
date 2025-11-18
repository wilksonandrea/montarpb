// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ : GameClientPacket
{
  private string string_0;
  private string string_1;
  private List<CartGoods> list_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null || player.Nickname == ((PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ) this).string_0)
        return;
      Account account = ClanManager.GetAccount(((PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ) this).string_0, 1, 286);
      if (account != null && account.IsOnline)
      {
        account.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(player.Nickname, ((PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ) this).string_1, player.UseChatGM()), true);
      }
      else
      {
        Console.WriteLine("null");
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_CAPSULE_ACK(((PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ) this).string_0, ((PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ) this).string_1, 2147483648U /*0x80000000*/));
      }
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    this.ReadD();
    int num1 = (int) this.ReadC();
    ((PROTOCOL_AUTH_SHOP_EXTEND_REQ) this).list_0.Add(new CartGoods()
    {
      GoodId = this.ReadD(),
      BuyType = (int) this.ReadC()
    });
    int num2 = (int) this.ReadC();
    this.ReadQ();
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
        List<GoodsItem> goods = ShopManager.GetGoods(((PROTOCOL_AUTH_SHOP_EXTEND_REQ) this).list_0, ref num1, ref num2, ref num3);
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
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_SERVER_MESSAGE_DISCONNECTED_HACK(Translation.GetLabel("STR_POPUP_EXTEND_SUCCESS")));
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
      CLogger.Print($"{this.GetType().Name}; {ex.Message}", LoggerType.Error, ex);
    }
  }
}
