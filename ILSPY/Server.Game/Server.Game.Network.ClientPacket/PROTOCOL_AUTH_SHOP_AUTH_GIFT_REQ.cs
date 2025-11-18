using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ : GameClientPacket
{
	private long long_0;

	public override void Read()
	{
		long_0 = ReadUD();
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
			if (player.Inventory.Items.Count >= 500)
			{
				Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(2147487785u));
				Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(2147483648u));
				return;
			}
			MessageModel message = DaoManagerSQL.GetMessage(long_0, player.PlayerId);
			if (message != null && message.Type == NoteMessageType.Gift)
			{
				GoodsItem good = ShopManager.GetGood((int)message.SenderId);
				if (good != null)
				{
					Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(1u, good.Item, player));
					DaoManagerSQL.DeleteMessage(long_0, player.PlayerId);
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(2147483648u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
