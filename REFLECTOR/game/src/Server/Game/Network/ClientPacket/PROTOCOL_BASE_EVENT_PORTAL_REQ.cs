namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_EVENT_PORTAL_REQ : GameClientPacket
    {
        private string string_0;

        public override void Read()
        {
            this.string_0 = base.ReadS(0x20);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    player.LoadedShop ??= true;
                    if (Bitwise.ReadFile(Environment.CurrentDirectory + "/Data/Raws/EventPortal.dat") == this.string_0)
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(false));
                    }
                    else
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(true));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_BASE_EVENT_PORTAL_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

