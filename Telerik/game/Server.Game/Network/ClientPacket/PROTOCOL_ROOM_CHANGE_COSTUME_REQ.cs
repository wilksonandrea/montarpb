using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_ROOM_CHANGE_COSTUME_REQ : GameClientPacket
	{
		private TeamEnum teamEnum_0;

		public PROTOCOL_ROOM_CHANGE_COSTUME_REQ()
		{
		}

		public override void Read()
		{
			this.teamEnum_0 = (TeamEnum)base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && (this.teamEnum_0 == TeamEnum.FR_TEAM || this.teamEnum_0 == TeamEnum.CT_TEAM))
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null && slot.State == SlotState.NORMAL && AllUtils.ChangeCostume(slot, this.teamEnum_0))
						{
							this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_COSTUME_ACK(slot));
						}
						room.UpdateSlotsInfo();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_CHANGE_COSTUME_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}