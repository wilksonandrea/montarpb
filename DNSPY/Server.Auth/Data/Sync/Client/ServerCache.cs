using System;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x0200005C RID: 92
	public class ServerCache
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000BA94 File Offset: 0x00009C94
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

		// Token: 0x06000143 RID: 323 RVA: 0x00002409 File Offset: 0x00000609
		public ServerCache()
		{
		}
	}
}
