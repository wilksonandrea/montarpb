using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000167 RID: 359
	public class PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ : GameClientPacket
	{
		// Token: 0x06000397 RID: 919 RVA: 0x0001BD04 File Offset: 0x00019F04
		public override void Read()
		{
			this.ushort_0 = base.ReadUH();
			this.ushort_1 = base.ReadUH();
			for (int i = 0; i < 18; i++)
			{
				this.list_0.Add(base.ReadUH());
			}
			for (int j = 0; j < 18; j++)
			{
				this.list_1.Add(base.ReadUH());
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001BD68 File Offset: 0x00019F68
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null)
						{
							if (slot.State == SlotState.BATTLE)
							{
								room.Bar1 = (int)this.ushort_0;
								room.Bar2 = (int)this.ushort_1;
								for (int i = 0; i < 18; i++)
								{
									SlotModel slotModel = room.Slots[i];
									if (slotModel.PlayerId > 0L && slotModel.State == SlotState.BATTLE)
									{
										slotModel.DamageBar1 = this.list_0[i];
										slotModel.DamageBar2 = this.list_1[i];
									}
								}
								using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK protocol_BATTLE_MISSION_DEFENCE_INFO_ACK = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(room))
								{
									room.SendPacketToPlayers(protocol_BATTLE_MISSION_DEFENCE_INFO_ACK, SlotState.BATTLE, 0);
								}
								if (this.ushort_0 == 0 && this.ushort_1 == 0)
								{
									RoomSadeSync.EndRound(room, (!room.SwapRound) ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00005177 File Offset: 0x00003377
		public PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ()
		{
		}

		// Token: 0x04000291 RID: 657
		private ushort ushort_0;

		// Token: 0x04000292 RID: 658
		private ushort ushort_1;

		// Token: 0x04000293 RID: 659
		private List<ushort> list_0 = new List<ushort>();

		// Token: 0x04000294 RID: 660
		private List<ushort> list_1 = new List<ushort>();
	}
}
