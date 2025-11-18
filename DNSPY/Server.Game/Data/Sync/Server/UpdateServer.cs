using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.XML;

namespace Server.Game.Data.Sync.Server
{
	// Token: 0x020001ED RID: 493
	public class UpdateServer
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x0002F5D4 File Offset: 0x0002D7D4
		public static void RefreshSChannel(int serverId)
		{
			try
			{
				if (ComDiv.GetDuration(UpdateServer.dateTime_0) >= (double)ConfigLoader.UpdateIntervalPlayersServer)
				{
					UpdateServer.dateTime_0 = DateTimeUtil.Now();
					int num = 0;
					foreach (ChannelModel channelModel in ChannelsXML.Channels)
					{
						num += channelModel.Players.Count;
					}
					foreach (SChannelModel schannelModel in SChannelXML.Servers)
					{
						if (schannelModel.Id == serverId)
						{
							schannelModel.LastPlayers = num;
						}
						else
						{
							IPEndPoint connection = SynchronizeXML.GetServer((int)schannelModel.Port).Connection;
							using (SyncServerPacket syncServerPacket = new SyncServerPacket())
							{
								syncServerPacket.WriteH(15);
								syncServerPacket.WriteD(serverId);
								syncServerPacket.WriteD(num);
								GameXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
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

		// Token: 0x060005CD RID: 1485 RVA: 0x000025DF File Offset: 0x000007DF
		public UpdateServer()
		{
		}

		// Token: 0x0400039B RID: 923
		private static DateTime dateTime_0;
	}
}
