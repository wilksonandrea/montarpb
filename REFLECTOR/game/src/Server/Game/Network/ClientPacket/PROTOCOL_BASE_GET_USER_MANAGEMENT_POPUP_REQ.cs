namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_REQ : GameClientPacket
    {
        private string string_0;

        public override void Read()
        {
            this.string_0 = base.ReadU(0x21);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (((player != null) && (player.Nickname.Length != 0)) && (player.Nickname != this.string_0))
                {
                    base.Client.SendPacket(new PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK());
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

