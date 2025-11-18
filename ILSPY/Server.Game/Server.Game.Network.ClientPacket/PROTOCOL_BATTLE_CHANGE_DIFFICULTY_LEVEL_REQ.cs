using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ : GameClientPacket
{
	public override void Read()
	{
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
			if (room == null || room.State != RoomState.BATTLE || room.IngameAiLevel >= 10)
			{
				return;
			}
			SlotModel slot = room.GetSlot(player.SlotId);
			if (slot == null || slot.State != SlotState.BATTLE)
			{
				return;
			}
			if (room.IngameAiLevel <= 9)
			{
				room.IngameAiLevel++;
			}
			using PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK packet = new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(room);
			room.SendPacketToPlayers(packet, SlotState.READY, 1);
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
