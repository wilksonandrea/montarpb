namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ : GameClientPacket
    {
        private int int_0 = -1;
        private int int_1;
        private int int_2;
        private int int_3;
        private string string_0;
        private StageOptions stageOptions_0;
        private MapIdEnum mapIdEnum_0;
        private MapRules mapRules_0;
        private RoomCondition roomCondition_0;
        private RoomWeaponsFlag roomWeaponsFlag_0;
        private RoomStageFlag roomStageFlag_0;

        public override void Read()
        {
            this.int_3 = base.ReadH();
            base.ReadD();
            base.ReadD();
            base.ReadH();
            this.string_0 = base.ReadU(0x2e);
            this.mapIdEnum_0 = (MapIdEnum) base.ReadC();
            this.mapRules_0 = (MapRules) base.ReadC();
            this.stageOptions_0 = (StageOptions) base.ReadC();
            this.roomCondition_0 = (RoomCondition) base.ReadC();
            base.ReadC();
            base.ReadC();
            this.int_1 = base.ReadC();
            this.int_2 = base.ReadC();
            this.roomWeaponsFlag_0 = (RoomWeaponsFlag) base.ReadH();
            this.roomStageFlag_0 = (RoomStageFlag) base.ReadD();
        }

        public override void Run()
        {
            try
            {
                MatchModel match;
                MatchModel match;
                Account player = base.Client.Player;
                if ((player != null) && (player.ClanId != 0))
                {
                    ChannelModel channel = player.GetChannel();
                    match = player.Match;
                    if ((channel != null) && (match != null))
                    {
                        match = channel.GetMatch(this.int_3);
                        if (match != null)
                        {
                            List<RoomModel> rooms = channel.Rooms;
                            lock (rooms)
                            {
                                int id = 0;
                                while (true)
                                {
                                    if (id < channel.MaxRooms)
                                    {
                                        if (channel.GetRoom(id) == null)
                                        {
                                            RoomModel model1 = new RoomModel(id, channel);
                                            model1.Name = this.string_0;
                                            model1.MapId = this.mapIdEnum_0;
                                            model1.Rule = this.mapRules_0;
                                            model1.Stage = this.stageOptions_0;
                                            model1.RoomType = this.roomCondition_0;
                                            RoomModel room = model1;
                                            room.SetSlotCount(this.int_1, true, false);
                                            room.Ping = this.int_2;
                                            room.WeaponsFlag = this.roomWeaponsFlag_0;
                                            room.Flag = this.roomStageFlag_0;
                                            room.Password = "";
                                            room.KillTime = 3;
                                            if (room.AddPlayer(player) >= 0)
                                            {
                                                channel.AddRoom(room);
                                                base.Client.SendPacket(new PROTOCOL_ROOM_CREATE_ACK(0, room));
                                                this.int_0 = id;
                                                break;
                                            }
                                        }
                                        id++;
                                        continue;
                                    }
                                    goto TR_0028;
                                }
                            }
                        }
                    }
                }
                return;
            TR_001D:
                using (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK protocol_clan_war_enemy_info_ack2 = new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match))
                {
                    using (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK protocol_clan_war_join_room_ack2 = new PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(match, this.int_0, 1))
                    {
                        byte[] completeBytes = protocol_clan_war_enemy_info_ack2.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-3");
                        byte[] data = protocol_clan_war_join_room_ack2.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-4");
                        foreach (Account account3 in match.GetAllPlayers())
                        {
                            account3.SendCompletePacket(completeBytes, protocol_clan_war_enemy_info_ack2.GetType().Name);
                            account3.SendCompletePacket(data, protocol_clan_war_join_room_ack2.GetType().Name);
                            if (account3.Match != null)
                            {
                                match.Slots[account3.MatchSlot].State = SlotMatchState.Ready;
                            }
                        }
                    }
                }
                return;
            TR_0028:
                using (PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK protocol_clan_war_enemy_info_ack = new PROTOCOL_CLAN_WAR_ENEMY_INFO_ACK(match))
                {
                    using (PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK protocol_clan_war_join_room_ack = new PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(match, this.int_0, 0))
                    {
                        byte[] completeBytes = protocol_clan_war_enemy_info_ack.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-1");
                        byte[] data = protocol_clan_war_join_room_ack.GetCompleteBytes("PROTOCOL_CLAN_WAR_CREATE_ROOM_REQ-2");
                        foreach (Account account2 in match.GetAllPlayers(match.Leader))
                        {
                            account2.SendCompletePacket(completeBytes, protocol_clan_war_enemy_info_ack.GetType().Name);
                            account2.SendCompletePacket(data, protocol_clan_war_join_room_ack.GetType().Name);
                            if (account2.Match != null)
                            {
                                match.Slots[account2.MatchSlot].State = SlotMatchState.Ready;
                            }
                        }
                    }
                }
                goto TR_001D;
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

