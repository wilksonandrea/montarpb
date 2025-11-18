// Decompiled with JetBrains decompiler
// Type: Server.Game.Network.GameClientPacket
// Assembly: Server.Game, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 0D150AF8-D042-4337-9729-D068C51BAF9E
// Assembly location: C:\Users\Administrator\Desktop\unpack\Server.Game.dll

using Microsoft.Win32.SafeHandles;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Sync;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Server.Game.Network;

public abstract class GameClientPacket : BaseClientPacket, IDisposable
{
  protected GameClient Client;

  [CompilerGenerated]
  [SpecialName]
  public static void set_SocketConnections(ConcurrentDictionary<string, int> Hours)
  {
    // ISSUE: reference to a compiler-generated field
    GameXender.concurrentDictionary_1 = Hours;
  }

  public static bool GetPlugin(int object_0, [In] string obj1, [In] int obj2)
  {
    try
    {
      GameXender.SocketSessions = new ConcurrentDictionary<int, GameClient>();
      GameClientPacket.set_SocketConnections(new ConcurrentDictionary<string, int>());
      GameXender.Sync = new GameSync(SynchronizeXML.GetServer(obj2).Connection);
      GameXender.Client = new GameManager(object_0, obj1, ConfigLoader.DEFAULT_PORT[1]++);
      GameXender.Sync.Start();
      GameXender.Client.Start();
      return true;
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return false;
    }
  }

  public static void UpdateEvents()
  {
    foreach (GameClient gameClient in (IEnumerable<GameClient>) GameXender.SocketSessions.Values)
      gameClient?.SendPacket((GameServerPacket) new PROTOCOL_BASE_GET_UID_LOBBY_ACK(true));
  }

  public static void UpdateShop()
  {
    foreach (GameClient gameClient in (IEnumerable<GameClient>) GameXender.SocketSessions.Values)
    {
      if (gameClient != null)
      {
        foreach (ShopData shopDataItem in ShopManager.ShopDataItems)
          gameClient.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_ACK(shopDataItem, ShopManager.TotalItems));
        foreach (ShopData shopDataGood in ShopManager.ShopDataGoods)
          gameClient.SendPacket((GameServerPacket) new PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(shopDataGood, ShopManager.TotalGoods));
        foreach (ShopData shopDataItemRepair in ShopManager.ShopDataItemRepairs)
          gameClient.SendPacket((GameServerPacket) new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_ACK(shopDataItemRepair, ShopManager.TotalRepairs));
        foreach (ShopData shopDataBattleBox in BattleBoxXML.ShopDataBattleBoxes)
          gameClient.SendPacket((GameServerPacket) new PROTOCOL_BATTLE_COUNT_DOWN_ACK(shopDataBattleBox, BattleBoxXML.TotalBoxes));
        gameClient.SendPacket((GameServerPacket) new PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK());
        gameClient.SendPacket((GameServerPacket) new PROTOCOL_SHOP_FLASH_SALE_LIST_ACK(true));
      }
    }
  }

  public GameClientPacket()
  {
  }

  public GameClientPacket()
  {
    this.Handle = (SafeHandle) new SafeFileHandle(IntPtr.Zero, true);
    this.Disposed = false;
    this.SECURITY_KEY = Bitwise.CRYPTO[0];
    this.HASH_CODE = Bitwise.CRYPTO[1];
    this.SEED_LENGTH = Bitwise.CRYPTO[2];
    this.NATIONS = ConfigLoader.National;
  }
}
