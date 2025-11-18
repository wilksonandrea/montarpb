using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000162 RID: 354
	public class PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ : GameClientPacket
	{
		// Token: 0x06000388 RID: 904 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001B520 File Offset: 0x00019720
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.State == RoomState.BATTLE && room.IngameAiLevel < 10)
					{
						SlotModel slot = room.GetSlot(player.SlotId);
						if (slot != null && slot.State == SlotState.BATTLE)
						{
							if (room.IngameAiLevel <= 9)
							{
								RoomModel roomModel = room;
								roomModel.IngameAiLevel += 1;
							}
							using (PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK protocol_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK = new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(room))
							{
								room.SendPacketToPlayers(protocol_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK, SlotState.READY, 1);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ()
		{
		}
	}
}
