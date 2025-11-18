using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_REQUEST_MAIN_REQ : GameClientPacket
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
			if (room != null)
			{
				if (room.State != RoomState.READY || room.LeaderSlot == player.SlotId)
				{
					return;
				}
				List<Account> allPlayers = room.GetAllPlayers();
				if (allPlayers.Count == 0)
				{
					return;
				}
				if (player.IsGM())
				{
					method_0(room, allPlayers, player.SlotId);
					return;
				}
				if (!room.RequestRoomMaster.Contains(player.PlayerId))
				{
					room.RequestRoomMaster.Add(player.PlayerId);
					if (room.RequestRoomMaster.Count() < allPlayers.Count / 2 + 1)
					{
						using PROTOCOL_ROOM_REQUEST_MAIN_ACK gameServerPacket_ = new PROTOCOL_ROOM_REQUEST_MAIN_ACK(player.SlotId);
						method_1(gameServerPacket_, allPlayers);
					}
				}
				if (room.RequestRoomMaster.Count() >= allPlayers.Count / 2 + 1)
				{
					method_0(room, allPlayers, player.SlotId);
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_ACK(2147483648u));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_REQUEST_MAIN_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_0(RoomModel roomModel_0, List<Account> list_0, int int_0)
	{
		roomModel_0.SetNewLeader(int_0, SlotState.EMPTY, -1, UpdateInfo: false);
		using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK gameServerPacket_ = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(int_0))
		{
			method_1(gameServerPacket_, list_0);
		}
		roomModel_0.UpdateSlotsInfo();
		roomModel_0.RequestRoomMaster.Clear();
	}

	private void method_1(GameServerPacket gameServerPacket_0, List<Account> list_0)
	{
		byte[] completeBytes = gameServerPacket_0.GetCompleteBytes("PROTOCOL_ROOM_REQUEST_MAIN_REQ");
		foreach (Account item in list_0)
		{
			item.SendCompletePacket(completeBytes, gameServerPacket_0.GetType().Name);
		}
	}
}
