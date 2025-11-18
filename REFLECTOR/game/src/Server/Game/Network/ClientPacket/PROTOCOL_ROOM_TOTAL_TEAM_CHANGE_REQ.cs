namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_ROOM_TOTAL_TEAM_CHANGE_REQ : GameClientPacket
    {
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
                    RoomModel room = player.Room;
                    if (((room != null) && ((room.LeaderSlot == player.SlotId) && ((room.State == RoomState.READY) && (ComDiv.GetDuration(room.LastChangeTeam) >= 1.5)))) && !room.ChangingSlots)
                    {
                        List<SlotChange> list = new List<SlotChange>();
                        SlotModel[] slots = room.Slots;
                        lock (slots)
                        {
                            room.ChangingSlots = true;
                            int[] numArray = room.FR_TEAM;
                            int index = 0;
                            while (true)
                            {
                                if (index >= numArray.Length)
                                {
                                    if (list.Count > 0)
                                    {
                                        using (PROTOCOL_ROOM_TEAM_BALANCE_ACK protocol_room_team_balance_ack = new PROTOCOL_ROOM_TEAM_BALANCE_ACK(list, room.LeaderSlot, 2))
                                        {
                                            byte[] completeBytes = protocol_room_team_balance_ack.GetCompleteBytes(base.GetType().Name);
                                            foreach (Account local1 in room.GetAllPlayers())
                                            {
                                                local1.SlotId = AllUtils.GetNewSlotId(local1.SlotId);
                                                local1.SendCompletePacket(completeBytes, protocol_room_team_balance_ack.GetType().Name);
                                            }
                                        }
                                    }
                                    break;
                                }
                                int oldSlotId = numArray[index];
                                int newSlotId = oldSlotId + 1;
                                if (oldSlotId == room.LeaderSlot)
                                {
                                    room.LeaderSlot = newSlotId;
                                }
                                else if (newSlotId == room.LeaderSlot)
                                {
                                    room.LeaderSlot = oldSlotId;
                                }
                                room.SwitchSlots(list, newSlotId, oldSlotId, true);
                                index++;
                            }
                            room.ChangingSlots = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_ROOM_CHANGE_TEAM_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

