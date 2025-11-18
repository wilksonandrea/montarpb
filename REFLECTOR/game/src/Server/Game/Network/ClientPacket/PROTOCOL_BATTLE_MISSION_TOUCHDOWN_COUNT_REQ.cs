namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;

        public override void Read()
        {
            this.int_1 = base.ReadC();
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if (((room != null) && (!room.RoundTime.IsTimer() && (room.State == RoomState.BATTLE))) && (room.TRex == this.int_1))
                    {
                        SlotModel slot = room.GetSlot(player.SlotId);
                        if ((slot != null) && (slot.State == SlotState.BATTLE))
                        {
                            if (slot.Team == TeamEnum.FR_TEAM)
                            {
                                room.FRDino += 5;
                            }
                            else
                            {
                                room.CTDino += 5;
                            }
                            using (PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK protocol_battle_mission_touchdown_count_ack = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(room))
                            {
                                room.SendPacketToPlayers(protocol_battle_mission_touchdown_count_ack, SlotState.BATTLE, 0);
                            }
                            AllUtils.CompleteMission(room, player, slot, MissionType.DEATHBLOW, this.int_0);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

