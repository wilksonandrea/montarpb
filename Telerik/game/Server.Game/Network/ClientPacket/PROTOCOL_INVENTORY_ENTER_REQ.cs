using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_INVENTORY_ENTER_REQ : GameClientPacket
	{
		public PROTOCOL_INVENTORY_ENTER_REQ()
		{
		}

		public override void Read()
		{
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null)
					{
						room.ChangeSlotState(player.SlotId, SlotState.INVENTORY, false);
						room.StopCountDown(player.SlotId);
						room.UpdateSlotsInfo();
					}
					this.Client.SendPacket(new PROTOCOL_INVENTORY_ENTER_ACK());
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}