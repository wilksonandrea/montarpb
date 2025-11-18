namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ : GameClientPacket
    {
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
                    ChannelModel channel = player.GetChannel();
                    if (channel != null)
                    {
                        base.Client.SendPacket(new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK(channel));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

