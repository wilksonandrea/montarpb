namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK : GameServerPacket
    {
        private readonly MatchModel matchModel_0;

        public PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(MatchModel matchModel_1)
        {
            this.matchModel_0 = matchModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1b1b);
            base.WriteH((short) this.matchModel_0.GetServerInfo());
            base.WriteC((byte) this.matchModel_0.State);
            base.WriteC((byte) this.matchModel_0.FriendId);
            base.WriteC((byte) this.matchModel_0.Training);
            base.WriteC((byte) this.matchModel_0.GetCountPlayers());
            base.WriteD(this.matchModel_0.Leader);
            base.WriteC(0);
            foreach (SlotMatch match in this.matchModel_0.Slots)
            {
                Account playerBySlot = this.matchModel_0.GetPlayerBySlot(match);
                if (playerBySlot == null)
                {
                    base.WriteB(new byte[0x2b]);
                }
                else
                {
                    base.WriteC((byte) playerBySlot.Rank);
                    base.WriteS(playerBySlot.Nickname, 0x21);
                    base.WriteQ(match.PlayerId);
                    base.WriteC((byte) match.State);
                }
            }
        }
    }
}

