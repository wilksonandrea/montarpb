using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Server.Game.Data.Sync.Server
{
	// Token: 0x020001EC RID: 492
	public class UpdateChannel
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x0002F534 File Offset: 0x0002D734
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
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000025DF File Offset: 0x000007DF
		public UpdateChannel()
		{
		}
	}
}
