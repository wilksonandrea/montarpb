namespace Server.Game.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.XML;
    using Server.Game;
    using Server.Game.Data.Models;
    using System;
    using System.Net;

    public class SendFriendInfo
    {
        public static void Load(Account Player, FriendModel Friend, int Type)
        {
            try
            {
                if (Player != null)
                {
                    SChannelModel server = GameXender.Sync.GetServer(Player.Status);
                    if (server != null)
                    {
                        IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            packet.WriteH((short) 0x11);
                            packet.WriteQ(Player.PlayerId);
                            packet.WriteC((byte) Type);
                            packet.WriteQ(Friend.PlayerId);
                            if (Type != 2)
                            {
                                packet.WriteC((byte) Friend.State);
                                packet.WriteC((byte) Friend.Removed);
                            }
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

