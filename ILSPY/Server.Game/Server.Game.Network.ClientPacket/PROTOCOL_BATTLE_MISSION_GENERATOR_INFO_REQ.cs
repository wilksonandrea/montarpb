using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ : GameClientPacket
{
	private ushort ushort_0;

	private ushort ushort_1;

	private List<ushort> list_0 = new List<ushort>();

	public override void Read()
	{
		ushort_0 = ReadUH();
		ushort_1 = ReadUH();
		for (int i = 0; i < 18; i++)
		{
			list_0.Add(ReadUH());
		}
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
			if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE)
			{
				return;
			}
			SlotModel slot = room.GetSlot(player.SlotId);
			if (slot == null || slot.State != SlotState.BATTLE)
			{
				return;
			}
			room.Bar1 = ushort_0;
			room.Bar2 = ushort_1;
			for (int i = 0; i < 18; i++)
			{
				SlotModel slotModel = room.Slots[i];
				if (slotModel.PlayerId > 0L && slotModel.State == SlotState.BATTLE)
				{
					slotModel.DamageBar1 = list_0[i];
					slotModel.EarnedEXP = (int)list_0[i] / 600;
				}
			}
			using (PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK packet = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(room))
			{
				room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
			}
			if (ushort_0 == 0)
			{
				RoomSadeSync.EndRound(room, (!room.SwapRound) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
			}
			else if (ushort_1 == 0)
			{
				RoomSadeSync.EndRound(room, room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
