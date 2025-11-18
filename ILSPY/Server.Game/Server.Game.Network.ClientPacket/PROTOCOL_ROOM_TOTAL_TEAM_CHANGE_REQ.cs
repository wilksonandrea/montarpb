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

public class PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ : GameClientPacket
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
			if (room == null || room.LeaderSlot != player.SlotId || room.State != RoomState.READY || !(ComDiv.GetDuration(room.LastChangeTeam) >= 1.5) || room.ChangingSlots)
			{
				return;
			}
			List<SlotChange> list = new List<SlotChange>();
			lock (room.Slots)
			{
				room.ChangingSlots = true;
				int[] fR_TEAM = room.FR_TEAM;
				foreach (int num in fR_TEAM)
				{
					int num2 = num + 1;
					if (num == room.LeaderSlot)
					{
						room.LeaderSlot = num2;
					}
					else if (num2 == room.LeaderSlot)
					{
						room.LeaderSlot = num;
					}
					room.SwitchSlots(list, num2, num, ChangeReady: true);
				}
				if (list.Count > 0)
				{
					using PROTOCOL_ROOM_TEAM_BALANCE_ACK pROTOCOL_ROOM_TEAM_BALANCE_ACK = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(list, room.LeaderSlot, 2);
					byte[] completeBytes = pROTOCOL_ROOM_TEAM_BALANCE_ACK.GetCompleteBytes(GetType().Name);
					foreach (Account allPlayer in room.GetAllPlayers())
					{
						allPlayer.SlotId = AllUtils.GetNewSlotId(allPlayer.SlotId);
						allPlayer.SendCompletePacket(completeBytes, pROTOCOL_ROOM_TEAM_BALANCE_ACK.GetType().Name);
					}
				}
				room.ChangingSlots = false;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_CHANGE_TEAM_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
