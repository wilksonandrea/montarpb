using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B3 RID: 435
	public class PROTOCOL_INVENTORY_ENTER_REQ : GameClientPacket
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00022D54 File Offset: 0x00020F54
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
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_INVENTORY_ENTER_REQ()
		{
		}
	}
}
