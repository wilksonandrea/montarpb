namespace Server.Auth.Data.Sync.Server
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.XML;
    using Server.Auth;
    using Server.Auth.Data.Managers;
    using Server.Auth.Data.Models;
    using System;
    using System.Net;

    public static class SendRefresh
    {
        public static void RefreshAccount(Account Player, bool IsConnect)
        {
            try
            {
                UpdateServer.RefreshSChannel(0);
                AccountManager.GetFriendlyAccounts(Player.Friend);
                foreach (FriendModel model in Player.Friend.Friends)
                {
                    PlayerInfo info = model.Info;
                    if (info != null)
                    {
                        SChannelModel server = SChannelXML.GetServer(info.Status.ServerId);
                        if (server != null)
                        {
                            SendRefreshPacket(0, Player.PlayerId, model.PlayerId, IsConnect, server);
                        }
                    }
                }
                if (Player.ClanId > 0)
                {
                    foreach (Account account in Player.ClanPlayers)
                    {
                        if ((account != null) && account.IsOnline)
                        {
                            SChannelModel server = SChannelXML.GetServer(account.Status.ServerId);
                            if (server != null)
                            {
                                SendRefreshPacket(1, Player.PlayerId, account.PlayerId, IsConnect, server);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public static void SendRefreshPacket(int Type, long PlayerId, long MemberId, bool IsConnect, SChannelModel Server)
        {
            IPEndPoint connection = SynchronizeXML.GetServer(Server.Port).Connection;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteH((short) 11);
                packet.WriteC((byte) Type);
                packet.WriteC((byte) IsConnect);
                packet.WriteQ(PlayerId);
                packet.WriteQ(MemberId);
                AuthXender.Sync.SendPacket(packet.ToArray(), connection);
            }
        }
    }
}

