using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	public override void Read()
	{
		int_1 = ReadC();
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
			if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE || room.TRex != int_1)
			{
				return;
			}
			SlotModel slot = room.GetSlot(player.SlotId);
			if (slot != null && slot.State == SlotState.BATTLE)
			{
				if (slot.Team == TeamEnum.FR_TEAM)
				{
					room.FRDino += 5;
				}
				else
				{
					room.CTDino += 5;
				}
				using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK packet = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(room))
				{
					room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
				}
				AllUtils.CompleteMission(room, player, slot, MissionType.DEATHBLOW, int_0);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
