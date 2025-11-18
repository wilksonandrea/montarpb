namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.JSON;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game;
    using System;

    public class ReloadConfig
    {
        public static void Load(SyncClientPacket C)
        {
            int configId = C.ReadC();
            ServerConfig config = ServerConfigJSON.GetConfig(configId);
            if ((config != null) && (config.ConfigId > 0))
            {
                GameXender.Client.Config = config;
                CLogger.Print($"Configuration (Database) Refills; Config: {configId}", LoggerType.Command, null);
            }
        }
    }
}

