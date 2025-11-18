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

    public class SendClanInfo
    {
        public static void Load(ClanModel Clan, int Type)
        {
            try
            {
                foreach (SChannelModel model in SChannelXML.Servers)
                {
                    if ((model.Id != 0) && (model.Id != GameXender.Client.ServerId))
                    {
                        IPEndPoint connection = SynchronizeXML.GetServer(model.Port).Connection;
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            packet.WriteH((short) 0x15);
                            packet.WriteC((byte) Type);
                            packet.WriteD(Clan.Id);
                            if (Type == 0)
                            {
                                packet.WriteQ(Clan.OwnerId);
                                packet.WriteD(Clan.CreationDate);
                                packet.WriteC((byte) (Clan.Name.Length + 1));
                                packet.WriteS(Clan.Name, Clan.Name.Length + 1);
                                packet.WriteC((byte) (Clan.Info.Length + 1));
                                packet.WriteS(Clan.Info, Clan.Info.Length + 1);
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

        public static void Load(Account Player, Account Member, int Type)
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
                            packet.WriteH((short) 0x10);
                            packet.WriteQ(Player.PlayerId);
                            packet.WriteC((byte) Type);
                            if (Type == 1)
                            {
                                packet.WriteQ(Member.PlayerId);
                                packet.WriteC((byte) (Member.Nickname.Length + 1));
                                packet.WriteS(Member.Nickname, Member.Nickname.Length + 1);
                                packet.WriteB(Member.Status.Buffer);
                                packet.WriteC((byte) Member.Rank);
                            }
                            else if (Type == 2)
                            {
                                packet.WriteQ(Member.PlayerId);
                            }
                            else if (Type == 3)
                            {
                                packet.WriteD(Player.ClanId);
                                packet.WriteC((byte) Player.ClanAccess);
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

        public static void Update(ClanModel Clan, int Type)
        {
            try
            {
                foreach (SChannelModel model in SChannelXML.Servers)
                {
                    if ((model.Id != 0) && (model.Id != GameXender.Client.ServerId))
                    {
                        IPEndPoint connection = SynchronizeXML.GetServer(model.Port).Connection;
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            packet.WriteH((short) 0x16);
                            packet.WriteC((byte) Type);
                            if (Type == 0)
                            {
                                packet.WriteQ(Clan.OwnerId);
                            }
                            else if (Type == 1)
                            {
                                packet.WriteC((byte) (Clan.Name.Length + 1));
                                packet.WriteS(Clan.Name, Clan.Name.Length + 1);
                            }
                            else if (Type == 2)
                            {
                                packet.WriteC((byte) Clan.NameColor);
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

