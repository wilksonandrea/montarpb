using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Server
{
	// Token: 0x020001E6 RID: 486
	public class BattleLeaveSync
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x0002EA7C File Offset: 0x0002CC7C
		public static void SendUDPPlayerLeave(RoomModel Room, int SlotId)
		{
			try
			{
				if (Room != null)
				{
					int playingPlayers = Room.GetPlayingPlayers(TeamEnum.TEAM_DRAW, SlotState.BATTLE, 0, SlotId);
					using (SyncServerPacket syncServerPacket = new SyncServerPacket())
					{
						syncServerPacket.WriteH(2);
						syncServerPacket.WriteD(Room.UniqueRoomId);
						syncServerPacket.WriteD(Room.Seed);
						syncServerPacket.WriteC((byte)SlotId);
						syncServerPacket.WriteC((byte)playingPlayers);
						GameXender.Sync.SendPacket(syncServerPacket.ToArray(), Room.UdpServer.Connection);
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000025DF File Offset: 0x000007DF
		public BattleLeaveSync()
		{
		}
	}
}
