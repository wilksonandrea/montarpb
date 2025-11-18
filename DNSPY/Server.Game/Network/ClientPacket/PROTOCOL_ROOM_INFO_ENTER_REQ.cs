using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001CC RID: 460
	public class PROTOCOL_ROOM_INFO_ENTER_REQ : GameClientPacket
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00025D1C File Offset: 0x00023F1C
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
						room.ChangeSlotState(player.SlotId, SlotState.INFO, false);
						room.StopCountDown(player.SlotId);
						room.UpdateSlotsInfo();
					}
					this.Client.SendPacket(new PROTOCOL_ROOM_INFO_ENTER_ACK());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_INFO_ENTER_REQ()
		{
		}
	}
}
