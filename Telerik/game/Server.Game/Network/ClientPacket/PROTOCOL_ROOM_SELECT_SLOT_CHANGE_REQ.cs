using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ()
		{
		}

		public override void Read()
		{
			this.int_0 = base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && !room.ChangingSlots)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null && slot.State == SlotState.NORMAL)
						{
							room.ChangingSlots = true;
							SlotModel slotModel = room.GetSlot(this.int_0);
							if (slotModel == null || slotModel.Team != room.CheckTeam(this.int_0) || slotModel.PlayerId != 0 || slotModel.State != SlotState.EMPTY)
							{
								return;
							}
							else
							{
								List<SlotChange> slotChanges = new List<SlotChange>();
								room.SwitchSlots(slotChanges, player, slot, slotModel, SlotState.NORMAL);
								if (slotChanges.Count > 0)
								{
									using (PROTOCOL_ROOM_TEAM_BALANCE_ACK pROTOCOLROOMTEAMBALANCEACK = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(slotChanges, room.LeaderSlot, 0))
									{
										room.SendPacketToPlayers(pROTOCOLROOMTEAMBALANCEACK);
									}
								}
								room.ChangingSlots = false;
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}