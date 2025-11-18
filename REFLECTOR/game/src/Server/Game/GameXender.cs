namespace Server.Game
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.XML;
    using Server.Game.Data.Sync;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;

    public class GameXender
    {
        public static unsafe bool GetPlugin(int ServerId, string Host, int Port)
        {
            try
            {
                SocketSessions = new ConcurrentDictionary<int, GameClient>();
                SocketConnections = new ConcurrentDictionary<string, int>();
                Sync = new GameSync(SynchronizeXML.GetServer(Port).Connection);
                int* numPtr1 = &(ConfigLoader.DEFAULT_PORT[1]);
                int num = numPtr1[0];
                numPtr1[0] = num + 1;
                Client = new GameManager(ServerId, Host, num);
                Sync.Start();
                Client.Start();
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        public static void UpdateEvents()
        {
            foreach (GameClient client in SocketSessions.Values)
            {
                if (client != null)
                {
                    client.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(true));
                }
            }
        }

        public static void UpdateShop()
        {
            foreach (GameClient client in SocketSessions.Values)
            {
                if (client != null)
                {
                    foreach (ShopData data in ShopManager.ShopDataItems)
                    {
                        client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(data, ShopManager.TotalItems));
                    }
                    foreach (ShopData data2 in ShopManager.ShopDataGoods)
                    {
                        client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(data2, ShopManager.TotalGoods));
                    }
                    foreach (ShopData data3 in ShopManager.ShopDataItemRepairs)
                    {
                        client.SendPacket(new PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(data3, ShopManager.TotalRepairs));
                    }
                    foreach (ShopData data4 in BattleBoxXML.ShopDataBattleBoxes)
                    {
                        client.SendPacket(new PROTOCOL_BATTLEBOX_GET_LIST_ACK(data4, BattleBoxXML.TotalBoxes));
                    }
                    client.SendPacket(new PROTOCOL_SHOP_TAG_INFO_ACK());
                    client.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(true));
                }
            }
        }

        public static GameSync Sync { get; set; }

        public static GameManager Client { get; set; }

        public static ConcurrentDictionary<int, GameClient> SocketSessions { get; set; }

        public static ConcurrentDictionary<string, int> SocketConnections { get; set; }
    }
}

