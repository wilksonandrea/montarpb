namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Game.Data.Models;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_BASE_ENTER_PASS_REQ : GameClientPacket
    {
        private int int_0;
        private string string_0;

        public override void Read()
        {
            this.int_0 = base.ReadH();
            this.string_0 = base.ReadS(0x10);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((player != null) && (player.ChannelId < 0))
                {
                    ChannelModel channel = ChannelsXML.GetChannel(base.Client.ServerId, this.int_0);
                    if (channel != null)
                    {
                        if (!this.string_0.Equals(channel.Password))
                        {
                            base.Client.SendPacket(new PROTOCOL_BASE_ENTER_PASS_ACK(0x80000000));
                        }
                        else
                        {
                            base.Client.SendPacket(new PROTOCOL_BASE_ENTER_PASS_ACK(0));
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

