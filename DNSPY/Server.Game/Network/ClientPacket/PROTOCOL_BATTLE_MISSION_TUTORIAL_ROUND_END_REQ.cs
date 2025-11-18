using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x0200016A RID: 362
	public class PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ : GameClientPacket
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x00004D24 File Offset: 0x00002F24
		public override void Read()
		{
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null)
					{
						room.State = RoomState.BATTLE_END;
						this.Client.SendPacket(new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(room));
						this.Client.SendPacket(new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(room, 0, RoundEndType.Tutorial));
						this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room));
						if (room.State == RoomState.BATTLE_END)
						{
							room.State = RoomState.READY;
							this.Client.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(player));
							this.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room));
						}
						AllUtils.ResetBattleInfo(room);
						this.Client.SendPacket(new PROTOCOL_ROOM_GET_SLOTINFO_ACK(room));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ()
		{
		}
	}
}
