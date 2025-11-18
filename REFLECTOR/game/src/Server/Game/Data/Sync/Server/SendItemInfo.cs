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

    public class SendItemInfo
    {
        public static void LoadGoldCash(Account Player)
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
                            packet.WriteH((short) 0x13);
                            packet.WriteQ(Player.PlayerId);
                            packet.WriteC(0);
                            packet.WriteC((byte) Player.Rank);
                            packet.WriteD(Player.Gold);
                            packet.WriteD(Player.Cash);
                            packet.WriteD(Player.Tags);
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

        public static void LoadItem(Account Player, ItemsModel Item)
        {
            try
            {
                if ((Player != null) && (Player.Status.ServerId != 0))
                {
                    SChannelModel server = GameXender.Sync.GetServer(Player.Status);
                    if (server != null)
                    {
                        IPEndPoint connection = SynchronizeXML.GetServer(server.Port).Connection;
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            packet.WriteH((short) 0x12);
                            packet.WriteQ(Player.PlayerId);
                            packet.WriteQ(Item.ObjectId);
                            packet.WriteD(Item.Id);
                            packet.WriteC((byte) Item.Equip);
                            packet.WriteC((byte) Item.Category);
                            packet.WriteD(Item.Count);
                            packet.WriteC((byte) Item.Name.Length);
                            packet.WriteS(Item.Name, Item.Name.Length);
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

