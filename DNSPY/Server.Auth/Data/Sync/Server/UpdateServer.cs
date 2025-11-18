using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;

namespace Server.Auth.Data.Sync.Server
{
	// Token: 0x02000050 RID: 80
	public class UpdateServer
	{
		// Token: 0x0600012E RID: 302 RVA: 0x0000B488 File Offset: 0x00009688
		public static void RefreshSChannel(int ServerId)
		{
			try
			{
				if (ComDiv.GetDuration(UpdateServer.dateTime_0) >= (double)ConfigLoader.UpdateIntervalPlayersServer)
				{
					UpdateServer.dateTime_0 = DateTimeUtil.Now();
					int count = AuthXender.SocketSessions.Count;
					foreach (SChannelModel schannelModel in SChannelXML.Servers)
					{
						if (schannelModel.Id == ServerId)
						{
							schannelModel.LastPlayers = count;
						}
						else
						{
							IPEndPoint connection = SynchronizeXML.GetServer((int)schannelModel.Port).Connection;
							using (SyncServerPacket syncServerPacket = new SyncServerPacket())
							{
								syncServerPacket.WriteH(15);
								syncServerPacket.WriteD(ServerId);
								syncServerPacket.WriteD(count);
								AuthXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00002409 File Offset: 0x00000609
		public UpdateServer()
		{
		}

		// Token: 0x0400009B RID: 155
		private static DateTime dateTime_0;
	}
}
