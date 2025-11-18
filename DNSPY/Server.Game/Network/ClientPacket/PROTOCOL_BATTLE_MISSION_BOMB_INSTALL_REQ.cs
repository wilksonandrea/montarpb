using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000165 RID: 357
	public class PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ : GameClientPacket
	{
		// Token: 0x06000391 RID: 913 RVA: 0x0001BB7C File Offset: 0x00019D7C
		public override void Read()
		{
			this.int_0 = base.ReadD();
			this.byte_0 = base.ReadC();
			this.int_1 = base.ReadD();
			this.float_0 = base.ReadT();
			this.float_1 = base.ReadT();
			this.float_2 = base.ReadT();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001BBD4 File Offset: 0x00019DD4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && !room.ActiveC4)
					{
						SlotModel slot = room.GetSlot(this.int_0);
						if (slot != null)
						{
							if (slot.State == SlotState.BATTLE)
							{
								RoomBombC4.InstallBomb(room, slot, this.byte_0, (this.int_1 == 0) ? 42 : 0, this.float_0, this.float_1, this.float_2);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ()
		{
		}

		// Token: 0x0400028A RID: 650
		private int int_0;

		// Token: 0x0400028B RID: 651
		private float float_0;

		// Token: 0x0400028C RID: 652
		private float float_1;

		// Token: 0x0400028D RID: 653
		private float float_2;

		// Token: 0x0400028E RID: 654
		private byte byte_0;

		// Token: 0x0400028F RID: 655
		private int int_1;
	}
}
