using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ : GameClientPacket
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
			if (player != null)
			{
				RoomModel room = player.Room;
				if (room != null && room.GetSlot(int_0, out var Slot) && player.SlotId == Slot.Id)
				{
					player.SendPacket(new PROTOCOL_BATTLE_TIMEOUTCLIENT_ACK());
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_TIMEOUTCLIENT_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
