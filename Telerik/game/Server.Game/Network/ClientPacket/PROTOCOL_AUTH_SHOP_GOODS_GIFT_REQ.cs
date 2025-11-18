using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ : GameClientPacket
	{
		private string string_0;

		private string string_1;

		private List<CartGoods> list_0 = new List<CartGoods>();

		public PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ()
		{
		}

		private MessageModel method_0(string string_2, long long_0, long long_1)
		{
			MessageModel messageModel = new MessageModel(15)
			{
				SenderName = string_2,
				SenderId = long_1,
				Text = this.string_0,
				State = NoteMessageState.Unreaded
			};
			if (DaoManagerSQL.CreateMessage(long_0, messageModel))
			{
				return messageModel;
			}
			this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(-2147483648));
			return null;
		}

		public override void Read()
		{
			byte num = base.ReadC();
			for (byte i = 0; i < num; i = (byte)(i + 1))
			{
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
			this.string_0 = base.ReadU(base.ReadC() * 2);
			this.string_1 = base.ReadU(base.ReadC() * 2);
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
					Account account = AccountManager.GetAccount(this.string_1, 1, 0);
					if (account == null || !account.IsOnline || !(player.Nickname != this.string_1))
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(-2147479527));
					}
					else if (account.Inventory.Items.Count < 500)
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
							if (DaoManagerSQL.GetMessagesCount(account.PlayerId) < 100)
							{
								MessageModel messageModel = this.method_0(player.Nickname, account.PlayerId, this.Client.PlayerId);
								if (messageModel != null)
								{
									account.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel), false);
								}
								account.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account, goods), false);
								this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK(1, goods, account));
								this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, player));
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(-2147479367));
								return;
							}
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(-2147479367));
						return;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}