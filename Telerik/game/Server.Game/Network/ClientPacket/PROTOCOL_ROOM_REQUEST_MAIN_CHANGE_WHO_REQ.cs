using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room == null || room.LeaderSlot == this.int_0 || room.Slots[this.int_0].PlayerId == 0)
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK((uint)-2147483648));
					}
					else if (room.State == RoomState.READY && room.LeaderSlot == player.SlotId)
					{
						room.SetNewLeader(this.int_0, SlotState.EMPTY, room.LeaderSlot, false);
						using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK pROTOCOLROOMREQUESTMAINCHANGEWHOACK = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(this.int_0))
						{
							room.SendPacketToPlayers(pROTOCOLROOMREQUESTMAINCHANGEWHOACK);
						}
						room.UpdateSlotsInfo();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}