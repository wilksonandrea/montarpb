namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;
        private TeamEnum teamEnum_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
            this.teamEnum_0 = (TeamEnum) base.ReadH();
            this.int_1 = base.ReadH();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (((player != null) && (player.ClanId != 0)) && (player.Match != null))
                {
                    ChannelModel model;
                    if ((player == null) || ((player.Nickname.Length <= 0) || ((player.Room != null) || !player.GetChannel(out model))))
                    {
                        base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x80001004, null));
                    }
                    else
                    {
                        RoomModel room = model.GetRoom(this.int_0);
                        if ((room == null) || (room.GetLeader() == null))
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x80001004, null));
                        }
                        else if ((room.Password.Length > 0) && !player.IsGM())
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x80001005, null));
                        }
                        else if ((room.Limit == 1) && (room.State >= RoomState.COUNTDOWN))
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x80001013, null));
                        }
                        else if (room.KickedPlayersVote.Contains(player.PlayerId))
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x8000100c, null));
                        }
                        else if (room.AddPlayer(player, this.teamEnum_0) < 0)
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x80001003, null));
                        }
                        else
                        {
                            using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK protocol_room_get_slotoneinfo_ack = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
                            {
                                room.SendPacketToPlayers(protocol_room_get_slotoneinfo_ack, player.PlayerId);
                            }
                            room.UpdateSlotsInfo();
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0, player));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CLAN_WAR_JOIN_ROOM_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

