// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.ClientPacket.PROTOCOL_BASE_SELECT_CHANNEL_REQ
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;

#nullable disable
namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_SELECT_CHANNEL_REQ : GameClientPacket
{
  private int int_0;

  public virtual void Run()
  {
    try
    {
      Account player = this.Client.Player;
      if (player == null)
        return;
      if (!player.LoadedShop)
      {
        player.LoadedShop = true;
        foreach (ShopData shopDataItem in ShopManager.ShopDataItems)
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_ACK(shopDataItem, ShopManager.TotalItems));
        foreach (ShopData shopDataGood in ShopManager.ShopDataGoods)
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(shopDataGood, ShopManager.TotalGoods));
        foreach (ShopData shopDataItemRepair in ShopManager.ShopDataItemRepairs)
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK(shopDataItemRepair, ShopManager.TotalRepairs));
        foreach (ShopData shopDataBattleBox in BattleBoxXML.ShopDataBattleBoxes)
          this.Client.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_COUNT_DOWN_ACK(shopDataBattleBox, BattleBoxXML.TotalBoxes));
        if (player.CafePC == CafeEnum.None)
        {
          foreach (ShopData shopData in ShopManager.ShopDataMt1)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(shopData, ShopManager.TotalMatching1));
        }
        else
        {
          foreach (ShopData shopData in ShopManager.ShopDataMt2)
            this.Client.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(shopData, ShopManager.TotalMatching2));
        }
      }
      if (Bitwise.ReadFile(Environment.CurrentDirectory + "/Data/Raws/RandomBox.dat") == ((PROTOCOL_BASE_RANDOMBOX_LIST_REQ) this).string_0)
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK(false));
      else
        this.Client.SendPacket((GameServerPacket) new PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK(true));
    }
    catch (Exception ex)
    {
      CLogger.Print("PROTOCOL_BASE_RANDOMBOX_LIST_REQ: " + ex.Message, LoggerType.Error, ex);
    }
  }

  public virtual void Read()
  {
  }
}
