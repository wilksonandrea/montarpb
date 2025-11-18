namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_ROOM_REQUEST_MAIN_REQ : GameClientPacket
    {
        private void method_0(RoomModel roomModel_0, List<Account> list_0, int int_0)
        {
            roomModel_0.SetNewLeader(int_0, SlotState.EMPTY, -1, false);
            using (PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK protocol_room_request_main_change_who_ack = new PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(int_0))
            {
                this.method_1(protocol_room_request_main_change_who_ack, list_0);
            }
            roomModel_0.UpdateSlotsInfo();
            roomModel_0.RequestRoomMaster.Clear();
        }

        private void method_1(GameServerPacket gameServerPacket_0, List<Account> list_0)
        {
            byte[] completeBytes = gameServerPacket_0.GetCompleteBytes("PROTOCOL_ROOM_REQUEST_MAIN_REQ");
            using (List<Account>.Enumerator enumerator = list_0.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    enumerator.Current.SendCompletePacket(completeBytes, gameServerPacket_0.GetType().Name);
                }
            }
        }

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
                    if (room == null)
                    {
                        base.Client.SendPacket(new PROTOCOL_ROOM_REQUEST_MAIN_ACK(0x80000000));
                    }
                    else if ((room.State == RoomState.READY) && (room.LeaderSlot != player.SlotId))
                    {
                        List<Account> allPlayers = room.GetAllPlayers();
                        if (allPlayers.Count != 0)
                        {
                            if (player.IsGM())
                            {
                                this.method_0(room, allPlayers, player.SlotId);
                            }
                            else
                            {
                                if (!room.RequestRoomMaster.Contains(player.PlayerId))
                                {
                                    room.RequestRoomMaster.Add(player.PlayerId);
                                    if (room.RequestRoomMaster.Count() < ((allPlayers.Count / 2) + 1))
                                    {
                                        using (PROTOCOL_ROOM_REQUEST_MAIN_ACK protocol_room_request_main_ack = new PROTOCOL_ROOM_REQUEST_MAIN_ACK(player.SlotId))
                                        {
                                            this.method_1(protocol_room_request_main_ack, allPlayers);
                                        }
                                    }
                                }
                                if (room.RequestRoomMaster.Count() >= ((allPlayers.Count / 2) + 1))
                                {
                                    this.method_0(room, allPlayers, player.SlotId);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_ROOM_REQUEST_MAIN_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

