using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ : GameClientPacket
{
	private List<CartGoods> list_0 = new List<CartGoods>();

	public override void Read()
	{
		byte b = ReadC();
		for (byte b2 = 0; b2 < b; b2 = (byte)(b2 + 1))
		{
			ReadC();
			CartGoods item = new CartGoods
			{
				GoodId = ReadD(),
				BuyType = ReadC()
			};
			list_0.Add(item);
			ReadC();
			ReadQ();
		}
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
			if (player.Inventory.Items.Count >= 500)
			{
				Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487929u));
				return;
			}
			int GoldPrice;
			int CashPrice;
			int TagsPrice;
			List<GoodsItem> goods = ShopManager.GetGoods(list_0, out GoldPrice, out CashPrice, out TagsPrice);
			if (goods.Count == 0)
			{
				Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487767u));
			}
			else if (0 <= player.Gold - GoldPrice && 0 <= player.Cash - CashPrice && 0 <= player.Tags - TagsPrice)
			{
				if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - GoldPrice, player.Cash - CashPrice, player.Tags - TagsPrice))
				{
					player.Gold -= GoldPrice;
					player.Cash -= CashPrice;
					player.Tags -= TagsPrice;
					foreach (GoodsItem item in goods)
					{
						if (ComDiv.GetIdStatics(item.Item.Id, 1) == 36)
						{
							AllUtils.ProcessBattlepassPremiumBuy(player);
							player.UpdateSeasonpass = false;
							player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(0u, player));
						}
						else
						{
							Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, item.Item));
						}
					}
					Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(1u, goods, player));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487769u));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487768u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
