using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200016B RID: 363
	public class PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ : GameClientPacket
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001C28C File Offset: 0x0001A48C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.State >= RoomState.LOADING && room.Slots[player.SlotId].State == SlotState.NORMAL)
					{
						this.Client.SendPacket(new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(room));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ()
		{
		}
	}
}
