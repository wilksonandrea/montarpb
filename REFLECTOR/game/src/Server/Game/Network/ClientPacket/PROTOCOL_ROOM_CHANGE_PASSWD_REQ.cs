namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_CHANGE_PASSWD_REQ : GameClientPacket
    {
        private string string_0;

        public override void Read()
        {
            this.string_0 = base.ReadS(4);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    RoomModel room = player.Room;
                    if (((room != null) && (room.LeaderSlot == player.SlotId)) && (room.Password != this.string_0))
                    {
                        room.Password = this.string_0;
                        using (PROTOCOL_ROOM_CHANGE_PASSWD_ACK protocol_room_change_passwd_ack = new PROTOCOL_ROOM_CHANGE_PASSWD_ACK(this.string_0))
                        {
                            room.SendPacketToPlayers(protocol_room_change_passwd_ack);
                        }
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

