using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ : GameClientPacket
	{
		private int int_0;

		private int int_1;

		public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ()
		{
		}

		public override void Read()
		{
			this.int_1 = base.ReadC();
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
					if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && room.TRex == this.int_1)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null)
						{
							if (slot.State != SlotState.BATTLE)
							{
								return;
							}
							if (slot.Team != TeamEnum.FR_TEAM)
							{
								room.CTDino += 5;
							}
							else
							{
								room.FRDino += 5;
							}
							using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK pROTOCOLBATTLEMISSIONTOUCHDOWNCOUNTACK = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(room))
							{
								room.SendPacketToPlayers(pROTOCOLBATTLEMISSIONTOUCHDOWNCOUNTACK, SlotState.BATTLE, 0);
							}
							AllUtils.CompleteMission(room, player, slot, MissionType.DEATHBLOW, this.int_0);
							goto Label1;
						}
						return;
					}
				Label1:
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}