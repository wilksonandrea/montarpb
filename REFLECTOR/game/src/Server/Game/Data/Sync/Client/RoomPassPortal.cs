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

    public static class RoomPassPortal
    {
        public static void Load(SyncClientPacket C)
        {
            int id = C.ReadH();
            int num2 = C.ReadH();
            int slotIdx = C.ReadC();
            C.ReadC();
            if (C.ToArray().Length > 10)
            {
                CLogger.Print($"Invalid Portal (Length > 10): {C.ToArray().Length}", LoggerType.Warning, null);
            }
            ChannelModel channel = ChannelsXML.GetChannel(C.ReadH(), num2);
            if (channel != null)
            {
                RoomModel room = channel.GetRoom(id);
                if ((room != null) && (!room.RoundTime.IsTimer() && ((room.State == RoomState.BATTLE) && room.IsDinoMode("DE"))))
                {
                    SlotModel slot = room.GetSlot(slotIdx);
                    if ((slot != null) && (slot.State == SlotState.BATTLE))
                    {
                        slot.PassSequence++;
                        if (slot.Team == TeamEnum.FR_TEAM)
                        {
                            room.FRDino += 5;
                        }
                        else
                        {
                            room.CTDino += 5;
                        }
                        smethod_0(room, slot);
                        using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK protocol_battle_mission_touchdown_ack = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK(room, slot))
                        {
                            using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK protocol_battle_mission_touchdown_count_ack = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(room))
                            {
                                room.SendPacketToPlayers(protocol_battle_mission_touchdown_ack, protocol_battle_mission_touchdown_count_ack, SlotState.BATTLE, 0);
                            }
                        }
                    }
                }
            }
        }

        private static void smethod_0(RoomModel roomModel_0, SlotModel slotModel_0)
        {
            MissionType nA = MissionType.NA;
            if (slotModel_0.PassSequence == 1)
            {
                nA = MissionType.TOUCHDOWN;
            }
            else if (slotModel_0.PassSequence == 2)
            {
                nA = MissionType.TOUCHDOWN_ACE_ATTACKER;
            }
            else if (slotModel_0.PassSequence == 3)
            {
                nA = MissionType.TOUCHDOWN_HATTRICK;
            }
            else if (slotModel_0.PassSequence >= 4)
            {
                nA = MissionType.TOUCHDOWN_GAME_MAKER;
            }
            if (nA != MissionType.NA)
            {
                AllUtils.CompleteMission(roomModel_0, slotModel_0, nA, 0);
            }
        }
    }
}

