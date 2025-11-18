namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.XML;
    using System;

    public class ServerCache
    {
        public static void Load(SyncClientPacket C)
        {
            int num = C.ReadD();
            SChannelModel server = SChannelXML.GetServer(C.ReadD());
            if (server != null)
            {
                server.LastPlayers = num;
            }
        }
    }
}

