// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ : GameClientPacket
{
  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (player.Inventory.Items.Count >= 500)
      {
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_JACKPOT_ACK(2147487785U, (ItemsModel) null, (Account) null));
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(2147483648U /*0x80000000*/, (ItemsModel) null, (Account) null));
      }
      else
      {
        MessageModel message = DaoManagerSQL.GetMessage(((PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ) this).long_0, player.PlayerId);
        if (message != null && message.Type == NoteMessageType.Gift)
        {
          GoodsItem good = ShopManager.GetGood((int) message.SenderId);
          if (good == null)
            return;
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(1U, good.Item, player));
          DaoManagerSQL.DeleteMessage(((PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ) this).long_0, player.PlayerId);
        }
        else
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(2147483648U /*0x80000000*/, (ItemsModel) null, (Account) null));
      }
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
    ((PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ) this).long_0 = (long) this.ReadUD();
  }
}
