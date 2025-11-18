using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200012E RID: 302
	public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ : GameClientPacket
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public override void Read()
		{
			this.long_0 = (long)((ulong)base.ReadUD());
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00015A18 File Offset: 0x00013C18
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					if (player.Inventory.Items.Count >= 500)
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487785U, null, null));
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(2147483648U, null, null));
					}
					else
					{
						MessageModel message = DaoManagerSQL.GetMessage(this.long_0, player.PlayerId);
						if (message != null && message.Type == NoteMessageType.Gift)
						{
							GoodsItem good = ShopManager.GetGood((int)message.SenderId);
							if (good != null)
							{
								this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(1U, good.Item, player));
								DaoManagerSQL.DeleteMessage(this.long_0, player.PlayerId);
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(2147483648U, null, null));
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ()
		{
		}

		// Token: 0x0400021F RID: 543
		private long long_0;
	}
}
