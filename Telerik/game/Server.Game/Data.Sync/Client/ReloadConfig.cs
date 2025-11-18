using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game;
using System;

namespace Server.Game.Data.Sync.Client
{
	public class ReloadConfig
	{
		public ReloadConfig()
		{
		}

		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadC();
			ServerConfig config = ServerConfigJSON.GetConfig(ınt32);
			if (config != null && config.ConfigId > 0)
			{
				GameXender.Client.Config = config;
				CLogger.Print(string.Format("Configuration (Database) Refills; Config: {0}", ınt32), LoggerType.Command, null);
			}
		}
	}
}