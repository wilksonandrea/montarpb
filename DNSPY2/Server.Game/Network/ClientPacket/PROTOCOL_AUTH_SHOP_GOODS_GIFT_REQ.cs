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

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200012B RID: 299
	public class PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ : GameClientPacket
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00015570 File Offset: 0x00013770
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
			this.string_0 = base.ReadU((int)(base.ReadC() * 2));
			this.string_1 = base.ReadU((int)(base.ReadC() * 2));
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000155F8 File Offset: 0x000137F8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					Account account = AccountManager.GetAccount(this.string_1, 1, 0);
					if (account != null && account.IsOnline && player.Nickname != this.string_1)
					{
						if (account.Inventory.Items.Count >= 500)
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
									if (DaoManagerSQL.GetMessagesCount(account.PlayerId) >= 100)
									{
										this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487929U));
									}
									else
									{
										MessageModel messageModel = this.method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
										if (messageModel != null)
										{
											account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
										}
										account.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account, goods), false);
										this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK(1U, goods, account));
										this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0U, player));
									}
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
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147487769U));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00015844 File Offset: 0x00013A44
		private MessageModel method_0(string string_2, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = string_2,
				SenderId = long_1,
				Text = this.string_0,
				State = NoteMessageState.Unreaded
			};
			if (!DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(2147483648U));
				return null;
			}
			return messageModel;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00004D5D File Offset: 0x00002F5D
		public PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ()
		{
		}

		// Token: 0x04000217 RID: 535
		private string string_0;

		// Token: 0x04000218 RID: 536
		private string string_1;

		// Token: 0x04000219 RID: 537
		private List<CartGoods> list_0 = new List<CartGoods>();
	}
}
