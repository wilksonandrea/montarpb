namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_REQ : GameClientPacket
    {
        private TeamEnum teamEnum_0;
        private int int_0;

        public override void Read()
        {
            this.teamEnum_0 = (TeamEnum) base.ReadD();
            this.int_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if (((room != null) && ((this.teamEnum_0 != TeamEnum.ALL_TEAM) || (this.teamEnum_0 != TeamEnum.TEAM_DRAW))) && !room.ChangingSlots)
                    {
                        SlotModel slot = room.GetSlot(player.SlotId);
                        if ((slot != null) && (slot.State == SlotState.NORMAL))
                        {
                            room.ChangingSlots = true;
                            SlotModel newSlot = room.GetSlot(this.int_0);
                            if (((newSlot != null) && ((newSlot.Team == this.teamEnum_0) && (newSlot.PlayerId == 0))) && (newSlot.State == SlotState.EMPTY))
                            {
                                List<SlotChange> slotChanges = new List<SlotChange>();
                                room.SwitchSlots(slotChanges, player, slot, newSlot, SlotState.NORMAL);
                                if (slotChanges.Count > 0)
                                {
                                    using (PROTOCOL_ROOM_TEAM_BALANCE_ACK protocol_room_team_balance_ack = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(slotChanges, room.LeaderSlot, 0))
                                    {
                                        room.SendPacketToPlayers(protocol_room_team_balance_ack);
                                    }
                                }
                                room.ChangingSlots = false;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(base.GetType().Name + ": " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

