namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ : GameClientPacket
    {
        private int int_0;

        public override void Read()
        {
            this.int_0 = base.ReadD();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    ChannelModel channel = player.GetChannel();
                    if (channel != null)
                    {
                        RoomModel room = channel.GetRoom(this.int_0);
                        if ((room != null) && (room.GetLeader() != null))
                        {
                            base.Client.SendPacket(new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(room));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

