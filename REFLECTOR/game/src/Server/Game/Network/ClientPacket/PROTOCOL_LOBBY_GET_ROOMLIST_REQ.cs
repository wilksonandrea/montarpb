namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_LOBBY_GET_ROOMLIST_REQ : GameClientPacket
    {
        private byte[] method_0(int int_0, ref int int_1, List<RoomModel> list_0)
        {
            byte[] buffer;
            int num = (int_0 == 0) ? 10 : 11;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                int num2 = int_0 * num;
                while (true)
                {
                    if (num2 < list_0.Count)
                    {
                        this.method_1(list_0[num2], packet);
                        int num3 = int_1 + 1;
                        int_1 = num3;
                        if (num3 != 10)
                        {
                            num2++;
                            continue;
                        }
                    }
                    buffer = packet.ToArray();
                    break;
                }
            }
            return buffer;
        }

        private void method_1(RoomModel roomModel_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteD(roomModel_0.RoomId);
            syncServerPacket_0.WriteU(roomModel_0.Name, 0x2e);
            syncServerPacket_0.WriteC((byte) roomModel_0.MapId);
            syncServerPacket_0.WriteC((byte) roomModel_0.Rule);
            syncServerPacket_0.WriteC((byte) roomModel_0.Stage);
            syncServerPacket_0.WriteC((byte) roomModel_0.RoomType);
            syncServerPacket_0.WriteC((byte) roomModel_0.State);
            syncServerPacket_0.WriteC((byte) roomModel_0.GetCountPlayers());
            syncServerPacket_0.WriteC((byte) roomModel_0.GetSlotCount());
            syncServerPacket_0.WriteC((byte) roomModel_0.Ping);
            syncServerPacket_0.WriteH((ushort) roomModel_0.WeaponsFlag);
            syncServerPacket_0.WriteD(roomModel_0.GetFlag());
            syncServerPacket_0.WriteH((short) 0);
            syncServerPacket_0.WriteB(roomModel_0.LeaderAddr);
            syncServerPacket_0.WriteC(0);
        }

        private byte[] method_2(int int_0, ref int int_1, List<Account> list_0)
        {
            byte[] buffer;
            int num = (int_0 == 0) ? 8 : 9;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                int num2 = int_0 * num;
                while (true)
                {
                    if (num2 < list_0.Count)
                    {
                        this.method_3(list_0[num2], packet);
                        int num3 = int_1 + 1;
                        int_1 = num3;
                        if (num3 != 8)
                        {
                            num2++;
                            continue;
                        }
                    }
                    buffer = packet.ToArray();
                    break;
                }
            }
            return buffer;
        }

        private void method_3(Account account_0, SyncServerPacket syncServerPacket_0)
        {
            ClanModel clan = ClanManager.GetClan(account_0.ClanId);
            syncServerPacket_0.WriteD(account_0.GetSessionId());
            syncServerPacket_0.WriteD(clan.Logo);
            syncServerPacket_0.WriteC((byte) clan.Effect);
            syncServerPacket_0.WriteU(clan.Name, 0x22);
            syncServerPacket_0.WriteH((short) account_0.GetRank());
            syncServerPacket_0.WriteU(account_0.Nickname, 0x42);
            syncServerPacket_0.WriteC((byte) account_0.NickColor);
            syncServerPacket_0.WriteC((byte) base.NATIONS);
            syncServerPacket_0.WriteD(account_0.Equipment.NameCardId);
            syncServerPacket_0.WriteC((byte) account_0.Bonus.NickBorderColor);
        }

        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ChannelModel channel = player.GetChannel();
                    if (channel != null)
                    {
                        channel.RemoveEmptyRooms();
                        List<RoomModel> rooms = channel.Rooms;
                        List<Account> waitPlayers = channel.GetWaitPlayers();
                        int num = (int) Math.Ceiling((double) (((double) rooms.Count) / 10.0));
                        int num2 = (int) Math.Ceiling((double) (((double) waitPlayers.Count) / 8.0));
                        if (player.LastRoomPage >= num)
                        {
                            player.LastRoomPage = 0;
                        }
                        if (player.LastPlayerPage >= num2)
                        {
                            player.LastPlayerPage = 0;
                        }
                        int num3 = 0;
                        int num4 = 0;
                        int lastRoomPage = player.LastRoomPage;
                        player.LastRoomPage = lastRoomPage + 1;
                        int num1 = lastRoomPage;
                        lastRoomPage = player.LastPlayerPage;
                        player.LastPlayerPage = lastRoomPage + 1;
                        base.Client.SendPacket(new PROTOCOL_LOBBY_GET_ROOMLIST_ACK(rooms.Count, waitPlayers.Count, num1, lastRoomPage, num3, num4, this.method_0(player.LastRoomPage, ref num3, rooms), this.method_2(player.LastPlayerPage, ref num4, waitPlayers)));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_LOBBY_GET_ROOMLIST_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

