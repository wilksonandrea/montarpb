using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ : GameClientPacket
	{
		private long long_0;

		public PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ()
		{
		}

		public override void Read()
		{
			this.long_0 = (long)base.ReadUD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Inventory.Items.Count < 500)
					{
						MessageModel message = DaoManagerSQL.GetMessage(this.long_0, player.PlayerId);
						if (message == null || message.Type != NoteMessageType.Gift)
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(-2147483648, null, null));
						}
						else
						{
							GoodsItem good = ShopManager.GetGood((int)message.SenderId);
							if (good != null)
							{
								this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(1, good.Item, player));
								DaoManagerSQL.DeleteMessage(this.long_0, player.PlayerId);
							}
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(-2147479511, null, null));
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(-2147483648, null, null));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}