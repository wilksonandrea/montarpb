using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ : GameClientPacket
{
	private int int_0;

	private float float_0;

	private float float_1;

	private float float_2;

	private byte byte_0;

	private int int_1;

	public override void Read()
	{
		int_0 = ReadD();
		byte_0 = ReadC();
		int_1 = ReadD();
		float_0 = ReadT();
		float_1 = ReadT();
		float_2 = ReadT();
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
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && !room.ActiveC4)
			{
				SlotModel slot = room.GetSlot(int_0);
				if (slot != null && slot.State == SlotState.BATTLE)
				{
					RoomBombC4.InstallBomb(room, slot, byte_0, (ushort)((int_1 == 0) ? 42u : 0u), float_0, float_1, float_2);
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
