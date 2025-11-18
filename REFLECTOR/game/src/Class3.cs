using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

internal class Class3 : GameClientPacket
{
    private List<SlotModel> list_0 = new List<SlotModel>();

    override void GameClientPacket.Read()
    {
    }

    override void GameClientPacket.Run()
    {
        try
        {
            Account player = base.Client.Player;
            if (player != null)
            {
                RoomModel room = player.Room;
                if (((room != null) && (room.LeaderSlot == player.SlotId)) && (room.State == RoomState.READY))
                {
                    SlotModel[] slots = room.Slots;
                    lock (slots)
                    {
                        for (int i = 0; i < 0x12; i++)
                        {
                            SlotModel item = room.Slots[i];
                            if ((item.PlayerId > 0L) && (i != room.LeaderSlot))
                            {
                                this.list_0.Add(item);
                            }
                        }
                    }
                    if (this.list_0.Count <= 0)
                    {
                        base.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(0x80000000));
                    }
                    else
                    {
                        SlotModel slot = this.list_0[new Random().Next(this.list_0.Count)];
                        if (room.GetPlayerBySlot(slot) == null)
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(0x80000000));
                        }
                        else
                        {
                            room.SetNewLeader(slot.Id, SlotState.EMPTY, room.LeaderSlot, false);
                            using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK protocol_room_request_main_change_ack = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(slot.Id))
                            {
                                room.SendPacketToPlayers(protocol_room_request_main_change_ack);
                            }
                            room.UpdateSlotsInfo();
                        }
                        this.list_0 = null;
                    }
                }
            }
        }
        catch (Exception exception)
        {
            CLogger.Print("ROOM_RANDOM_HOST2_REC: " + exception.Message, LoggerType.Error, exception);
        }
    }
}

