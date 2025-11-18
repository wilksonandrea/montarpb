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

    public class PROTOCOL_BASE_CHATTING_REQ : GameClientPacket
    {
        private string string_0;
        private ChattingType chattingType_0;

        public override void Read()
        {
            this.chattingType_0 = (ChattingType) base.ReadH();
            this.string_0 = base.ReadU(base.ReadH() * 2);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (((player != null) && (!string.IsNullOrEmpty(this.string_0) && (this.string_0.Length <= 60))) && (player.Nickname.Length != 0))
                {
                    SlotModel model2;
                    SlotModel[] slots;
                    RoomModel room = player.Room;
                    switch (this.chattingType_0)
                    {
                        case ChattingType.All:
                        case ChattingType.Lobby:
                            if (room == null)
                            {
                                ChannelModel channel = player.GetChannel();
                                if ((channel != null) && !AllUtils.ServerCommands(player, this.string_0))
                                {
                                    using (PROTOCOL_LOBBY_CHATTING_ACK protocol_lobby_chatting_ack = new PROTOCOL_LOBBY_CHATTING_ACK(player, this.string_0, false))
                                    {
                                        channel.SendPacketToWaitPlayers(protocol_lobby_chatting_ack);
                                    }
                                }
                            }
                            else if (!AllUtils.ServerCommands(player, this.string_0))
                            {
                                model2 = room.Slots[player.SlotId];
                                using (PROTOCOL_ROOM_CHATTING_ACK protocol_room_chatting_ack2 = new PROTOCOL_ROOM_CHATTING_ACK((int) this.chattingType_0, model2.Id, player.UseChatGM(), this.string_0))
                                {
                                    byte[] completeBytes = protocol_room_chatting_ack2.GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-2");
                                    slots = room.Slots;
                                    lock (slots)
                                    {
                                        foreach (SlotModel model4 in room.Slots)
                                        {
                                            Account playerBySlot = room.GetPlayerBySlot(model4);
                                            if ((playerBySlot != null) && AllUtils.SlotValidMessage(model2, model4))
                                            {
                                                playerBySlot.SendCompletePacket(completeBytes, protocol_room_chatting_ack2.GetType().Name);
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case ChattingType.Team:
                            if (room != null)
                            {
                                model2 = room.Slots[player.SlotId];
                                int[] teamArray = room.GetTeamArray(model2.Team);
                                using (PROTOCOL_ROOM_CHATTING_ACK protocol_room_chatting_ack = new PROTOCOL_ROOM_CHATTING_ACK((int) this.chattingType_0, model2.Id, player.UseChatGM(), this.string_0))
                                {
                                    byte[] completeBytes = protocol_room_chatting_ack.GetCompleteBytes("PROTOCOL_BASE_CHATTING_REQ-1");
                                    slots = room.Slots;
                                    lock (slots)
                                    {
                                        foreach (int num2 in teamArray)
                                        {
                                            SlotModel slot = room.Slots[num2];
                                            if (slot != null)
                                            {
                                                Account playerBySlot = room.GetPlayerBySlot(slot);
                                                if ((playerBySlot != null) && AllUtils.SlotValidMessage(model2, slot))
                                                {
                                                    playerBySlot.SendCompletePacket(completeBytes, protocol_room_chatting_ack.GetType().Name);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

