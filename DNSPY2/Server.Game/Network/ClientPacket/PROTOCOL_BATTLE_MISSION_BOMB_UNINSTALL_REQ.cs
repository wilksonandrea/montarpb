using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000166 RID: 358
	public class PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ : GameClientPacket
	{
		// Token: 0x06000394 RID: 916 RVA: 0x00005169 File Offset: 0x00003369
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001BC98 File Offset: 0x00019E98
		public override void Run()
		{
			Account player = this.Client.Player;
			if (player == null)
			{
				return;
			}
			RoomModel room = player.Room;
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && room.ActiveC4)
			{
				SlotModel slot = room.GetSlot(this.int_0);
				if (slot != null)
				{
					if (slot.State == SlotState.BATTLE)
					{
						RoomBombC4.UninstallBomb(room, slot);
						return;
					}
				}
				return;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ()
		{
		}

		// Token: 0x04000290 RID: 656
		private int int_0;
	}
}
