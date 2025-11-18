using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ : GameClientPacket
{
	private string string_0;

	private string string_1;

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
		string_0 = ReadU(ReadC() * 2);
		string_1 = ReadU(ReadC() * 2);
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
			Account account = AccountManager.GetAccount(string_1, 1, 0);
			if (account != null && account.IsOnline && player.Nickname != string_1)
			{
				if (account.Inventory.Items.Count >= 500)
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
						if (DaoManagerSQL.GetMessagesCount(account.PlayerId) >= 100)
						{
							Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487929u));
							return;
						}
						MessageModel messageModel = method_0(player.Nickname, account.PlayerId, Client.PlayerId);
						if (messageModel != null)
						{
							account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), OnlyInServer: false);
						}
						account.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account, goods), OnlyInServer: false);
						Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK(1u, goods, account));
						Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0u, player));
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
			else
			{
				Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487769u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private MessageModel method_0(string string_2, long long_0, long long_1)
	{
		MessageModel messageModel = new MessageModel(15.0)
		{
			SenderName = string_2,
			SenderId = long_1,
			Text = string_0,
			State = NoteMessageState.Unreaded
		};
		if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
		{
			Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147483648u));
			return null;
		}
		return messageModel;
	}
}
