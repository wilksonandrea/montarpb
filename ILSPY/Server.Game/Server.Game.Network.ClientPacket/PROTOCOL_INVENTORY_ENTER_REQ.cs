using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_INVENTORY_ENTER_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null)
			{
				RoomModel room = player.Room;
				if (room != null)
				{
					room.ChangeSlotState(player.SlotId, SlotState.INVENTORY, SendInfo: false);
					room.StopCountDown(player.SlotId);
					room.UpdateSlotsInfo();
				}
				Client.SendPacket(new PROTOCOL_INVENTORY_ENTER_ACK());
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
