using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ : GameClientPacket
	{
		public PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ()
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
					if (room != null && room.LeaderSlot == player.SlotId && room.State == RoomState.READY && ComDiv.GetDuration(room.LastChangeTeam) >= 1.5 && !room.ChangingSlots)
					{
						List<SlotChange> slotChanges = new List<SlotChange>();
						lock (room.Slots)
						{
							room.ChangingSlots = true;
							int[] fRTEAM = room.FR_TEAM;
							for (int i = 0; i < (int)fRTEAM.Length; i++)
							{
								int ınt32 = fRTEAM[i];
								int ınt321 = ınt32 + 1;
								if (ınt32 == room.LeaderSlot)
								{
									room.LeaderSlot = ınt321;
								}
								else if (ınt321 == room.LeaderSlot)
								{
									room.LeaderSlot = ınt32;
								}
								room.SwitchSlots(slotChanges, ınt321, ınt32, true);
							}
							if (slotChanges.Count > 0)
							{
								using (PROTOCOL_ROOM_TEAM_BALANCE_ACK pROTOCOLROOMTEAMBALANCEACK = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(slotChanges, room.LeaderSlot, 2))
								{
									byte[] completeBytes = pROTOCOLROOMTEAMBALANCEACK.GetCompleteBytes(base.GetType().Name);
									foreach (Account allPlayer in room.GetAllPlayers())
									{
										allPlayer.SlotId = AllUtils.GetNewSlotId(allPlayer.SlotId);
										allPlayer.SendCompletePacket(completeBytes, pROTOCOLROOMTEAMBALANCEACK.GetType().Name);
									}
								}
							}
							room.ChangingSlots = false;
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_CHANGE_TEAM_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}