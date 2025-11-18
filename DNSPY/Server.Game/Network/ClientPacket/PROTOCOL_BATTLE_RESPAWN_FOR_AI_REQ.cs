using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200016F RID: 367
	public class PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ : GameClientPacket
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00005217 File Offset: 0x00003417
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.State == RoomState.BATTLE && player.SlotId == room.LeaderSlot)
					{
						SlotModel slot = room.GetSlot(this.int_0);
						if (slot != null)
						{
							slot.AiLevel = (int)room.IngameAiLevel;
							room.SpawnsCount++;
						}
						using (PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK protocol_BATTLE_RESPAWN_FOR_AI_ACK = new PROTOCOL_BATTLE_RESPAWN_FOR_AI_ACK(this.int_0))
						{
							room.SendPacketToPlayers(protocol_BATTLE_RESPAWN_FOR_AI_ACK, SlotState.BATTLE, 0);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ()
		{
		}

		// Token: 0x040002A0 RID: 672
		private int int_0;
	}
}
