using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_GET_PLAYERINFO_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
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
			RoomModel room = player.Room;
			if (room == null || player.SlotId == int_0 || !room.GetPlayerBySlot(int_0, out var Player))
			{
				return;
			}
			uint uint_ = 0u;
			int[] array = new int[2];
			SlotModel slot = room.GetSlot(Player.SlotId);
			if (slot != null)
			{
				switch (room.ValidateTeam(slot.Team, slot.CostumeTeam))
				{
				case TeamEnum.FR_TEAM:
					array[0] = slot.Equipment.CharaRedId;
					break;
				case TeamEnum.CT_TEAM:
					array[0] = slot.Equipment.CharaBlueId;
					break;
				}
				array[1] = slot.Equipment.AccessoryId;
			}
			else
			{
				uint_ = 134217728u;
			}
			Client.SendPacket(new PROTOCOL_ROOM_GET_PLAYERINFO_ACK(uint_, Player, array));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
