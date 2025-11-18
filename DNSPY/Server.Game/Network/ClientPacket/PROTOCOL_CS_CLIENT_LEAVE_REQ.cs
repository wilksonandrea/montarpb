using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000195 RID: 405
	public class PROTOCOL_CS_CLIENT_LEAVE_REQ : GameClientPacket
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000205B4 File Offset: 0x0001E7B4
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
					this.Client.SendPacket(new PROTOCOL_CS_CLIENT_LEAVE_ACK());
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_CS_CLIENT_LEAVE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_CS_CLIENT_LEAVE_REQ()
		{
		}
	}
}
