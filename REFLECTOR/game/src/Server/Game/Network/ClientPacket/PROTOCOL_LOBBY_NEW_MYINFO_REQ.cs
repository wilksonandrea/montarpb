namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_LOBBY_NEW_MYINFO_REQ : GameClientPacket
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
                    base.Client.SendPacket(new PROTOCOL_LOBBY_NEW_MYINFO_ACK(player));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_LOBBY_NEW_MYINFO_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

