using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadC();
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
			RoomModel room = player.Room;
			if (room == null || player.SlotId == int_0 || !room.GetSlot(int_0, out var Slot))
			{
				return;
			}
			uint uint_ = 0u;
			int[] array = new int[2];
			PlayerEquipment equipment = Slot.Equipment;
			if (equipment != null)
			{
				switch (room.ValidateTeam(Slot.Team, Slot.CostumeTeam))
				{
				case TeamEnum.FR_TEAM:
					array[0] = equipment.CharaRedId;
					break;
				case TeamEnum.CT_TEAM:
					array[0] = equipment.CharaBlueId;
					break;
				}
				array[1] = equipment.AccessoryId;
			}
			else
			{
				uint_ = 134217728u;
			}
			Client.SendPacket(new PROTOCOL_ROOM_GET_USER_EQUIPMENT_ACK(uint_, equipment, array, (byte)Slot.Id));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
