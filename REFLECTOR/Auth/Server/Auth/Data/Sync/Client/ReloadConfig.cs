namespace Server.Auth.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.JSON;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Auth;
    using System;

    public class ReloadConfig
    {
        public static void Load(SyncClientPacket C)
        {
            int configId = C.ReadC();
            ServerConfig config = ServerConfigJSON.GetConfig(configId);
            if ((config != null) && (config.ConfigId > 0))
            {
                AuthXender.Client.Config = config;
                CLogger.Print($"Configuration (Database) Refills; Config: {configId}", LoggerType.Command, null);
            }
        }
    }
}

