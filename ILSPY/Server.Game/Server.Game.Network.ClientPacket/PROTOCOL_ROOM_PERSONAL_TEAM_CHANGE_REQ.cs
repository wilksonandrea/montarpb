using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ : GameClientPacket
{
	private TeamEnum teamEnum_0;

	private int int_0;

	public override void Read()
	{
		teamEnum_0 = (TeamEnum)ReadD();
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
			if (room == null || (teamEnum_0 == TeamEnum.ALL_TEAM && teamEnum_0 == TeamEnum.TEAM_DRAW) || room.ChangingSlots)
			{
				return;
			}
			SlotModel slot = room.GetSlot(player.SlotId);
			if (slot == null || slot.State != SlotState.NORMAL)
			{
				return;
			}
			room.ChangingSlots = true;
			SlotModel slot2 = room.GetSlot(int_0);
			if (slot2 == null || slot2.Team != teamEnum_0 || slot2.PlayerId != 0L || slot2.State != 0)
			{
				return;
			}
			List<SlotChange> list = new List<SlotChange>();
			room.SwitchSlots(list, player, slot, slot2);
			if (list.Count > 0)
			{
				using PROTOCOL_ROOM_TEAM_BALANCE_ACK packet = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(list, room.LeaderSlot, 0);
				room.SendPacketToPlayers(packet);
			}
			room.ChangingSlots = false;
		}
		catch (Exception ex)
		{
			CLogger.Print(GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
		}
	}
}
