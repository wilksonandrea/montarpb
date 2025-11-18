using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000169 RID: 361
	public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ : GameClientPacket
	{
		// Token: 0x0600039D RID: 925 RVA: 0x000051A8 File Offset: 0x000033A8
		public override void Read()
		{
			this.int_1 = (int)base.ReadC();
			this.int_0 = base.ReadD();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE && room.TRex == this.int_1)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null)
						{
							if (slot.State == SlotState.BATTLE)
							{
								if (slot.Team == TeamEnum.FR_TEAM)
								{
									room.FRDino += 5;
								}
								else
								{
									room.CTDino += 5;
								}
								using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK protocol_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(room))
								{
									room.SendPacketToPlayers(protocol_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK, SlotState.BATTLE, 0);
								}
								AllUtils.CompleteMission(room, player, slot, MissionType.DEATHBLOW, this.int_0);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ()
		{
		}

		// Token: 0x04000298 RID: 664
		private int int_0;

		// Token: 0x04000299 RID: 665
		private int int_1;
	}
}
