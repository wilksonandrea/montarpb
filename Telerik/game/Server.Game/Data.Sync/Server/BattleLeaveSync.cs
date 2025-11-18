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
	public class BattleLeaveSync
	{
		public BattleLeaveSync()
		{
		}

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
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}
}