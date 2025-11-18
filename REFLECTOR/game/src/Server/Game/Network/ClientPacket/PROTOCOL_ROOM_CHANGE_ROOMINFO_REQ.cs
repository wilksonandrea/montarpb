namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_CHANGE_ROOMINFO_REQ : GameClientPacket
    {
        private string string_0;
        private string string_1;
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
            this.int_4 = base.ReadC();
            this.int_1 = base.ReadC();
            this.int_2 = base.ReadC();
            this.roomWeaponsFlag_0 = (RoomWeaponsFlag) base.ReadH();
            this.roomStageFlag_0 = (RoomStageFlag) base.ReadD();
            base.ReadH();
            this.int_0 = base.ReadD();
            base.ReadH();
            this.string_1 = base.ReadU(0x42);
            this.int_3 = base.ReadD();
            this.byte_2 = base.ReadC();
            this.byte_3 = base.ReadC();
            this.teamBalance_0 = (TeamBalance) base.ReadH();
            this.byte_0 = base.ReadB(0x18);
            this.byte_6 = base.ReadC();
            this.byte_1 = base.ReadB(4);
            this.byte_7 = base.ReadC();
            base.ReadH();
            this.byte_4 = base.ReadC();
            this.byte_5 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if ((room != null) && (room.LeaderSlot == player.SlotId))
                    {
                        bool flag = !room.Name.Equals(this.string_0);
                        bool flag2 = ((room.Rule != this.mapRules_0) || (room.Stage != this.stageOptions_0)) || (room.RoomType != this.roomCondition_0);
                        room.Name = this.string_0;
                        room.MapId = this.mapIdEnum_0;
                        room.Rule = this.mapRules_0;
                        room.Stage = this.stageOptions_0;
                        room.RoomType = this.roomCondition_0;
                        room.Ping = this.int_2;
                        room.Flag = this.roomStageFlag_0;
                        room.NewInt = this.int_0;
                        room.KillTime = this.int_3;
                        room.Limit = this.byte_2;
                        room.WatchRuleFlag = (room.RoomType == RoomCondition.Ace) ? ((byte) 0x8e) : this.byte_3;
                        room.BalanceType = (room.RoomType == RoomCondition.Ace) ? TeamBalance.None : this.teamBalance_0;
                        room.BalanceType = this.teamBalance_0;
                        room.RandomMaps = this.byte_0;
                        room.CountdownIG = this.byte_6;
                        room.LeaderAddr = this.byte_1;
                        room.KillCam = this.byte_7;
                        room.AiCount = this.byte_4;
                        room.AiLevel = this.byte_5;
                        room.SetSlotCount(this.int_1, false, true);
                        room.CountPlayers = this.int_4;
                        if (((((this.roomState_0 < RoomState.READY) || (this.string_1.Equals("") || !this.string_1.Equals(player.Nickname))) | flag) | flag2) || ((this.roomWeaponsFlag_0 != room.WeaponsFlag) || (this.int_1 != room.CountMaxSlots)))
                        {
                            room.State = (this.roomState_0 < RoomState.READY) ? RoomState.READY : this.roomState_0;
                            room.LeaderName = (this.string_1.Equals("") || !this.string_1.Equals(player.Nickname)) ? player.Nickname : this.string_1;
                            room.WeaponsFlag = this.roomWeaponsFlag_0;
                            room.CountMaxSlots = this.int_1;
                            room.CountdownIG = 0;
                            if (room.ResetReadyPlayers() > 0)
                            {
                                room.UpdateSlotsInfo();
                            }
                        }
                        room.UpdateRoomInfo();
                        using (PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK protocol_room_change_room_optioninfo_ack = new PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK(room))
                        {
                            room.SendPacketToPlayers(protocol_room_change_room_optioninfo_ack);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_CHANGE_ROOMINFO_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

