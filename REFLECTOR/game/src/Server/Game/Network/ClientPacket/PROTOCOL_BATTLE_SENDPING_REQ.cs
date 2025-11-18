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

    public class PROTOCOL_BATTLE_SENDPING_REQ : GameClientPacket
    {
        private byte[] byte_0;

        public override void Read()
        {
            this.byte_0 = base.ReadB(0x10);
        }

        public override void Run()
        {
            try
            {
                RoomModel room;
                int num;
                Account player = base.Client.Player;
                if (player != null)
                {
                    room = player.Room;
                    if (room == null)
                    {
                        return;
                    }
                    else
                    {
                        SlotModel model2;
                        if (!room.GetSlot(player.SlotId, out model2))
                        {
                            return;
                        }
                        else
                        {
                            num = 0;
                            if (model2 == null)
                            {
                                return;
                            }
                            else if (model2.State < SlotState.BATTLE_READY)
                            {
                                return;
                            }
                            else
                            {
                                if (room.State == RoomState.BATTLE)
                                {
                                    room.Ping = this.byte_0[room.LeaderSlot];
                                }
                                using (PROTOCOL_BATTLE_SENDPING_ACK protocol_battle_sendping_ack = new PROTOCOL_BATTLE_SENDPING_ACK(this.byte_0))
                                {
                                    List<Account> allPlayers = room.GetAllPlayers(SlotState.READY, 1);
                                    if (allPlayers.Count != 0)
                                    {
                                        byte[] completeBytes = protocol_battle_sendping_ack.GetCompleteBytes(base.GetType().Name);
                                        foreach (Account account2 in allPlayers)
                                        {
                                            SlotModel slot = room.GetSlot(account2.SlotId);
                                            if ((slot != null) && (slot.State >= SlotState.BATTLE_READY))
                                            {
                                                account2.SendCompletePacket(completeBytes, protocol_battle_sendping_ack.GetType().Name);
                                                continue;
                                            }
                                            num++;
                                        }
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return;
                }
                if (num == 0)
                {
                    room.SpawnReadyPlayers();
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BATTLE_SENDPING_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

