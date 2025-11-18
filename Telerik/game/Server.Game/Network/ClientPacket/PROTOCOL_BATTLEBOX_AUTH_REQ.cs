using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLEBOX_AUTH_REQ : GameClientPacket
	{
		private long long_0;

		private int int_0;

		public PROTOCOL_BATTLEBOX_AUTH_REQ()
		{
		}

		public MessageModel CreateMessage(string SenderName, long OwnerId, string Text)
		{
			MessageModel messageModel = new MessageModel(15)
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

		public override void Read()
		{
			this.long_0 = (long)base.ReadUD();
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					ItemsModel ıtem = player.Inventory.GetItem(this.long_0);
					if (ıtem == null)
					{
						this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(-2147483648, null, 0));
					}
					else
					{
						BattleBoxModel battleBox = BattleBoxXML.GetBattleBox(ıtem.Id);
						if (battleBox == null || battleBox.RequireTags != this.int_0)
						{
							this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(-2147483648, null, 0));
						}
						else if (this.int_0 > player.Tags)
						{
							this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(-2147483648, null, 0));
						}
						else if (DaoManagerSQL.UpdateAccountTags(player.PlayerId, player.Tags - this.int_0))
						{
							GoodsItem good = ShopManager.GetGood(battleBox.GetItemWithProbabilities<int>(battleBox.Goods, battleBox.Probabilities));
							if (good != null)
							{
								player.Tags -= this.int_0;
								if (ComDiv.UpdateDB("accounts", "tags", player.Tags, "player_id", player.PlayerId))
								{
									this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, good.Item));
								}
								ItemsModel count = ıtem;
								count.Count = count.Count - 1;
								if (ıtem.Count > 0)
								{
									ComDiv.UpdateDB("player_items", "count", (long)((ulong)ıtem.Count), "owner_id", player.PlayerId, "id", ıtem.Id);
									this.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, player, ıtem));
								}
								else
								{
									if (DaoManagerSQL.DeletePlayerInventoryItem(ıtem.ObjectId, player.PlayerId))
									{
										player.Inventory.RemoveItem(ıtem);
									}
									this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(1, this.long_0));
								}
								this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(0, player, good.Item.Id));
							}
							else
							{
								this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(-2147483648, null, 0));
								return;
							}
						}
						else
						{
							this.Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(-2147483648, null, 0));
						}
					}
				}
			}
			catch (Exception exception)
			{
				CLogger.Print(exception.ToString(), LoggerType.Error, null);
			}
		}

		public void SendGiftMessage(Account Player, ItemsModel Item)
		{
			string label = Translation.GetLabel("BattleBoxGift");
			MessageModel messageModel = this.CreateMessage("GM", Player.PlayerId, string.Concat(label, "\n\n", Item.Name));
			if (messageModel != null)
			{
				Player.Connection.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel));
			}
		}
	}
}