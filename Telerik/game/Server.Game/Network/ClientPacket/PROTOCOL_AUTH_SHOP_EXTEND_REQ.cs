using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_SHOP_EXTEND_REQ : GameClientPacket
	{
		private List<CartGoods> list_0 = new List<CartGoods>();

		public PROTOCOL_AUTH_SHOP_EXTEND_REQ()
		{
		}

		public override void Read()
		{
			base.ReadD();
			base.ReadC();
			CartGoods cartGood = new CartGoods()
			{
				GoodId = base.ReadD(),
				BuyType = base.ReadC()
			};
			this.list_0.Add(cartGood);
			base.ReadC();
			base.ReadQ();
		}

		public override void Run()
		{
			int ınt32;
			int ınt321;
			int ınt322;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Inventory.Items.Count < 500)
					{
						List<GoodsItem> goods = ShopManager.GetGoods(this.list_0, out ınt32, out ınt321, out ınt322);
						if (goods.Count == 0)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(-2147479529));
						}
						else if (0 > player.Gold - ınt32 || 0 > player.Cash - ınt321 || 0 > player.Tags - ınt322)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(-2147479528));
						}
						else if (!DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - ınt32, player.Cash - ınt321, player.Tags - ınt322))
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(-2147479527));
						}
						else
						{
							player.Gold -= ınt32;
							player.Cash -= ınt321;
							player.Tags -= ınt322;
							foreach (GoodsItem good in goods)
							{
								if (ComDiv.GetIdStatics(good.Item.Id, 1) != 36)
								{
									this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, good.Item));
								}
								else
								{
									AllUtils.ProcessBattlepassPremiumBuy(player);
									player.UpdateSeasonpass = false;
									player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(0, player));
								}
							}
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(1, goods, player));
							this.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("STR_POPUP_EXTEND_SUCCESS")));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(-2147479367));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat(base.GetType().Name, "; ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}