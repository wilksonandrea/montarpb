namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
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
                    if ((room == null) || ((room.LeaderSlot == this.int_0) || (room.Slots[this.int_0].PlayerId == 0)))
                    {
                        base.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(0x80000000));
                    }
                    else if ((room.State == RoomState.READY) && (room.LeaderSlot == player.SlotId))
                    {
                        room.SetNewLeader(this.int_0, SlotState.EMPTY, room.LeaderSlot, false);
                        using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK protocol_room_request_main_change_who_ack = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(this.int_0))
                        {
                            room.SendPacketToPlayers(protocol_room_request_main_change_who_ack);
                        }
                        room.UpdateSlotsInfo();
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

