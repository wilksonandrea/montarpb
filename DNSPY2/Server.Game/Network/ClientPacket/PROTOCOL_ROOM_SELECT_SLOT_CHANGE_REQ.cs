using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001D5 RID: 469
	public class PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ : GameClientPacket
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x0000584A File Offset: 0x00003A4A
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000268C8 File Offset: 0x00024AC8
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
							SlotModel slot2 = room.GetSlot(this.int_0);
							if (slot2 != null && slot2.Team == room.CheckTeam(this.int_0) && slot2.PlayerId == 0L && slot2.State == SlotState.EMPTY)
							{
								List<SlotChange> list = new List<SlotChange>();
								room.SwitchSlots(list, player, slot, slot2, SlotState.NORMAL);
								if (list.Count > 0)
								{
									using (PROTOCOL_ROOM_TEAM_BALANCE_ACK protocol_ROOM_TEAM_BALANCE_ACK = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(list, room.LeaderSlot, 0))
									{
										room.SendPacketToPlayers(protocol_ROOM_TEAM_BALANCE_ACK);
									}
								}
								room.ChangingSlots = false;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ()
		{
		}

		// Token: 0x04000383 RID: 899
		private int int_0;
	}
}
