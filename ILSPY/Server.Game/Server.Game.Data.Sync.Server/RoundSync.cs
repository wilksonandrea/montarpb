using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;

namespace Server.Game.Data.Sync.Server;

public class RoundSync
{
	public static void SendUDPRoundSync(RoomModel Room)
	{
		try
		{
			if (Room == null)
			{
				return;
			}
			using SyncServerPacket syncServerPacket = new SyncServerPacket();
			syncServerPacket.WriteH(3);
			syncServerPacket.WriteD(Room.UniqueRoomId);
			syncServerPacket.WriteD(Room.Seed);
			syncServerPacket.WriteC((byte)Room.Rounds);
			syncServerPacket.WriteC(Room.SwapRound ? ((byte)1) : ((byte)0));
			GameXender.Sync.SendPacket(syncServerPacket.ToArray(), Room.UdpServer.Connection);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
