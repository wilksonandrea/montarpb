using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ : GameClientPacket
{
	private ViewerType viewerType_0;

	public override void Read()
	{
		viewerType_0 = (ViewerType)ReadC();
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
			if (room == null || !player.IsGM())
			{
				return;
			}
			SlotModel slot = room.GetSlot(player.SlotId);
			if (slot != null)
			{
				slot.ViewType = viewerType_0;
				if (slot.ViewType == ViewerType.SpecGM)
				{
					slot.SpecGM = true;
				}
				Client.SendPacket(new PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_ACK(slot.Id));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(GetType().Name + "; " + ex.Message, LoggerType.Error, ex);
		}
	}
}
