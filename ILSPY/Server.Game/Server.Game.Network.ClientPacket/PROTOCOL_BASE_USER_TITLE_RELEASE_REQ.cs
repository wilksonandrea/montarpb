using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_USER_TITLE_RELEASE_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	public override void Read()
	{
		int_0 = ReadC();
		int_1 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || int_0 >= 3 || player.Title == null)
			{
				return;
			}
			PlayerTitles title = player.Title;
			int equip = title.GetEquip(int_0);
			if (int_0 < 3 && int_1 < 45 && equip == int_1 && DaoManagerSQL.UpdateEquipedPlayerTitle(title.OwnerId, int_0, 0))
			{
				title.SetEquip(int_0, 0);
				if (TitleAwardXML.Contains(equip, player.Equipment.BeretItem) && ComDiv.UpdateDB("player_equipments", "beret_item_part", 0, "owner_id", player.PlayerId))
				{
					player.Equipment.BeretItem = 0;
					RoomModel room = player.Room;
					if (room != null)
					{
						AllUtils.UpdateSlotEquips(player, room);
					}
				}
				Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(0u, int_0, int_1));
			}
			else
			{
				Client.SendPacket(new PROTOCOL_BASE_USER_TITLE_RELEASE_ACK(2147483648u, -1, -1));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_USER_TITLE_RELEASE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
