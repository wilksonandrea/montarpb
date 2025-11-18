namespace Server.Game.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.XML;
    using Server.Game;
    using System;
    using System.Net;

    public class UpdateChannel
    {
        public static void RefreshChannel(int ServerId, int ChannelId, int Count)
        {
            try
            {
                SChannelModel server = GameXender.Sync.GetServer(0);
                if (server != null)
                {
                    IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
                    using (SyncServerPacket packet = new SyncServerPacket())
                    {
                        packet.WriteH((short) 0x21);
                        packet.WriteD(ServerId);
                        packet.WriteD(ChannelId);
                        packet.WriteD(Count);
                        GameXender.Sync.SendPacket(packet.ToArray(), connection);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

