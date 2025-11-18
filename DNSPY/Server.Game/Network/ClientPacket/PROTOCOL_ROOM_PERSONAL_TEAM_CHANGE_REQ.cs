using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001D1 RID: 465
	public class PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ : GameClientPacket
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x0000580F File Offset: 0x00003A0F
		public override void Read()
		{
			this.teamEnum_0 = (TeamEnum)base.ReadD();
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000262B8 File Offset: 0x000244B8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && (this.teamEnum_0 != TeamEnum.ALL_TEAM || this.teamEnum_0 != TeamEnum.TEAM_DRAW) && !room.ChangingSlots)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null && slot.State == SlotState.NORMAL)
						{
							room.ChangingSlots = true;
							SlotModel slot2 = room.GetSlot(this.int_0);
							if (slot2 != null && slot2.Team == this.teamEnum_0 && slot2.PlayerId == 0L && slot2.State == SlotState.EMPTY)
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
				CLogger.Print(base.GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ()
		{
		}

		// Token: 0x0400037F RID: 895
		private TeamEnum teamEnum_0;

		// Token: 0x04000380 RID: 896
		private int int_0;
	}
}
