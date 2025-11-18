namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_CHANGE_SLOT_REQ : GameClientPacket
    {
        private int int_0;
        private uint uint_0;

        private void method_0(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0)
        {
            if (slotModel_0.State == SlotState.EMPTY)
            {
                if (roomModel_0.Competitive && !AllUtils.CanCloseSlotCompetitive(roomModel_0, slotModel_0))
                {
                    account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), account_0.Session.SessionId, account_0.NickColor, true, Translation.GetLabel("CompetitiveSlotMin")));
                }
                roomModel_0.ChangeSlotState(slotModel_0, SlotState.CLOSE, true);
            }
        }

        private void method_1(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0)
        {
            if (((this.int_0 & 0x10000000) == 0x10000000) && (slotModel_0.State == SlotState.CLOSE))
            {
                MapMatch mapLimit = SystemMapXML.GetMapLimit((int) roomModel_0.MapId, (int) roomModel_0.Rule);
                if ((mapLimit != null) && (slotModel_0.Id < mapLimit.Limit))
                {
                    if (roomModel_0.Competitive && !AllUtils.CanOpenSlotCompetitive(roomModel_0, slotModel_0))
                    {
                        account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK(Translation.GetLabel("Competitive"), account_0.Session.SessionId, account_0.NickColor, true, Translation.GetLabel("CompetitiveSlotMax")));
                    }
                    if (!roomModel_0.IsBotMode())
                    {
                    }
                    roomModel_0.ChangeSlotState(slotModel_0, SlotState.EMPTY, true);
                }
            }
        }

        public override void Read()
        {
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                RoomModel room;
                SlotModel model2;
                Account playerBySlot;
                Account player = base.Client.Player;
                if (player != null)
                {
                    room = player.Room;
                    if ((room == null) || ((room.LeaderSlot != player.SlotId) || !room.GetSlot(this.int_0 & 0xfffffff, out model2)))
                    {
                        this.uint_0 = 0x80000401;
                        goto TR_0002;
                    }
                    else
                    {
                        if ((this.int_0 & 0x10000000) == 0x10000000)
                        {
                            this.method_1(player, room, model2);
                        }
                        else
                        {
                            this.method_0(player, room, model2);
                        }
                        SlotState state = model2.State;
                        if ((state - 2) <= SlotState.GIFTSHOP)
                        {
                            playerBySlot = room.GetPlayerBySlot(model2);
                            if (((playerBySlot == null) || playerBySlot.AntiKickGM) || (((model2.State == SlotState.READY) || (((room.ChannelType != ChannelType.Clan) || (room.State == RoomState.COUNTDOWN)) && (room.ChannelType == ChannelType.Clan))) && ((model2.State != SlotState.READY) || (((room.ChannelType != ChannelType.Clan) || (room.State != RoomState.READY)) && (room.ChannelType == ChannelType.Clan)))))
                            {
                                goto TR_0002;
                            }
                        }
                        else
                        {
                            if ((state - SlotState.LOAD) <= SlotState.CLAN)
                            {
                                this.uint_0 = 0x80000401;
                            }
                            goto TR_0002;
                        }
                    }
                    goto TR_0008;
                }
                return;
            TR_0002:
                base.Client.SendPacket(new PROTOCOL_ROOM_CHANGE_SLOT_ACK(this.uint_0));
                return;
            TR_0008:
                playerBySlot.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK());
                if (!room.KickedPlayersHost.ContainsKey(player.PlayerId))
                {
                    room.KickedPlayersHost.Add(player.PlayerId, DateTimeUtil.Now());
                }
                else
                {
                    room.KickedPlayersHost[player.PlayerId] = DateTimeUtil.Now();
                }
                room.RemovePlayer(playerBySlot, model2, false, 0);
                goto TR_0002;
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_ROOM_CHANGE_SLOT_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

