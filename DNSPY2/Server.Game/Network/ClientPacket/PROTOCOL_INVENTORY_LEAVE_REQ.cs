using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B4 RID: 436
	public class PROTOCOL_INVENTORY_LEAVE_REQ : GameClientPacket
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00022DD0 File Offset: 0x00020FD0
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
						room.ChangeSlotState(player.SlotId, SlotState.NORMAL, true);
					}
					this.Client.SendPacket(new PROTOCOL_INVENTORY_LEAVE_ACK());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_INVENTORY_LEAVE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_INVENTORY_LEAVE_REQ()
		{
		}
	}
}
