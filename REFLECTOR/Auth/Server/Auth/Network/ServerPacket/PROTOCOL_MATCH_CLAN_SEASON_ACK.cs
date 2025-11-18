namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_MATCH_CLAN_SEASON_ACK : AuthServerPacket
    {
        private readonly int int_0;

        public PROTOCOL_MATCH_CLAN_SEASON_ACK(int int_1)
        {
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1e14);
            base.WriteH((short) 0);
            base.WriteD(2);
            base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
            base.WriteB(ComDiv.AddressBytes("255.255.255.255"));
            base.WriteB(new byte[0x6d]);
            base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
            base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
            base.WriteB(ComDiv.AddressBytes("255.255.255.255"));
            base.WriteB(new byte[0x67]);
        }
    }
}

