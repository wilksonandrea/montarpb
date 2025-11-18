using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200014D RID: 333
	public class PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ : GameClientPacket
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000501C File Offset: 0x0000321C
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001959C File Offset: 0x0001779C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					Account account;
					if (room != null && room.GetPlayerBySlot(this.int_0, out account))
					{
						this.Client.SendPacket(new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK(account));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ()
		{
		}

		// Token: 0x04000256 RID: 598
		private int int_0;
	}
}
