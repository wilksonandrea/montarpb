namespace Server.Auth.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.XML;
    using Server.Auth;
    using Server.Auth.Data.Models;
    using System;
    using System.Net;

    public class AuthLogin
    {
        public static void SendLoginKickInfo(Account Player)
        {
            try
            {
                int serverId = Player.Status.ServerId;
                if ((serverId == 0xff) || (serverId == 0))
                {
                    Player.SetOnlineStatus(false);
                }
                else
                {
                    SChannelModel server = SChannelXML.GetServer(serverId);
                    if (server != null)
                    {
                        IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            packet.WriteH((short) 10);
                            packet.WriteQ(Player.PlayerId);
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

