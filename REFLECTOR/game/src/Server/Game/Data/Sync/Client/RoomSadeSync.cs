namespace Server.Game.Data.Sync.Client
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Data.XML;
    using Server.Game.Network.ServerPacket;
    using System;

    public class RoomSadeSync
    {
        public static void EndRound(RoomModel Room, TeamEnum Winner)
        {
            if (Winner == TeamEnum.FR_TEAM)
            {
                Room.FRRounds++;
            }
            else if (Winner == TeamEnum.CT_TEAM)
            {
                Room.CTRounds++;
            }
            AllUtils.BattleEndRound(Room, Winner, RoundEndType.Normal);
        }

        public static void Load(SyncClientPacket C)
        {
            int id = C.ReadH();
            int num2 = C.ReadH();
            byte slotId = C.ReadC();
            ushort num4 = C.ReadUH();
            ushort num5 = C.ReadUH();
            int num6 = C.ReadC();
            ushort num7 = C.ReadUH();
            if (C.ToArray().Length > 0x10)
            {
                CLogger.Print($"Invalid Sabotage (Length > 16): {C.ToArray().Length}", LoggerType.Warning, null);
            }
            ChannelModel channel = ChannelsXML.GetChannel(C.ReadH(), num2);
            if (channel != null)
            {
                SlotModel model3;
                RoomModel room = channel.GetRoom(id);
                if ((room != null) && (!room.RoundTime.IsTimer() && ((room.State == RoomState.BATTLE) && room.GetSlot(slotId, out model3))))
                {
                    room.Bar1 = num4;
                    room.Bar2 = num5;
                    RoomCondition roomType = room.RoomType;
                    int num8 = 0;
                    if (num6 == 1)
                    {
                        model3.DamageBar1 = (ushort) (model3.DamageBar1 + num7);
                        num8 += model3.DamageBar1 / 600;
                    }
                    else if (num6 == 2)
                    {
                        model3.DamageBar2 = (ushort) (model3.DamageBar2 + num7);
                        num8 += model3.DamageBar2 / 600;
                    }
                    model3.EarnedEXP = num8;
                    if (roomType != RoomCondition.Destroy)
                    {
                        if (roomType == RoomCondition.Defense)
                        {
                            using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK protocol_battle_mission_defence_info_ack = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(room))
                            {
                                room.SendPacketToPlayers(protocol_battle_mission_defence_info_ack, SlotState.BATTLE, 0);
                            }
                            if ((room.Bar1 == 0) && (room.Bar2 == 0))
                            {
                                EndRound(room, !room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
                            }
                        }
                    }
                    else
                    {
                        using (PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK protocol_battle_mission_generator_info_ack = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(room))
                        {
                            room.SendPacketToPlayers(protocol_battle_mission_generator_info_ack, SlotState.BATTLE, 0);
                        }
                        if (room.Bar1 == 0)
                        {
                            EndRound(room, !room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
                        }
                        else if (room.Bar2 == 0)
                        {
                            EndRound(room, !room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM);
                        }
                    }
                }
            }
        }
    }
}

