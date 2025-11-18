using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

internal class Class2 : GameClientPacket
{
    private uint uint_0;
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
                if ((room == null) || ((room.LeaderSlot != player.SlotId) || (room.State != RoomState.READY)))
                {
                    this.uint_0 = 0x80000000;
                }
                else
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
                        this.uint_0 = 0x80000000;
                    }
                    else
                    {
                        int num2 = new Random().Next(this.list_0.Count);
                        SlotModel slot = this.list_0[num2];
                        this.uint_0 = (room.GetPlayerBySlot(slot) != null) ? ((uint) slot.Id) : 0x80000000;
                        this.list_0 = null;
                    }
                }
                base.Client.SendPacket(new PROTOCOL_ROOM_CHECK_MAIN_ACK(this.uint_0));
            }
        }
        catch (Exception exception)
        {
            CLogger.Print("PROTOCOL_ROOM_CHECK_MAIN_REQ: " + exception.Message, LoggerType.Error, exception);
        }
    }
}

