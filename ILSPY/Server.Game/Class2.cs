using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;

internal class Class2 : GameClientPacket
{
	private uint uint_0;

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
			if (room != null && room.LeaderSlot == player.SlotId && room.State == RoomState.READY)
			{
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
					int index = new Random().Next(list_0.Count);
					SlotModel slotModel2 = list_0[index];
					uint_0 = ((room.GetPlayerBySlot(slotModel2) != null) ? ((uint)slotModel2.Id) : 2147483648u);
					list_0 = null;
				}
				else
				{
					uint_0 = 2147483648u;
				}
			}
			else
			{
				uint_0 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_ROOM_CHECK_MAIN_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_CHECK_MAIN_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
