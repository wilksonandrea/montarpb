using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Network;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F7 RID: 503
	public class ReloadConfig
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0002FBD8 File Offset: 0x0002DDD8
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			ServerConfig config = ServerConfigJSON.GetConfig(num);
			if (config != null && config.ConfigId > 0)
			{
				GameXender.Client.Config = config;
				CLogger.Print(string.Format("Configuration (Database) Refills; Config: {0}", num), LoggerType.Command, null);
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000025DF File Offset: 0x000007DF
		public ReloadConfig()
		{
		}
	}
}
