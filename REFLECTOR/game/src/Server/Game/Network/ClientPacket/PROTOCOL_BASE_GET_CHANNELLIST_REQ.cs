namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.XML;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_GET_CHANNELLIST_REQ : GameClientPacket
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
                if (base.Client.Player != null)
                {
                    List<ChannelModel> channels = ChannelsXML.GetChannels(this.int_0);
                    if (channels.Count == 11)
                    {
                        base.Client.SendPacket(new PROTOCOL_BASE_GET_CHANNELLIST_ACK(SChannelXML.GetServer(this.int_0), channels));
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

