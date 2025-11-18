using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Sync;
using System;
using System.Net;

namespace Server.Game.Data.Sync.Server
{
	public class UpdateChannel
	{
		public UpdateChannel()
		{
		}

		public static void RefreshChannel(int ServerId, int ChannelId, int Count)
		{
			try
			{
				SChannelModel server = GameXender.Sync.GetServer(0);
				if (server != null)
				{
					IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
					using (SyncServerPacket syncServerPacket = new SyncServerPacket())
					{
						syncServerPacket.WriteH(33);
						syncServerPacket.WriteD(ServerId);
						syncServerPacket.WriteD(ChannelId);
						syncServerPacket.WriteD(Count);
						GameXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
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