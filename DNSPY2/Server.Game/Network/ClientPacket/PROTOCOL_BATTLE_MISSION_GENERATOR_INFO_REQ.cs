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
	// Token: 0x02000168 RID: 360
	public class PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ : GameClientPacket
	{
		// Token: 0x0600039A RID: 922 RVA: 0x0001BED8 File Offset: 0x0001A0D8
		public override void Read()
		{
			this.ushort_0 = base.ReadUH();
			this.ushort_1 = base.ReadUH();
			for (int i = 0; i < 18; i++)
			{
				this.list_0.Add(base.ReadUH());
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001BF1C File Offset: 0x0001A11C
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
										slotModel.EarnedEXP = (int)(this.list_0[i] / 600);
									}
								}
								using (PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK protocol_BATTLE_MISSION_GENERATOR_INFO_ACK = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(room))
								{
									room.SendPacketToPlayers(protocol_BATTLE_MISSION_GENERATOR_INFO_ACK, SlotState.BATTLE, 0);
								}
								if (this.ushort_0 == 0)
								{
									RoomSadeSync.EndRound(room, (!room.SwapRound) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
								}
								else if (this.ushort_1 == 0)
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

		// Token: 0x0600039C RID: 924 RVA: 0x00005195 File Offset: 0x00003395
		public PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ()
		{
		}

		// Token: 0x04000295 RID: 661
		private ushort ushort_0;

		// Token: 0x04000296 RID: 662
		private ushort ushort_1;

		// Token: 0x04000297 RID: 663
		private List<ushort> list_0 = new List<ushort>();
	}
}
