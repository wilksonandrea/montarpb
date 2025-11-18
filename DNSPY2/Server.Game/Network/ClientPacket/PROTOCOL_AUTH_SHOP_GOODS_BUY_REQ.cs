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

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000131 RID: 305
	public class PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ : GameClientPacket
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00016108 File Offset: 0x00014308
		public override void Read()
		{
			byte b = base.ReadC();
			for (byte b2 = 0; b2 < b; b2 += 1)
			{
				base.ReadC();
				CartGoods cartGoods = new CartGoods
				{
					GoodId = base.ReadD(),
					BuyType = (int)base.ReadC()
				};
				this.list_0.Add(cartGoods);
				base.ReadC();
				base.ReadQ();
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00016168 File Offset: 0x00014368
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Inventory.Items.Count >= 500)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487929U));
					}
					else
					{
						int num;
						int num2;
						int num3;
						List<GoodsItem> goods = ShopManager.GetGoods(this.list_0, out num, out num2, out num3);
						if (goods.Count == 0)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487767U));
						}
						else if (0 <= player.Gold - num && 0 <= player.Cash - num2 && 0 <= player.Tags - num3)
						{
							if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - num, player.Cash - num2, player.Tags - num3))
							{
								player.Gold -= num;
								player.Cash -= num2;
								player.Tags -= num3;
								foreach (GoodsItem goodsItem in goods)
								{
									if (ComDiv.GetIdStatics(goodsItem.Item.Id, 1) == 36)
									{
										AllUtils.ProcessBattlepassPremiumBuy(player);
										player.UpdateSeasonpass = false;
										player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(0U, player));
									}
									else
									{
										this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, goodsItem.Item));
									}
								}
								this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(1U, goods, player));
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487769U));
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487768U));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00004DF1 File Offset: 0x00002FF1
		public PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ()
		{
		}

		// Token: 0x04000222 RID: 546
		private List<CartGoods> list_0 = new List<CartGoods>();
	}
}
