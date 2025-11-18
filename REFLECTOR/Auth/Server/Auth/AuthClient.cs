namespace Server.Auth
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Auth.Data.Models;
    using Server.Auth.Data.Sync.Server;
    using Server.Auth.Network;
    using Server.Auth.Network.ClientPacket;
    using Server.Auth.Network.ServerPacket;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class AuthClient : IDisposable
    {
        public int ServerId;
        public Socket Client;
        public Account Player;
        public DateTime SessionDate;
        public int SessionId;
        public ushort SessionSeed;
        private ushort ushort_0;
        public int SessionShift;
        public int FirstPacketId;
        private bool bool_0;
        private bool bool_1;
        private readonly SafeHandle safeHandle_0 = new SafeFileHandle(IntPtr.Zero, true);
        private readonly ushort[] ushort_1 = new ushort[] { 0x406, 0x44b, 0x9c3 };

        public AuthClient(int int_0, Socket socket_0)
        {
            this.ServerId = int_0;
            this.Client = socket_0;
        }

        public void CheckOut(byte[] BufferTotal, int FirstLength)
        {
            int length = BufferTotal.Length;
            try
            {
                if (length > (FirstLength + 3))
                {
                    byte[] destinationArray = new byte[(length - FirstLength) - 3];
                    Array.Copy(BufferTotal, FirstLength + 3, destinationArray, 0, destinationArray.Length);
                    if (destinationArray.Length != 0)
                    {
                        int num2 = BitConverter.ToUInt16(destinationArray, 0) & 0x7fff;
                        if ((num2 <= 0) || (num2 > (destinationArray.Length - 2)))
                        {
                            CLogger.Print($"Invalid PacketLength in CheckOut. IP: {this.GetIPAddress()}; Length: {num2}; RawBytes: {destinationArray.Length}", LoggerType.Hack, null);
                            this.Close(0, true);
                        }
                        else
                        {
                            byte[] buffer2 = new byte[num2];
                            Array.Copy(destinationArray, 2, buffer2, 0, buffer2.Length);
                            byte[] buffer3 = Bitwise.Decrypt(buffer2, this.SessionShift);
                            ushort num3 = BitConverter.ToUInt16(buffer3, 0);
                            ushort packetSeed = BitConverter.ToUInt16(buffer3, 2);
                            if (!this.CheckSeed(packetSeed, false))
                            {
                                this.Close(0, true);
                            }
                            else
                            {
                                this.method_7(num3, buffer3, "REQ");
                                this.CheckOut(destinationArray, num2);
                            }
                        }
                    }
                }
            }
            catch
            {
                this.Close(0, true);
            }
        }

        public bool CheckSeed(ushort PacketSeed, bool IsTheFirstPacket)
        {
            if (PacketSeed == this.method_6())
            {
                return true;
            }
            CLogger.Print($"Connection blocked. IP: {this.GetIPAddress()}; Date: {DateTimeUtil.Now()}; SessionId: {this.SessionId}; PacketSeed: {PacketSeed} / NextSessionSeed: {this.ushort_0}; PrimarySeed: {this.SessionSeed}", LoggerType.Hack, null);
            if (IsTheFirstPacket)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_12));
            }
            return false;
        }

        public void Close(int TimeMS, bool DestroyConnection)
        {
            if (!this.bool_1)
            {
                try
                {
                    this.bool_1 = true;
                    AuthXender.Client.RemoveSession(this);
                    Account player = this.Player;
                    if (!DestroyConnection)
                    {
                        if (player != null)
                        {
                            player.SimpleClear();
                            player.UpdateCacheInfo();
                            this.Player = null;
                        }
                    }
                    else
                    {
                        if (player != null)
                        {
                            player.SetOnlineStatus(false);
                            if (player.Status.ServerId == 0)
                            {
                                SendRefresh.RefreshAccount(player, false);
                            }
                            player.Status.ResetData(player.PlayerId);
                            player.SimpleClear();
                            player.UpdateCacheInfo();
                            this.Player = null;
                        }
                        if (this.Client != null)
                        {
                            this.Client.Close(TimeMS);
                        }
                        Thread.Sleep(TimeMS);
                        this.Dispose();
                    }
                    UpdateServer.RefreshSChannel(this.ServerId);
                }
                catch (Exception exception)
                {
                    CLogger.Print("AuthClient.Close: " + exception.Message, LoggerType.Error, exception);
                }
            }
        }

        public void Dispose()
        {
            try
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!this.bool_0)
                {
                    this.Player = null;
                    if (this.Client != null)
                    {
                        this.Client.Dispose();
                        this.Client = null;
                    }
                    if (disposing)
                    {
                        this.safeHandle_0.Dispose();
                    }
                    this.bool_0 = true;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        public IPAddress GetAddress()
        {
            try
            {
                return (((this.Client == null) || (this.Client.RemoteEndPoint == null)) ? null : (this.Client.RemoteEndPoint as IPEndPoint).Address);
            }
            catch
            {
                return null;
            }
        }

        public string GetIPAddress()
        {
            try
            {
                return (((this.Client == null) || (this.Client.RemoteEndPoint == null)) ? "" : (this.Client.RemoteEndPoint as IPEndPoint).Address.ToString());
            }
            catch
            {
                return "";
            }
        }

        public void HeartBeatCounter()
        {
            Class0 class2 = new Class0 {
                authClient_0 = this,
                timerState_0 = new Plugin.Core.Utility.TimerState()
            };
            class2.timerState_0.StartTimer(TimeSpan.FromMinutes(20.0), new TimerCallback(class2.method_0));
        }

        private void method_0()
        {
            Thread.Sleep(0x2710);
            if ((this.Client != null) && (this.FirstPacketId == 0))
            {
                CLogger.Print("Connection destroyed due to no responses. IPAddress: " + this.GetIPAddress(), LoggerType.Hack, null);
                this.Close(0, true);
            }
        }

        private void method_1()
        {
            this.SendPacket(new PROTOCOL_BASE_CONNECT_ACK(this));
        }

        [CompilerGenerated]
        private void method_10(object object_0)
        {
            this.method_0();
        }

        [CompilerGenerated]
        private void method_11(object object_0)
        {
            try
            {
                this.method_3();
            }
            catch (Exception exception)
            {
                CLogger.Print("Error processing packet in thread pool for IP: " + this.GetIPAddress() + ": " + exception.Message, LoggerType.Error, exception);
                this.Close(0, true);
            }
        }

        [CompilerGenerated]
        private void method_12(object object_0)
        {
            this.method_3();
        }

        private void method_2(IAsyncResult iasyncResult_0)
        {
            try
            {
                Socket asyncState = iasyncResult_0.AsyncState as Socket;
                if ((asyncState != null) && asyncState.Connected)
                {
                    asyncState.EndSend(iasyncResult_0);
                }
            }
            catch
            {
                this.Close(0, true);
            }
        }

        private void method_3()
        {
            try
            {
                StateObject obj1 = new StateObject();
                obj1.WorkSocket = this.Client;
                obj1.RemoteEP = new IPEndPoint(IPAddress.Any, 0);
                StateObject state = obj1;
                this.Client.BeginReceive(state.TcpBuffer, 0, 0x22d0, SocketFlags.None, new AsyncCallback(this.method_4), state);
            }
            catch
            {
                this.Close(0, true);
            }
        }

        private void method_4(IAsyncResult iasyncResult_0)
        {
            StateObject asyncState = iasyncResult_0.AsyncState as StateObject;
            try
            {
                int length = asyncState.WorkSocket.EndReceive(iasyncResult_0);
                if (length > 0)
                {
                    if (length > 0x22d0)
                    {
                        CLogger.Print("Received data exceeds buffer size. IP: " + this.GetIPAddress(), LoggerType.Error, null);
                        this.Close(0, true);
                    }
                    else
                    {
                        byte[] destinationArray = new byte[length];
                        Array.Copy(asyncState.TcpBuffer, 0, destinationArray, 0, length);
                        int num2 = BitConverter.ToUInt16(destinationArray, 0) & 0x7fff;
                        if ((num2 <= 0) || (num2 > (length - 2)))
                        {
                            CLogger.Print($"Invalid PacketLength. IP: {this.GetIPAddress()}; Length: {num2}; RawBytes: {length}", LoggerType.Hack, null);
                            this.Close(0, true);
                        }
                        else
                        {
                            byte[] buffer2 = new byte[num2];
                            Array.Copy(destinationArray, 2, buffer2, 0, buffer2.Length);
                            byte[] buffer3 = Bitwise.Decrypt(buffer2, this.SessionShift);
                            ushort num3 = BitConverter.ToUInt16(buffer3, 0);
                            ushort packetSeed = BitConverter.ToUInt16(buffer3, 2);
                            this.method_5(num3);
                            if (!this.CheckSeed(packetSeed, true))
                            {
                                this.Close(0, true);
                            }
                            else if (!this.bool_1)
                            {
                                this.method_7(num3, buffer3, "REQ");
                                this.CheckOut(destinationArray, num2);
                                ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_11));
                            }
                        }
                    }
                }
            }
            catch
            {
                this.Close(0, true);
            }
        }

        private void method_5(ushort ushort_2)
        {
            if (this.FirstPacketId == 0)
            {
                this.FirstPacketId = ushort_2;
                if ((ushort_2 != 0x501) && (ushort_2 != 0x905))
                {
                    CLogger.Print($"Connection destroyed due to unknown first packet. Opcode: {ushort_2}; IPAddress: {this.GetIPAddress()}", LoggerType.Hack, null);
                    this.Close(0, true);
                }
            }
        }

        private ushort method_6()
        {
            this.ushort_0 = (ushort) ((((this.ushort_0 * 0x343fd) + 0x269ec3) >> 0x10) & 0x7fff);
            return this.ushort_0;
        }

        private void method_7(ushort ushort_2, byte[] byte_0, string string_0)
        {
            Class1 class2 = new Class1 {
                authClient_0 = this
            };
            try
            {
                class2.authClientPacket_0 = null;
                if (ushort_2 > 0x91c)
                {
                    if (ushort_2 > 0x99b)
                    {
                        if (ushort_2 > 0x9d4)
                        {
                            if (ushort_2 == 0x1e01)
                            {
                                class2.authClientPacket_0 = new PROTOCOL_MATCH_SERVER_IDX_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x1e13)
                            {
                                class2.authClientPacket_0 = new PROTOCOL_MATCH_CLAN_SEASON_REQ();
                                goto TR_000A;
                            }
                        }
                        else if (ushort_2 == 0x9b9)
                        {
                            class2.authClientPacket_0 = new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
                            goto TR_000A;
                        }
                        else if (ushort_2 == 0x9d4)
                        {
                            class2.authClientPacket_0 = new PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_REQ();
                            goto TR_000A;
                        }
                    }
                    else if (ushort_2 == 0x95f)
                    {
                        class2.authClientPacket_0 = new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
                        goto TR_000A;
                    }
                    else if (ushort_2 == 0x96e)
                    {
                        class2.authClientPacket_0 = new PROTOCOL_BASE_DAILY_RECORD_REQ();
                        goto TR_000A;
                    }
                    else if (ushort_2 == 0x99b)
                    {
                        class2.authClientPacket_0 = new PROTOCOL_BASE_GET_MAP_INFO_REQ();
                        goto TR_000A;
                    }
                }
                else if (ushort_2 > 0x907)
                {
                    switch (ushort_2)
                    {
                        case 0x90a:
                            class2.authClientPacket_0 = new PROTOCOL_BASE_GET_SYSTEM_INFO_REQ();
                            goto TR_000A;

                        case 0x90b:
                        case 0x90d:
                        case 0x90f:
                        case 0x911:
                            break;

                        case 0x90c:
                            class2.authClientPacket_0 = new PROTOCOL_BASE_GET_USER_INFO_REQ();
                            goto TR_000A;

                        case 0x90e:
                            class2.authClientPacket_0 = new PROTOCOL_BASE_GET_INVEN_INFO_REQ();
                            goto TR_000A;

                        case 0x910:
                            class2.authClientPacket_0 = new PROTOCOL_BASE_GET_OPTION_REQ();
                            goto TR_000A;

                        case 0x912:
                            class2.authClientPacket_0 = new PROTOCOL_BASE_OPTION_SAVE_REQ();
                            goto TR_000A;

                        default:
                            if (ushort_2 == 0x918)
                            {
                                class2.authClientPacket_0 = new PROTOCOL_BASE_USER_LEAVE_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x91c)
                            {
                                class2.authClientPacket_0 = new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
                                goto TR_000A;
                            }
                            break;
                    }
                }
                else if (ushort_2 == 0x421)
                {
                    class2.authClientPacket_0 = new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
                    goto TR_000A;
                }
                else if (ushort_2 == 0x501)
                {
                    class2.authClientPacket_0 = new PROTOCOL_BASE_LOGIN_REQ();
                    goto TR_000A;
                }
                else
                {
                    switch (ushort_2)
                    {
                        case 0x903:
                            class2.authClientPacket_0 = new PROTOCOL_BASE_LOGOUT_REQ();
                            goto TR_000A;

                        case 0x905:
                            class2.authClientPacket_0 = new PROTOCOL_BASE_KEEP_ALIVE_REQ();
                            goto TR_000A;

                        case 0x907:
                            class2.authClientPacket_0 = new PROTOCOL_BASE_GAMEGUARD_REQ();
                            goto TR_000A;

                        default:
                            break;
                    }
                }
                CLogger.Print(Bitwise.ToHexData($"Opcode Not Found: [{ushort_2}] | {string_0}", byte_0), LoggerType.Opcode, null);
            TR_000A:
                if (class2.authClientPacket_0 != null)
                {
                    using (class2.authClientPacket_0)
                    {
                        if (ConfigLoader.DebugMode)
                        {
                            CLogger.Print($"{class2.authClientPacket_0.GetType().Name}; Address: {this.Client.RemoteEndPoint}; Opcode: [{ushort_2}]", LoggerType.Debug, null);
                        }
                        class2.authClientPacket_0.Makeme(this, byte_0);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(class2.method_0));
                        class2.authClientPacket_0.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }

        [CompilerGenerated]
        private void method_8(object object_0)
        {
            this.method_1();
        }

        [CompilerGenerated]
        private void method_9(object object_0)
        {
            this.method_3();
        }

        public void SendCompletePacket(byte[] OriginalData, string PacketName)
        {
            try
            {
                byte[] bytes = BitConverter.GetBytes((ushort) (OriginalData.Length + 2));
                byte[] destinationArray = new byte[OriginalData.Length + bytes.Length];
                Array.Copy(bytes, 0, destinationArray, 0, bytes.Length);
                Array.Copy(OriginalData, 0, destinationArray, 2, OriginalData.Length);
                byte[] buffer3 = new byte[destinationArray.Length + 5];
                Array.Copy(destinationArray, 0, buffer3, 0, destinationArray.Length);
                if (ConfigLoader.DebugMode)
                {
                    ushort num = BitConverter.ToUInt16(buffer3, 2);
                    Bitwise.ToByteString(buffer3);
                    CLogger.Print($"{PacketName}; Address: {this.Client.RemoteEndPoint}; Opcode: [{num}]", LoggerType.Debug, null);
                }
                Bitwise.ProcessPacket(buffer3, 2, 5, this.ushort_1);
                if ((this.Client != null) && (buffer3.Length != 0))
                {
                    this.Client.BeginSend(buffer3, 0, buffer3.Length, SocketFlags.None, new AsyncCallback(this.method_2), this.Client);
                }
                buffer3 = null;
            }
            catch
            {
                this.Close(0, true);
            }
        }

        public void SendPacket(AuthServerPacket Packet)
        {
            try
            {
                using (Packet)
                {
                    byte[] bytes = Packet.GetBytes("AuthClient.SendPacket");
                    this.SendPacket(bytes, Packet.GetType().Name);
                    Packet.Dispose();
                }
            }
            catch
            {
                this.Close(0, true);
            }
        }

        public void SendPacket(byte[] OriginalData, string PacketName)
        {
            try
            {
                if (OriginalData.Length >= 2)
                {
                    this.SendCompletePacket(OriginalData, PacketName);
                }
            }
            catch
            {
                this.Close(0, true);
            }
        }

        public void StartSession()
        {
            try
            {
                this.ushort_0 = this.SessionSeed;
                this.SessionShift = ((this.SessionId + Bitwise.CRYPTO[0]) % 7) + 1;
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_8));
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_9));
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.method_10));
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                this.Close(0, true);
            }
        }

        [CompilerGenerated]
        private sealed class Class0
        {
            public AuthClient authClient_0;
            public Plugin.Core.Utility.TimerState timerState_0;

            internal void method_0(object object_0)
            {
                if (!this.authClient_0.bool_1)
                {
                    CLogger.Print("Connection destroyed due to no response for 20 minutes (HeartBeat). IPAddress: " + this.authClient_0.GetIPAddress(), LoggerType.Hack, null);
                    this.authClient_0.Close(0, true);
                }
                object obj2 = object_0;
                lock (obj2)
                {
                    this.timerState_0.StopJob();
                }
            }
        }

        [CompilerGenerated]
        private sealed class Class1
        {
            public AuthClient authClient_0;
            public AuthClientPacket authClientPacket_0;

            internal void method_0(object object_0)
            {
                try
                {
                    this.authClientPacket_0.Run();
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                    this.authClient_0.Close(50, true);
                }
            }
        }
    }
}

