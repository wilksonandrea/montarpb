namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_ROOM_CREATE_REQ : GameClientPacket
    {
        private uint uint_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private MapIdEnum mapIdEnum_0;
        private MapRules mapRules_0;
        private StageOptions stageOptions_0;
        private TeamBalance teamBalance_0;
        private byte[] byte_0;
        private byte[] byte_1;
        private int int_0;
        private int int_1;
        private int int_2;
        private int int_3;
        private int int_4;
        private byte byte_2;
        private byte byte_3;
        private byte byte_4;
        private byte byte_5;
        private byte byte_6;
        private byte byte_7;
        private byte byte_8;
        private RoomCondition roomCondition_0;
        private RoomState roomState_0;
        private RoomWeaponsFlag roomWeaponsFlag_0;
        private RoomStageFlag roomStageFlag_0;

        public override void Read()
        {
            base.ReadD();
            this.string_0 = base.ReadU(0x2e);
            this.mapIdEnum_0 = (MapIdEnum) base.ReadC();
            this.mapRules_0 = (MapRules) base.ReadC();
            this.stageOptions_0 = (StageOptions) base.ReadC();
            this.roomCondition_0 = (RoomCondition) base.ReadC();
            this.roomState_0 = (RoomState) base.ReadC();
            this.int_3 = base.ReadC();
            this.int_0 = base.ReadC();
            this.int_1 = base.ReadC();
            this.roomWeaponsFlag_0 = (RoomWeaponsFlag) base.ReadH();
            this.roomStageFlag_0 = (RoomStageFlag) base.ReadD();
            base.ReadH();
            this.int_4 = base.ReadD();
            base.ReadH();
            this.string_2 = base.ReadU(0x42);
            this.int_2 = base.ReadD();
            this.byte_2 = base.ReadC();
            this.byte_3 = base.ReadC();
            this.teamBalance_0 = (TeamBalance) base.ReadH();
            this.byte_0 = base.ReadB(0x18);
            this.byte_8 = base.ReadC();
            this.byte_1 = base.ReadB(4);
            this.byte_7 = base.ReadC();
            base.ReadH();
            this.string_1 = base.ReadS(4);
            this.byte_4 = base.ReadC();
            this.byte_5 = base.ReadC();
            this.byte_6 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ChannelModel channel = player.GetChannel();
                    if ((channel != null) && ((player.Nickname.Length != 0) && ((player.Room == null) && (player.Match == null))))
                    {
                        List<RoomModel> rooms = channel.Rooms;
                        lock (rooms)
                        {
                            int id = 0;
                            goto TR_0013;
                        TR_0006:
                            id++;
                        TR_0013:
                            while (true)
                            {
                                if (id >= channel.MaxRooms)
                                {
                                    break;
                                }
                                if (channel.GetRoom(id) == null)
                                {
                                    bool flag2;
                                    RoomModel model1 = new RoomModel(id, channel);
                                    model1.Name = this.string_0;
                                    model1.MapId = this.mapIdEnum_0;
                                    model1.Rule = this.mapRules_0;
                                    model1.Stage = this.stageOptions_0;
                                    model1.RoomType = this.roomCondition_0;
                                    RoomModel room = model1;
                                    room.GenerateSeed();
                                    room.State = (this.roomState_0 < RoomState.READY) ? RoomState.READY : this.roomState_0;
                                    room.LeaderName = (this.string_2.Equals("") || !this.string_2.Equals(player.Nickname)) ? player.Nickname : this.string_2;
                                    room.Ping = this.int_1;
                                    room.WeaponsFlag = this.roomWeaponsFlag_0;
                                    room.Flag = this.roomStageFlag_0;
                                    room.NewInt = this.int_4;
                                    if ((flag2 = room.IsBotMode()) && (room.ChannelType == ChannelType.Clan))
                                    {
                                        this.uint_0 = 0x8000107d;
                                        break;
                                    }
                                    room.KillTime = this.int_2;
                                    room.Limit = (channel.Type == ChannelType.Clan) ? ((byte) 1) : this.byte_2;
                                    room.WatchRuleFlag = (room.RoomType == RoomCondition.Ace) ? ((byte) 0x8e) : this.byte_3;
                                    room.BalanceType = ((channel.Type == ChannelType.Clan) || (room.RoomType == RoomCondition.Ace)) ? TeamBalance.None : this.teamBalance_0;
                                    room.RandomMaps = this.byte_0;
                                    room.CountdownIG = this.byte_8;
                                    room.LeaderAddr = this.byte_1;
                                    room.KillCam = this.byte_7;
                                    room.Password = this.string_1;
                                    if (flag2)
                                    {
                                        room.AiCount = this.byte_4;
                                        room.AiLevel = this.byte_5;
                                        room.AiType = this.byte_6;
                                    }
                                    room.SetSlotCount(this.int_0, true, false);
                                    room.CountPlayers = this.int_3;
                                    room.CountMaxSlots = this.int_0;
                                    if (room.AddPlayer(player) >= 0)
                                    {
                                        player.ResetPages();
                                        channel.AddRoom(room);
                                        base.Client.SendPacket(new PROTOCOL_ROOM_CREATE_ACK(this.uint_0, room));
                                        if (room.IsBotMode())
                                        {
                                            room.ChangeSlotState(1, SlotState.CLOSE, true);
                                            room.ChangeSlotState(3, SlotState.CLOSE, true);
                                            room.ChangeSlotState(5, SlotState.CLOSE, true);
                                            room.ChangeSlotState(7, SlotState.CLOSE, true);
                                            base.Client.SendPacket(new PROTOCOL_ROOM_GET_SLOTINFO_ACK(room));
                                        }
                                    }
                                    else
                                    {
                                        goto TR_0006;
                                    }
                                    break;
                                }
                                goto TR_0006;
                            }
                            return;
                        }
                    }
                    this.uint_0 = 0x80000000;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_LOBBY_CREATE_ROOM_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

