using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Sync;
using System;

namespace Server.Game.Data.Sync.Server
{
	public class RoundSync
	{
		public RoundSync()
		{
		}

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
						syncServerPacket.WriteC((byte)Room.SwapRound);
						GameXender.Sync.SendPacket(syncServerPacket.ToArray(), Room.UdpServer.Connection);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}