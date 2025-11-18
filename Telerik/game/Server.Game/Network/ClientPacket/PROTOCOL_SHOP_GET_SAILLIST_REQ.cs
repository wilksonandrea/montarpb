using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_SHOP_GET_SAILLIST_REQ : GameClientPacket
	{
		private string string_0;

		public PROTOCOL_SHOP_GET_SAILLIST_REQ()
		{
		}

		public override void Read()
		{
			this.string_0 = base.ReadS(32);
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (!player.LoadedShop)
					{
						player.LoadedShop = true;
						foreach (ShopData shopDataItem in ShopManager.ShopDataItems)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(shopDataItem, ShopManager.TotalItems));
						}
						foreach (ShopData shopDataGood in ShopManager.ShopDataGoods)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(shopDataGood, ShopManager.TotalGoods));
						}
						foreach (ShopData shopDataItemRepair in ShopManager.ShopDataItemRepairs)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(shopDataItemRepair, ShopManager.TotalRepairs));
						}
						foreach (ShopData shopDataBattleBox in BattleBoxXML.ShopDataBattleBoxes)
						{
							this.Client.SendPacket(new PROTOCOL_BATTLEBOX_GET_LIST_ACK(shopDataBattleBox, BattleBoxXML.TotalBoxes));
						}
						if (player.CafePC != CafeEnum.None)
						{
							foreach (ShopData shopDataMt2 in ShopManager.ShopDataMt2)
							{
								this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(shopDataMt2, ShopManager.TotalMatching2));
							}
						}
						else
						{
							foreach (ShopData shopDataMt1 in ShopManager.ShopDataMt1)
							{
								this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(shopDataMt1, ShopManager.TotalMatching1));
							}
						}
					}
					this.Client.SendPacket(new PROTOCOL_SHOP_TAG_INFO_ACK());
					if (Bitwise.ReadFile(string.Concat(Environment.CurrentDirectory, "/Data/Raws/Shop.dat")) != this.string_0)
					{
						this.Client.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(true));
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(false));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_SHOP_GET_SAILLIST_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}