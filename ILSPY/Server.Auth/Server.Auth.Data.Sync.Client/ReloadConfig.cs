using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.JSON;
using Plugin.Core.Models;
using Plugin.Core.Network;

namespace Server.Auth.Data.Sync.Client;

public class ReloadConfig
{
	public static void Load(SyncClientPacket C)
	{
		int num = C.ReadC();
		ServerConfig config = ServerConfigJSON.GetConfig(num);
		if (config != null && config.ConfigId > 0)
		{
			AuthXender.Client.Config = config;
			CLogger.Print($"Configuration (Database) Refills; Config: {num}", LoggerType.Command);
		}
	}
}
