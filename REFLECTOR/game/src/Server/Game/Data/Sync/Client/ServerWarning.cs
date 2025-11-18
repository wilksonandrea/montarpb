namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Network;
    using Plugin.Core.XML;
    using Server.Game;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Threading;

    public class ServerWarning
    {
        public static void LoadGMWarning(SyncClientPacket C)
        {
            string valor = C.ReadS(C.ReadC());
            string str2 = C.ReadS(C.ReadC());
            string str3 = C.ReadS(C.ReadH());
            Account account = AccountManager.GetAccountDB(valor, 0, 0x1f);
            if ((account != null) && ((account.Password == str2) && (account.Access >= AccessLevel.GAMEMASTER)))
            {
                int num = 0;
                using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_server_message_announce_ack = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str3))
                {
                    num = GameXender.Client.SendPacketToAllClients(protocol_server_message_announce_ack);
                }
                CLogger.Print($"Message sent to '{num}' Players: '{str3}'; by Username: '{valor}'", LoggerType.Command, null);
            }
        }

        public static void LoadServerUpdate(SyncClientPacket C)
        {
            int serverId = C.ReadC();
            SChannelXML.UpdateServer(serverId);
            CLogger.Print($"Server updated. (Id: {serverId})", LoggerType.Command, null);
        }

        public static void LoadShopRestart(SyncClientPacket C)
        {
            int type = C.ReadC();
            ShopManager.Reset();
            ShopManager.Load(type);
            CLogger.Print($"Shop restarted. (Type: {type})", LoggerType.Command, null);
        }

        public static void LoadShutdown(SyncClientPacket C)
        {
            string valor = C.ReadS(C.ReadC());
            string str2 = C.ReadS(C.ReadC());
            Account account = AccountManager.GetAccountDB(valor, 0, 0x1f);
            if ((account != null) && ((account.Password == str2) && (account.Access >= AccessLevel.GAMEMASTER)))
            {
                int num = 0;
                foreach (GameClient local1 in GameXender.SocketSessions.Values)
                {
                    local1.Client.Shutdown(SocketShutdown.Both);
                    local1.Client.Close(0x2710);
                    num++;
                }
                CLogger.Print($"Disconnected Players: {num} (By: {valor})", LoggerType.Warning, null);
                GameXender.Client.ServerIsClosed = true;
                GameXender.Client.MainSocket.Close(0x1388);
                CLogger.Print("1/2 Step", LoggerType.Warning, null);
                Thread.Sleep(0x1388);
                GameXender.Sync.Close();
                CLogger.Print("2/2 Step", LoggerType.Warning, null);
                using (IEnumerator<GameClient> enumerator = GameXender.SocketSessions.Values.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.Close(0, true, false);
                    }
                }
                CLogger.Print($"Shutdowned Server: {num} players disconnected; by Login: '" + valor + ";", LoggerType.Command, null);
            }
        }
    }
}

