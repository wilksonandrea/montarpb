namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Game;
    using Server.Game.Data.Models;
    using Server.Game.Data.XML;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_CONNECT_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly int int_1;
        private readonly ushort ushort_0;
        private readonly List<byte[]> list_0;

        public PROTOCOL_BASE_CONNECT_ACK(GameClient gameClient_0)
        {
            this.int_0 = gameClient_0.ServerId;
            this.int_1 = gameClient_0.SessionId;
            this.ushort_0 = gameClient_0.SessionSeed;
            this.list_0 = Bitwise.GenerateRSAKeyPair(this.int_1, base.SECURITY_KEY, base.SEED_LENGTH);
        }

        public override void Write()
        {
            base.WriteH((short) 0x902);
            base.WriteH((short) 0);
            base.WriteC((byte) ChannelsXML.GetChannels(this.int_0).Count);
            foreach (ChannelModel model in ChannelsXML.GetChannels(this.int_0))
            {
                base.WriteC((byte) model.Type);
            }
            base.WriteH((ushort) ((this.list_0[0].Length + this.list_0[1].Length) + 2));
            base.WriteH((ushort) this.list_0[0].Length);
            base.WriteB(this.list_0[0]);
            base.WriteB(this.list_0[1]);
            base.WriteC(3);
            base.WriteH((short) 80);
            base.WriteH(this.ushort_0);
            base.WriteD(this.int_1);
        }
    }
}

