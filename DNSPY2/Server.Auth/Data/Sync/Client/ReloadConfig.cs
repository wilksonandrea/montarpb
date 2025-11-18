using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Network;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x0200005A RID: 90
	public class ReloadConfig
	{
		// Token: 0x0600013E RID: 318 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			ServerConfig config = ServerConfigJSON.GetConfig(num);
			if (config != null && config.ConfigId > 0)
			{
				AuthXender.Client.Config = config;
				CLogger.Print(string.Format("Configuration (Database) Refills; Config: {0}", num), LoggerType.Command, null);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00002409 File Offset: 0x00000609
		public ReloadConfig()
		{
		}
	}
}
