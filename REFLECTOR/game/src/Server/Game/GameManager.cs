namespace Server.Game
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.JSON;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class GameManager
    {
        private readonly string string_0;
        private readonly int int_0;
        public readonly int ServerId;
        public ServerConfig Config;
        public Socket MainSocket;
        public bool ServerIsClosed;

        public GameManager(int int_1, string string_1, int int_2)
        {
            this.string_0 = string_1;
            this.int_0 = int_2;
            this.ServerId = int_1;
        }

        public void AddSession(GameClient Client)
        {
            try
            {
                if (Client == null)
                {
                    CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning, null);
                }
                else
                {
                    DateTime time = DateTimeUtil.Now();
                    int key = 1;
                    while (true)
                    {
                        if (key >= 0x186a0)
                        {
                            CLogger.Print($"Unable to add session list. IPAddress: {Client.GetIPAddress()}; Date: {time}", LoggerType.Warning, null);
                            Client.Close(500, true, false);
                        }
                        else
                        {
                            if (GameXender.SocketSessions.ContainsKey(key) || !GameXender.SocketSessions.TryAdd(key, Client))
                            {
                                key++;
                                continue;
                            }
                            Client.SessionDate = time;
                            Client.SessionId = key;
                            Client.SessionSeed = (ushort) new Random(time.Millisecond).Next(key, 0x7fff);
                            Client.StartSession();
                        }
                        break;
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public int KickActiveClient(double Hours)
        {
            int num = 0;
            DateTime time = DateTimeUtil.Now();
            using (IEnumerator<GameClient> enumerator = GameXender.SocketSessions.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Account player = enumerator.Current.Player;
                    if (((player != null) && ((player.Room == null) && ((player.ChannelId > -1) && !player.IsGM()))) && ((time - player.LastLobbyEnter).TotalHours >= Hours))
                    {
                        num++;
                        player.Close(0x1388, false);
                    }
                }
            }
            return num;
        }

        public int KickCountActiveClient(double Hours)
        {
            int num = 0;
            DateTime time = DateTimeUtil.Now();
            using (IEnumerator<GameClient> enumerator = GameXender.SocketSessions.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Account player = enumerator.Current.Player;
                    if (((player != null) && ((player.Room == null) && ((player.ChannelId > -1) && !player.IsGM()))) && ((time - player.LastLobbyEnter).TotalHours >= Hours))
                    {
                        num++;
                    }
                }
            }
            return num;
        }

        private void method_0()
        {
            try
            {
                this.MainSocket.BeginAccept(new AsyncCallback(this.method_1), this.MainSocket);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private void method_1(IAsyncResult iasyncResult_0)
        {
            if (!this.ServerIsClosed)
            {
                Socket asyncState = iasyncResult_0.AsyncState as Socket;
                Socket socket2 = null;
                try
                {
                    socket2 = asyncState.EndAccept(iasyncResult_0);
                }
                catch (Exception exception)
                {
                    CLogger.Print($"Accept Callback Date: {DateTimeUtil.Now()}; Exception: {exception.Message}", LoggerType.Error, null);
                }
                this.method_2(socket2, asyncState);
            }
        }

        private void method_2(Socket socket_0, Socket socket_1)
        {
            try
            {
                Thread.Sleep(5);
                this.method_0();
                if (socket_0 != null)
                {
                    this.AddSession(new GameClient(this.ServerId, socket_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        [CompilerGenerated]
        private void method_3(object object_0)
        {
            try
            {
                this.method_0();
            }
            catch (Exception exception)
            {
                CLogger.Print("Error processing TCP packet from " + this.string_0 + ": " + exception.Message, LoggerType.Error, exception);
            }
        }

        public bool RemoveSession(GameClient Client)
        {
            try
            {
                bool flag;
                if ((Client == null) || (Client.SessionId == 0))
                {
                    flag = false;
                }
                else
                {
                    GameClient client;
                    if (!GameXender.SocketSessions.ContainsKey(Client.SessionId) || !GameXender.SocketSessions.TryRemove(Client.SessionId, out client))
                    {
                        Client = null;
                        goto TR_0000;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                return flag;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        TR_0000:
            return false;
        }

        public Account SearchActiveClient(long PlayerId)
        {
            Account account2;
            if (GameXender.SocketSessions.Count == 0)
            {
                return null;
            }
            using (IEnumerator<GameClient> enumerator = GameXender.SocketSessions.Values.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Account player = enumerator.Current.Player;
                        if ((player == null) || (player.PlayerId != PlayerId))
                        {
                            continue;
                        }
                        account2 = player;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return account2;
        }

        public Account SearchActiveClient(uint SessionId)
        {
            Account player;
            if (GameXender.SocketSessions.Count == 0)
            {
                return null;
            }
            using (IEnumerator<GameClient> enumerator = GameXender.SocketSessions.Values.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        GameClient current = enumerator.Current;
                        if ((current.Player == null) || (current.SessionId != SessionId))
                        {
                            continue;
                        }
                        player = current.Player;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return player;
        }

        public int SendPacketToAllClients(GameServerPacket Packet)
        {
            int num = 0;
            if (GameXender.SocketSessions.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("GameManager.SendPacketToAllClients");
                using (IEnumerator<GameClient> enumerator = GameXender.SocketSessions.Values.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Account player = enumerator.Current.Player;
                        if ((player != null) && player.IsOnline)
                        {
                            player.SendCompletePacket(completeBytes, Packet.GetType().Name);
                            num++;
                        }
                    }
                }
            }
            return num;
        }

        public bool Start()
        {
            try
            {
                this.Config = ServerConfigJSON.GetConfig(ConfigLoader.ConfigId);
                this.MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(this.string_0), this.int_0);
                this.MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                this.MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
                this.MainSocket.DontFragment = false;
                this.MainSocket.NoDelay = true;
                this.MainSocket.Bind(localEP);
                this.MainSocket.Listen(ConfigLoader.BackLog);
                CLogger.Print($"Game Serv Address {this.string_0}:{this.int_0}", LoggerType.Info, null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_3));
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }
    }
}

