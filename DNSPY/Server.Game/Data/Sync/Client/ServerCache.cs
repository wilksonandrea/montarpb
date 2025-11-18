using System;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001FE RID: 510
	public class ServerCache
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x00030F08 File Offset: 0x0002F108
		public static void Load(SyncClientPacket C)
		{
			int num = C.ReadD();
			int num2 = C.ReadD();
			SChannelModel server = SChannelXML.GetServer(num);
			if (server != null)
			{
				server.LastPlayers = num2;
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000025DF File Offset: 0x000007DF
		public ServerCache()
		{
		}
	}
}
