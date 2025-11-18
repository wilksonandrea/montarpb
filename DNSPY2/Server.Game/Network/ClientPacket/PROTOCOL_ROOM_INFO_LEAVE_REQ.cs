using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001CD RID: 461
	public class PROTOCOL_ROOM_INFO_LEAVE_REQ : GameClientPacket
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00025D98 File Offset: 0x00023F98
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
					this.Client.SendPacket(new PROTOCOL_ROOM_INFO_LEAVE_ACK());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_INFO_LEAVE_REQ()
		{
		}
	}
}
