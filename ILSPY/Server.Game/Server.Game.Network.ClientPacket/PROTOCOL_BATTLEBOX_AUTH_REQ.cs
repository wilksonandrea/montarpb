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

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLEBOX_AUTH_REQ : GameClientPacket
{
	private long long_0;

	private int int_0;

	public override void Read()
	{
		long_0 = ReadUD();
		int_0 = ReadD();
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
			ItemsModel ıtem = player.Inventory.GetItem(long_0);
			if (ıtem != null)
			{
				BattleBoxModel battleBox = BattleBoxXML.GetBattleBox(ıtem.Id);
				if (battleBox != null && battleBox.RequireTags == int_0)
				{
					if (int_0 > player.Tags)
					{
						Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648u));
						return;
					}
					if (!DaoManagerSQL.UpdateAccountTags(player.PlayerId, player.Tags - int_0))
					{
						Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648u));
						return;
					}
					GoodsItem good = ShopManager.GetGood(battleBox.GetItemWithProbabilities(battleBox.Goods, battleBox.Probabilities));
					if (good == null)
					{
						Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648u));
						return;
					}
					player.Tags -= int_0;
					if (ComDiv.UpdateDB("accounts", "tags", player.Tags, "player_id", player.PlayerId))
					{
						Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, good.Item));
					}
					ıtem.Count--;
					if (ıtem.Count == 0)
					{
						if (DaoManagerSQL.DeletePlayerInventoryItem(ıtem.ObjectId, player.PlayerId))
						{
							player.Inventory.RemoveItem(ıtem);
						}
						Client.SendPacket(new PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(1u, long_0));
					}
					else
					{
						ComDiv.UpdateDB("player_items", "count", (long)ıtem.Count, "owner_id", player.PlayerId, "id", ıtem.Id);
						Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(1, player, ıtem));
					}
					Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(0u, player, good.Item.Id));
				}
				else
				{
					Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648u));
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_BATTLEBOX_AUTH_ACK(2147483648u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.ToString(), LoggerType.Error);
		}
	}

	public void SendGiftMessage(Account Player, ItemsModel Item)
	{
		string label = Translation.GetLabel("BattleBoxGift");
		MessageModel messageModel = CreateMessage("GM", Player.PlayerId, label + "\n\n" + Item.Name);
		if (messageModel != null)
		{
			Player.Connection.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(messageModel));
		}
	}

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
}
