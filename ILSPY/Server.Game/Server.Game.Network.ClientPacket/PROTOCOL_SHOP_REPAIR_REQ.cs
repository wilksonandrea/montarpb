using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_SHOP_REPAIR_REQ : GameClientPacket
{
	private int int_0;

	private List<long> list_0 = new List<long>();

	public override void Read()
	{
		int_0 = ReadC();
		for (int i = 0; i < int_0; i++)
		{
			uint num = ReadUD();
			list_0.Add(num);
		}
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
			int Gold;
			int Cash;
			uint Error;
			List<ItemsModel> list = AllUtils.RepairableItems(player, list_0, out Gold, out Cash, out Error);
			if (list.Count > 0)
			{
				player.Gold -= Gold;
				player.Cash -= Cash;
				if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, new string[2] { "gold", "cash" }, player.Gold, player.Cash))
				{
					Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(2, player, list));
				}
				Client.SendPacket(new PROTOCOL_SHOP_REPAIR_ACK(Error, list, player));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_SHOP_REPAIR_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
