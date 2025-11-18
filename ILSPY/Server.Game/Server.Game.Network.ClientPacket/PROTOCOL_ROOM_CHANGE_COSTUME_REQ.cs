using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_COSTUME_REQ : GameClientPacket
{
	private TeamEnum teamEnum_0;

	public override void Read()
	{
		teamEnum_0 = (TeamEnum)ReadC();
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
			if (room != null && (teamEnum_0 == TeamEnum.FR_TEAM || teamEnum_0 == TeamEnum.CT_TEAM))
			{
				SlotModel slot = room.GetSlot(player.SlotId);
				if (slot != null && slot.State == SlotState.NORMAL && AllUtils.ChangeCostume(slot, teamEnum_0))
				{
					Client.SendPacket(new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(slot));
				}
				room.UpdateSlotsInfo();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_CHANGE_COSTUME_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
