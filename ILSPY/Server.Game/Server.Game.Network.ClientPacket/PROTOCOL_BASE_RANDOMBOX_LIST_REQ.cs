using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_RANDOMBOX_LIST_REQ : GameClientPacket
{
	private string string_0;

	public override void Read()
	{
		string_0 = ReadS(32);
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			if (!player.LoadedShop)
			{
				player.LoadedShop = true;
				foreach (ShopData shopDataItem in ShopManager.ShopDataItems)
				{
					Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(shopDataItem, ShopManager.TotalItems));
				}
				foreach (ShopData shopDataGood in ShopManager.ShopDataGoods)
				{
					Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(shopDataGood, ShopManager.TotalGoods));
				}
				foreach (ShopData shopDataItemRepair in ShopManager.ShopDataItemRepairs)
				{
					Client.SendPacket(new PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(shopDataItemRepair, ShopManager.TotalRepairs));
				}
				foreach (ShopData shopDataBattleBox in BattleBoxXML.ShopDataBattleBoxes)
				{
					Client.SendPacket(new PROTOCOL_BATTLEBOX_GET_LIST_ACK(shopDataBattleBox, BattleBoxXML.TotalBoxes));
				}
				if (player.CafePC == CafeEnum.None)
				{
					foreach (ShopData item in ShopManager.ShopDataMt1)
					{
						Client.SendPacket(new PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(item, ShopManager.TotalMatching1));
					}
				}
				else
				{
					foreach (ShopData item2 in ShopManager.ShopDataMt2)
					{
						Client.SendPacket(new PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(item2, ShopManager.TotalMatching2));
					}
				}
			}
			if (Bitwise.ReadFile(Environment.CurrentDirectory + "/Data/Raws/RandomBox.dat") == string_0)
			{
				Client.SendPacket(new PROTOCOL_BASE_RANDOMBOX_LIST_ACK(bool_1: false));
			}
			else
			{
				Client.SendPacket(new PROTOCOL_BASE_RANDOMBOX_LIST_ACK(bool_1: true));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_RANDOMBOX_LIST_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
