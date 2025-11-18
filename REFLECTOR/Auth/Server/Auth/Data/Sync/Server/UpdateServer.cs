namespace Server.Auth.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Auth;
    using System;
    using System.Net;

    public class UpdateServer
    {
        private static DateTime dateTime_0;

        public static void RefreshSChannel(int ServerId)
        {
            try
            {
                if (ComDiv.GetDuration(dateTime_0) >= ConfigLoader.UpdateIntervalPlayersServer)
                {
                    dateTime_0 = DateTimeUtil.Now();
                    int count = AuthXender.SocketSessions.Count;
                    foreach (SChannelModel model in SChannelXML.Servers)
                    {
                        if (model.Id == ServerId)
                        {
                            model.LastPlayers = count;
                            continue;
                        }
                        IPEndPoint connection = SynchronizeXML.GetServer(model.Port).Connection;
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            packet.WriteH((short) 15);
                            packet.WriteD(ServerId);
                            packet.WriteD(count);
                            AuthXender.Sync.SendPacket(packet.ToArray(), connection);
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

