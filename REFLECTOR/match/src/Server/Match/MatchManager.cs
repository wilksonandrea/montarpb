namespace Server.Match
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class MatchManager
    {
        private readonly string string_0;
        private readonly int int_0;
        public Socket MainSocket;
        public bool ServerIsClosed;

        public MatchManager(string string_1, int int_1)
        {
            this.string_0 = string_1;
            this.int_0 = int_1;
        }

        public void BeginReceive(MatchClient Client, byte[] Buffer)
        {
            try
            {
                if (Client == null)
                {
                    CLogger.Print("Destroyed after failed to add to list.", LoggerType.Warning, null);
                }
                else
                {
                    Client.BeginReceive(Buffer, DateTimeUtil.Now());
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
                StateObject obj1 = new StateObject();
                obj1.WorkSocket = this.MainSocket;
                obj1.RemoteEP = new IPEndPoint(IPAddress.Any, 0);
                StateObject state = obj1;
                this.MainSocket.BeginReceiveFrom(state.UdpBuffer, 0, 0x1fa0, SocketFlags.None, ref state.RemoteEP, new AsyncCallback(this.method_1), state);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private void method_1(IAsyncResult iasyncResult_0)
        {
            if (!iasyncResult_0.IsCompleted)
            {
                CLogger.Print("IAsyncResult is not completed.", LoggerType.Warning, null);
            }
            StateObject asyncState = iasyncResult_0.AsyncState as StateObject;
            Socket workSocket = asyncState.WorkSocket;
            IPEndPoint remoteEP = asyncState.RemoteEP as IPEndPoint;
            try
            {
                EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                int count = workSocket.EndReceiveFrom(iasyncResult_0, ref endPoint);
                if (count > 0)
                {
                    byte[] dst = new byte[count];
                    Buffer.BlockCopy(asyncState.UdpBuffer, 0, dst, 0, count);
                    if ((dst.Length >= 0x16) && (dst.Length <= 0x1fa0))
                    {
                        this.BeginReceive(new MatchClient(workSocket, endPoint as IPEndPoint), dst);
                    }
                    else
                    {
                        CLogger.Print($"Invalid Buffer Length: {dst.Length}; IP: {remoteEP}", LoggerType.Hack, null);
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("Error during EndReceiveCallback: " + exception.Message, LoggerType.Error, exception);
                this.method_2(remoteEP);
                this.method_3($"{remoteEP.Address}");
            }
            finally
            {
                this.method_0();
            }
        }

        private bool method_2(IPEndPoint ipendPoint_0)
        {
            bool flag;
            try
            {
                if (ipendPoint_0 != null)
                {
                    Socket socket;
                    if (MatchXender.UdpClients.ContainsKey(ipendPoint_0) && MatchXender.UdpClients.TryGetValue(ipendPoint_0, out socket))
                    {
                        flag = MatchXender.UdpClients.TryRemove(ipendPoint_0, out socket);
                    }
                    else
                    {
                        goto TR_0000;
                    }
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                goto TR_0000;
            }
            return flag;
        TR_0000:
            return false;
        }

        private bool method_3(string string_1)
        {
            bool flag;
            try
            {
                if (string.IsNullOrEmpty(string_1) || string_1.Equals("0.0.0.0"))
                {
                    flag = false;
                }
                else
                {
                    int num;
                    if (MatchXender.SpamConnections.ContainsKey(string_1) && MatchXender.SpamConnections.TryGetValue(string_1, out num))
                    {
                        flag = MatchXender.SpamConnections.TryRemove(string_1, out num);
                    }
                    else
                    {
                        goto TR_0000;
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                goto TR_0000;
            }
            return flag;
        TR_0000:
            return false;
        }

        private void method_4(IAsyncResult iasyncResult_0)
        {
            try
            {
                Socket asyncState = iasyncResult_0.AsyncState as Socket;
                if ((asyncState != null) && asyncState.Connected)
                {
                    asyncState.EndSend(iasyncResult_0);
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("Error during EndSendCallback: " + exception.Message, LoggerType.Error, exception);
            }
        }

        [CompilerGenerated]
        private void method_5(object object_0)
        {
            try
            {
                this.method_0();
            }
            catch (Exception exception)
            {
                CLogger.Print("Error processing UDP packet from " + this.string_0 + ": " + exception.Message, LoggerType.Error, exception);
            }
        }

        public void SendPacket(byte[] Data, IPEndPoint Address)
        {
            try
            {
                this.MainSocket.BeginSendTo(Data, 0, Data.Length, SocketFlags.None, Address, new AsyncCallback(this.method_4), this.MainSocket);
            }
            catch (Exception exception)
            {
                CLogger.Print($"Failed to send package to {Address}: {exception.Message}", LoggerType.Error, exception);
            }
        }

        public bool Start()
        {
            try
            {
                this.MainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                byte[] optionInValue = new byte[] { Convert.ToByte(false) };
                this.MainSocket.IOControl(-1744830452, optionInValue, null);
                IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(this.string_0), this.int_0);
                this.MainSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                this.MainSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
                this.MainSocket.DontFragment = false;
                this.MainSocket.Ttl = 0x80;
                this.MainSocket.Bind(localEP);
                CLogger.Print($"Match Serv Address {localEP.Address}:{localEP.Port}", LoggerType.Info, null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_5));
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

