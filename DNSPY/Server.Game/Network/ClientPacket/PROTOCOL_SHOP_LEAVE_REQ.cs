using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001DA RID: 474
	public class PROTOCOL_SHOP_LEAVE_REQ : GameClientPacket
	{
		// Token: 0x0600050C RID: 1292 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00026FD0 File Offset: 0x000251D0
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
					this.Client.SendPacket(new PROTOCOL_SHOP_LEAVE_ACK());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_SHOP_LEAVE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_SHOP_LEAVE_REQ()
		{
		}
	}
}
