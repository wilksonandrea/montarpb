namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Auth.Data.Models;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_GET_CHANNELLIST_ACK : AuthServerPacket
    {
        private readonly SChannelModel schannelModel_0;
        private readonly List<ChannelModel> list_0;

        public PROTOCOL_BASE_GET_CHANNELLIST_ACK(SChannelModel schannelModel_1, List<ChannelModel> list_1)
        {
            this.schannelModel_0 = schannelModel_1;
            this.list_0 = list_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x91d);
            base.WriteD(0);
            base.WriteD(1);
            base.WriteD(0);
            base.WriteH((short) 0);
            base.WriteC(0);
            base.WriteC((byte) this.list_0.Count);
            foreach (ChannelModel model in this.list_0)
            {
                base.WriteH((ushort) model.TotalPlayers);
            }
            base.WriteH((short) 310);
            base.WriteH((ushort) this.schannelModel_0.ChannelPlayers);
            base.WriteC((byte) this.list_0.Count);
            base.WriteC(0);
        }
    }
}

