using System;
using System.Net;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth.Data.Models;

namespace Server.Auth.Data.Sync.Server
{
	// Token: 0x0200004E RID: 78
	public class AuthLogin
	{
		// Token: 0x0600012A RID: 298 RVA: 0x0000B224 File Offset: 0x00009424
		public static void SendLoginKickInfo(Account Player)
		{
			try
			{
				int serverId = (int)Player.Status.ServerId;
				if (serverId != 255 && serverId != 0)
				{
					SChannelModel server = SChannelXML.GetServer(serverId);
					if (server == null)
					{
						return;
					}
					IPEndPoint connection = SynchronizeXML.GetServer((int)server.Port).Connection;
					using (SyncServerPacket syncServerPacket = new SyncServerPacket())
					{
						syncServerPacket.WriteH(10);
						syncServerPacket.WriteQ(Player.PlayerId);
						AuthXender.Sync.SendPacket(syncServerPacket.ToArray(), connection);
						goto IL_72;
					}
				}
				Player.SetOnlineStatus(false);
				IL_72:;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00002409 File Offset: 0x00000609
		public AuthLogin()
		{
		}
	}
}
