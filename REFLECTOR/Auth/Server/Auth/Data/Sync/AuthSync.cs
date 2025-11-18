namespace Server.Auth.Data.Sync
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Auth;
    using Server.Auth.Data.Sync.Client;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class AuthSync
    {
        protected Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private bool bool_0;

        public AuthSync(IPEndPoint ipendPoint_0)
        {
            this.ClientSocket.Bind(ipendPoint_0);
            byte[] optionInValue = new byte[] { Convert.ToByte(false) };
            this.ClientSocket.IOControl(-1744830452, optionInValue, null);
        }

        public void Close()
        {
            if (!this.bool_0)
            {
                this.bool_0 = true;
                try
                {
                    if (this.ClientSocket != null)
                    {
                        this.ClientSocket.Close();
                        this.ClientSocket.Dispose();
                        this.ClientSocket = null;
                    }
                }
                catch (Exception exception)
                {
                    CLogger.Print("Error closing AuthSync: " + exception.Message, LoggerType.Error, exception);
                }
            }
        }

        private void method_0()
        {
            if (!this.bool_0)
            {
                try
                {
                    StateObject obj1 = new StateObject();
                    obj1.WorkSocket = this.ClientSocket;
                    obj1.RemoteEP = new IPEndPoint(IPAddress.Any, 0x1f40);
                    StateObject state = obj1;
                    this.ClientSocket.BeginReceiveFrom(state.UdpBuffer, 0, 0x1fa0, SocketFlags.None, ref state.RemoteEP, new AsyncCallback(this.method_1), state);
                }
                catch (ObjectDisposedException)
                {
                    CLogger.Print("AuthSync socket disposed during StartReceive.", LoggerType.Warning, null);
                    this.Close();
                }
                catch (Exception exception)
                {
                    CLogger.Print("Error in StartReceive: " + exception.Message, LoggerType.Error, exception);
                    this.Close();
                }
            }
        }

        private void method_1(IAsyncResult iasyncResult_0)
        {
            Class3 class2 = new Class3 {
                authSync_0 = this
            };
            if (!this.bool_0 && ((AuthXender.Client != null) && !AuthXender.Client.ServerIsClosed))
            {
                StateObject asyncState = iasyncResult_0.AsyncState as StateObject;
                try
                {
                    int length = this.ClientSocket.EndReceiveFrom(iasyncResult_0, ref asyncState.RemoteEP);
                    if (length > 0)
                    {
                        class2.byte_0 = new byte[length];
                        Array.Copy(asyncState.UdpBuffer, 0, class2.byte_0, 0, length);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(class2.method_0));
                    }
                }
                catch (ObjectDisposedException)
                {
                    CLogger.Print("AuthSync socket disposed during ReceiveCallback.", LoggerType.Warning, null);
                    this.Close();
                }
                catch (Exception exception)
                {
                    CLogger.Print("Error in ReceiveCallback: " + exception.Message, LoggerType.Error, exception);
                }
                finally
                {
                    this.method_0();
                }
            }
        }

        private void method_2(byte[] byte_0)
        {
            try
            {
                SyncClientPacket c = new SyncClientPacket(byte_0);
                short num = c.ReadH();
                switch (num)
                {
                    case 11:
                        FriendInfo.Load(c);
                        return;

                    case 12:
                    case 14:
                    case 0x12:
                    case 0x15:
                    case 0x19:
                    case 0x1a:
                    case 0x1b:
                    case 0x1c:
                    case 0x1d:
                    case 30:
                        break;

                    case 13:
                        AccountInfo.Load(c);
                        return;

                    case 15:
                        ServerCache.Load(c);
                        return;

                    case 0x10:
                        ClanSync.Load(c);
                        return;

                    case 0x11:
                        FriendSync.Load(c);
                        return;

                    case 0x13:
                        PlayerSync.Load(c);
                        return;

                    case 20:
                        ServerWarning.LoadGMWarning(c);
                        return;

                    case 0x16:
                        ServerWarning.LoadShopRestart(c);
                        return;

                    case 0x17:
                        ServerWarning.LoadServerUpdate(c);
                        return;

                    case 0x18:
                        ServerWarning.LoadShutdown(c);
                        return;

                    case 0x1f:
                        EventInfo.LoadEventInfo(c);
                        return;

                    case 0x20:
                        ReloadConfig.Load(c);
                        return;

                    case 0x21:
                        ChannelCache.Load(c);
                        return;

                    case 0x22:
                        ReloadPermn.Load(c);
                        return;

                    default:
                        if (num != 0x1c03)
                        {
                            break;
                        }
                        ServerMessage.Load(c);
                        return;
                }
                CLogger.Print(Bitwise.ToHexData($"Auth - Opcode Not Found: [{num}]", c.ToArray()), LoggerType.Opcode, null);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        private void method_3(IAsyncResult iasyncResult_0)
        {
            try
            {
                Socket asyncState = iasyncResult_0.AsyncState as Socket;
                if ((asyncState != null) && asyncState.Connected)
                {
                    asyncState.EndSend(iasyncResult_0);
                }
            }
            catch (ObjectDisposedException)
            {
                CLogger.Print("AuthSync socket disposed during SendCallback.", LoggerType.Warning, null);
            }
            catch (Exception exception)
            {
                CLogger.Print("Error in SendCallback: " + exception.Message, LoggerType.Error, exception);
            }
        }

        [CompilerGenerated]
        private void method_4(object object_0)
        {
            this.method_0();
        }

        public void SendPacket(byte[] Data, IPEndPoint Address)
        {
            if (!this.bool_0)
            {
                try
                {
                    this.ClientSocket.BeginSendTo(Data, 0, Data.Length, SocketFlags.None, Address, new AsyncCallback(this.method_3), this.ClientSocket);
                }
                catch (ObjectDisposedException)
                {
                    CLogger.Print($"AuthSync socket disposed during SendPacket to {Address}.", LoggerType.Warning, null);
                }
                catch (Exception exception)
                {
                    CLogger.Print($"Error sending UDP packet to {Address}: {exception.Message}", LoggerType.Error, exception);
                }
            }
        }

        public bool Start()
        {
            try
            {
                IPEndPoint localEndPoint = this.ClientSocket.LocalEndPoint as IPEndPoint;
                CLogger.Print($"Auth Sync Address {localEndPoint.Address}:{localEndPoint.Port}", LoggerType.Info, null);
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_4));
                return true;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                return false;
            }
        }

        [CompilerGenerated]
        private sealed class Class3
        {
            public AuthSync authSync_0;
            public byte[] byte_0;

            internal void method_0(object object_0)
            {
                try
                {
                    this.authSync_0.method_2(this.byte_0);
                }
                catch (Exception exception)
                {
                    CLogger.Print("Error processing AuthSync packet in thread pool: " + exception.Message, LoggerType.Error, exception);
                }
            }
        }
    }
}

