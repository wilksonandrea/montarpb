using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Server
{
	// Token: 0x020001E8 RID: 488
	public class RoundSync
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x0002EDBC File Offset: 0x0002CFBC
		public static void SendUDPRoundSync(RoomModel Room)
		{
			try
			{
				if (Room != null)
				{
					using (SyncServerPacket syncServerPacket = new SyncServerPacket())
					{
						syncServerPacket.WriteH(3);
						syncServerPacket.WriteD(Room.UniqueRoomId);
						syncServerPacket.WriteD(Room.Seed);
						syncServerPacket.WriteC((byte)Room.Rounds);
						syncServerPacket.WriteC((Room.SwapRound > false) ? 1 : 0);
						GameXender.Sync.SendPacket(syncServerPacket.ToArray(), Room.UdpServer.Connection);
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000025DF File Offset: 0x000007DF
		public RoundSync()
		{
		}
	}
}
