using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ : GameClientPacket
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
			if (room == null || room.State != RoomState.BATTLE || player.SlotId != room.LeaderSlot)
			{
				return;
			}
			SlotModel slot = room.GetSlot(int_0);
			if (slot != null)
			{
				slot.AiLevel = room.IngameAiLevel;
				room.SpawnsCount++;
			}
			using PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK packet = new PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK(int_0);
			room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
