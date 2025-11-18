namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_GET_USER_SUBTASK_REQ : GameClientPacket
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
                    PlayerSession session = player.GetChannel().GetPlayer(this.int_0);
                    if (session != null)
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_GET_USER_SUBTASK_ACK(session));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_GET_USER_SUBTASK_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

