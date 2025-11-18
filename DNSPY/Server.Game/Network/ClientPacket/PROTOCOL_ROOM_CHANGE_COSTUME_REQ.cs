using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001C3 RID: 451
	public class PROTOCOL_ROOM_CHANGE_COSTUME_REQ : GameClientPacket
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00005759 File Offset: 0x00003959
		public override void Read()
		{
			this.teamEnum_0 = (TeamEnum)base.ReadC();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00024C14 File Offset: 0x00022E14
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
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_CHANGE_COSTUME_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_CHANGE_COSTUME_REQ()
		{
		}

		// Token: 0x0400033F RID: 831
		private TeamEnum teamEnum_0;
	}
}
