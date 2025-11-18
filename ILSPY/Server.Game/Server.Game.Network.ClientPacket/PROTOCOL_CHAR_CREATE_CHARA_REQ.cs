using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CHAR_CREATE_CHARA_REQ : GameClientPacket
{
	private string string_0;

	private List<CartGoods> list_0 = new List<CartGoods>();

	public override void Read()
	{
		ReadC();
		string_0 = ReadU(ReadC() * 2);
		ReadC();
		CartGoods item = new CartGoods
		{
			GoodId = ReadD(),
			BuyType = ReadC()
		};
		list_0.Add(item);
		ReadC();
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
			if (player.Inventory.Items.Count < 500 && player.Character.Characters.Count < 64)
			{
				int GoldPrice;
				int CashPrice;
				int TagsPrice;
				List<GoodsItem> goods = ShopManager.GetGoods(list_0, out GoldPrice, out CashPrice, out TagsPrice);
				if (goods.Count == 0)
				{
					Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147487767u, byte.MaxValue, null, null));
				}
				else if (0 <= player.Gold - GoldPrice && 0 <= player.Cash - CashPrice && 0 <= player.Tags - TagsPrice)
				{
					if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - GoldPrice, player.Cash - CashPrice, player.Tags - TagsPrice))
					{
						player.Gold -= GoldPrice;
						player.Cash -= CashPrice;
						player.Tags -= TagsPrice;
						CharacterModel characterModel = method_0(player, goods);
						if (characterModel != null)
						{
							player.Character.AddCharacter(characterModel);
							if (player.Character.GetCharacter(characterModel.Id) != null)
							{
								DaoManagerSQL.CreatePlayerCharacter(characterModel, player.PlayerId);
							}
						}
						Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, goods));
						Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(0u, 1, characterModel, player));
					}
					else
					{
						Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147487769u, byte.MaxValue, null, null));
					}
				}
				else
				{
					Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147487768u, byte.MaxValue, null, null));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_CHAR_CREATE_CHARA_ACK(2147487929u, byte.MaxValue, null, null));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	private CharacterModel method_0(Account account_0, List<GoodsItem> list_1)
	{
		foreach (GoodsItem item in list_1)
		{
			if (item != null && item.Item.Id != 0)
			{
				return new CharacterModel
				{
					Id = item.Item.Id,
					Slot = account_0.Character.GenSlotId(item.Item.Id),
					Name = string_0,
					CreateDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm")),
					PlayTime = 0u
				};
			}
		}
		return null;
	}
}
