using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x020001B2 RID: 434
	public class PROTOCOL_GM_LOG_ROOM_REQ : GameClientPacket
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x00005684 File Offset: 0x00003884
		public override void Read()
		{
			this.int_0 = (int)base.ReadC();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00022CD4 File Offset: 0x00020ED4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.IsGM())
				{
					RoomModel room = player.Room;
					Account account;
					if (room != null && room.GetPlayerBySlot(this.int_0, out account))
					{
						this.Client.SendPacket(new PROTOCOL_GM_LOG_ROOM_ACK(0U, account));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_GM_LOG_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_GM_LOG_ROOM_REQ()
		{
		}

		// Token: 0x04000326 RID: 806
		private int int_0;
	}
}
