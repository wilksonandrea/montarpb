namespace Server.Game.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game;
    using Server.Game.Data.Models;
    using Server.Game.Data.XML;
    using System;
    using System.Net;

    public class UpdateServer
    {
        private static DateTime dateTime_0;

        public static void RefreshSChannel(int serverId)
        {
            try
            {
                if (ComDiv.GetDuration(dateTime_0) >= ConfigLoader.UpdateIntervalPlayersServer)
                {
                    dateTime_0 = DateTimeUtil.Now();
                    int num = 0;
                    foreach (ChannelModel model in ChannelsXML.Channels)
                    {
                        num += model.Players.Count;
                    }
                    foreach (SChannelModel model2 in SChannelXML.Servers)
                    {
                        if (model2.Id == serverId)
                        {
                            model2.LastPlayers = num;
                            continue;
                        }
                        IPEndPoint connection = SynchronizeXML.GetServer(model2.Port).Connection;
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            packet.WriteH((short) 15);
                            packet.WriteD(serverId);
                            packet.WriteD(num);
                            GameXender.Sync.SendPacket(packet.ToArray(), connection);
                        }
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

