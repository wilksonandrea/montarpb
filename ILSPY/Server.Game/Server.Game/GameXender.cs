using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Sync;
using Server.Game.Network.ServerPacket;

namespace Server.Game;

public class GameXender
{
	[CompilerGenerated]
	private static GameSync gameSync_0;

	[CompilerGenerated]
	private static GameManager gameManager_0;

	[CompilerGenerated]
	private static ConcurrentDictionary<int, GameClient> concurrentDictionary_0;

	[CompilerGenerated]
	private static ConcurrentDictionary<string, int> concurrentDictionary_1;

	public static GameSync Sync
	{
		[CompilerGenerated]
		get
		{
			return gameSync_0;
		}
		[CompilerGenerated]
		set
		{
			gameSync_0 = value;
		}
	}

	public static GameManager Client
	{
		[CompilerGenerated]
		get
		{
			return gameManager_0;
		}
		[CompilerGenerated]
		set
		{
			gameManager_0 = value;
		}
	}

	public static ConcurrentDictionary<int, GameClient> SocketSessions
	{
		[CompilerGenerated]
		get
		{
			return concurrentDictionary_0;
		}
		[CompilerGenerated]
		set
		{
			concurrentDictionary_0 = value;
		}
	}

	public static ConcurrentDictionary<string, int> SocketConnections
	{
		[CompilerGenerated]
		get
		{
			return concurrentDictionary_1;
		}
		[CompilerGenerated]
		set
		{
			concurrentDictionary_1 = value;
		}
	}

	public static bool GetPlugin(int ServerId, string Host, int Port)
	{
		try
		{
			SocketSessions = new ConcurrentDictionary<int, GameClient>();
			SocketConnections = new ConcurrentDictionary<string, int>();
			Sync = new GameSync(SynchronizeXML.GetServer(Port).Connection);
			Client = new GameManager(ServerId, Host, ConfigLoader.DEFAULT_PORT[1]++);
			Sync.Start();
			Client.Start();
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
		foreach (GameClient value in SocketSessions.Values)
		{
			value?.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(bool_1: true));
		}
	}

	public static void UpdateShop()
	{
		foreach (GameClient value in SocketSessions.Values)
		{
			if (value == null)
			{
				continue;
			}
			foreach (ShopData shopDataItem in ShopManager.ShopDataItems)
			{
				value.SendPacket(new PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(shopDataItem, ShopManager.TotalItems));
			}
			foreach (ShopData shopDataGood in ShopManager.ShopDataGoods)
			{
				value.SendPacket(new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(shopDataGood, ShopManager.TotalGoods));
			}
			foreach (ShopData shopDataItemRepair in ShopManager.ShopDataItemRepairs)
			{
				value.SendPacket(new PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(shopDataItemRepair, ShopManager.TotalRepairs));
			}
			foreach (ShopData shopDataBattleBox in BattleBoxXML.ShopDataBattleBoxes)
			{
				value.SendPacket(new PROTOCOL_BATTLEBOX_GET_LIST_ACK(shopDataBattleBox, BattleBoxXML.TotalBoxes));
			}
			value.SendPacket(new PROTOCOL_SHOP_TAG_INFO_ACK());
			value.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(bool_1: true));
		}
	}
}
