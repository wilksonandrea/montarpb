using System;
using Plugin.Core.Network;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x02000052 RID: 82
	public class ChannelCache
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000B5E4 File Offset: 0x000097E4
		public static void Load(SyncClientPacket C)
		{
			int num = C.ReadD();
			int num2 = C.ReadD();
			int num3 = C.ReadD();
			ChannelModel channel = ChannelsXML.GetChannel(num, num2);
			if (channel != null)
			{
				channel.TotalPlayers = num3;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00002409 File Offset: 0x00000609
		public ChannelCache()
		{
		}
	}
}
