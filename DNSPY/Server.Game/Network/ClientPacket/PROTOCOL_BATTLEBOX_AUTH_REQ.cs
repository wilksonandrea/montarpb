using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000161 RID: 353
	public class PROTOCOL_BATTLEBOX_AUTH_REQ : GameClientPacket
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000513F File Offset: 0x0000333F
		public override void Read()
		{
			this.long_0 = (long)((ulong)base.ReadUD());
			this.int_0 = base.ReadD();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001B228 File Offset: 0x00019428
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ItemsModel item = player.Inventory.GetItem(this.long_0);
					if (item != null)
					{
						BattleBoxModel battleBox = BattleBoxXML.GetBattleBox(item.Id);
						if (battleBox != null && battleBox.RequireTags == this.int_0)
						{
							if (this.int_0 > player.Tags)
							{
								this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648U, null, 0));
							}
							else if (!DaoManagerSQL.UpdateAccountTags(player.PlayerId, player.Tags - this.int_0))
							{
								this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648U, null, 0));
							}
							else
							{
								GoodsItem good = ShopManager.GetGood(battleBox.GetItemWithProbabilities<int>(battleBox.Goods, battleBox.Probabilities));
								if (good == null)
								{
									this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648U, null, 0));
								}
								else
								{
									player.Tags -= this.int_0;
									if (ComDiv.UpdateDB("accounts", "tags", player.Tags, "player_id", player.PlayerId))
									{
										this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, good.Item));
									}
									item.Count -= 1U;
									if (item.Count <= 0U)
									{
										if (DaoManagerSQL.DeletePlayerInventoryItem(item.ObjectId, player.PlayerId))
										{
											player.Inventory.RemoveItem(item);
										}
										this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(1U, this.long_0));
									}
									else
									{
										ComDiv.UpdateDB("player_items", "count", (long)((ulong)item.Count), "owner_id", player.PlayerId, "id", item.Id);
										this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, player, item));
									}
									this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(0U, player, good.Item.Id));
								}
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648U, null, 0));
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648U, null, 0));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.ToString(), LoggerType.Error, null);
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001B48C File Offset: 0x0001968C
		public void SendGiftMessage(Account Player, ItemsModel Item)
		{
			string label = Translation.GetLabel("BattleBoxGift");
			MessageModel messageModel = this.CreateMessage("GM", Player.PlayerId, label + "\n\n" + Item.Name);
			if (messageModel != null)
			{
				Player.Connection.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel));
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001B4DC File Offset: 0x000196DC
		public MessageModel CreateMessage(string SenderName, long OwnerId, string Text)
		{
			MessageModel messageModel = new MessageModel(15.0)
			{
				SenderName = SenderName,
				Text = Text,
				Type = NoteMessageType.Gift,
				State = NoteMessageState.Unreaded
			};
			if (!DaoManagerSQL.CreateMessage(OwnerId, messageModel))
			{
				return null;
			}
			return messageModel;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLEBOX_AUTH_REQ()
		{
		}

		// Token: 0x04000284 RID: 644
		private long long_0;

		// Token: 0x04000285 RID: 645
		private int int_0;
	}
}
