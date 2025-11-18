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

    public static class RoomBombC4
    {
        public static void InstallBomb(RoomModel Room, SlotModel Slot, byte Zone, ushort Unk, float X, float Y, float Z)
        {
            if (!Room.ActiveC4)
            {
                using (PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK protocol_battle_mission_bomb_install_ack = new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(Slot.Id, Zone, Unk, X, Y, Z))
                {
                    Room.SendPacketToPlayers(protocol_battle_mission_bomb_install_ack, SlotState.BATTLE, 0);
                }
                if (Room.RoomType == RoomCondition.Tutorial)
                {
                    Room.ActiveC4 = true;
                }
                else
                {
                    Room.ActiveC4 = true;
                    Slot.Objects++;
                    AllUtils.CompleteMission(Room, Slot, MissionType.C4_PLANT, 0);
                    Room.StartBomb();
                }
            }
        }

        public static void Load(SyncClientPacket C)
        {
            int id = C.ReadH();
            int num2 = C.ReadH();
            int num3 = C.ReadC();
            int slotIdx = C.ReadC();
            byte zone = 0;
            ushort unk = 0;
            float x = 0f;
            float y = 0f;
            float z = 0f;
            if (num3 != 0)
            {
                if ((num3 == 1) && (C.ToArray().Length > 10))
                {
                    CLogger.Print($"Invalid Bomb Type[1] (Length > 10): {C.ToArray().Length}", LoggerType.Warning, null);
                }
            }
            else
            {
                zone = C.ReadC();
                x = C.ReadT();
                y = C.ReadT();
                z = C.ReadT();
                unk = C.ReadUH();
                if (C.ToArray().Length > 0x19)
                {
                    CLogger.Print($"Invalid Bomb (Length > 25): {C.ToArray().Length}", LoggerType.Warning, null);
                }
            }
            ChannelModel channel = ChannelsXML.GetChannel(C.ReadH(), num2);
            if (channel != null)
            {
                RoomModel room = channel.GetRoom(id);
                if ((room != null) && (!room.RoundTime.IsTimer() && (room.State == RoomState.BATTLE)))
                {
                    SlotModel slot = room.GetSlot(slotIdx);
                    if ((slot != null) && (slot.State == SlotState.BATTLE))
                    {
                        if (num3 == 0)
                        {
                            InstallBomb(room, slot, zone, unk, x, y, z);
                        }
                        else if (num3 == 1)
                        {
                            UninstallBomb(room, slot);
                        }
                    }
                }
            }
        }

        public static void UninstallBomb(RoomModel Room, SlotModel Slot)
        {
            if (Room.ActiveC4)
            {
                using (PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK protocol_battle_mission_bomb_uninstall_ack = new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(Slot.Id))
                {
                    Room.SendPacketToPlayers(protocol_battle_mission_bomb_uninstall_ack, SlotState.BATTLE, 0);
                }
                if (Room.RoomType == RoomCondition.Tutorial)
                {
                    Room.ActiveC4 = false;
                }
                else
                {
                    Slot.Objects++;
                    if (Room.SwapRound)
                    {
                        Room.FRRounds++;
                    }
                    else
                    {
                        Room.CTRounds++;
                    }
                    AllUtils.CompleteMission(Room, Slot, MissionType.C4_DEFUSE, 0);
                    AllUtils.BattleEndRound(Room, Room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM, RoundEndType.Uninstall);
                }
            }
        }
    }
}

