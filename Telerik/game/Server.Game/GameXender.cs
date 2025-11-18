using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Sync;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Server.Game
{
	public class GameXender
	{
		public static GameManager Client
		{
			get;
			set;
		}

		public static ConcurrentDictionary<string, int> SocketConnections
		{
			get;
			set;
		}

		public static ConcurrentDictionary<int, GameClient> SocketSessions
		{
			get;
			set;
		}

		public static GameSync Sync
		{
			get;
			set;
		}

		public GameXender()
		{
		}

		public static bool GetPlugin(int ServerId, string Host, int Port)
		{
			bool flag;
			try
			{
				GameXender.SocketSessions = new ConcurrentDictionary<int, GameClient>();
				GameXender.SocketConnections = new ConcurrentDictionary<string, int>();
				GameXender.Sync = new GameSync(SynchronizeXML.GetServer(Port).Connection);
				ref int dEFAULTPORT = ref ConfigLoader.DEFAULT_PORT[1];
				int ınt32 = dEFAULTPORT;
				dEFAULTPORT = ınt32 + 1;
				GameXender.Client = new GameManager(ServerId, Host, ınt32);
				GameXender.Sync.Start();
				GameXender.Client.Start();
				flag = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
				flag = false;
			}
			return flag;
		}

		public static void UpdateEvents()
		{
			foreach (GameClient value in GameXender.SocketSessions.Values)
			{
				if (value == null)
				{
					continue;
				}
				value.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(true));
			}
		}

		public static void UpdateShop()
		{
			foreach (GameClient value in GameXender.SocketSessions.Values)
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
				value.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(true));
			}
		}
	}
}