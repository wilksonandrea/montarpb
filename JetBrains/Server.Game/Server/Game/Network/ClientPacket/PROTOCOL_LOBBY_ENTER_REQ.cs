// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_LOBBY_ENTER_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_ENTER_REQ : GameClientPacket
{
  public virtual void Read()
  {
    ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).long_0 = (long) this.ReadD();
    ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0 = this.ReadB((int) this.ReadC());
  }

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      ItemsModel list_1 = player.Inventory.GetItem(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).long_0);
      if (list_1 != null && list_1.Id > 1700000)
      {
        int itemId = ComDiv.CreateItemId(16 /*0x10*/, 0, ComDiv.GetIdStatics(list_1.Id, 3));
        uint uint32 = Convert.ToUInt32(DateTimeUtil.Now().AddDays((double) ComDiv.GetIdStatics(list_1.Id, 2)).ToString("yyMMddHHmm"));
        switch (itemId)
        {
          case 1600005:
          case 1600052:
            ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0 = BitConverter.ToUInt32(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0, 0);
            break;
          case 1600010:
          case 1600047:
          case 1600051:
            ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).string_0 = Bitwise.HexArrayToString(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0, ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0.Length);
            break;
          default:
            if (((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0.Length != 0)
            {
              ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_0 = (uint) ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).byte_0[0];
              break;
            }
            break;
        }
        ((PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ) this).method_0(itemId, uint32, player);
      }
      else
        ((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1 = 2147483648U /*0x80000000*/;
      this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_JACKPOT_ACK(((PROTOCOL_INVENTORY_USE_ITEM_REQ) this).uint_1, list_1, player));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_INVENTORY_USE_ITEM_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }
}
