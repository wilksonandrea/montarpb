namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_START_KICKVOTE_ACK : GameServerPacket
    {
        private readonly VoteKickModel voteKickModel_0;

        public PROTOCOL_BATTLE_START_KICKVOTE_ACK(VoteKickModel voteKickModel_1)
        {
            this.voteKickModel_0 = voteKickModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xd47);
            base.WriteC((byte) this.voteKickModel_0.CreatorIdx);
            base.WriteC((byte) this.voteKickModel_0.VictimIdx);
            base.WriteC((byte) this.voteKickModel_0.Motive);
        }
    }
}

