using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ : GameClientPacket
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
			if (room != null && room.LeaderSlot != int_0 && room.Slots[int_0].PlayerId != 0L)
			{
				if (room.State == RoomState.READY && room.LeaderSlot == player.SlotId)
				{
					room.SetNewLeader(int_0, SlotState.EMPTY, room.LeaderSlot, UpdateInfo: false);
					using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK packet = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(int_0))
					{
						room.SendPacketToPlayers(packet);
					}
					room.UpdateSlotsInfo();
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(2147483648u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
