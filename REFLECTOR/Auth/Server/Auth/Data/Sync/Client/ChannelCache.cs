namespace Server.Auth.Data.Sync.Client
{
    using Plugin.Core.Network;
    using Server.Auth.Data.Models;
    using Server.Auth.Data.XML;
    using System;

    public class ChannelCache
    {
        public static void Load(SyncClientPacket C)
        {
            int num2 = C.ReadD();
            ChannelModel channel = ChannelsXML.GetChannel(C.ReadD(), C.ReadD());
            if (channel != null)
            {
                channel.TotalPlayers = num2;
            }
        }
    }
}

