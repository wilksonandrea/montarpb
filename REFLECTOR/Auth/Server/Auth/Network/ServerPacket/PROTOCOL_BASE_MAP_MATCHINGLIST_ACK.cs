namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.XML;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_MAP_MATCHINGLIST_ACK : AuthServerPacket
    {
        private readonly List<MapMatch> list_0;
        private readonly int int_0;

        public PROTOCOL_BASE_MAP_MATCHINGLIST_ACK(List<MapMatch> list_1, int int_1)
        {
            this.list_0 = list_1;
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x9a0);
            base.WriteH((short) 0);
            base.WriteC((byte) this.list_0.Count);
            foreach (MapMatch match in this.list_0)
            {
                base.WriteD(match.Mode);
                base.WriteC((byte) match.Id);
                base.WriteC((byte) SystemMapXML.GetMapRule(match.Mode).Rule);
                base.WriteC((byte) SystemMapXML.GetMapRule(match.Mode).StageOptions);
                base.WriteC((byte) SystemMapXML.GetMapRule(match.Mode).Conditions);
                base.WriteC((byte) match.Limit);
                base.WriteC((byte) match.Tag);
                this.WriteC(((match.Tag == 2) || (match.Tag == 3)) ? ((byte) 1) : ((byte) 0));
                base.WriteC(1);
            }
            base.WriteD((int) (this.list_0.Count != 100));
            base.WriteH((ushort) (this.int_0 - 100));
            base.WriteH((ushort) SystemMapXML.Matches.Count);
        }
    }
}

