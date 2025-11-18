namespace Server.Game
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ClientPacket;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class GameClient : IDisposable
    {
        public int ServerId;
        public long PlayerId;
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

        public GameClient(int int_0, Socket socket_0)
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
                            this.Close(0, true, false);
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
                                this.Close(0, true, false);
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
                this.Close(0, true, false);
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

        public void Close(int TimeMS, bool DestroyConnection, bool Kicked = false)
        {
            if (!this.bool_1)
            {
                try
                {
                    this.bool_1 = true;
                    GameXender.Client.RemoveSession(this);
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
                        if ((this.PlayerId > 0L) && (player != null))
                        {
                            player.SetOnlineStatus(false);
                            RoomModel room = player.Room;
                            if (room != null)
                            {
                                room.RemovePlayer(player, false, (int) Kicked);
                            }
                            MatchModel match = player.Match;
                            if (match != null)
                            {
                                match.RemovePlayer(player);
                            }
                            ChannelModel channel = player.GetChannel();
                            if (channel != null)
                            {
                                channel.RemovePlayer(player);
                            }
                            player.Status.ResetData(this.PlayerId);
                            AllUtils.SyncPlayerToFriends(player, false);
                            AllUtils.SyncPlayerToClanMembers(player);
                            player.SimpleClear();
                            player.UpdateCacheInfo();
                            this.Player = null;
                        }
                        this.PlayerId = 0L;
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
                    CLogger.Print("GameClient.Close: " + exception.Message, LoggerType.Error, exception);
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
                    this.PlayerId = 0L;
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

        private void method_0()
        {
            Thread.Sleep(0x2710);
            if ((this.Client != null) && (this.FirstPacketId == 0))
            {
                CLogger.Print("Connection destroyed due to no responses. IPAddress: " + this.GetIPAddress(), LoggerType.Hack, null);
                this.Close(0, true, false);
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
                this.Close(0, true, false);
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
                this.Close(0, true, false);
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
                this.Close(0, true, false);
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
                        this.Close(0, true, false);
                    }
                    else
                    {
                        byte[] destinationArray = new byte[length];
                        Array.Copy(asyncState.TcpBuffer, 0, destinationArray, 0, length);
                        int num2 = BitConverter.ToUInt16(destinationArray, 0) & 0x7fff;
                        if ((num2 <= 0) || (num2 > (length - 2)))
                        {
                            CLogger.Print($"Invalid PacketLength. IP: {this.GetIPAddress()}; Length: {num2}; RawBytes: {length}", LoggerType.Hack, null);
                            this.Close(0, true, false);
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
                                this.Close(0, true, false);
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
                this.Close(0, true, false);
            }
        }

        private void method_5(ushort ushort_2)
        {
            if (this.FirstPacketId == 0)
            {
                this.FirstPacketId = ushort_2;
                if ((ushort_2 != 0x91a) && (ushort_2 != 0x905))
                {
                    CLogger.Print($"Connection destroyed due to unknown first packet. Opcode: {ushort_2}; IPAddress: {this.GetIPAddress()}", LoggerType.Hack, null);
                    this.Close(0, true, false);
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
                gameClient_0 = this
            };
            try
            {
                class2.gameClientPacket_0 = null;
                if (ushort_2 > 0x9cc)
                {
                    if (ushort_2 > 0xf0c)
                    {
                        if (ushort_2 > 0x1444)
                        {
                            if (ushort_2 > 0x1807)
                            {
                                if (ushort_2 > 0x1d05)
                                {
                                    if (ushort_2 > 0x1e13)
                                    {
                                        if (ushort_2 == 0x2101)
                                        {
                                            class2.gameClientPacket_0 = new PROTOCOL_SEASON_CHALLENGE_INFO_REQ();
                                            goto TR_000A;
                                        }
                                        else if (ushort_2 == 0x2105)
                                        {
                                            class2.gameClientPacket_0 = new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_REQ();
                                            goto TR_000A;
                                        }
                                    }
                                    else if (ushort_2 == 0x1e01)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_MATCH_SERVER_IDX_REQ();
                                        goto TR_000A;
                                    }
                                    else if (ushort_2 == 0x1e13)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_MATCH_CLAN_SEASON_REQ();
                                        goto TR_000A;
                                    }
                                }
                                else
                                {
                                    switch (ushort_2)
                                    {
                                        case 0x1a01:
                                            class2.gameClientPacket_0 = new PROTOCOL_GMCHAT_START_CHAT_REQ();
                                            goto TR_000A;

                                        case 0x1a02:
                                        case 0x1a04:
                                        case 0x1a06:
                                        case 0x1a08:
                                            break;

                                        case 0x1a03:
                                            class2.gameClientPacket_0 = new PROTOCOL_GMCHAT_SEND_CHAT_REQ();
                                            goto TR_000A;

                                        case 0x1a05:
                                            class2.gameClientPacket_0 = new PROTOCOL_GMCHAT_END_CHAT_REQ();
                                            goto TR_000A;

                                        case 0x1a07:
                                            class2.gameClientPacket_0 = new PROTOCOL_GMCHAT_APPLY_PENALTY_REQ();
                                            goto TR_000A;

                                        case 0x1a09:
                                            class2.gameClientPacket_0 = new PROTOCOL_GMCHAT_NOTI_USER_PENALTY_REQ();
                                            goto TR_000A;

                                        default:
                                            if (ushort_2 == 0x1b35)
                                            {
                                                class2.gameClientPacket_0 = new PROTOCOL_CLAN_WAR_RESULT_REQ();
                                                goto TR_000A;
                                            }
                                            else if (ushort_2 == 0x1d05)
                                            {
                                                class2.gameClientPacket_0 = new PROTOCOL_BATTLEBOX_AUTH_REQ();
                                                goto TR_000A;
                                            }
                                            break;
                                    }
                                }
                            }
                            else if (ushort_2 > 0x14ac)
                            {
                                if (ushort_2 > 0x1801)
                                {
                                    if (ushort_2 == 0x1805)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_CHAR_CHANGE_EQUIP_REQ();
                                        goto TR_000A;
                                    }
                                    else if (ushort_2 == 0x1807)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_CHAR_DELETE_CHARA_REQ();
                                        goto TR_000A;
                                    }
                                }
                                else if (ushort_2 == 0x1501)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_LOBBY_QUICKJOIN_ROOM_REQ();
                                    goto TR_000A;
                                }
                                else if (ushort_2 == 0x1801)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_CHAR_CREATE_CHARA_REQ();
                                    goto TR_000A;
                                }
                            }
                            else if (ushort_2 == 0x148e)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x149c)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BATTLE_USER_SOPETYPE_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x14ac)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BASE_UNKNOWN_PACKET_REQ();
                                goto TR_000A;
                            }
                        }
                        else if (ushort_2 > 0x1426)
                        {
                            if (ushort_2 > 0x1434)
                            {
                                if (ushort_2 > 0x143c)
                                {
                                    if (ushort_2 == 0x143e)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ();
                                        goto TR_000A;
                                    }
                                    else if (ushort_2 == 0x1444)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_REQ();
                                        goto TR_000A;
                                    }
                                }
                                else if (ushort_2 == 0x1436)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_BATTLE_RESPAWN_FOR_AI_REQ();
                                    goto TR_000A;
                                }
                                else if (ushort_2 == 0x143c)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_REQ();
                                    goto TR_000A;
                                }
                            }
                            else if (ushort_2 == 0x142e)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x1430)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BATTLE_TIMERSYNC_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x1434)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_REQ();
                                goto TR_000A;
                            }
                        }
                        else if (ushort_2 > 0x1419)
                        {
                            if (ushort_2 == 0x141a)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BATTLE_SENDPING_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x1424)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x1426)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_REQ();
                                goto TR_000A;
                            }
                        }
                        else if (ushort_2 == 0x1403)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_BATTLE_READYBATTLE_REQ();
                            goto TR_000A;
                        }
                        else
                        {
                            switch (ushort_2)
                            {
                                case 0x1409:
                                    class2.gameClientPacket_0 = new PROTOCOL_BATTLE_PRESTARTBATTLE_REQ();
                                    goto TR_000A;

                                case 0x140a:
                                case 0x140c:
                                case 0x140e:
                                case 0x1410:
                                    break;

                                case 0x140b:
                                    class2.gameClientPacket_0 = new PROTOCOL_BATTLE_STARTBATTLE_REQ();
                                    goto TR_000A;

                                case 0x140d:
                                    class2.gameClientPacket_0 = new PROTOCOL_BATTLE_GIVEUPBATTLE_REQ();
                                    goto TR_000A;

                                case 0x140f:
                                    class2.gameClientPacket_0 = new PROTOCOL_BATTLE_DEATH_REQ();
                                    goto TR_000A;

                                case 0x1411:
                                    class2.gameClientPacket_0 = new PROTOCOL_BATTLE_RESPAWN_REQ();
                                    goto TR_000A;

                                default:
                                    if (ushort_2 != 0x1419)
                                    {
                                        break;
                                    }
                                    goto TR_000A;
                            }
                        }
                    }
                    else if (ushort_2 > 0xe0c)
                    {
                        if (ushort_2 > 0xe3b)
                        {
                            if (ushort_2 > 0xe51)
                            {
                                if (ushort_2 > 0xe66)
                                {
                                    if (ushort_2 == 0xf0a)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_COMMUNITY_USER_REPORT_REQ();
                                        goto TR_000A;
                                    }
                                    else if (ushort_2 == 0xf0c)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_REQ();
                                        goto TR_000A;
                                    }
                                }
                                else
                                {
                                    switch (ushort_2)
                                    {
                                        case 0xe57:
                                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_INFO_ENTER_REQ();
                                            goto TR_000A;

                                        case 0xe58:
                                        case 0xe5a:
                                        case 0xe5c:
                                        case 0xe5e:
                                        case 0xe61:
                                            break;

                                        case 0xe59:
                                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_INFO_LEAVE_REQ();
                                            goto TR_000A;

                                        case 0xe5b:
                                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ();
                                            goto TR_000A;

                                        case 0xe5d:
                                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_COSTUME_REQ();
                                            goto TR_000A;

                                        case 0xe5f:
                                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ();
                                            goto TR_000A;

                                        case 0xe60:
                                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ();
                                            goto TR_000A;

                                        case 0xe62:
                                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_SELECT_SLOT_CHANGE_REQ();
                                            goto TR_000A;

                                        default:
                                            if (ushort_2 != 0xe66)
                                            {
                                                break;
                                            }
                                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_OBSERVER_SLOT_REQ();
                                            goto TR_000A;
                                    }
                                }
                            }
                            else if (ushort_2 == 0xe3f)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_GM_EXIT_COMMAND_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0xe49)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_ROOM_LOADING_START_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0xe51)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_ROOM_GET_USER_EQUIPMENT_REQ();
                                goto TR_000A;
                            }
                        }
                        else if (ushort_2 > 0xe23)
                        {
                            if (ushort_2 > 0xe33)
                            {
                                if (ushort_2 == 0xe37)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ();
                                    goto TR_000A;
                                }
                                else if (ushort_2 == 0xe3b)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_GM_KICK_COMMAND_REQ();
                                    goto TR_000A;
                                }
                            }
                            else if (ushort_2 == 0xe2f)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0xe33)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BASE_UNKNOWN_3635_REQ();
                                goto TR_000A;
                            }
                        }
                        else if (ushort_2 == 0xe12)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_PASSWD_REQ();
                            goto TR_000A;
                        }
                        else if (ushort_2 == 0xe14)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_ROOM_CHANGE_SLOT_REQ();
                            goto TR_000A;
                        }
                        else
                        {
                            switch (ushort_2)
                            {
                                case 0xe19:
                                    class2.gameClientPacket_0 = new PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ();
                                    goto TR_000A;

                                case 0xe1b:
                                    class2.gameClientPacket_0 = new PROTOCOL_ROOM_REQUEST_MAIN_REQ();
                                    goto TR_000A;

                                case 0xe1d:
                                    class2.gameClientPacket_0 = new Class3();
                                    goto TR_000A;

                                case 0xe1f:
                                    class2.gameClientPacket_0 = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ();
                                    goto TR_000A;

                                case 0xe21:
                                    class2.gameClientPacket_0 = new Class2();
                                    goto TR_000A;

                                case 0xe23:
                                    class2.gameClientPacket_0 = new PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ();
                                    goto TR_000A;

                                default:
                                    break;
                            }
                        }
                    }
                    else if (ushort_2 > 0xa17)
                    {
                        if (ushort_2 > 0xd01)
                        {
                            if (ushort_2 > 0xe01)
                            {
                                if (ushort_2 == 0xe08)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_ROOM_CREATE_REQ();
                                    goto TR_000A;
                                }
                                else if (ushort_2 == 0xe0c)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_ROOM_GET_PLAYERINFO_REQ();
                                    goto TR_000A;
                                }
                            }
                            else if (ushort_2 == 0xd03)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_INVENTORY_LEAVE_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0xe01)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_ROOM_JOIN_REQ();
                                goto TR_000A;
                            }
                        }
                        else if (ushort_2 == 0xa1b)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_LOBBY_GET_ROOMLIST_REQ();
                            goto TR_000A;
                        }
                        else if (ushort_2 == 0xc0b)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ();
                            goto TR_000A;
                        }
                        else if (ushort_2 == 0xd01)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_INVENTORY_ENTER_REQ();
                            goto TR_000A;
                        }
                    }
                    else if (ushort_2 > 0x9d7)
                    {
                        if (ushort_2 == 0xa01)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_LOBBY_LEAVE_REQ();
                            goto TR_000A;
                        }
                        else if (ushort_2 == 0xa07)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ();
                            goto TR_000A;
                        }
                        else if (ushort_2 == 0xa17)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_LOBBY_ENTER_REQ();
                            goto TR_000A;
                        }
                    }
                    else if (ushort_2 == 0x9ce)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_BASE_EVENT_PORTAL_REQ();
                        goto TR_000A;
                    }
                    else if ((ushort_2 == 0x9d6) || (ushort_2 == 0x9d7))
                    {
                        goto TR_000A;
                    }
                }
                else if (ushort_2 > 0x713)
                {
                    if (ushort_2 > 0x93c)
                    {
                        if (ushort_2 > 0x96e)
                        {
                            if (ushort_2 > 0x9a1)
                            {
                                if (ushort_2 > 0x9bb)
                                {
                                    if (ushort_2 == 0x9c2)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_RANDOMBOX_LIST_REQ();
                                        goto TR_000A;
                                    }
                                    else if (ushort_2 == 0x9cc)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_TICKET_UPDATE_REQ();
                                        goto TR_000A;
                                    }
                                }
                                else if (ushort_2 == 0x9b9)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_CHANNELTYPE_CHANGE_CONDITION_REQ();
                                    goto TR_000A;
                                }
                                else if (ushort_2 == 0x9bb)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_LOBBY_NEW_MYINFO_REQ();
                                    goto TR_000A;
                                }
                            }
                            else
                            {
                                switch (ushort_2)
                                {
                                    case 0x976:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_GET_USER_DETAIL_INFO_REQ();
                                        goto TR_000A;

                                    case 0x977:
                                        break;

                                    case 0x978:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ();
                                        goto TR_000A;

                                    case 0x979:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_GET_USER_BASIC_INFO_REQ();
                                        goto TR_000A;

                                    case 0x97a:
                                        class2.gameClientPacket_0 = new PROTOCOL_AUTH_FIND_USER_REQ();
                                        goto TR_000A;

                                    default:
                                        if (ushort_2 == 0x98f)
                                        {
                                            class2.gameClientPacket_0 = new PROTOCOL_BASE_GET_USER_SUBTASK_REQ();
                                            goto TR_000A;
                                        }
                                        else if (ushort_2 == 0x9a1)
                                        {
                                            class2.gameClientPacket_0 = new PROTOCOL_BASE_URL_LIST_REQ();
                                            goto TR_000A;
                                        }
                                        break;
                                }
                            }
                        }
                        else if (ushort_2 > 0x950)
                        {
                            if (ushort_2 > 0x95f)
                            {
                                if (ushort_2 == 0x961)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_ENTER_PASS_REQ();
                                    goto TR_000A;
                                }
                                else if (ushort_2 == 0x96e)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_DAILY_RECORD_REQ();
                                    goto TR_000A;
                                }
                            }
                            else if (ushort_2 == 0x958)
                            {
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x95f)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_BASE_GAME_SERVER_STATE_REQ();
                                goto TR_000A;
                            }
                        }
                        else if (ushort_2 == 0x93e)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_BASE_QUEST_DELETE_CARD_SET_REQ();
                            goto TR_000A;
                        }
                        else
                        {
                            switch (ushort_2)
                            {
                                case 0x948:
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_USER_TITLE_CHANGE_REQ();
                                    goto TR_000A;

                                case 0x949:
                                case 0x94b:
                                    break;

                                case 0x94a:
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_USER_TITLE_EQUIP_REQ();
                                    goto TR_000A;

                                case 0x94c:
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_USER_TITLE_RELEASE_REQ();
                                    goto TR_000A;

                                default:
                                    if (ushort_2 != 0x950)
                                    {
                                        break;
                                    }
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_CHATTING_REQ();
                                    goto TR_000A;
                            }
                        }
                    }
                    else if (ushort_2 > 0x903)
                    {
                        if (ushort_2 > 0x912)
                        {
                            if (ushort_2 > 0x92e)
                            {
                                if (ushort_2 == 0x938)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_QUEST_ACTIVE_IDX_CHANGE_REQ();
                                    goto TR_000A;
                                }
                                else if (ushort_2 == 0x93c)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_QUEST_BUY_CARD_SET_REQ();
                                    goto TR_000A;
                                }
                            }
                            else
                            {
                                switch (ushort_2)
                                {
                                    case 0x916:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_CREATE_NICK_REQ();
                                        goto TR_000A;

                                    case 0x917:
                                    case 0x919:
                                    case 0x91b:
                                    case 0x91d:
                                    case 0x91f:
                                    case 0x921:
                                        break;

                                    case 0x918:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_USER_LEAVE_REQ();
                                        goto TR_000A;

                                    case 0x91a:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_USER_ENTER_REQ();
                                        goto TR_000A;

                                    case 0x91c:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_GET_CHANNELLIST_REQ();
                                        goto TR_000A;

                                    case 0x91e:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_SELECT_CHANNEL_REQ();
                                        goto TR_000A;

                                    case 0x920:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_ATTENDANCE_REQ();
                                        goto TR_000A;

                                    case 0x922:
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_ATTENDANCE_CLEAR_ITEM_REQ();
                                        goto TR_000A;

                                    default:
                                        if (ushort_2 != 0x92e)
                                        {
                                            break;
                                        }
                                        class2.gameClientPacket_0 = new PROTOCOL_BASE_GET_RECORD_INFO_DB_REQ();
                                        goto TR_000A;
                                }
                            }
                        }
                        else if (ushort_2 == 0x905)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_BASE_KEEP_ALIVE_REQ();
                            goto TR_000A;
                        }
                        else if (ushort_2 == 0x908)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_BASE_GAMEGUARD_REQ();
                            goto TR_000A;
                        }
                        else if (ushort_2 == 0x912)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_BASE_OPTION_SAVE_REQ();
                            goto TR_000A;
                        }
                    }
                    else if (ushort_2 > 0x724)
                    {
                        if (ushort_2 == 0x781)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_SEND_REQ();
                            goto TR_000A;
                        }
                        else
                        {
                            switch (ushort_2)
                            {
                                case 0x786:
                                    class2.gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_CHECK_READED_REQ();
                                    goto TR_000A;

                                case 0x787:
                                case 0x789:
                                    break;

                                case 0x788:
                                    class2.gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_DELETE_REQ();
                                    goto TR_000A;

                                case 0x78a:
                                    class2.gameClientPacket_0 = new PROTOCOL_MESSENGER_NOTE_RECEIVE_REQ();
                                    goto TR_000A;

                                default:
                                    if (ushort_2 != 0x903)
                                    {
                                        break;
                                    }
                                    class2.gameClientPacket_0 = new PROTOCOL_BASE_LOGOUT_REQ();
                                    goto TR_000A;
                            }
                        }
                    }
                    else
                    {
                        switch (ushort_2)
                        {
                            case 0x718:
                                class2.gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_ACCEPT_REQ();
                                goto TR_000A;

                            case 0x719:
                            case 0x71b:
                                break;

                            case 0x71a:
                                class2.gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_INSERT_REQ();
                                goto TR_000A;

                            case 0x71c:
                                class2.gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_DELETE_REQ();
                                goto TR_000A;

                            default:
                                if (ushort_2 == 0x722)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_AUTH_SEND_WHISPER_NOPID_REQ();
                                    goto TR_000A;
                                }
                                else if (ushort_2 == 0x724)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_AUTH_SEND_WHISPER_REQ();
                                    goto TR_000A;
                                }
                                break;
                        }
                    }
                }
                else if (ushort_2 > 0x37c)
                {
                    if (ushort_2 > 0x419)
                    {
                        if (ushort_2 > 0x433)
                        {
                            if (ushort_2 > 0x43c)
                            {
                                if (ushort_2 == 0x43f)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_ITEM_CHANGE_DATA_REQ();
                                    goto TR_000A;
                                }
                                else if (ushort_2 == 0x713)
                                {
                                    class2.gameClientPacket_0 = new PROTOCOL_AUTH_FRIEND_INVITED_REQ();
                                    goto TR_000A;
                                }
                            }
                            else if (ushort_2 == 0x434)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_SHOP_REPAIR_REQ();
                                goto TR_000A;
                            }
                            else if (ushort_2 == 0x43c)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ();
                                goto TR_000A;
                            }
                        }
                        else
                        {
                            switch (ushort_2)
                            {
                                case 0x41d:
                                    class2.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ();
                                    goto TR_000A;

                                case 0x41e:
                                case 0x420:
                                    break;

                                case 0x41f:
                                    class2.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_DELETE_ITEM_REQ();
                                    goto TR_000A;

                                case 0x421:
                                    class2.gameClientPacket_0 = new PROTOCOL_AUTH_GET_POINT_CASH_REQ();
                                    goto TR_000A;

                                default:
                                    if (ushort_2 == 0x425)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_AUTH_USE_ITEM_CHECK_NICK_REQ();
                                        goto TR_000A;
                                    }
                                    else if (ushort_2 == 0x433)
                                    {
                                        class2.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_EXTEND_REQ();
                                        goto TR_000A;
                                    }
                                    break;
                            }
                        }
                    }
                    else if (ushort_2 > 0x3e5)
                    {
                        if (ushort_2 == 0x3e7)
                        {
                            class2.gameClientPacket_0 = new PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_REQ();
                            goto TR_000A;
                        }
                        else
                        {
                            switch (ushort_2)
                            {
                                case 0x401:
                                    class2.gameClientPacket_0 = new PROTOCOL_SHOP_ENTER_REQ();
                                    goto TR_000A;

                                case 0x402:
                                case 0x404:
                                    break;

                                case 0x403:
                                    class2.gameClientPacket_0 = new PROTOCOL_SHOP_LEAVE_REQ();
                                    goto TR_000A;

                                case 0x405:
                                    class2.gameClientPacket_0 = new PROTOCOL_SHOP_GET_SAILLIST_REQ();
                                    goto TR_000A;

                                default:
                                    switch (ushort_2)
                                    {
                                        case 0x411:
                                            class2.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_GET_GIFTLIST_REQ();
                                            goto TR_000A;

                                        case 0x413:
                                            class2.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ();
                                            goto TR_000A;

                                        case 0x415:
                                            class2.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ();
                                            goto TR_000A;

                                        case 0x417:
                                            class2.gameClientPacket_0 = new PROTOCOL_AUTH_SHOP_ITEM_AUTH_REQ();
                                            goto TR_000A;

                                        case 0x419:
                                            class2.gameClientPacket_0 = new PROTOCOL_INVENTORY_USE_ITEM_REQ();
                                            goto TR_000A;

                                        default:
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                    else if (ushort_2 == 0x392)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_CS_CREATE_CLAN_CONDITION_REQ();
                        goto TR_000A;
                    }
                    else if (ushort_2 == 0x394)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_CS_CHECK_DUPLICATE_REQ();
                        goto TR_000A;
                    }
                    else if (ushort_2 == 0x3e5)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_CS_CLAN_LIST_FILTER_REQ();
                        goto TR_000A;
                    }
                }
                else if (ushort_2 > 830)
                {
                    if (ushort_2 > 0x347)
                    {
                        if (ushort_2 > 0x364)
                        {
                            if (ushort_2 == 0x36d)
                            {
                                class2.gameClientPacket_0 = new PROTOCOL_CS_ROOM_INVITED_REQ();
                                goto TR_000A;
                            }
                            else
                            {
                                switch (ushort_2)
                                {
                                    case 0x376:
                                        class2.gameClientPacket_0 = new PROTOCOL_CS_PAGE_CHATTING_REQ();
                                        goto TR_000A;

                                    case 0x378:
                                        class2.gameClientPacket_0 = new PROTOCOL_CS_INVITE_REQ();
                                        goto TR_000A;

                                    case 890:
                                        class2.gameClientPacket_0 = new PROTOCOL_CS_INVITE_ACCEPT_REQ();
                                        goto TR_000A;

                                    case 0x37c:
                                        class2.gameClientPacket_0 = new PROTOCOL_CS_NOTE_REQ();
                                        goto TR_000A;

                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (ushort_2)
                            {
                                case 0x356:
                                    class2.gameClientPacket_0 = new PROTOCOL_CS_CHATTING_REQ();
                                    goto TR_000A;

                                case 0x357:
                                case 0x359:
                                case 0x35b:
                                    break;

                                case 0x358:
                                    class2.gameClientPacket_0 = new PROTOCOL_CS_CHECK_MARK_REQ();
                                    goto TR_000A;

                                case 0x35a:
                                    class2.gameClientPacket_0 = new PROTOCOL_CS_REPLACE_NOTICE_REQ();
                                    goto TR_000A;

                                case 860:
                                    class2.gameClientPacket_0 = new PROTOCOL_CS_REPLACE_INTRO_REQ();
                                    goto TR_000A;

                                default:
                                    if (ushort_2 != 0x364)
                                    {
                                        break;
                                    }
                                    class2.gameClientPacket_0 = new PROTOCOL_CS_REPLACE_MANAGEMENT_REQ();
                                    goto TR_000A;
                            }
                        }
                    }
                    else if (ushort_2 == 0x341)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_CS_COMMISSION_MASTER_REQ();
                        goto TR_000A;
                    }
                    else if (ushort_2 == 0x344)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_CS_COMMISSION_STAFF_REQ();
                        goto TR_000A;
                    }
                    else if (ushort_2 == 0x347)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_CS_COMMISSION_REGULAR_REQ();
                        goto TR_000A;
                    }
                }
                else if (ushort_2 > 0x336)
                {
                    if (ushort_2 == 0x339)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_CS_DENIAL_REQUEST_REQ();
                        goto TR_000A;
                    }
                    else if (ushort_2 == 0x33c)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_CS_SECESSION_CLAN_REQ();
                        goto TR_000A;
                    }
                    else if (ushort_2 == 830)
                    {
                        class2.gameClientPacket_0 = new PROTOCOL_CS_DEPORTATION_REQ();
                        goto TR_000A;
                    }
                }
                else if (ushort_2 == 0x301)
                {
                    class2.gameClientPacket_0 = new PROTOCOL_CS_CLIENT_ENTER_REQ();
                    goto TR_000A;
                }
                else if (ushort_2 == 0x303)
                {
                    class2.gameClientPacket_0 = new PROTOCOL_CS_CLIENT_LEAVE_REQ();
                    goto TR_000A;
                }
                else
                {
                    switch (ushort_2)
                    {
                        case 800:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_DETAIL_INFO_REQ();
                            goto TR_000A;

                        case 0x322:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_MEMBER_CONTEXT_REQ();
                            goto TR_000A;

                        case 0x324:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_MEMBER_LIST_REQ();
                            goto TR_000A;

                        case 0x326:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_CREATE_CLAN_REQ();
                            goto TR_000A;

                        case 0x328:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_CLOSE_CLAN_REQ();
                            goto TR_000A;

                        case 810:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_CHECK_JOIN_AUTHORITY_ERQ();
                            goto TR_000A;

                        case 0x32c:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_JOIN_REQUEST_REQ();
                            goto TR_000A;

                        case 0x32e:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_CANCEL_REQUEST_REQ();
                            goto TR_000A;

                        case 0x330:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_REQUEST_CONTEXT_REQ();
                            goto TR_000A;

                        case 0x332:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_REQUEST_LIST_REQ();
                            goto TR_000A;

                        case 820:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_REQUEST_INFO_REQ();
                            goto TR_000A;

                        case 0x336:
                            class2.gameClientPacket_0 = new PROTOCOL_CS_ACCEPT_REQUEST_REQ();
                            goto TR_000A;

                        default:
                            break;
                    }
                }
                CLogger.Print(Bitwise.ToHexData($"Opcode Not Found: [{ushort_2}] | {string_0}", byte_0), LoggerType.Opcode, null);
            TR_000A:
                if (class2.gameClientPacket_0 != null)
                {
                    using (class2.gameClientPacket_0)
                    {
                        if (ConfigLoader.DebugMode)
                        {
                            CLogger.Print($"{class2.gameClientPacket_0.GetType().Name}; Address: {this.Client.RemoteEndPoint}; Opcode: [{ushort_2}]", LoggerType.Debug, null);
                        }
                        class2.gameClientPacket_0.Makeme(this, byte_0);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(class2.method_0));
                        class2.gameClientPacket_0.Dispose();
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
                this.Close(0, true, false);
            }
        }

        public void SendPacket(GameServerPacket Packet)
        {
            try
            {
                using (Packet)
                {
                    byte[] bytes = Packet.GetBytes("GameClient.SendPacket");
                    this.SendPacket(bytes, Packet.GetType().Name);
                    Packet.Dispose();
                }
            }
            catch
            {
                this.Close(0, true, false);
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
                this.Close(0, true, false);
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
                this.Close(0, true, false);
            }
        }

        [CompilerGenerated]
        private sealed class Class1
        {
            public GameClient gameClient_0;
            public GameClientPacket gameClientPacket_0;

            internal void method_0(object object_0)
            {
                try
                {
                    this.gameClientPacket_0.Run();
                }
                catch (Exception exception)
                {
                    CLogger.Print(exception.Message, LoggerType.Error, exception);
                    this.gameClient_0.Close(50, true, false);
                }
            }
        }
    }
}

