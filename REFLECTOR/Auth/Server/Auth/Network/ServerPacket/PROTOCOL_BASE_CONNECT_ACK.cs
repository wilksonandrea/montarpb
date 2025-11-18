namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Auth;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_CONNECT_ACK : AuthServerPacket
    {
        private readonly int int_0;
        private readonly ushort ushort_0;
        private readonly List<byte[]> list_0;

        public PROTOCOL_BASE_CONNECT_ACK(AuthClient authClient_0)
        {
            this.int_0 = authClient_0.SessionId;
            this.ushort_0 = authClient_0.SessionSeed;
            this.list_0 = Bitwise.GenerateRSAKeyPair(this.int_0, base.SECURITY_KEY, base.SEED_LENGTH);
        }

        public override void Write()
        {
            base.WriteH((short) 0x902);
            base.WriteH((short) 0);
            base.WriteC(11);
            base.WriteB(Bitwise.HexStringToByteArray("00 00 00 00 00 00 00 00 00 00 00"));
            base.WriteH((ushort) ((this.list_0[0].Length + this.list_0[1].Length) + 2));
            base.WriteH((ushort) this.list_0[0].Length);
            base.WriteB(this.list_0[0]);
            base.WriteB(this.list_0[1]);
            base.WriteC(3);
            base.WriteH((short) 80);
            base.WriteH(this.ushort_0);
            base.WriteD(this.int_0);
        }
    }
}

