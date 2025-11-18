namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_JOIN_REQ : GameClientPacket
    {
        private int int_0;
        private int int_1;
        private string string_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
            this.string_0 = base.ReadS(4);
            this.int_1 = base.ReadC();
            base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ChannelModel model;
                    if ((player.Nickname.Length <= 0) || ((player.Room != null) || ((player.Match != null) || !player.GetChannel(out model))))
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
                        else if (room.RoomType == RoomCondition.Tutorial)
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x8000107c, null));
                        }
                        else if ((room.Password.Length > 0) && ((this.string_0 != room.Password) && ((player.Rank != 0x35) && (!player.IsGM() && (this.int_1 != 1)))))
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x80001005, null));
                        }
                        else if ((room.Limit == 1) && ((room.State >= RoomState.COUNTDOWN) && !player.IsGM()))
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x80001013, null));
                        }
                        else if (room.KickedPlayersVote.Contains(player.PlayerId) && !player.IsGM())
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x8000100c, null));
                        }
                        else if (room.KickedPlayersHost.ContainsKey(player.PlayerId) && (ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId]) < ConfigLoader.IntervalEnterRoomAfterKickSeconds))
                        {
                            object[] argumens = new object[] { ConfigLoader.IntervalEnterRoomAfterKickSeconds, (int) ComDiv.GetDuration(room.KickedPlayersHost[player.PlayerId]) };
                            base.Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("KickByHostMessage", argumens)));
                        }
                        else if (room.AddPlayer(player) < 0)
                        {
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0x80001003, null));
                        }
                        else
                        {
                            player.ResetPages();
                            using (PROTOCOL_ROOM_GET_SLOTONEINFO_ACK protocol_room_get_slotoneinfo_ack = new PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(player))
                            {
                                room.SendPacketToPlayers(protocol_room_get_slotoneinfo_ack, player.PlayerId);
                            }
                            if (room.Competitive)
                            {
                                AllUtils.SendCompetitiveInfo(player);
                            }
                            base.Client.SendPacket(new PROTOCOL_ROOM_JOIN_ACK(0, player));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_LOBBY_JOIN_ROOM_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

