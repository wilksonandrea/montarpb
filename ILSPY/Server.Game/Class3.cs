using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;

internal class Class3 : GameClientPacket
{
	private List<SlotModel> list_0 = new List<SlotModel>();

	void GameClientPacket.Read()
	{
	}

	void GameClientPacket.Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			RoomModel room = player.Room;
			if (room == null || room.LeaderSlot != player.SlotId || room.State != RoomState.READY)
			{
				return;
			}
			lock (room.Slots)
			{
				for (int i = 0; i < 18; i++)
				{
					SlotModel slotModel = room.Slots[i];
					if (slotModel.PlayerId > 0L && i != room.LeaderSlot)
					{
						list_0.Add(slotModel);
					}
				}
			}
			if (list_0.Count > 0)
			{
				SlotModel slotModel2 = list_0[new Random().Next(list_0.Count)];
				if (room.GetPlayerBySlot(slotModel2) != null)
				{
					room.SetNewLeader(slotModel2.Id, SlotState.EMPTY, room.LeaderSlot, UpdateInfo: false);
					using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK packet = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(slotModel2.Id))
					{
						room.SendPacketToPlayers(packet);
					}
					room.UpdateSlotsInfo();
				}
				else
				{
					Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(2147483648u));
				}
				list_0 = null;
			}
			else
			{
				Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(2147483648u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("ROOM_RANDOM_HOST2_REC: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
