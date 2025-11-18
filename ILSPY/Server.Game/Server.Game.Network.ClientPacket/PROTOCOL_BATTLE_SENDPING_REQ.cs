using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_SENDPING_REQ : GameClientPacket
{
	private byte[] byte_0;

	public override void Read()
	{
		byte_0 = ReadB(16);
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
			if (room == null || !room.GetSlot(player.SlotId, out var Slot))
			{
				return;
			}
			int num = 0;
			if (Slot == null || Slot.State < SlotState.BATTLE_READY)
			{
				return;
			}
			if (room.State == RoomState.BATTLE)
			{
				room.Ping = byte_0[room.LeaderSlot];
			}
			using (PROTOCOL_BATTLE_SENDPING_ACK pROTOCOL_BATTLE_SENDPING_ACK = new PROTOCOL_BATTLE_SENDPING_ACK(byte_0))
			{
				List<Account> allPlayers = room.GetAllPlayers(SlotState.READY, 1);
				if (allPlayers.Count == 0)
				{
					return;
				}
				byte[] completeBytes = pROTOCOL_BATTLE_SENDPING_ACK.GetCompleteBytes(GetType().Name);
				foreach (Account item in allPlayers)
				{
					SlotModel slot = room.GetSlot(item.SlotId);
					if (slot != null && slot.State >= SlotState.BATTLE_READY)
					{
						item.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_SENDPING_ACK.GetType().Name);
					}
					else
					{
						num++;
					}
				}
			}
			if (num == 0)
			{
				room.SpawnReadyPlayers();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_SENDPING_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
