namespace Server.Auth
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.JSON;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Auth.Data.Models;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class AuthManager
    {
        private readonly string string_0;
        private readonly int int_0;
        public readonly int ServerId;
        public ServerConfig Config;
        public Socket MainSocket;
        public bool ServerIsClosed;

        public AuthManager(int int_1, string string_1, int int_2)
        {
            this.string_0 = string_1;
            this.int_0 = int_2;
            this.ServerId = int_1;
        }

        public void AddSession(AuthClient Client)
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
                            Client.Close(500, true);
                        }
                        else
                        {
                            if (AuthXender.SocketSessions.ContainsKey(key) || !AuthXender.SocketSessions.TryAdd(key, Client))
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
                this.method_2(socket2);
            }
        }

        private void method_2(Socket socket_0)
        {
            try
            {
                Thread.Sleep(5);
                this.method_0();
                if (socket_0 != null)
                {
                    this.AddSession(new AuthClient(this.ServerId, socket_0));
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

        public bool RemoveSession(AuthClient Client)
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
                    AuthClient client;
                    if (!AuthXender.SocketSessions.ContainsKey(Client.SessionId) || !AuthXender.SocketSessions.TryRemove(Client.SessionId, out client))
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
            if (AuthXender.SocketSessions.Count == 0)
            {
                return null;
            }
            using (IEnumerator<AuthClient> enumerator = AuthXender.SocketSessions.Values.GetEnumerator())
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

        public int SendPacketToAllClients(AuthServerPacket Packet)
        {
            int num = 0;
            if (AuthXender.SocketSessions.Count != 0)
            {
                byte[] completeBytes = Packet.GetCompleteBytes("AuthManager.SendPacketToAllClients");
                using (IEnumerator<AuthClient> enumerator = AuthXender.SocketSessions.Values.GetEnumerator())
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
                CLogger.Print($"Auth Serv Address {this.string_0}:{this.int_0}", LoggerType.Info, null);
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

